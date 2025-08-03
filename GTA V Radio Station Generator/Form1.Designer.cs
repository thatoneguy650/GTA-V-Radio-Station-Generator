namespace GTA_V_Radio_Station_Generator
{
    partial class Form1
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
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateStations = new System.Windows.Forms.Button();
            this.dgvRadioStations = new System.Windows.Forms.DataGridView();
            this.dgvVanilla = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvGenre = new System.Windows.Forms.DataGridView();
            this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenreID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbTrackID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRadioStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVanilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGenre)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(3, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(191, 58);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnOpenFolder, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCreateStations, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.dgvRadioStations, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvVanilla, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvGenre, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1714, 988);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnCreateStations
            // 
            this.btnCreateStations.Location = new System.Drawing.Point(1471, 921);
            this.btnCreateStations.Name = "btnCreateStations";
            this.btnCreateStations.Size = new System.Drawing.Size(150, 63);
            this.btnCreateStations.TabIndex = 1;
            this.btnCreateStations.Text = "Create Stations";
            this.btnCreateStations.UseVisualStyleBackColor = true;
            this.btnCreateStations.Click += new System.EventHandler(this.btnCreateStations_Click);
            // 
            // dgvRadioStations
            // 
            this.dgvRadioStations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvRadioStations, 3);
            this.dgvRadioStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRadioStations.Location = new System.Drawing.Point(3, 190);
            this.dgvRadioStations.Name = "dgvRadioStations";
            this.dgvRadioStations.Size = new System.Drawing.Size(1708, 319);
            this.dgvRadioStations.TabIndex = 2;
            // 
            // dgvVanilla
            // 
            this.dgvVanilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvVanilla, 2);
            this.dgvVanilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVanilla.Location = new System.Drawing.Point(3, 596);
            this.dgvVanilla.Name = "dgvVanilla";
            this.dgvVanilla.Size = new System.Drawing.Size(1462, 319);
            this.dgvVanilla.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 562);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1708, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vanilla Radio Stations";
            // 
            // label2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 3);
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1708, 58);
            this.label2.TabIndex = 5;
            this.label2.Text = "New Radio Stations List";
            // 
            // dgvGenre
            // 
            this.dgvGenre.AllowUserToAddRows = false;
            this.dgvGenre.AllowUserToDeleteRows = false;
            this.dgvGenre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGenre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Genre,
            this.GenreID});
            this.dgvGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGenre.Location = new System.Drawing.Point(1471, 596);
            this.dgvGenre.Name = "dgvGenre";
            this.dgvGenre.Size = new System.Drawing.Size(240, 319);
            this.dgvGenre.TabIndex = 6;
            // 
            // Genre
            // 
            this.Genre.Frozen = true;
            this.Genre.HeaderText = "Genre";
            this.Genre.Name = "Genre";
            this.Genre.ReadOnly = true;
            // 
            // GenreID
            // 
            this.GenreID.Frozen = true;
            this.GenreID.HeaderText = "GenreID";
            this.GenreID.Name = "GenreID";
            this.GenreID.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tbTrackID, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1471, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(240, 100);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tbTrackID
            // 
            this.tbTrackID.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbTrackID.Location = new System.Drawing.Point(3, 53);
            this.tbTrackID.Name = "tbTrackID";
            this.tbTrackID.Size = new System.Drawing.Size(114, 20);
            this.tbTrackID.TabIndex = 0;
            this.tbTrackID.Text = "18500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "TrackID Start";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1714, 988);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "GTA V Radio Station Generator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRadioStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVanilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGenre)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCreateStations;
        private System.Windows.Forms.DataGridView dgvRadioStations;
        private System.Windows.Forms.DataGridView dgvVanilla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvGenre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenreID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbTrackID;
        private System.Windows.Forms.Label label3;
    }
}

