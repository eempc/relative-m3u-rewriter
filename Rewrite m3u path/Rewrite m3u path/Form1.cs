using System;
using System.Windows.Forms;
using System.IO;

namespace Rewrite_m3u_path
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";
        }

        //Foreword: since I did not name my stupid boxes, here they are: 
        //textBox1 = left main window with the contents of the file
        //textbox2 = file path of the original file
        //textbox3 = file path of the new file to be written
        //textbox4 = preview of the contents of the file to be written

        //Open file
        string filePath;
        private void button1_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        //Load file via dialogue, populate the boxes, preview the right boxes
        public void openFile()
        {
            OpenFileDialog myOFD = new OpenFileDialog();
            myOFD.Filter = "M3U (*.m3u)|*.m3u|TXT (*.txt)|*.txt|All (*.*)|*.*";

            if (myOFD.ShowDialog() == DialogResult.OK)
            {
                filePath = myOFD.FileName;
                toolStripStatusLabel1.Text = "File loaded: " + filePath;
                textBox2.Text = filePath;
                textBox3.Text = filePath;
                string[] inputLines = File.ReadAllLines(filePath); //Should automatically append each new line to the array
                foreach (string line in inputLines)
                {
                    textBox1.AppendText(line + "\r\n"); //This is unaltered, i.e. the before modification
                    if (line[1] == ':') //identify is this is a directory line
                    {
                        textBox4.AppendText(line.Substring(GetDirectoryLength(filePath) + 1) + "\r\n");
                    }
                    else
                    {
                        textBox4.AppendText(line + "\r\n");
                    }
                }
            }
        }

        //Self explanatory, this is for the substring method to cut off the first part of the directory, i.e. "D:\Music" 
        public int GetDirectoryLength(string filePath)
        {
            string folderNames = Path.GetDirectoryName(filePath);
            return folderNames.Length;
        }

        //Stream Writer to write a new file with the amended directories, I do not need to iterate through the string (needs investigating)
        //Should I make this a dialogue? Yes let's make this a dialogue, nah let's not
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        public void saveFile()
        {
            using (StreamWriter newFile = new StreamWriter(textBox3.Text))
            {
                newFile.WriteLine(textBox4.Text);
                toolStripStatusLabel1.Text = "Saved file: " + textBox3.Text;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
