using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Notepad : Form
    {
        private string fileName = null;
        private bool isUnsaved = false;
        private bool textChangedEvent = false;
        public Notepad()
        {
            InitializeComponent();
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            string file;
            if (string.IsNullOrEmpty(fileName))
                file = "Unnamed";
            else
                file = Path.GetFileName(fileName);


            if (isUnsaved)
                Text = file + "* - Notepad";
            else
                Text = file + " - Notepad";


        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    fileName = saveFileDialog1.FileName;
                else
                    return;
            }


            File.WriteAllText(fileName, textBox1.Text);
            isUnsaved = false;
            UpdateTitle();
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var eventArgs = new FormClosingEventArgs(CloseReason.None, false);
            Notepad_FormClosing(null, eventArgs);

            if (eventArgs.Cancel)
                return;

            textBox1.Text = string.Empty;
            fileName = null;
            isUnsaved = false;
            UpdateTitle();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var eventArgs = new FormClosingEventArgs(CloseReason.None, false);
            Notepad_FormClosing(null, eventArgs);

            if (eventArgs.Cancel)
                return;

            if ( openFileDialog1.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                textChangedEvent = true;
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                isUnsaved = false;
                UpdateTitle();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChangedEvent)
            {
                textChangedEvent = false;
                return;
            }

            isUnsaved = true;
            UpdateTitle();
        }

        private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUnsaved)
            {
                var dialogResult = MessageBox.Show(this, "Would you like to save?", "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (dialogResult == System.Windows.Forms.DialogResult.No)
                {

                }
                else if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notepad Support", "Notepad");
        }

        private void kopieraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void klistraInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void klippUtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void ångraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void ersättToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
