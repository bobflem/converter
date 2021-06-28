using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace converter
{
    public class Globals
    {
        public static string ffmpegFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"converter_ffmpeg\");

        public static wait _waitForm;

        public static void ShowWaitForm()
        {
            // don't display more than one wait form at a time
            if (_waitForm != null && !_waitForm.IsDisposed)
            {
                return;
            }

            _waitForm = new wait();
            _waitForm.TopMost = true;
            _waitForm.StartPosition = FormStartPosition.CenterScreen;
            _waitForm.Show();
            _waitForm.Refresh();
        }

        public static void OnLoaded()
        {
            _waitForm.Close();
        }
    }
}
