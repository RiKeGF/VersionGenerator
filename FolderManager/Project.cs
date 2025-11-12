namespace FolderManager
{
   public class Project
   {
      public Project(string name, string reference, ProjectType type)
      {
         this.Name = name;
         this.Reference = reference;
         this.Type = type;
      }

      public string Name { get; set; }

      public string Reference { get; set; }

      public ProjectType Type { get; set; }
   }

   public enum ProjectType
   {
      API, 
      WinForms, 
      AspNetMvc,
      Outro,
   }
}
