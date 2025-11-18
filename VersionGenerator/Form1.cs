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
      private int Qtd;

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
            string path = $@"{TxtPathProject.Text}{item.Reference}";

            var listFiles = new List<string>();

            listFiles.AddRange(Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories));
            listFiles.AddRange(Directory.GetFiles(path, "*.vbproj", SearchOption.AllDirectories));

            if (listFiles.Count > 0)
            {
               foreach (var file in listFiles)
               {
                  var xml = XDocument.Load(file);
                  var ns = xml.Root.Name.Namespace;

                  var group = xml.Root.Elements(ns + "PropertyGroup")
                                      .FirstOrDefault(e => e.Attribute("Condition") == null)
                             ?? new XElement(ns + "PropertyGroup");
                  if (group.Parent == null) xml.Root.AddFirst(group);

                  group.SetElementValue(ns + "Version", RetornarVersao());
                  group.SetElementValue(ns + "FileVersion", RetornarVersao());
                  group.SetElementValue(ns + "AssemblyVersion", RetornarVersao());
                  group.SetElementValue(ns + "InformationalVersion", RetornarVersao());

                  xml.Save(file);
               }
            }

            listFiles = Directory.GetFiles(path, "AssemblyInfo.*", SearchOption.AllDirectories).ToList();

            if (listFiles.Count > 0)
            {
               foreach (var file in listFiles)
               {
                  var txt = System.IO.File.ReadAllText(file);

                  // substitui ou injeta
                  string ReplaceOrAdd(string input, string attr, string value)
                  {
                     if (Path.GetExtension(file).Equals(".vb"))
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

                  System.IO.File.WriteAllText(file, txt);
               }
            }
         }
      }

      private void ChangeReleaseDirectory()
      {
         foreach (var item in this.ListProjects.FindAll(x => x.IsSelected && x.Type != ProjectType.API))
         {
            string path = $@"{TxtPathProject.Text}{item.Reference}";

            var listFiles = new List<string>();

            listFiles.AddRange(Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories));
            listFiles.AddRange(Directory.GetFiles(path, "*.vbproj", SearchOption.AllDirectories));

            if (listFiles.Count > 0)
            {
               foreach (var file in listFiles)
               {
                  var xml = XDocument.Load(file);
                  var ns = xml.Root.Name.Namespace;

                  var group = xml.Root.Elements(ns + "PropertyGroup").FirstOrDefault(e => ((string)e.Attribute("Condition") ?? "")
                                                                     .Replace(" ", "") == "'$(Configuration)|$(Platform)'=='Release|AnyCPU'");

                  if (group == null || group.Parent == null) continue;

                  group.SetElementValue(ns + "OutputPath", $@"{TxtPathVersions.Text}{item.Name}\");

                  xml.Save(file);
               }
            }
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
         if (!this.ListProjects.Exists(x => x.IsSelected))
         {
            MessageBox.Show("Selecione um projeto para gerar");
            return false;
         }

         return true;
      }

      private void GenerateVersions(List<Project> list, string batname = "")
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

            foreach (var item in list.FindAll(x => x.IsSelected))
            {
               // acha .csproj/.vbproj
               var baseProjDir = System.IO.Path.Combine(TxtPathProject.Text, item.Reference);

               var listFiles = new List<string>();

               listFiles.AddRange(Directory.GetFiles(baseProjDir, "*.csproj", SearchOption.AllDirectories));
               listFiles.AddRange(Directory.GetFiles(baseProjDir, "*.vbproj", SearchOption.AllDirectories));

               if (listFiles.Count == 0) continue;

               foreach (var file in listFiles)
               {
                  var label = item.Name;

                  if (!string.IsNullOrEmpty(TxtVersion.Text))
                     label = $"{item.Name} - {TxtVersion.Text}";

                  var outDir = System.IO.Path.Combine(TxtPathVersions.Text, label);

                  bat.AppendLine("REM ---------------------------");
                  bat.AppendLine($@"REM Projeto: {item.Name}");
                  bat.AppendLine("REM ---------------------------");
                  bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
                  bat.AppendLine($@"set SolutionFile=""{file}""");
                  bat.AppendLine($@"set VSEnvCmd=""C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat""");
                  bat.AppendLine("echo -----------------------------------------------");
                  bat.AppendLine($@"echo {label}");
                  bat.AppendLine("echo -----------------------------------------------");
                  bat.AppendLine("ping 127.0.0.1 -n 6 >nul");
                  bat.AppendLine();
                  bat.AppendLine(@"CALL %VSEnvCmd%");
                  bat.AppendLine(@"echo Iniciando a build da solução em modo Release...");

                  if (item.Type.Equals(ProjectType.API))
                     bat.AppendLine($@"msbuild ""{file}"" /t:WebPublish /p:Configuration=Release /p:DeployOnBuild=true /p:WebPublishMethod=FileSystem /p:PublishUrl=""{outDir}""  /p:DeleteExistingFiles=true /p:UseWPP_CopyWebApplication=True /p:PipelineDependsOnBuild=False");
                  else if (item.Type.Equals(ProjectType.API6) || item.Type.Equals(ProjectType.API8))
                     bat.AppendLine($@"msbuild ""{file}"" /t:Publish /p:Configuration=Release /p:PublishDir=""{outDir}"" /p:DeleteExistingFiles=true");
                  else
                     bat.AppendLine($@"msbuild ""{file}"" /t:Build /p:Configuration=Release");

                  
                  //  bat.AppendLine($@"msbuild ""{file}"" /p:Configuration=Release /p:Platform=""Any CPU"" /p:DeployOnBuild=true /p:WebPublishMethod=FileSystem /p:PublishUrl=""{outDir}""");
                  //bat.AppendLine($@"msbuild ""{file}"" /t:Publish /p:Configuration=Release /p:PublishDir=""{outDir}"" /p:DeleteExistingFiles=true");
                  //bat.AppendLine($@"msbuild ""{file}"" /t:Build /p:Configuration=Release /p:OutDir=""{outDir}"" /p:OutputPath=""{outDir}""");
                  // bat.AppendLine($@"msbuild ""{file}"" /t:Build /p:Configuration=Release /p:OutputPath=""{outDir}""");

                  bat.AppendLine(@"IF %ERRORLEVEL% EQU 0 (
                                 echo Build Release concluida com SUCESSO!
                             ) ELSE (
                                 echo Ocorreu um ERRO durante a build Release.
                                 pause
                             )");

                  bat.AppendLine();
                  bat.AppendLine();
                  bat.AppendLine();
                  bat.AppendLine();
                  bat.AppendLine();
                  bat.AppendLine();
                  bat.AppendLine();
               }
            }

            var batPath = System.IO.Path.Combine(TxtPathVersions.Text, $"gerar{batname}_v{RetornarVersao()}.bat");
            File.WriteAllText(batPath, bat.ToString(), Encoding.Default);
         }
         catch (Exception)
         {
            throw;
         }
      }

      private void ExecuteBat(string batname = "")
      {
         this.Qtd++;
         var path = $@"{TxtPathVersions.Text}gerar{batname}_v{RetornarVersao()}.bat";
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
               this.ListInconsistProjects.AddRange(this.ListProjects.FindAll(x => x.IsSelected && Directory.GetFiles(Path.Combine(TxtPathVersions.Text, (string.IsNullOrEmpty(RetornarVersao())) ? x.Name : string.Concat(x.Name, " - ", RetornarVersao()))).Length == 0));

               if (this.ListInconsistProjects.Count > 0 && this.Qtd <= 3)
               {
                  GenerateVersions(this.ListInconsistProjects, "_inconsist");
                  ExecuteBat("_inconsist");
               }
               else
               {
                  Finish();
               }
            }
         }
      }

      private void Finish()
      {
         ZipFiles();
         Thread.Sleep(10);
         MessageBox.Show("Versões Geradas com Sucesso");

         if (this.ListInconsistProjects.Count > 0)
            MessageBox.Show(string.Format("Versões não Geradas:\n\n{0}", string.Join("\n", this.ListInconsistProjects.ConvertAll(x => x.Name))));

         Process.Start(TxtPathVersions.Text);
      }

      private void BtnGenerate_Click(object sender, EventArgs e)
      {
         try
         {
            this.Qtd = 0;

            if (Validar())
            {
               CleanDirectory();
               CreateFoldersVersions();
               SaveTempProjects();
               SaveLastPath();
               ChangeAssemblyVersion();
               ChangeReleaseDirectory();
               GenerateVersions(this.ListProjects);

               ExecuteBat();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
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
                      includeBaseDirectory: true
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

      private void ChkAll_CheckedChanged(object sender, EventArgs e)
      {
         this.ListProjects.ForEach(x => x.IsSelected = ChkAll.Checked);
         Dgv.Refresh();
      }
   }
}
