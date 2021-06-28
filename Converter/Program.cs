using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace converter
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Directory.Exists(Globals.ffmpegFolder))
            {
                try
                {
                    Directory.CreateDirectory(Globals.ffmpegFolder);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error creating temporary folder. Debug:\r" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                try
                {
                    Globals.ShowWaitForm();
                    string link = "https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip";
                    using (var wclient = new WebClient())
                    {
                        wclient.DownloadFile(link, Globals.ffmpegFolder + "ffmpeg.zip");
                    }
                    using (ZipArchive archive = ZipFile.OpenRead(Globals.ffmpegFolder + "ffmpeg.zip"))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries.Where(e => e.FullName.Contains("ffmpeg.exe")))
                        {
                            entry.ExtractToFile(Globals.ffmpegFolder + "ffmpeg.exe");
                        }
                    }
                    File.Delete(Globals.ffmpegFolder + "ffmpeg.zip");
                    Globals.OnLoaded();
                }
                catch (Exception e)
                {
                    try
                    {
                        Directory.Delete(Globals.ffmpegFolder);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error downloading ffmpeg. Please delete the ffmpeg folder located at:\r" + Globals.ffmpegFolder + "Debug:\r" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                    MessageBox.Show("Error downloading ffmpeg. Debug:\r" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
			Application.Run(new Form1());
		}

	}
}
