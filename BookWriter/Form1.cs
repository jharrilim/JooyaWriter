using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace BookWriter
{
    public partial class Form1 : Form
    {
        private InstalledFontCollection installedFontCollection = new InstalledFontCollection();
        private FontFamily fontFamily = new FontFamily("Consolas");
        private List<string> fonts = FontFamily.Families.Select(f => f.Name).ToList();
        private string[] fontSizes = {"8", "9", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30"};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fontComboBox.Items.AddRange(fonts.ToArray());
            fontComboBox.SelectedIndex = fontComboBox.FindStringExact("Arial");
            fontSizeComboBox.Items.AddRange(fontSizes);
            fontSizeComboBox.SelectedIndex = fontSizeComboBox.FindStringExact("12");
            this.ActiveControl = mainTxt;
        }
        //TODO: New File and close tab functionality
        #region File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openDialog.FileName) == ".txt")
                {
                    mainTxt.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);
                    tabControl1.Name = openDialog.FileName;
                }
                else if (Path.GetExtension(openDialog.FileName) == ".rtf")
                {
                    mainTxt.LoadFile(openDialog.FileName, RichTextBoxStreamType.RichText);
                    tabControl1.Name = openDialog.FileName;
                }
                    
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Rich Text File (*.rtf)|*.rtf|Plain Text File (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                mainTxt.SaveFile(saveDialog.FileName, RichTextBoxStreamType.RichText);
                tabControl1.Name = saveDialog.FileName;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Edit
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTxt.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTxt.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTxt.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTxt.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTxt.Redo();
        }
        #endregion

        #region Font
        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                mainTxt.SelectionFont = fontDialog.Font;
                fontComboBox.SelectedItem = fontDialog.Font.Name;
                fontSizeComboBox.SelectedItem = fontDialog.Font.Size.ToString();
            }
        }
        #endregion

        #region Search
        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == "Search...") searchBox.Text = "";
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search...";
                for (int i = 0; i < mainTxt.TextLength; i++)
                {
                    mainTxt.SelectionStart = i;
                    mainTxt.SelectionLength = 1;
                    if (mainTxt.SelectionBackColor == Color.Yellow)
                    {
                        mainTxt.Select(i, 1);
                        mainTxt.SelectionBackColor = Color.Transparent;
                    }
                }
            }
        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    string query = searchBox.Text.ToLower();
                    int queryLength = searchBox.Text.Length;
                    for (int i = 0; i < mainTxt.TextLength; i++)
                    {
                        if (mainTxt.Text.Substring(i, queryLength).ToLower() == query)
                        {
                            //Change BackColour to yellow where substring is same as search results
                            mainTxt.SelectionStart = i;
                            mainTxt.SelectionLength = queryLength;
                            mainTxt.SelectionBackColor = Color.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mainTxt.SelectionFont = new Font(fontComboBox.SelectedItem.ToString(), Convert.ToSingle(fontSizeComboBox.SelectedItem.ToString()));
            }
            catch (FormatException ex)
            {
                mainTxt.AppendText(fontComboBox.SelectedItem.ToString());
            }
            catch (NullReferenceException ex)
            {

            }
        }
        
        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mainTxt.SelectionFont = new Font(fontComboBox.SelectedItem.ToString(), Convert.ToSingle(fontSizeComboBox.SelectedItem.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

    }
}
