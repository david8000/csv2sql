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
        private string[] columnDataTypes;


        private List<string[]> csvLines;

        public void main()
        {
            readCSVfile(); //will also populate "columnDataTypes"
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
                readDataTypes(); //pupulate this class var when headers read


                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] a = line.Split(delimiter);

                    if (Form1.Instance.FormatDecimalValues)
                        a = formatDecVals(a);

                    csvLines.Add(a);
                }
            }
        }
        private void readDataTypes()
        {
            columnDataTypes = new string[csvHeaders.Length];
            for (int i = 0; i < csvHeaders.Length; i++)
            {
                columnDataTypes[i] = getDataType(csvHeaders[i]);
            }
        }


        private string[] formatDecVals(string[] datarow)
        {
            for (int i = 0; i < datarow.Length; i++)
            {
                //string header = csvHeaders[i];
                //string useType = getDataType(header);
                string useType = columnDataTypes[i];
                bool dec = isDecType(useType);
                if (dec)
                {
                    datarow[i] = datarow[i].Replace(" ", ""); //remove spaces
                    datarow[i] = datarow[i].Replace(",", ".");

                }
            }
            return datarow;
        }

        private bool isDecType(string typeName)
        {
            bool rslt = false;
            string[] types = { "float", "real", "decimal", "money", "numeric", "dec", "fixed", "double" };
            //note: "dec", "fixed", "double" are MySQL types. The rest is SQL Server.

            foreach (string type in types)
                if (typeName.Contains(type, StringComparison.InvariantCultureIgnoreCase))
                    rslt = true;

            return rslt;

        }



        private void outputSQLscript()
        {
            string pathFile = Form1.Instance.Workfolder + '\\' + outputFileName;

            //create table statement:
            string create = "CREATE TABLE " + defaultTableName + " ( ";


            for (int i = 0; i < csvHeaders.Length; i++)
            {
                create += "[" + csvHeaders[i] + "]";
                create += " " + columnDataTypes[i] + ", ";
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
                    if (b < Form1.Instance.BatchSize - 1 && rowno < csvLines.Count - 1)
                        writer.Write(", ");
                    else
                    {
                        writer.Write("; ");
                        writer.WriteLine("");
                    }
                    writer.WriteLine("");

                    rowno++;
                    b = b == Form1.Instance.BatchSize - 1 ? 0 : b + 1;
                }
            }



        }



        /// <summary>
        /// Read the data grid view.
        /// Try to only use this once for performance on large files :)
        /// Ie for pupulating a class var.
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



