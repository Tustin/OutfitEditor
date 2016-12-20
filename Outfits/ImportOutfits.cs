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
using Newtonsoft.Json;

namespace Outfits
{
    public partial class ImportOutfits : Form
    {
        public ImportOutfits() {
            InitializeComponent();
            Outfit.currentOutfits = Outfit.FetchAllOutfits();
            Outfit.customOutfits = Outfit.FetchOutfitsFromFile("outfits.json");
            foreach (OutfitStruct myCustomOutfits in Outfit.customOutfits) {
                lstCustomOutfits.Items.Add(myCustomOutfits.outfitName);
            }
        }


        private void ImportOutfits_Load(object sender, EventArgs e) {

        }
        private void ReloadLists(List<OutfitStruct> outfits) {
            lstCustomOutfits.Items.Clear();

            foreach (var o in outfits) {
                lstCustomOutfits.Items.Add(o.outfitName);
            }
        }
        private void importOutfitsFromFileToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON|*.json";
            if (ofd.ShowDialog() == DialogResult.OK) {
                string loadedOutfits;
                using (Stream s = new FileStream(ofd.FileName, FileMode.Open))
                using (StreamReader sr = new StreamReader(s)) {
                    loadedOutfits = sr.ReadToEnd();
                    sr.Close();
                    s.Close();
                }
                if (loadedOutfits == null) {
                    return;
                }

                List<OutfitStruct> o = JsonConvert.DeserializeObject<List<OutfitStruct>>(loadedOutfits);
                int outfitsImported = 0;
                foreach (OutfitStruct importedOutfit in o) {
                    if (Outfit.customOutfits.Any(customOutfit => customOutfit == importedOutfit)) {
                        continue;
                    }
                    Outfit.customOutfits.Add(importedOutfit);
                    outfitsImported++;
                }
                if (outfitsImported == 0) {
                    MessageBox.Show("No new outfits were imported\r\nThis means all the outfits in the file you selected are already in your custom outfits!");
                    
                } else {
                    string message = $"Successully added {outfitsImported} outfits to custom outfits!";
                    int outfitsSkipped = o.Count - outfitsImported;
                    if (outfitsSkipped > 0) {
                        message += $"\n{outfitsSkipped} outfits were skipped because they already exist in your custom outfits.";
                    }
                    MessageBox.Show(message);
                    Outfit.SaveCustomOutfitsToFile();
                }
                ReloadLists(Outfit.customOutfits);
            }
        }

        private void btnChooseOutfit_Click(object sender, EventArgs e) {
            if (lstCustomOutfits.SelectedIndex == -1) {
                return;
            }
            OutfitStruct selectedOutfit = Outfit.customOutfits[lstCustomOutfits.SelectedIndex];
            Outfit.outfitToImport = selectedOutfit;
            this.Close();
        }

        private void lstCustomOutfits_SelectedIndexChanged(object sender, EventArgs e) {
            btnChooseOutfit.Enabled = lstCustomOutfits.SelectedIndex > -1;
        }
    }
}
