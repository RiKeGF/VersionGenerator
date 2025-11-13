using Forza.Shared.Utilitarios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VersionGenerator
{
   public partial class Form1 : Form
   {
      private List<Project> ListProjects = new List<Project>();
      private List<Project> ListInconsistProjects = new List<Project>();
      private string LocalPath;
      private string LastPath;

      public Form1()
      {
         InitializeComponent();
         CmbType.DataSource = Enum.GetValues(typeof(ProjectType));
         this.LocalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"folders.txt");

         if (File.Exists(this.LocalPath))
         {
            using (StreamReader reader = new StreamReader(this.LocalPath))
            {
               string line;
               while ((line = reader.ReadLine()) != null)
               {
                  string[] l = line.Split(';');
                  this.ListProjects.Add(new Project(l[0], l[1], (ProjectType)Convert.ToInt16(l[2])));
               }
            }
         }

         RefreshGrid();

         string lastPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"lastpath.txt");

         if (File.Exists(lastPath))
         {
            using (StreamReader reader = new StreamReader(lastPath))
            {
               string line;

               if ((line = reader.ReadLine()) != null)
                  this.LastPath = line;
            }
         }

         TxtPathVersions.Text = this.LastPath;
      }

      private void RefreshGrid()
      {
         Dgv.DataSource = this.ListProjects.ToList();
      }

      private void BtnPath_Click(object sender, EventArgs e)
      {
         DirectoryInfo info = null;

         FolderBrowserDialog folderDialog = new FolderBrowserDialog();

         if (folderDialog.ShowDialog() == DialogResult.OK)
            info = new DirectoryInfo(folderDialog.SelectedPath);

         if (info != null)
         {
            if (info.FullName.Last().ToString().Equals(@"\"))
               TxtPathVersions.Text = info.FullName;
            else
               TxtPathVersions.Text = info.FullName + @"\";
         }
         else
            TxtPathVersions.Text = string.Empty;
      }

      private void BtnAdd_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(TxtFolderName.Text))
         {
            this.ListProjects.Add(new Project(TxtFolderName.Text, TxtReference.Text, (ProjectType)CmbType.SelectedItem));
            RefreshGrid();
            TxtFolderName.Text = string.Empty;
            TxtReference.Text = string.Empty;
            TxtFolderName.Focus();
         }
      }

      private void BtnRemove_Click(object sender, EventArgs e)
      {
         var selectedRows = Dgv.SelectedRows.Cast<DataGridViewRow>().ToList();

         foreach (DataGridViewRow row in selectedRows)
            this.ListProjects.RemoveAll(x => x.Equals((Project)row.DataBoundItem));

         RefreshGrid();
      }

      private void CleanDirectory()
      {
         foreach (string file in Directory.GetFiles(TxtPathVersions.Text))
            File.Delete(file);

         foreach (string directory in Directory.GetDirectories(TxtPathVersions.Text))
            Directory.Delete(directory, true);
      }

      private void CreateFoldersVersions()
      {
         foreach (var item in this.ListProjects.FindAll(x => x.IsSelected))
         {
            if (string.IsNullOrEmpty(RetornarVersao()))
               Directory.CreateDirectory(Path.Combine(TxtPathVersions.Text, item.Name));
            else
               Directory.CreateDirectory(Path.Combine(TxtPathVersions.Text, string.Concat(item.Name, " - ", TxtVersion.Text)));
         }
      }

      private void SaveTempProjects()
      {
         using (StreamWriter writer = new StreamWriter(this.LocalPath, append: false))
         {
            foreach (var item in this.ListProjects)
               writer.WriteLine(string.Concat(item.Name, ";", item.Reference, ";", (int)item.Type));
         }
      }

      private void SaveLastPath()
      {
         using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"lastpath.txt"), append: false))
         {
            writer.WriteLine(TxtPathVersions.Text);
         }
      }

      private void ChangeAssemblyVersion()
      {
         if (string.IsNullOrEmpty(RetornarVersao())) return;

         foreach (var item in this.ListProjects.FindAll(x => x.IsSelected))
         {
            string path = $@"{TxtPathProject.Text}{item.Reference}\{item.Reference}";

            if (File.Exists(string.Concat(path, ".csproj")))
               path += ".csproj";

            if (File.Exists(string.Concat(path, ".vbproj")))
               path += ".vbproj";

            if (!path.Contains(".csproj") && !path.Contains(".vbproj"))
            {
               path = $@"{TxtPathProject.Text}{item.Reference}";

               var files = Directory.GetDirectories(path);

               if (files.Count() > 0)
               {
                  path = $@"{files.First()}\{item.Reference}";

                  if (File.Exists(string.Concat(path, ".csproj")))
                     path += ".csproj";

                  if (File.Exists(string.Concat(path, ".vbproj")))
                     path += ".vbproj";

                  if (!File.Exists(path))
                     continue;
               }
               else
                  continue;
            }

            var xml = XDocument.Load(path);
            var ns = xml.Root.Name.Namespace;

            var group = xml.Root.Elements(ns + "PropertyGroup")
                                .FirstOrDefault(e => e.Attribute("Condition") == null)
                       ?? new XElement(ns + "PropertyGroup");
            if (group.Parent == null) xml.Root.AddFirst(group);

            group.SetElementValue(ns + "Version", RetornarVersao());
            group.SetElementValue(ns + "FileVersion", RetornarVersao());
            group.SetElementValue(ns + "AssemblyVersion", RetornarVersao());
            group.SetElementValue(ns + "InformationalVersion", RetornarVersao());

            xml.Save(path);

            var csproj = $@"{TxtPathProject.Text}{item.Reference}\AssemblyInfo.cs";
            var csprojProp = $@"{TxtPathProject.Text}{item.Reference}\Properties\AssemblyInfo.cs";
            var vbproj = $@"{TxtPathProject.Text}{item.Reference}\AssemblyInfo.vb";
            var vbprojProp = $@"{TxtPathProject.Text}{item.Reference}\Properties\AssemblyInfo.vb";
            var projectPath = File.Exists(csprojProp) ? csprojProp :
                              File.Exists(vbprojProp) ? vbprojProp :
                              File.Exists(csproj) ? csproj :
                              File.Exists(vbproj) ? vbproj :
                              "";

            if (string.IsNullOrEmpty(projectPath)) continue;

            var txt = System.IO.File.ReadAllText(projectPath);

            // substitui ou injeta
            string ReplaceOrAdd(string input, string attr, string value)
            {
               if (Path.GetExtension(projectPath).Equals(".vb"))
               {
                  var rgx = new Regex($@"^\s*\<Assembly:\s*{attr}\s*\(\s*""[^""]*""\s*\)\s*\>\s*$",
                                      RegexOptions.Multiline);
                  if (rgx.IsMatch(input))
                     return rgx.Replace(input, $@"<Assembly: {attr}(""{value}"")>");
                  return input + Environment.NewLine + $@"<Assembly: {attr}(""{value}"")>";
               }
               else
               {
                  var rgx = new Regex($@"^\s*\[assembly:\s*{attr}\s*\(\s*""[^""]*""\s*\)\s*\]\s*$",
                                      RegexOptions.Multiline);
                  if (rgx.IsMatch(input))
                     return rgx.Replace(input, $@"[assembly: {attr}(""{value}"")]");
                  return input + Environment.NewLine + $@"[assembly: {attr}(""{value}"")]";
               }
            }

            txt = ReplaceOrAdd(txt, "AssemblyVersion", RetornarVersao());
            txt = ReplaceOrAdd(txt, "AssemblyFileVersion", RetornarVersao());
            txt = ReplaceOrAdd(txt, "AssemblyInformationalVersion", RetornarVersao());

            System.IO.File.WriteAllText(projectPath, txt);
         }
      }

      private string RetornarVersao()
      {
         string texto = TxtVersion.Text;

         var regex = new Regex(@"\bv?(?<ver>\d+(?:\.\d+){3})\b", RegexOptions.IgnoreCase);

         var match = regex.Match(texto);
         return match.Success ? match.Groups["ver"].Value : string.Empty;
      }

      private bool Validar()
      {
         if (string.IsNullOrEmpty(TxtPathProject.Text) || !Directory.Exists(TxtPathProject.Text))
         {
            MessageBox.Show("Por favor, selecione um caminho de projeto válido.");
            return false;
         }
         if (string.IsNullOrEmpty(TxtPathVersions.Text) || !Directory.Exists(TxtPathVersions.Text))
         {
            MessageBox.Show("Por favor, selecione um caminho para as versões válido.");
            return false;
         }

         return true;
      }

      private void GenerateVersions()
      {
         try
         {
            var v = RetornarVersao();

            var bat = new StringBuilder();

            bat.AppendLine("@echo off");
            bat.AppendLine("color 0A");
            bat.AppendLine("REM ============================================================");
            bat.AppendLine("REM   Gerador de Versoes");
            bat.AppendLine("REM   Autor: Luiz Henrique Finger");
            bat.AppendLine("REM ============================================================");
            bat.AppendLine();
            bat.AppendLine("echo ================================================");
            bat.AppendLine($"echo Versao: {RetornarVersao()}");
            bat.AppendLine("echo ================================================");
            bat.AppendLine();
            bat.AppendLine("ping 127.0.0.1 -n 1 >nul");

            foreach (var item in this.ListProjects.FindAll(x => x.IsSelected))
            {
               // acha .csproj/.vbproj
               var baseProjDir = System.IO.Path.Combine(TxtPathProject.Text, item.Reference);
               var csproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.csproj");
               var vbproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.vbproj");
               var projectPath = File.Exists(csproj) ? csproj :
                                 File.Exists(vbproj) ? vbproj : "";

               if (!File.Exists(projectPath))
               {
                  var files = Directory.GetDirectories(baseProjDir);

                  if (files.Count() > 0)
                  {
                     baseProjDir = files.First();
                     csproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.csproj");
                     vbproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.vbproj");
                     projectPath = File.Exists(csproj) ? csproj :
                                   File.Exists(vbproj) ? vbproj : "";

                     if (!File.Exists(projectPath))
                        continue;
                  }
                  else
                     continue;
               }

               var label = item.Name;

               if (!string.IsNullOrEmpty(TxtVersion.Text))
                  label = $"{item.Name} - {TxtVersion.Text}";

               var outDir = System.IO.Path.Combine(TxtPathVersions.Text, label);

               bat.AppendLine("REM ---------------------------");
               bat.AppendLine($@"REM Projeto: {item.Name}");
               bat.AppendLine("REM ---------------------------");
               bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
               bat.AppendLine($@"set SolutionFile=""{projectPath}""");
               bat.AppendLine($@"set VSEnvCmd=""C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat""");
               bat.AppendLine("echo -----------------------------------------------");
               bat.AppendLine($@"echo {label}");
               bat.AppendLine("echo -----------------------------------------------");
               bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
               bat.AppendLine();
               bat.AppendLine(@"CALL %VSEnvCmd%");
               bat.AppendLine(@"echo Iniciando a build da solução em modo Release...");
               bat.AppendLine($@"msbuild %SolutionFile% /t:Build /p:Configuration=Release /p:Platform=""Any CPU"" /p:OutPutPath=""{outDir}""");
               bat.AppendLine(@"IF %ERRORLEVEL% EQU 0 (
                                 echo Build Release concluida com SUCESSO!
                             ) ELSE (
                                 echo Ocorreu um ERRO durante a build Release.
                             )");

               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
            }

            var batPath = System.IO.Path.Combine(TxtPathVersions.Text, $"gerar_v{RetornarVersao()}.bat");
            File.WriteAllText(batPath, bat.ToString(), Encoding.Default);
         }
         catch (Exception)
         {
            throw;
         }
      }

      private void GenerateVersionsInconsist()
      {
         try
         {
            var v = RetornarVersao();

            var bat = new StringBuilder();

            bat.AppendLine("@echo off");
            bat.AppendLine("color 0A");
            bat.AppendLine("REM ============================================================");
            bat.AppendLine("REM   Gerador de Versoes");
            bat.AppendLine("REM   Autor: Luiz Henrique Finger");
            bat.AppendLine("REM ============================================================");
            bat.AppendLine();
            bat.AppendLine("echo ================================================");
            bat.AppendLine($"echo Versao: {RetornarVersao()}");
            bat.AppendLine("echo ================================================");
            bat.AppendLine();
            bat.AppendLine("ping 127.0.0.1 -n 1 >nul");

            foreach (var item in this.ListInconsistProjects)
            {
               // acha .csproj/.vbproj
               var baseProjDir = System.IO.Path.Combine(TxtPathProject.Text, item.Reference);
               var csproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.csproj");
               var vbproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.vbproj");
               var projectPath = File.Exists(csproj) ? csproj :
                                 File.Exists(vbproj) ? vbproj : "";

               if (!File.Exists(projectPath))
               {
                  var files = Directory.GetDirectories(baseProjDir);

                  if (files.Count() > 0)
                  {
                     baseProjDir = files.First();
                     csproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.csproj");
                     vbproj = System.IO.Path.Combine(baseProjDir, $"{item.Reference}.vbproj");
                     projectPath = File.Exists(csproj) ? csproj :
                                   File.Exists(vbproj) ? vbproj : "";

                     if (!File.Exists(projectPath))
                        continue;
                  }
                  else
                     continue;
               }

               var label = item.Name;

               if (!string.IsNullOrEmpty(TxtVersion.Text))
                  label = $"{item.Name} - {TxtVersion.Text}";

               var outDir = System.IO.Path.Combine(TxtPathVersions.Text, label);

               bat.AppendLine("REM ---------------------------");
               bat.AppendLine($@"REM Projeto: {item.Name}");
               bat.AppendLine("REM ---------------------------");
               bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
               bat.AppendLine($@"set SolutionFile=""{projectPath}""");
               bat.AppendLine($@"set VSEnvCmd=""C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat""");
               bat.AppendLine("echo -----------------------------------------------");
               bat.AppendLine($@"echo {label}");
               bat.AppendLine("echo -----------------------------------------------");
               bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
               bat.AppendLine();
               bat.AppendLine(@"CALL %VSEnvCmd%");
               bat.AppendLine(@"echo Iniciando a build da solução em modo Release...");
               bat.AppendLine($@"msbuild %SolutionFile% /t:Build /p:Configuration=Release /p:Platform=""Any CPU"" /p:OutPutPath=""{outDir}""");
               bat.AppendLine(@"IF %ERRORLEVEL% EQU 0 (
                                 echo Build Release concluida com SUCESSO!
                             ) ELSE (
                                 echo Ocorreu um ERRO durante a build Release.
                             )");

               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
               bat.AppendLine();
            }

            var batPath = System.IO.Path.Combine(TxtPathVersions.Text, $"gerarinconsist_v{RetornarVersao()}.bat");
            File.WriteAllText(batPath, bat.ToString(), Encoding.Default);
         }
         catch (Exception)
         {
            throw;
         }
      }

      private void ExecuteBat()
      {
         var path = $@"{TxtPathVersions.Text}gerar_v{RetornarVersao()}.bat";
         var psi = new ProcessStartInfo
         {
            FileName = "cmd.exe",
            Arguments = $"/c \"{path}\"",
            WorkingDirectory = Path.GetDirectoryName(path),
            UseShellExecute = true,
         };

         using (var proc = Process.Start(psi))
         {
            proc.WaitForExit();

            if (proc.ExitCode == 0 || proc.ExitCode == 255)
            {
               this.ListInconsistProjects.Clear();
               this.ListInconsistProjects.AddRange(this.ListProjects.FindAll(x => Directory.GetFiles(Path.Combine(TxtPathVersions.Text, string.Concat(x.Name, " - ", TxtVersion.Text))).Length == 0));

               if (this.ListInconsistProjects.Count > 0)
               {
                  GenerateVersionsInconsist();
                  ExecuteBatInconsist(false);
               }
               else
               {
                  Finish();
               }
            }
         }
      }

      private void ExecuteBatInconsist(bool isReprocessamento)
      {
         var path = $@"{TxtPathVersions.Text}gerarinconsist_v{RetornarVersao()}.bat";
         var psi = new ProcessStartInfo
         {
            FileName = "cmd.exe",
            Arguments = $"/c \"{path}\"",
            WorkingDirectory = Path.GetDirectoryName(path),
            UseShellExecute = true,
         };

         using (var proc = Process.Start(psi))
         {
            proc.WaitForExit();

            if (proc.ExitCode == 0 || proc.ExitCode == 255)
            {
               if (!isReprocessamento)
               {
                  this.ListInconsistProjects.Clear();
                  this.ListInconsistProjects.AddRange(this.ListProjects.FindAll(x => Directory.GetFiles(Path.Combine(TxtPathVersions.Text, string.Concat(x.Name, " - ", TxtVersion.Text))).Length == 0));
                  if (this.ListInconsistProjects.Count > 0)
                  {
                     GenerateVersionsInconsist();
                     ExecuteBatInconsist(true);
                  }
                  else
                  {
                     Finish();
                  }
               }
               else
                  MessageBox.Show(string.Format("Versões não Geradas:\n\n{0}", string.Join("\n", this.ListInconsistProjects.ConvertAll(x => x.Name))));
            }
         }
      }

      private void Finish()
      {
         ZipFiles();
         Thread.Sleep(10);
         MessageBox.Show("Versões Geradas com Sucesso");
         Process.Start(TxtPathVersions.Text);
      }

      private void BtnGenerate_Click(object sender, EventArgs e)
      {
         try
         {
            if (Validar())
            {
               CleanDirectory();
               CreateFoldersVersions();
               SaveTempProjects();
               SaveLastPath();
               ChangeAssemblyVersion();
               GenerateVersions();

               ExecuteBat();               
            }
         }
         catch (Exception)
         {
            MessageBox.Show("Ocorreu um erro ao criar as pastas. Verifique se o caminho está correto e se você tem permissão para modificar o diretório.");
         }
      }

      private void BtnPathProject_Click(object sender, EventArgs e)
      {
         DirectoryInfo info = null;

         FolderBrowserDialog folderDialog = new FolderBrowserDialog();

         if (folderDialog.ShowDialog() == DialogResult.OK)
            info = new DirectoryInfo(folderDialog.SelectedPath);

         if (info != null)
         {
            if (info.FullName.Last().ToString().Equals(@"\"))
               TxtPathProject.Text = info.FullName;
            else
               TxtPathProject.Text = info.FullName + @"\";
         }
         else
            TxtPathProject.Text = string.Empty;
      }

      private void ZipFiles()
      {
         var zips = Directory.EnumerateFiles(TxtPathVersions.Text, "*.zip", SearchOption.TopDirectoryOnly);
         foreach (var zip in zips)
            File.Delete(zip);

         var folders = Directory.GetDirectories(TxtPathVersions.Text);

         if (folders.Count() > 0)
         {
            using (var load = new LoadingProgress("Zipando Arquivos..."))
            {
               load.TotalRegistros = folders.Count();

               foreach (var folder in folders)
               {
                  var folderName = Path.GetFileName(folder.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                  var zipPath = Path.Combine(TxtPathVersions.Text, folderName + ".zip");

                  ZipFile.CreateFromDirectory(
                      folder,
                      zipPath,
                      CompressionLevel.Optimal,
                      includeBaseDirectory: false
                  );

                  load.TotalRegistrosProcessados++;
                  load.AtualizarProgresso();
               }
            }
         }
      }

      private void BtnZiparVersoes_Click(object sender, EventArgs e)
      {
         try
         {
            if (string.IsNullOrEmpty(TxtPathVersions.Text) || !Directory.Exists(TxtPathVersions.Text))
            {
               MessageBox.Show("Por favor, selecione um caminho para as versões válido.");
               return;
            }

            ZipFiles();

            Thread.Sleep(10);
            MessageBox.Show("Pastas Zipadas com Sucesso");
         }
         catch (Exception)
         {
            MessageBox.Show("Ocorreu um erro ao zipar as pastas. Verifique se o caminho está correto e se você tem permissão para modificar o diretório.");
         }
      }
   }
}
