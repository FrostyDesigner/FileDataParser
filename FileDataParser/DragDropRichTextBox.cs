using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//this is a new class that inherits from the RichTextBox class and extends the features
//it adds drag and drop file reading

namespace FileDataParser
{
    class DragDropRichTextBox : RichTextBox
    {
        public DragDropRichTextBox()
        {
            this.AllowDrop = true;
            this.DragDrop += DragDropRichTextBox_DragDrop;
        }

        private void DragDropRichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (filenames != null)
            {
                foreach (string name in filenames)
                {
                    try
                    {
                        //line below to read the contents and display in window
                        //this.AppendText(File.ReadAllText(name));
                        //we only want to show the filenames
                        this.AppendText(Path.GetFullPath(name) + "\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
