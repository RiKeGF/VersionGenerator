namespace VersionGenerator
{
    internal partial class LoadProgress
   {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadProgress));
         this.LblDescricao = new System.Windows.Forms.Label();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.PgBarProgresso = new System.Windows.Forms.ProgressBar();
         this.LblProgress = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         this.SuspendLayout();
         // 
         // LblDescricao
         // 
         this.LblDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LblDescricao.BackColor = System.Drawing.Color.Black;
         this.LblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.LblDescricao.ForeColor = System.Drawing.Color.Lime;
         this.LblDescricao.Location = new System.Drawing.Point(0, 41);
         this.LblDescricao.Name = "LblDescricao";
         this.LblDescricao.Size = new System.Drawing.Size(440, 29);
         this.LblDescricao.TabIndex = 0;
         this.LblDescricao.Text = "Convertendo";
         this.LblDescricao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
         this.pictureBox1.Location = new System.Drawing.Point(0, 10);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(220, 90);
         this.pictureBox1.TabIndex = 1;
         this.pictureBox1.TabStop = false;
         // 
         // pictureBox2
         // 
         this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
         this.pictureBox2.Location = new System.Drawing.Point(220, 10);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(220, 90);
         this.pictureBox2.TabIndex = 2;
         this.pictureBox2.TabStop = false;
         // 
         // PgBarProgresso
         // 
         this.PgBarProgresso.BackColor = System.Drawing.Color.White;
         this.PgBarProgresso.ForeColor = System.Drawing.Color.Lime;
         this.PgBarProgresso.Location = new System.Drawing.Point(81, 87);
         this.PgBarProgresso.Name = "PgBarProgresso";
         this.PgBarProgresso.Size = new System.Drawing.Size(279, 10);
         this.PgBarProgresso.TabIndex = 3;
         // 
         // LblProgress
         // 
         this.LblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LblProgress.BackColor = System.Drawing.Color.Black;
         this.LblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.LblProgress.ForeColor = System.Drawing.Color.Lime;
         this.LblProgress.Location = new System.Drawing.Point(199, 72);
         this.LblProgress.Name = "LblProgress";
         this.LblProgress.Size = new System.Drawing.Size(42, 13);
         this.LblProgress.TabIndex = 4;
         this.LblProgress.Text = "0%";
         this.LblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // LoadProgress
         // 
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(14)))), ((int)(((byte)(0)))));
         this.ClientSize = new System.Drawing.Size(441, 111);
         this.Controls.Add(this.LblProgress);
         this.Controls.Add(this.PgBarProgresso);
         this.Controls.Add(this.LblDescricao);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.pictureBox1);
         this.ForeColor = System.Drawing.Color.Lime;
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LoadProgress";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBoxGears;
      private System.Windows.Forms.Label LblDescricao;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.ProgressBar PgBarProgresso;
      private System.Windows.Forms.Label LblProgress;
   }
}