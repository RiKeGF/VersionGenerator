using FolderManager;
using System;
using System.Drawing;
using System.Threading;

namespace Forza.Shared.Utilitarios
{
   /// <summary>
   /// Invoca o Formulário de Carregamento Com Relatório de Progresso.
   /// </summary>
   public class LoadingProgress : IDisposable
   {
      private Thread thread = null;

      /// <summary>
      /// Representa o Formulário Para Exibição dos Dados.
      /// </summary>
      private LoadProgress load = null;

      /// <summary>
      /// Representa a Mensagem a Ser Exibida Durante o Processamento.
      /// </summary>
      private string msg = string.Empty;

      /// <summary>
      /// Representa o Total de Registros a Serem Processados.
      /// </summary>
      public int TotalRegistros { get; set; }

      /// <summary>
      /// Representa o Total de Registros Processados.
      /// </summary>
      public int TotalRegistrosProcessados { get; set; }

      /// <summary>
      /// Representa o Percentual de Progresso.
      /// </summary>
      public int PercentualProgresso { get; set; }

      /// <summary>
      /// Representa o Processo Atualmente Sendo Executado.
      /// </summary>
      public string ProcessoAtual { get; set; }

      /// <summary>
      /// Cria uma Nova Instância da Tela de Carregamento Recebendo a Descrição do Processo Por Parâmetro.
      /// </summary>
      /// <param name="msg"></param>
      public LoadingProgress(string msg)
      {
         this.msg = msg;
         this.load = new LoadProgress(msg);

         this.StartWork();
      }

      /// <summary>
      /// Cria uma Nova Instância da Tela de Carregamento Recebendo a Descrição do Processo e o Tamanho da Tela.
      /// </summary>
      /// <param name="msg"></param>
      /// <param name="tamanho"></param>
      public LoadingProgress(string msg, Size tamanho)
      {
         this.msg = msg;
         this.load = new LoadProgress(msg);
         this.load.Size = tamanho;

         this.StartWork();
      }

      private void StartWork()
      {
         if (load == null)
            load = new LoadProgress(msg);

         thread = new Thread(IniciaLoad);
         thread.Start();
      }
      private void IniciaLoad()
      {
         load.ShowDialog();
      }

      public void Dispose()
      {
         Thread.Sleep(250);
         load.End();
      }

      /// <summary>
      /// Atualiza as Informações do Progresso do Processo em Tela.
      /// </summary>
      public void AtualizarProgresso()
      {
         if (this.TotalRegistros > 0)
            this.PercentualProgresso = (int)(this.TotalRegistrosProcessados * 100 / this.TotalRegistros);

         this.load.AtualizarProgresso(this.PercentualProgresso);
      }

      public void SetTitulo(string msg)
      {
         this.msg = msg;
         this.load.SetTitulo(msg);
      }

      public void Fechar()
      {
         Dispose();
      }
   }
}
