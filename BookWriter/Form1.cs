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
            
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fontComboBox.Items.AddRange(fonts.ToArray());
            fontComboBox.SelectedIndex = fontComboBox.FindStringExact("Arial");
        }

        #region File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                mainTxt.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);
            }
            tabControl1.Name = openDialog.FileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Plain Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                mainTxt.SaveFile(saveDialog.FileName, RichTextBoxStreamType.RichText);
            }
            tabControl1.Name = saveDialog.FileName;
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

        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == "Search...") searchBox.Text = "";
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(searchBox.Text)) searchBox.Text = "Search...";
        }

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
