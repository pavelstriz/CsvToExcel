using CsvToExcel3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;


namespace CsvToExcel3
{
    public partial class UC_AddCsv : UserControl
    {
        private Form1 form1;
        public UC_AddCsv(Form1 _form1)
        {
            InitializeComponent();

            form1 = _form1;
        }

        public OpenFileDialog openFileDialog;


        public string fileName;

        public string directoryPath;


        public void UpdateProgress(int percent)
        {
            RunOnUiThread(() => customProgressBar1.Value = percent);
        }

        public void RunOnUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void btnOpenCSV_Click(object sender, EventArgs e)
        {
            

            openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Properties.Settings.Default.openCsvCache;//"c:\\users\\"+Environment.UserName+" \\Downloads";
            openFileDialog.Filter = "txt files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length == 1)
                {

                    txtFilePath.Text = openFileDialog.FileName;

                    fileName = openFileDialog.SafeFileName;

                    directoryPath = Path.GetDirectoryName(openFileDialog.FileName);

                    Properties.Settings.Default.openCsvCache = directoryPath + "\\";
                    Properties.Settings.Default.Save();
                }
                else
                {
                    string[] selectedFiles = (string[])openFileDialog.FileNames;


                    form1.flowLayoutPanel1.Controls.Clear();
                    form1.documentNumber = 0;
                    form1.flpTempCount = 0;

                    foreach (string file in selectedFiles)
                    {
                        //MessageBox.Show(file);

                        if (form1.flowLayoutPanel1.Controls.Count < 10)
                        {

                            form1.documentNumber++;

                            form1.uC_AddCsv = new UC_AddCsv(form1);
                            form1.uC_AddCsv.txtFilePath.Text = file;
                            form1.uC_AddCsv.fileName = Path.GetFileName(file);
                            form1.uC_AddCsv.txtCsvNumber.Text = "Csv #" + form1.documentNumber.ToString();
                            form1.flowLayoutPanel1.Controls.Add(form1.uC_AddCsv);

                            form1.MinimumSize = new Size(form1.Height, 306);
                            //form1.resizeHeightChanged = form1.uC_AddCsv.Height + 5;

                        }
                        else
                        {
                            MessageBox.Show("Bylo dosaženo maximálního počtu dokumentů (10)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                }
            }
        }
        string csvPath;
        string xlsPath;


        public void ExecuteConvertion()
        {
            try
            {

                try
                {
                    UpdateProgress(0);
                    csvPath = txtFilePath.Text;
                    xlsPath = form1.directoryPath + "\\" + fileName + ".xlsx";
                    UpdateProgress(25);
                    // Load source file CSV for conversion
                    var converter = new GroupDocs.Conversion.Converter(csvPath);
                    UpdateProgress(50);
                    // Prepare conversion options for target format XLSX
                    var convertOptions = converter.GetPossibleConversions()["xlsx"].ConvertOptions;
                    // Convert to XLSX format
                    UpdateProgress(75);
                    converter.Convert(xlsPath, convertOptions);
                    UpdateProgress(100);


                }
                catch (Exception ex)
                {
                    UpdateProgress(0);

                }
                finally
                {
                    //Closes last 10 sec process with name "Excel"
                    /*  System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                      foreach (System.Diagnostics.Process p in process)
                      {
                          if (!string.IsNullOrEmpty(p.ProcessName) && p.StartTime.AddSeconds(+5) > DateTime.Now)
                          {
                              try
                              {
                                  p.Kill();
                              }
                              catch { }
                          }
                      }*/
                    
                    //if(File.Exists(this.txtFilePath.Text))

                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Resources.minusFocus;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Resources.minus;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Parent.Controls.Remove(this);

            /*if (form1.flowLayoutPanel1.Controls.Count <= 2)
            {
                form1.flowLayoutPanel1.AutoScroll = true;

                form1.MinimumSize = new Size(form1.Width, form1.Height - (this.Height + 5));

                form1.Height = form1.Height - (this.Height + 5);

                form1.resizeHeightChanged = form1.Height;//form1.mainFormStartSize + (this.Height + 5) * (form1.flowLayoutPanel1.Controls.Count - 1);
            }
            else if (form1.flowLayoutPanel1.Controls.Count > 2)
            {
                form1.flowLayoutPanel1.AutoSize = false;
                form1.resizeHeightChanged = form1.MinimumSize.Height + (this.Height + 5) * (form1.flowLayoutPanel1.Controls.Count - 3);
            }*/

        }

        private void UC_AddCsv_Load(object sender, EventArgs e)
        {
            if (form1.flowLayoutPanel1.Controls.Count == 1)
            {
                pictureBox1.Hide();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox1, "Odebrat dokument");
        }
    }
}
