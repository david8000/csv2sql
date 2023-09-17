using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

                switch (Form1.Instance.CSVqualifier)
                {
                    case "":
                        csvHeaders = line.Split(delimiter);
                        break;
                    default:
                        csvHeaders = splitCSVLineQualifier(line);
                        break;
                }


                readDataTypes(); //pupulate this class var when headers read


                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    if (line.Trim() != "") //skip empty
                    {

                        string[] a;
                        if (Form1.Instance.CSVqualifier == "") //no csv qualifier
                            a = line.Split(delimiter);
                        else
                            a = splitCSVLineQualifier(line); //use csv qualifier


                        if (Form1.Instance.FormatDecimalValues) //option format dec values
                        {
                            //debug
                            //if (a[7].Contains(","))
                            //{
                            //    int kalle = 1;
                            //}

                            a = formatDecVals(a);
                        }
                        csvLines.Add(a);
                    }
                }
            }
        }


        private string[] splitCSVLineQualifier(string line)
        {
            string[] rslt;
            string[] a = line.Split(Form1.Instance.CSVqualifier);
            List<string> L = new List<string>();
            for (int i = 0; i < a.Length; i++)
            {
                string s = a[i].Trim();
                if (s != "" && s != delimiter)
                    L.Add(s);
            }
            rslt = L.ToArray();
            return rslt;
        }

        /*
       // Custom method to split a CSV line while handling the specified qualifier
       //AI generated :)
        private string[] SplitCSVLine(string line, char qualifier)
        {
            // Define a regular expression pattern to split the line
            string pattern = $"{Regex.Escape(qualifier.ToString())}(.*?[^{Regex.Escape(qualifier.ToString())}])(?:{Regex.Escape(qualifier.ToString())}|$)|([^,]+)";

            // Use regular expression to match and capture fields
            MatchCollection matches = Regex.Matches(line, pattern);

            // Extract captured values and remove empty entries
            string[] fields = matches.Cast<Match>()
                .Select(m => m.Value.Trim(qualifier).Replace(new string(qualifier, 2), qualifier.ToString())) // Remove qualifiers and handle escaped qualifiers
                .ToArray();

            return fields;
        }
        */



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
            const int colIndex = 0;
            const int typeIndex = 1;
            string rslt = Form1.Instance.DefaultDataType;

            for (int i = 0; i < Form1.Instance.GridView.Rows.Count - 1; i++) //always empty row at end
            {
                //string col = row.ToString().Split(",")[0];
                string col = "";
                col = Form1.Instance.GridView.Rows[i].Cells[colIndex].Value.ToString();

                if (col.Trim() == colname.Trim())
                    rslt = Form1.Instance.GridView.Rows[i].Cells[typeIndex].Value.ToString();

            }
            return rslt;
        }

        /*
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
        */



        /*
         * 
         * */




    }


}



