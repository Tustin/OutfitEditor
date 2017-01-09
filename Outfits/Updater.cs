using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Outfits
{
    public class UpdateResponse
    {
        public string version { get; set; }
        public string changelog { get; set; }
    }

    public class Updater
    {
        private static string version = "1.3";

        public static void NewUpdateCheck() {
            try {
                var req = WebRequest.Create("https://tusticles.com/outfit/version.json");
                var resp = req.GetResponse().GetResponseStream();
                using (StreamReader sr = new StreamReader(resp)) {
                    var data = JsonConvert.DeserializeObject<UpdateResponse>(sr.ReadToEnd());
                    if (data.version != version) {
                        var callback = MessageBox.Show($"A new update has been released.\nWould you like to visit the download page?\n\nChanges:\n{data.changelog }", "New Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (callback == DialogResult.Yes) {
                            Process.Start("https://tusticles.com/outfit/");
                        }
                    }
                }
            }
            catch {
                return;
            }
        }
    }
}
