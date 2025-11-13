using System.Windows.Forms;

namespace FolderManager
{
   internal partial class Load : Form
   {
      private delegate void SafeCallDelegate(string msg);
      private bool IsReady = false;

      public Load()
      {
         InitializeComponent();
         this.IsReady = true;
#if DEBUG
         this.TopMost = false;
#else
         this.TopMost = true;
#endif
      }

      public Load(string mensagem)
      {
         InitializeComponent();
         this.LblDescricao.Text = mensagem;
         this.IsReady = true;

#if DEBUG
         this.TopMost = false;
#else
         this.TopMost = true;
#endif
      }

      public bool CheckIsReady()
      {
         return this.IsReady;
      }

      private delegate void EndDelegate();
      public void End()
      {
         if (this.InvokeRequired)
            this.BeginInvoke(new EndDelegate(End));
         else
            this.Close();
      }

      public void AtualizarMensagem(string msg)
      {
         if (this.LblDescricao.InvokeRequired)
         {
            SafeCallDelegate d = new SafeCallDelegate(AtualizarMensagem);
            this.LblDescricao.Invoke(d, new object[] { msg });
         }
         else
         {
            this.LblDescricao.Text = msg;
            this.LblDescricao.Refresh();
         }
      }

      private const int CP_NOCLOSE_BUTTON = 0x200;
      protected override CreateParams CreateParams
      {
         get
         {
            CreateParams myCp = base.CreateParams;
            myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
            return myCp;
         }
      }
   }
}
