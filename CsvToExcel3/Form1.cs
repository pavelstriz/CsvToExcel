using CsvToExcel3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace CsvToExcel3
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public UC_AddCsv uC_AddCsv;

        string csv;
        string xls;


        UC_AddCsv[] listItems = new UC_AddCsv[10];
        ToolTip tt = new ToolTip();
        ToolTip tt2 = new ToolTip();

        public string[] pathDirectory = new string[10];
        public string[] fileNameDirectory = new string[10];

        public string directoryPath = Properties.Settings.Default.destinationFolderCache;

        private void btnConvert_Click(object sender, EventArgs e)
        {
            //PŘIDAT FOREACH   

            backgroundWorker1.RunWorkerAsync();


        }
        public Point mainFormStartLocation;
        //Size mainFormStartSize;

        public int mainFormStartSize;
        int maximalizedFormHeight;

        private void Form1_Load(object sender, EventArgs e)
        {
            mainFormStartLocation = this.Location;
            mainFormStartSize = this.Height;
            maximalizedFormHeight = this.Height;

            //this.txtFilePath.AutoSize = false;
            //this.txtFilePath.Size = new System.Drawing.Size(txtFilePath.Width, btnOpen.Height + 15);

            txtSavePath.Text = directoryPath;



            uC_AddCsv = new UC_AddCsv(this);
            uC_AddCsv.txtCsvNumber.Text = "Csv #" + documentNumber.ToString();
            flowLayoutPanel1.Controls.Add(uC_AddCsv);


            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);


            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height + (uC_AddCsv.Height * 10));

        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                txtSavePath.Text = folderPath;
                directoryPath = folderPath;
                Properties.Settings.Default.destinationFolderCache = directoryPath;
                Properties.Settings.Default.Save();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSavePath.Text) && Directory.Exists(txtSavePath.Text))
            {
                savePathExists = true;
                foreach (UC_AddCsv userControl in flowLayoutPanel1.Controls)
                {


                    if (!String.IsNullOrEmpty(userControl.txtFilePath.Text) && File.Exists(userControl.txtFilePath.Text))
                    {

                        userControl.ExecuteConvertion();

                        if (this.flowLayoutPanel1.Controls.Count == 1)
                        {
                            MessageBox.Show("Soubor byl uložen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            if (MessageBox.Show("Přejete si soubor otevřít?", "Akce", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                FileInfo fi = new FileInfo(directoryPath + "\\" + userControl.fileName + ".xlsx");
                                if (fi.Exists)
                                {
                                    System.Diagnostics.Process.Start(directoryPath + "\\" + userControl.fileName + ".xlsx");
                                }
                            }
                            else
                            {

                            }
                            userControl.UpdateProgress(0);
                        }
                        else
                        {
                            //MessageBox.Show("path doesnt exsits");
                            //return;
                        }
                    }
                    else
                    {

                        MessageBox.Show("Zvolte cestu k souboru.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;
                    }


                    //MessageBox.Show(userControl.ExecuteConvertion().ToString());

                }
                backgroundWorker1.CancelAsync();
            }
            else
            {
                savePathExists = false;
                MessageBox.Show("Vyberte cestu pro uložení.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
        }
        private bool savePathExists = false;

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 1 && savePathExists == true)
            {

                MessageBox.Show("Soubory byly uloženy.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (UC_AddCsv userControl in flowLayoutPanel1.Controls)
                {
                    userControl.UpdateProgress(0);
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            beingMoved = false;
            if (beingMoved)
            {
                //this.MinimumSize = new System.Drawing.Size(this.Width, maximalizedFormHeight);
                //this.MaximumSize = new System.Drawing.Size(this.Width, maximalizedFormHeight);
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (beingMoved != true)
            {
                resizeHeightChanged = this.Height;
            }
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Resources.plus3Focus;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Resources.plus;
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

            tt.SetToolTip(this.pictureBox1, "Přidat dokument");
        }


        //MAXIMALIZE & RELEASE
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [Obsolete("This function is unsafe. Use GetWindowLongPtr instead."),
        DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
        int Y, int cx, int cy, uint uFlags);

        const int WM_NCLBUTTONDBLCLK = 0x00A3; //double click on a title bar a.k.a. non-client area of the form
        const int WS_MAXIMIZE = 0x01000000;
        const int WS_MINIMIZE = 0x02000000;
        const int GWL_STYLE = -16;
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_RESTORE = 0xF120;
        const int SC_MAXIMIZETITLE = 0xF032;
        const int SC_RESTORETITLE = 0xF122;
        IntPtr HWND_TOP = (IntPtr)0;
        const int SWP_SHOWWINDOW = 0x0040;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_DRAWFRAME = 0x0020;

        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private const int WM_GETMINMAXINFO = 0x0024;

        // Prevent form from resizing if user attempts to left click
        // on title bar of form and drag it
        int WM_NCLBUTTONDOWN = 0xA1;
        int HTCAPTION = 0x02;
        int SC_MOVE = 0xF010;

        private const int WM_MOVING = 0x0216;
        int clicks = 0;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if ((int)m.WParam == SC_MAXIMIZE || (int)m.WParam == SC_MAXIMIZETITLE) //&& flowLayoutPanel1.Controls.Count > 3)
                {
                    maximalizedForm = true;
                    //MessageBox.Show("Maximized!!");
                    int iStyle = GetWindowLong(this.Handle, GWL_STYLE);
                    SetWindowLong(this.Handle, GWL_STYLE, iStyle | WS_MAXIMIZE);
                    SetWindowPos(this.Handle, HWND_TOP, 0, 0, this.Width, resizeHeightChanged, //this.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 3),
                     SWP_NOMOVE | SWP_NOZORDER | SWP_SHOWWINDOW);

                    maximalizedFormHeight = this.Height;

                    //this.Height = this.Height + ((uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 3));
                    //Center(this);
                    //MessageBox.Show("test");
                    return;

                }
                if ((int)m.WParam == SC_RESTORE || (int)m.WParam == SC_RESTORETITLE)
                {
                    //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height - ((uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 3)));
                    //MessageBox.Show("Restored down!!");
                    int iStyle = GetWindowLong(this.Handle, GWL_STYLE);
                    SetWindowLong(this.Handle, GWL_STYLE, iStyle & ~WS_MAXIMIZE);
                    SetWindowPos(this.Handle, HWND_TOP, 0, 0, this.Width, this.MinimumSize.Height,
                    SWP_DRAWFRAME | SWP_NOMOVE | SWP_NOZORDER | SWP_SHOWWINDOW);

                    return;
                }


            }
            base.WndProc(ref m);

        }
        private bool maximalizedForm = false;
        public int resizeHeightChanged = 0;
        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        private void pbOpenFolder_MouseEnter(object sender, EventArgs e)
        {
            pbOpenFolder.BackgroundImage = Resources.openFolderFocus;
        }

        private void pbOpenFolder_MouseLeave(object sender, EventArgs e)
        {
            pbOpenFolder.BackgroundImage = Resources.openFolder;
        }

        private void pbOpenFolder_MouseHover(object sender, EventArgs e)
        {
            tt2.SetToolTip(this.pbOpenFolder, "Otevřít adresář");
        }

        private void pbOpenFolder_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start(directoryPath);
        }
        public int documentNumber = 1;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            documentNumber++;

            if (flowLayoutPanel1.Controls.Count < 10)
            {
                this.BringToFront();


                uC_AddCsv = new UC_AddCsv(this);
                uC_AddCsv.txtCsvNumber.Text = "Csv #" + documentNumber.ToString();
                flowLayoutPanel1.Controls.Add(uC_AddCsv);

            }
            else
            {
                MessageBox.Show("Bylo dosaženo maximálního počtu dokumentů (10)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            txtNumberOfDocuments.Text = "Počet dokumentů: " + flowLayoutPanel1.Controls.Count.ToString();
            if (flowLayoutPanel1.Controls.Count <= 3)
                flpTempCount++;

            //MessageBox.Show(flpTempCount.ToString());

            if (flowLayoutPanel1.Controls.Count > 1 && flowLayoutPanel1.Controls.Count <= 3 && WindowState == FormWindowState.Normal && this.Height < 306)
            {


                this.flowLayoutPanel1.AutoScroll = true;
                this.Height = this.Height + uC_AddCsv.Height + 5;

                this.MinimumSize = new Size(this.Width, this.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - flpTempCount)); // -2
                resizeHeightChanged = this.MinimumSize.Height;
            }
            else if (flowLayoutPanel1.Controls.Count > 3)
            {
                this.flowLayoutPanel1.AutoSize = false;


                resizeHeightChanged = this.MinimumSize.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 3);

            }

        }
        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            //flpTempCount--;
            //this.flowLayoutPanel1.AutoScroll = false;


            if (flowLayoutPanel1.Controls.Count > 1 && flowLayoutPanel1.Controls.Count < 3 && WindowState == FormWindowState.Normal && this.Height < 306)
            {


                this.flowLayoutPanel1.AutoScroll = true;
                this.Height = this.Height + uC_AddCsv.Height + 5;


                this.MinimumSize = new Size(this.Width, this.Height + (uC_AddCsv.Height + 5)); // -2
                resizeHeightChanged = this.MinimumSize.Height;
            }
            else if (flowLayoutPanel1.Controls.Count >= 3)
            {
                this.flowLayoutPanel1.AutoSize = false;

                resizeHeightChanged = this.MinimumSize.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 3);

            }


            txtNumberOfDocuments.Text = "Počet dokumentů: " + flowLayoutPanel1.Controls.Count.ToString();



        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            beingMoved = true;
        }
        bool beingMoved = false;
        bool needsExplicitSizing;



        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        public int flpTempCount = 0; //-1
        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (MessageBox.Show("Přejete si přepsat stávající dokumenty?", "Akce", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                flowLayoutPanel1.Controls.Clear();
                documentNumber = 0;
                flpTempCount = 0;

                foreach (string file in files)
                {


                    if (flowLayoutPanel1.Controls.Count < 10)
                    {

                        documentNumber++;

                        uC_AddCsv = new UC_AddCsv(this);
                        uC_AddCsv.txtFilePath.Text = file;
                        uC_AddCsv.fileName = Path.GetFileName(file);
                        uC_AddCsv.txtCsvNumber.Text = "Csv #" + documentNumber.ToString();
                        flowLayoutPanel1.Controls.Add(uC_AddCsv);

                        this.MinimumSize = new Size(this.Height, 306);
                        //resizeHeightChanged = uC_AddCsv.Height + 5;

                    }
                    else
                    {
                        MessageBox.Show("Bylo dosaženo maximálního počtu dokumentů (10)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }



            }
            else
            {
                foreach (string file in files)
                {
                    if (flowLayoutPanel1.Controls.Count < 10)
                    {
                        if (flowLayoutPanel1.Controls.Count <= 2 && WindowState == FormWindowState.Normal && this.Height < 306)
                        {
                            //if (flowLayoutPanel1.Controls.Count >= 3)


                            this.flowLayoutPanel1.AutoScroll = true;
                            this.Height = this.Height + uC_AddCsv.Height + 5;

                            //resizeHeightDifference = this.Height;


                            this.MinimumSize = new Size(this.Width, this.Height + (uC_AddCsv.Height + 5));
                        

                        }
                        else if (flowLayoutPanel1.Controls.Count > 2)
                        {
                            
                            //this.MinimumSize = new Size(this.Width, this.Height + (uC_AddCsv.Height + 5) * 3);
                            this.flowLayoutPanel1.AutoSize = false;

                            //resizeHeightChanged = this.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - 2);
                            resizeHeightChanged = this.MinimumSize.Height + (uC_AddCsv.Height + 5) * (flowLayoutPanel1.Controls.Count - flpTempCount);
                        }


                        documentNumber++;

                        uC_AddCsv = new UC_AddCsv(this);
                        uC_AddCsv.txtFilePath.Text = file;
                        uC_AddCsv.fileName = Path.GetFileName(file);
                        uC_AddCsv.txtCsvNumber.Text = "Csv #" + documentNumber.ToString();
                        flowLayoutPanel1.Controls.Add(uC_AddCsv);

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
}

