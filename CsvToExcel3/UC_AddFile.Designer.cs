
namespace CsvToExcel3
{
    partial class UC_AddCsv
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenCSV = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCsvNumber = new System.Windows.Forms.Label();
            this.customProgressBar1 = new CsvToExcel3.CustomControls.customProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenCSV
            // 
            this.btnOpenCSV.FlatAppearance.BorderSize = 0;
            this.btnOpenCSV.Location = new System.Drawing.Point(323, 19);
            this.btnOpenCSV.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenCSV.Name = "btnOpenCSV";
            this.btnOpenCSV.Size = new System.Drawing.Size(52, 21);
            this.btnOpenCSV.TabIndex = 15;
            this.btnOpenCSV.Text = "Otevřít";
            this.btnOpenCSV.UseVisualStyleBackColor = true;
            this.btnOpenCSV.Click += new System.EventHandler(this.btnOpenCSV_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtFilePath, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(319, 26);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFilePath.Location = new System.Drawing.Point(2, 2);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(315, 23);
            this.txtFilePath.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CsvToExcel3.Properties.Resources.minus;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(416, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 19);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // txtCsvNumber
            // 
            this.txtCsvNumber.AutoSize = true;
            this.txtCsvNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCsvNumber.Location = new System.Drawing.Point(2, 0);
            this.txtCsvNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtCsvNumber.Name = "txtCsvNumber";
            this.txtCsvNumber.Size = new System.Drawing.Size(43, 17);
            this.txtCsvNumber.TabIndex = 17;
            this.txtCsvNumber.Text = "Csv #";
            // 
            // customProgressBar1
            // 
            this.customProgressBar1.CustomText = null;
            this.customProgressBar1.DisplayStyle = CsvToExcel3.CustomControls.ProgressBarDisplayText.Percentage;
            this.customProgressBar1.Location = new System.Drawing.Point(378, 19);
            this.customProgressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.customProgressBar1.Name = "customProgressBar1";
            this.customProgressBar1.Size = new System.Drawing.Size(31, 20);
            this.customProgressBar1.TabIndex = 19;
            // 
            // UC_AddCsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customProgressBar1);
            this.Controls.Add(this.btnOpenCSV);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCsvNumber);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_AddCsv";
            this.Size = new System.Drawing.Size(434, 42);
            this.Load += new System.EventHandler(this.UC_AddCsv_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenCSV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label txtCsvNumber;
        private CustomControls.customProgressBar customProgressBar1;
    }
}
