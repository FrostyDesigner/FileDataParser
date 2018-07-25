using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace FileDataParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void parse_single()
        {
            string CurDir = string.Empty;
            string text = "04/29/2004  08:34 AM         1,720,308 100 squid sleep for wolf kind.tif";
                
            string pat = @"[0-9]{2}\W[0-9]{2}\W[0-9]{4}\s{2}[0-9]{2}\W[0-9]{2}\s\w{2}";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(text);
            //
            char[] delimiters = new char[] { '\r', '\n', ' ' };

            if (m.Length > 0)
            {
                //string line = m.ToString();
                string line = text;
                string[] splitLine = line.Split(delimiters, 5, StringSplitOptions.RemoveEmptyEntries);
                string filedate = splitLine[0];
                string filetime = splitLine[1];
                string filemeridiem = splitLine[2];
                string filesize = splitLine[3];
                string filename = splitLine[4];
            }
            else
            {
                MessageBox.Show("no match");
            }
        }

        private void parse_batch_Click(object sender, EventArgs e)
        {
            char[] tbDelimiters = new char[] { '\r', '\n' };
            string[] fnames = tbSourceFile.Text.Split(tbDelimiters);

            if (tbSourceFile.Text == string.Empty)
            {
                MessageBox.Show("Application needs at least one source file and a destination file.", "Not Enough Information.");
                return;
            }

            if (tbDestinationFile.Text == string.Empty)
            {
                string destName = fnames[0];
                destName = destName.Replace(".txt", ".csv");
                tbDestinationFile.Text = destName;
            }
                // Here we create a DataTable with four columns.
                DataTable table = new DataTable();
                table.Columns.Add("_index", typeof(int));
                table.Columns.Add("_fullname", typeof(string));
                table.Columns.Add("_filedate", typeof(string));
                table.Columns.Add("_filesize", typeof(string));

            string CurDir = string.Empty;

            try
            {
                foreach (var fname in fnames)
                {
                    if (fname == "")
                    {
                        continue;
                    }

                    //this reads in the contents of multiple files
                    List<string> allLinesText = File.ReadAllLines(fname).ToList();

                    int i = 0;
                    foreach (var item in allLinesText)
                    {
                        string pat = @"[0-9]{2}\W[0-9]{2}\W[0-9]{4}\s{2}[0-9]{2}\W[0-9]{2}\s\w{2}";

                        // Instantiate the regular expression object.
                        Regex r = new Regex(pat, RegexOptions.IgnoreCase);

                        // Match the regular expression pattern against a text string.
                        Match m = r.Match(item);
                    
                        char[] delimiters = new char[] { '\r', '\n', ' ' };

                        if (item.Contains("Directory of "))
                        {
                            CurDir = item.Replace("Directory of ", "");
                            //continue;
                        }
                        else if (m.Length > 0)
                        {
                            if (item.Contains("  <DIR>  "))
                            {
                                continue;
                            }
                            else
                            {
                                //string line = m.ToString();
                                string line = item;
                                string[] splitLine = line.Split(delimiters, 5, StringSplitOptions.RemoveEmptyEntries);
                                string filedate = splitLine[0];
                                string filetime = splitLine[1];
                                string filemeridiem = splitLine[2];
                                string filesize = splitLine[3];
                                string filename = splitLine[4];
                                //replace illegal characters
                                filename = GetSafeFilename(filename);
                                //filename = filename.Replace("[\\/:*?\"'<>|]", "_");
                                string fullname = Path.Combine(CurDir, filename);
                                fullname = fullname.Trim();

                                //Here add DataRows.
                                //table.Rows.Add("0", "Cooper PRODDENG x.txt", "04 /29/2004", "1,720,308");
                                table.Rows.Add(i, fullname, filedate, filesize);
                                i = i + 1;
                            }
                        }
                        else if (item == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                table.WriteToCsvFile(tbDestinationFile.Text);
                MessageBox.Show("Success");
            }
            catch(ArgumentException ae)
            {
                    MessageBox.Show(ae.Message, "Argument Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }
        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public string GetSafeFilename(string filename)
        {

            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));

        }
    }

    //new class
    public static class DataTableExtensions
    {
        public static void WriteToCsvFile(this DataTable dataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());
        }
    }
}
