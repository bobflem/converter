using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace converter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var FD = new System.Windows.Forms.OpenFileDialog();
			if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string fileToOpen = FD.FileName;
				System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
				textBox1.Text = File.FullName;
			}
            if (textBox1.Text != "")
            {
                comboBox1.Enabled = true;
                button2.BackColor = Color.LimeGreen;
                button2.Enabled = true;
            }
            if (comboBox1.Text == "")
                comboBox1.SelectedIndex = 0;
        }

		private void button2_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Globals.ffmpegFolder + "ffmpeg.exe";
                proc.StartInfo.Arguments = "-i \"" + textBox1.Text + "\" -map_metadata 0 -id3v2_version 3 \"" + Path.GetDirectoryName(textBox1.Text) + "\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "." + comboBox1.Text.ToLower() + "\"";
                proc.Start();
                proc.WaitForExit();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    Directory.Delete(Globals.ffmpegFolder, true);
                }
                catch (Exception) {
                    MessageBox.Show("Please delete this folder: " + Globals.ffmpegFolder, "Error deleting temp folder",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                Environment.Exit(4);
            }
			Cursor.Current = Cursors.Default;
		}
    }
}
