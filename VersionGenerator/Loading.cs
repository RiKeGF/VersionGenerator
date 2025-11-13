using System;
using System.Threading;
using VersionGenerator;

namespace Forza.Shared.Utilitarios
{
   /// <summary>
   /// Invoca o Formulário de Carregamento Padrão.
   /// </summary>
   public class Loading : IDisposable
   {
      private static Thread thread = null;
      private static Load load = null;
      private static string Mensagem = string.Empty;

      /// <summary>
      /// Cria uma Nova Instância da Tela de Carregamento. Opcional: Informar a Descrição do Processo Por Parâmetro.
      /// </summary>
      /// <param name="msg"></param>
      public Loading(string msg = null)
      {
         Mensagem = msg;
         StartWork();
      }

      private static void StartWork()
      {
         load = new Load((string.IsNullOrEmpty(Mensagem) ? "Aguarde... Processando" : Mensagem));

         thread = new Thread(IniciaLoad);
         thread.Start();
      }
      private static void IniciaLoad()
      {
         load.ShowDialog();
      }

      public void Dispose()
      {
         Thread.Sleep(250);
         load.End();
      }

      /// <summary>
      /// Atualiza a Mensagem Exibida no Título da Tela de Carregamento.
      /// </summary>
      /// <param name="msg"></param>
      public void AtualizarMensagem(string msg)
      {
         load.AtualizarMensagem(msg);
      }

      /// <summary>
      /// Exibe a Tela de Carregamento. Deve Ser Encerrada de Forma Manual.
      /// </summary>
      /// <param name="msg"></param>
      public static void Show(string msg = null)
      {
         if (load == null)
            load = new Load(string.IsNullOrEmpty(msg) ? "Aguarde... Carregando." : msg);

         StartWork();
      }

      /// <summary>
      /// Finaliza a Tela de Carregamento.
      /// </summary>
      public static void Close()
      {
         load.End();
         Thread.Sleep(250);
      }
   }
}
