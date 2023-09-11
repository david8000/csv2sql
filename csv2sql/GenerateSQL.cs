using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace csv2sql
{
    internal class GenerateSQL
    {

        public const string outputFileName = "_output_.sql";
        public const string delimiter = ";";
        public const string defaultTableName = "_MYNEWTABLE_";

        private string[] csvHeaders;
        private List<string[]> csvLines;



        public void main()
        {
            readCSVfile();
            outputSQLscript();
        }


        private void readCSVfile()
        {
            string csv = Form1.Instance.InputCsv;
            csvLines = new List<string[]>();

            using (StreamReader reader = new StreamReader(csv))
            {
                string line = "";
                while (line.Trim() == "")
                { line = reader.ReadLine(); }

                csvHeaders = line.Split(delimiter);

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    csvLines.Add(line.Split(delimiter));
                }
            }
        }




        private void outputSQLscript()
        {
            string pathFile = Form1.Instance.Workfolder + '\\' + outputFileName;

            //create table statement:
            string create = "CREATE TABLE " + defaultTableName + " ( ";
            foreach (string s in csvHeaders)
            {
                create += "[" + s + "]";
                create += " " + getDataType(s) + ", ";
            }
            create = create.Trim(new char[] { ' ', ',' }); //remove last comma
            create += " );";


            //insert statement:
            string insert = "INSERT INTO " + defaultTableName + " (";
            foreach (string s in csvHeaders)
            {
                insert += "[" + s + "], ";
            }
            insert = insert.Trim(new char[] { ' ', ',' }); //remove last comma
            insert += " )";

            //insert values statements:
            //string values = "VALUES ";


            //write to output file:
            using (StreamWriter writer = new StreamWriter(pathFile))
            {
                writer.WriteLine(create);
                writer.WriteLine("");


                int rowno = 0;
                int b = 0; //batch size
                foreach (string[] s in csvLines)
                {

                    if (b == 0)
                    {
                        writer.WriteLine(insert);
                        writer.WriteLine("VALUES");
                        
                    }

                    writer.Write(" (");
                    for (int i = 0; i < s.Length; i++)
                    {
                        writer.Write("'" + s[i] + "' ");
                        if (i < s.Length - 1) { writer.Write(", "); }
                    }

                    writer.Write(")");
                    if (b < Form1.Instance.BatchSize - 1 && rowno < csvLines.Count -1)
                        writer.Write(", ");
                    else
                    {
                        writer.Write("; ");
                        writer.WriteLine("");
                    }
                    writer.WriteLine("");

                    rowno++;
                    b = b == Form1.Instance.BatchSize -1 ? 0 : b + 1;
                }
            }



        }






        /// <summary>
        /// Read the data grid view
        /// </summary>
        private string getDataType(string colname)
        {
            string rslt = Form1.Instance.DefaultDataType;
            foreach (DataGridViewRow row in Form1.Instance.GridView.Rows)
            {
                //string col = row.ToString().Split(",")[0];
                string col = "";
                try
                {
                    col = row.Cells[0].Value.ToString();
                }
                catch { }
                if (col.Trim() == colname.Trim())
                {
                    rslt = row.Cells[1].Value.ToString();  //the datatype
                }
            }
            return rslt;
        }


    }


}



