namespace VersionGenerator
{
   partial class Form1
   {
      /// <summary>
      /// Variável de designer necessária.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Limpar os recursos que estão sendo usados.
      /// </summary>
      /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Código gerado pelo Windows Form Designer

      /// <summary>
      /// Método necessário para suporte ao Designer - não modifique 
      /// o conteúdo deste método com o editor de código.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.BtnPathProject = new System.Windows.Forms.Button();
         this.TxtPathProject = new System.Windows.Forms.TextBox();
         this.TxtPathVersions = new System.Windows.Forms.TextBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.TxtVersion = new System.Windows.Forms.TextBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.CmbType = new System.Windows.Forms.ComboBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.TxtReference = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.BtnRemove = new System.Windows.Forms.Button();
         this.Dgv = new System.Windows.Forms.DataGridView();
         this.BtnAdd = new System.Windows.Forms.Button();
         this.TxtFolderName = new System.Windows.Forms.TextBox();
         this.BtnGenerate = new System.Windows.Forms.Button();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.BtnPathVersions = new System.Windows.Forms.Button();
         this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.BtnZiparVersoes = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.Dgv)).BeginInit();
         this.groupBox4.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.BtnPathProject);
         this.groupBox1.Controls.Add(this.TxtPathProject);
         this.groupBox1.Location = new System.Drawing.Point(12, 12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(641, 49);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Caminho Projeto";
         // 
         // BtnPathProject
         // 
         this.BtnPathProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.BtnPathProject.Image = ((System.Drawing.Image)(resources.GetObject("BtnPathProject.Image")));
         this.BtnPathProject.Location = new System.Drawing.Point(606, 17);
         this.BtnPathProject.Name = "BtnPathProject";
         this.BtnPathProject.Size = new System.Drawing.Size(25, 25);
         this.BtnPathProject.TabIndex = 1;
         this.BtnPathProject.UseVisualStyleBackColor = true;
         this.BtnPathProject.Click += new System.EventHandler(this.BtnPathProject_Click);
         // 
         // TxtPathProject
         // 
         this.TxtPathProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.TxtPathProject.Location = new System.Drawing.Point(7, 19);
         this.TxtPathProject.Name = "TxtPathProject";
         this.TxtPathProject.Size = new System.Drawing.Size(594, 20);
         this.TxtPathProject.TabIndex = 0;
         // 
         // TxtPathVersions
         // 
         this.TxtPathVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.TxtPathVersions.Location = new System.Drawing.Point(6, 20);
         this.TxtPathVersions.Name = "TxtPathVersions";
         this.TxtPathVersions.Size = new System.Drawing.Size(594, 20);
         this.TxtPathVersions.TabIndex = 0;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.TxtVersion);
         this.groupBox2.Location = new System.Drawing.Point(659, 12);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(123, 49);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Versão";
         // 
         // TxtVersion
         // 
         this.TxtVersion.Location = new System.Drawing.Point(6, 19);
         this.TxtVersion.Name = "TxtVersion";
         this.TxtVersion.Size = new System.Drawing.Size(111, 20);
         this.TxtVersion.TabIndex = 1;
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.CmbType);
         this.groupBox3.Controls.Add(this.label2);
         this.groupBox3.Controls.Add(this.label3);
         this.groupBox3.Controls.Add(this.TxtReference);
         this.groupBox3.Controls.Add(this.label1);
         this.groupBox3.Controls.Add(this.BtnRemove);
         this.groupBox3.Controls.Add(this.Dgv);
         this.groupBox3.Controls.Add(this.BtnAdd);
         this.groupBox3.Controls.Add(this.TxtFolderName);
         this.groupBox3.Location = new System.Drawing.Point(12, 118);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(641, 309);
         this.groupBox3.TabIndex = 2;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Pastas";
         // 
         // CmbType
         // 
         this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.CmbType.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.CmbType.FormattingEnabled = true;
         this.CmbType.Location = new System.Drawing.Point(504, 19);
         this.CmbType.Name = "CmbType";
         this.CmbType.Size = new System.Drawing.Size(97, 21);
         this.CmbType.TabIndex = 2;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(467, 23);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(31, 13);
         this.label2.TabIndex = 10;
         this.label2.Text = "Tipo:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(273, 23);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(27, 13);
         this.label3.TabIndex = 9;
         this.label3.Text = "Ref:";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TxtReference
         // 
         this.TxtReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.TxtReference.Location = new System.Drawing.Point(306, 20);
         this.TxtReference.Name = "TxtReference";
         this.TxtReference.Size = new System.Drawing.Size(155, 20);
         this.TxtReference.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(7, 23);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(38, 13);
         this.label1.TabIndex = 6;
         this.label1.Text = "Nome:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // BtnRemove
         // 
         this.BtnRemove.Image = global::VersionGenerator.Properties.Resources.est_remove_16_16;
         this.BtnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.BtnRemove.Location = new System.Drawing.Point(1, 278);
         this.BtnRemove.Name = "BtnRemove";
         this.BtnRemove.Size = new System.Drawing.Size(75, 27);
         this.BtnRemove.TabIndex = 4;
         this.BtnRemove.Text = "Remover";
         this.BtnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.BtnRemove.UseVisualStyleBackColor = true;
         this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
         // 
         // Dgv
         // 
         this.Dgv.AllowUserToAddRows = false;
         this.Dgv.AllowUserToDeleteRows = false;
         this.Dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.Dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
         this.Dgv.Location = new System.Drawing.Point(0, 45);
         this.Dgv.Name = "Dgv";
         this.Dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.Dgv.Size = new System.Drawing.Size(641, 228);
         this.Dgv.TabIndex = 3;
         // 
         // BtnAdd
         // 
         this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.BtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("BtnAdd.Image")));
         this.BtnAdd.Location = new System.Drawing.Point(607, 17);
         this.BtnAdd.Name = "BtnAdd";
         this.BtnAdd.Size = new System.Drawing.Size(25, 25);
         this.BtnAdd.TabIndex = 2;
         this.BtnAdd.UseVisualStyleBackColor = true;
         this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
         // 
         // TxtFolderName
         // 
         this.TxtFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.TxtFolderName.Location = new System.Drawing.Point(51, 20);
         this.TxtFolderName.Name = "TxtFolderName";
         this.TxtFolderName.Size = new System.Drawing.Size(213, 20);
         this.TxtFolderName.TabIndex = 0;
         // 
         // BtnGenerate
         // 
         this.BtnGenerate.Image = global::VersionGenerator.Properties.Resources.ok;
         this.BtnGenerate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
         this.BtnGenerate.Location = new System.Drawing.Point(692, 329);
         this.BtnGenerate.Name = "BtnGenerate";
         this.BtnGenerate.Size = new System.Drawing.Size(84, 47);
         this.BtnGenerate.TabIndex = 3;
         this.BtnGenerate.Text = "Gerar Versões";
         this.BtnGenerate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
         this.BtnGenerate.UseVisualStyleBackColor = true;
         this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
         // 
         // groupBox4
         // 
         this.groupBox4.Controls.Add(this.BtnPathVersions);
         this.groupBox4.Controls.Add(this.TxtPathVersions);
         this.groupBox4.Location = new System.Drawing.Point(13, 63);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(641, 49);
         this.groupBox4.TabIndex = 2;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "Caminho Versões";
         // 
         // BtnPathVersions
         // 
         this.BtnPathVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.BtnPathVersions.Image = ((System.Drawing.Image)(resources.GetObject("BtnPathVersions.Image")));
         this.BtnPathVersions.Location = new System.Drawing.Point(606, 17);
         this.BtnPathVersions.Name = "BtnPathVersions";
         this.BtnPathVersions.Size = new System.Drawing.Size(25, 25);
         this.BtnPathVersions.TabIndex = 1;
         this.BtnPathVersions.UseVisualStyleBackColor = true;
         this.BtnPathVersions.Click += new System.EventHandler(this.BtnPath_Click);
         // 
         // Column3
         // 
         this.Column3.DataPropertyName = "IsSelected";
         this.Column3.FillWeight = 25F;
         this.Column3.HeaderText = "";
         this.Column3.MinimumWidth = 25;
         this.Column3.Name = "Column3";
         this.Column3.Width = 25;
         // 
         // Column1
         // 
         this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.Column1.DataPropertyName = "Name";
         this.Column1.HeaderText = "Nome da Pasta";
         this.Column1.Name = "Column1";
         this.Column1.ReadOnly = true;
         this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         // 
         // Column2
         // 
         this.Column2.DataPropertyName = "Reference";
         this.Column2.FillWeight = 200F;
         this.Column2.HeaderText = "Referência";
         this.Column2.Name = "Column2";
         this.Column2.ReadOnly = true;
         this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         this.Column2.Width = 200;
         // 
         // BtnZiparVersoes
         // 
         this.BtnZiparVersoes.Image = ((System.Drawing.Image)(resources.GetObject("BtnZiparVersoes.Image")));
         this.BtnZiparVersoes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
         this.BtnZiparVersoes.Location = new System.Drawing.Point(692, 382);
         this.BtnZiparVersoes.Name = "BtnZiparVersoes";
         this.BtnZiparVersoes.Size = new System.Drawing.Size(84, 47);
         this.BtnZiparVersoes.TabIndex = 4;
         this.BtnZiparVersoes.Text = "Zipar Versões";
         this.BtnZiparVersoes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
         this.BtnZiparVersoes.UseVisualStyleBackColor = true;
         this.BtnZiparVersoes.Click += new System.EventHandler(this.BtnZiparVersoes_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 436);
         this.Controls.Add(this.BtnZiparVersoes);
         this.Controls.Add(this.groupBox4);
         this.Controls.Add(this.BtnGenerate);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.MaximizeBox = false;
         this.Name = "Form1";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.Dgv)).EndInit();
         this.groupBox4.ResumeLayout(false);
         this.groupBox4.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button BtnPathProject;
      private System.Windows.Forms.TextBox TxtPathVersions;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.TextBox TxtVersion;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button BtnRemove;
      private System.Windows.Forms.DataGridView Dgv;
      private System.Windows.Forms.Button BtnAdd;
      private System.Windows.Forms.TextBox TxtFolderName;
      private System.Windows.Forms.Button BtnGenerate;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Button BtnPathVersions;
      private System.Windows.Forms.TextBox TxtPathProject;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox TxtReference;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox CmbType;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
      private System.Windows.Forms.Button BtnZiparVersoes;
   }
}

