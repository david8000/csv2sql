using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace csv2sql
{
    public partial class Form1 : Form
    {

        public static Form1 Instance;

        
        //private SaveFileDialog saveFileDialog1;
        //private OpenFileDialog openFileDialog1;


        public string Workfolder
        {
            get { return textBox1.Text; }
        }

        public string InputCsv
        {
            get { return textBox3.Text; }
        }

        public string DefaultDataType
        {
            get { return textBox2.Text; }
        }

        public int BatchSize
        {
            get { return (int)numericUpDown1.Value; }
        }

        public string AppSettinsPathFile
        {

            get { return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) 
                    + @"\"+ AppDomain.CurrentDomain.FriendlyName; }
        }


        public DataGridView GridView { get { return dataGridView1; } }


        public Form1()
        {
            Instance = this;

            InitializeComponent();


            //grid view
            DataTable dt = new DataTable();
            dt.Columns.Add("Column Name", typeof(string));
            dt.Columns.Add("Data Type", typeof(string));
            dt.Rows.Add("Examplecol_1", "int");
            dt.Rows.Add("Examplecol_2", "datetime2");
            dt.Rows.Add("Examplecol_3", "bit");
            dataGridView1.DataSource = dt;


            /*

            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();

            // Set the properties of the SaveFileDialog object
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save As";
            saveFileDialog1.InitialDirectory = @"C:\temp";
            saveFileDialog1.FileName = "MyFile.txt";

            // Optional: Add an event handler for the FileOk event
            //saveFileDialog1.FileOk += new CancelEventHandler(saveFileDialog1_FileOk);

            */


        }

        //Save button:
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save datatype settings";
            save.Filter = "Data files (*.dat)|*.dat";
            save.InitialDirectory = Form1.Instance.Workfolder;

            if (save.ShowDialog() == DialogResult.OK)
            {
                string fileName = save.FileName;
                SaveDataGridViewToCsv(fileName);
            }
        }

        //Load button
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open datatype settings";
            open.Filter = "Data files (*.dat)|*.dat";
            open.InitialDirectory = Form1.Instance.Workfolder;

            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                RestoreDataGridViewFromCsv(fileName);
            }

        }




        // Define a method to save the data
        private void SaveDataGridViewToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                // Write the header row
                var header = dataGridView1.Columns.Cast<DataGridViewColumn>().Select(column => column.HeaderText);
                writer.WriteLine(string.Join(",", header));

                // Write the data rows
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString());
                        writer.WriteLine(string.Join(",", cells));
                    }
                    catch { };
                }
            }
        }




        private void RestoreDataGridViewFromCsv(string filePath)
        {

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            using (var reader = new StreamReader(filePath))
            {
                // Read the header row and create columns
                var header = reader.ReadLine()?.Split(',');
                if (header != null)
                {
                    foreach (var columnName in header)
                    {
                        dataGridView1.Columns.Add(columnName, columnName);
                    }
                }

                // Read and add data rows
                while (!reader.EndOfStream)
                {
                    var data = reader.ReadLine()?.Split(',');
                    if (data != null)
                    {
                        dataGridView1.Rows.Add(data);
                    }
                }





            }
        }

        //work directory changed
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //save setting to app settins folder
            string filename = this.AppSettinsPathFile + @"\" + "workfolder.txt";
            string directoryPath = Path.GetDirectoryName(filename);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(textBox1.Text);
            }
        }

        //Start button
        private void button3_Click(object sender, EventArgs e)
        {
            GenerateSQL g = new GenerateSQL();
            g.main();

        }

        //Choose csv file button
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open";
            open.Filter = "Csv files (*.csv)|*.csv";
            open.InitialDirectory = Form1.Instance.Workfolder;
            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileName = open.FileName;
                textBox3.Text = fileName;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Choose work folder:
        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string path = folder.SelectedPath;
                textBox1.Text = path;
            }

        }
    }
}