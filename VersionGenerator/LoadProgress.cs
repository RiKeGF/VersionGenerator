using System.Windows.Forms;

namespace VersionGenerator
{
   internal partial class LoadProgress : Form
   {

      private delegate void SafeCallDelegate(string processo, int percentualProcessado);

      public LoadProgress(string mensagem)
      {
         InitializeComponent();
         this.LblDescricao.Text = mensagem;
      }

      private delegate void EndDelegate();
      public void End()
      {
         if (this.InvokeRequired)
            this.BeginInvoke(new EndDelegate(End));
         else
            this.Close();
      }

      public void SetTitulo(string msg)
      {
         if (this.InvokeRequired)
         {
            Invoke((MethodInvoker)delegate
            {
               this.LblDescricao.Text = msg;
            });
         }
      }

      public void AtualizarProgresso(int percentualProcessado)
      {
         if (this.InvokeRequired)
         {
            Invoke((MethodInvoker)delegate
            {
               if (percentualProcessado != this.PgBarProgresso.Value)
               {
                  this.LblProgress.Text = string.Concat(percentualProcessado.ToString(), "%");
                  this.LblProgress.Refresh();
                  this.PgBarProgresso.Value = percentualProcessado;
                  this.PgBarProgresso.Refresh();
               }
            });
         }

      }
   }
}
