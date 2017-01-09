using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PS3Lib;
using XDevkit;
using JRPC_Client;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading;

namespace Outfits
{
    public partial class Form1 : Form
    {
        public PS3API PS3;
        public IXboxConsole X360;

        public Form1() {
            InitializeComponent();
            Outfit.currentOutfits = new List<OutfitStruct>();
        }

        private int LoadOutfits() {
            listBox1.Items.Clear();
            Outfit.currentOutfits = Outfit.FetchAllOutfits();
            int items = Outfit.currentOutfits.Count;
            foreach (OutfitStruct outfit in Outfit.currentOutfits) {
                string outfitName = outfit.outfitName;
                if (outfit.outfitName.Length > (int)Outfit.outfitNameLen) {
                    outfitName = outfit.outfitName.Substring(0, (int)Outfit.outfitNameLen);
                }
                if (outfitName.ToLower().Contains("tustin")) {
                    MessageBox.Show("Nice outfit name :)");
                }
                listBox1.Items.Add(outfitName);
            }
            return items;
        }
        private void FlipControls(bool value) {
            foreach (Control c in panelFormControls.Controls) {
                c.Enabled = value;
            }
            lblRefreshOutfits.Enabled = value;
            btnSetOutfit.Enabled = value;
        }
        private void Form1_Load(object sender, EventArgs e) {
            Updater.NewUpdateCheck();
            if (Properties.Settings.Default.gtaVersion == "126") {
                toolStripMenuItem126.Checked = true;
                toolStripMenuItem127.Checked = false;
            }
            else {
                toolStripMenuItem126.Checked = false;
                toolStripMenuItem127.Checked = true;
            }
            FlipControls(false);
            if (File.Exists("outfits.json")) {
                Outfit.customOutfits = Outfit.FetchOutfitsFromFile("outfits.json");
            }
            else {
                Outfit.customOutfits = new List<OutfitStruct>();
            }
        }
        private void ConnectX360()
        {
            try
            {
                X360.Connect(out X360);
                if (Outfit.InitX360(X360))
                {
                    FlipControls(true);
                    int items = LoadOutfits();
                    if (items == 0)
                    {
                        MessageBox.Show($"Unable to find any outfits. Please make an outfit in game, then hit the refresh button!", "Oh No!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        label1.Text = $"{items} outfits";
                        MessageBox.Show($"Successfully loaded {items} outfits!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listBox1.SelectedIndex = 0;
                        gTAVersionToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to find outfit structure address. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                DialogResult callback = MessageBox.Show("Failed to connect to Xbox360. Please try again.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (callback == DialogResult.Retry)
                {
                    ConnectX360();
                }
            }

        }
        private void ConnectAttach(SelectAPI API) {
            PS3 = new PS3API(API);
            try {
                PS3.ConnectTarget();
                PS3.AttachProcess();
                if (Outfit.Init(PS3)) {
                    FlipControls(true);
                    int items = LoadOutfits();
                    if (items == 0) {
                        MessageBox.Show($"Unable to find any outfits. Please make an outfit in game, then hit the refresh button!", "Oh No!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        label1.Text = $"{items} outfits";
                        MessageBox.Show($"Successfully loaded {items} outfits!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listBox1.SelectedIndex = 0;
                        gTAVersionToolStripMenuItem.Enabled = false;
                    }
                }
                else {
                    MessageBox.Show("Unable to find outfit structure address. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                DialogResult callback = MessageBox.Show("Failed to connect to PS3. Please try again.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (callback == DialogResult.Retry) {
                    ConnectAttach(API);
                }
            }
        }
        public void PopulateFields(OutfitStruct o) {
            mask.Value = o.mask;
            torso.Value = o.torso;
            pants.Value = o.pants;
            parachute.Value = o.parachute;
            shoes.Value = o.shoes;
            misc1.Value = o.misc1;
            tops1.Value = o.tops1;
            armour.Value = o.armour;
            crew.Value = o.crew;
            tops2.Value = o.tops2;

            maskTexture.Value = o.maskTexture;
            torsoTexture.Value = o.torsoTexture;
            pantsTexture.Value = o.pantsTexture;
            parachuteTexture.Value = o.parachuteTexture;
            shoesTexture.Value = o.shoesTexture;
            misc1Texture.Value = o.misc1Texture;
            tops1Texture.Value = o.tops1Texture;
            armourTexture.Value = o.armourTexture;
            crewTexture.Value = o.crewTexture;
            tops2Texture.Value = o.tops2Texture;

            hat.Value = o.hat;
            glasses.Value = o.glasses;
            earpiece.Value = o.earpiece;

            hatTexture.Value = o.hatTexture;
            glassesTexture.Value = o.glassesTexture;
            earpieceTexture.Value = o.earpieceTexture;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex > 10) {
                MessageBox.Show("Invalid outfit selected");
                return;
            }
            OutfitStruct outfit = Outfit.Fetch(listBox1.SelectedIndex);
            PopulateFields(outfit);
            outfitName.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void save_Click(object sender, EventArgs e) {
            int lastSaved = listBox1.SelectedIndex;
            OutfitStruct o = new OutfitStruct();
            o.mask = Convert.ToInt32(mask.Value);
            o.torso = Convert.ToInt32(torso.Value);
            o.pants = Convert.ToInt32(pants.Value);
            o.parachute = Convert.ToInt32(parachute.Value);
            o.shoes = Convert.ToInt32(shoes.Value);
            o.misc1 = Convert.ToInt32(misc1.Value);
            o.tops1 = Convert.ToInt32(tops1.Value);
            o.armour = Convert.ToInt32(armour.Value);
            o.crew = Convert.ToInt32(crew.Value);
            o.tops2 = Convert.ToInt32(tops2.Value);

            o.maskTexture = Convert.ToInt32(maskTexture.Value);
            o.torsoTexture = Convert.ToInt32(torsoTexture.Value);
            o.pantsTexture = Convert.ToInt32(pantsTexture.Value);
            o.parachuteTexture = Convert.ToInt32(parachuteTexture.Value);
            o.shoesTexture = Convert.ToInt32(shoesTexture.Value);
            o.misc1Texture = Convert.ToInt32(misc1Texture.Value);
            o.tops1Texture = Convert.ToInt32(tops1Texture.Value);
            o.armourTexture = Convert.ToInt32(armourTexture.Value);
            o.crewTexture = Convert.ToInt32(crewTexture.Value);
            o.tops2Texture = Convert.ToInt32(tops2Texture.Value);

            o.hat = Convert.ToInt32(hat.Value);
            o.glasses = Convert.ToInt32(glasses.Value);
            o.earpiece = Convert.ToInt32(earpiece.Value);

            o.hatTexture = Convert.ToInt32(hatTexture.Value);
            o.glassesTexture = Convert.ToInt32(glassesTexture.Value);
            o.earpieceTexture = Convert.ToInt32(earpieceTexture.Value);
            if (Outfit.Poke(o, outfitName.Text, listBox1.SelectedIndex)) {
                MessageBox.Show($"Successfully saved {outfitName.Text }");
                LoadOutfits();
                listBox1.SelectedIndex = lastSaved;
            }
            else {
                MessageBox.Show($"Error saving outfit. Did you delete this outfit in game?");
            }

        }

        private void outfitName_KeyDown(object sender, KeyEventArgs e) {
            if (outfitName.Text.Length >= Outfit.outfitNameLen) {
                return;
            }
        }

        private void outfitName_KeyPress(object sender, KeyPressEventArgs e) {
            if (outfitName.Text.Length >= Outfit.outfitNameLen) {
                return;
            }
        }

        private void toolStripMenuItem127_Click(object sender, EventArgs e) {
            jRPCToolStripMenuItem.Visible = false;
            tMAPIToolStripMenuItem.Visible = true;
            cCAPIToolStripMenuItem.Visible = true;
            fileToolStripMenuItem.Text = "Connect to PS3";
            toolStripMenuItem126.Checked = false;
            toolStripMenuItem127.Checked = true;
            tU27X360ToolStripMenuItem.Checked = false;
            Properties.Settings.Default.gtaVersion = "127";
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem126_Click(object sender, EventArgs e) {
            jRPCToolStripMenuItem.Visible = false;
            tMAPIToolStripMenuItem.Visible = true;
            cCAPIToolStripMenuItem.Visible = true;
            fileToolStripMenuItem.Text = "Connect to PS3";
            toolStripMenuItem126.Checked = true;
            toolStripMenuItem127.Checked = false;
            tU27X360ToolStripMenuItem.Checked = false;
            Properties.Settings.Default.gtaVersion = "126";
            Properties.Settings.Default.Save();
        }

        private void lblReload_Click(object sender, EventArgs e) {
            int lastSaved = listBox1.SelectedIndex;
            int items = LoadOutfits();
            if (items == 0) {
                label1.Text = $"No outfits";
                MessageBox.Show($"Unable to find any outfits. Please make an outfit in game, then hit the refresh button!", "Oh No!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                label1.Text = $"{items} outfits";
                if ((lastSaved >= 0 && lastSaved < 10) && lastSaved < items) {
                    OutfitStruct outfit = Outfit.Fetch(lastSaved);
                    PopulateFields(outfit);
                    outfitName.Text = listBox1.Items[lastSaved].ToString();
                    listBox1.SelectedIndex = lastSaved;
                }
            }
        }

        private void lblReload_MouseHover(object sender, EventArgs e) {
            Cursor = Cursors.Hand;
        }

        private void lblReload_MouseLeave(object sender, EventArgs e) {
            Cursor = Cursors.Default;
        }

        private void btnExportOutfit_Click(object sender, EventArgs e) {
            if (listBox1.SelectedIndex == -1) {
                return;
            }
            OutfitStruct currentOutfit = Outfit.currentOutfits[listBox1.SelectedIndex];

            if (Outfit.customOutfits.Any(customOutfit => customOutfit == currentOutfit)) {
                MessageBox.Show("This outfit has already been exported.");
                return;
            }

            Outfit.customOutfits.Add(currentOutfit);
            if (!File.Exists("outfits.json")) {
                var stupid = File.Create("outfits.json");
                stupid.Close();
            }
            using (Stream s = new FileStream("outfits.json", FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(s)) {
                sw.Write(JsonConvert.SerializeObject(Outfit.customOutfits, Formatting.Indented));
                sw.Close();
                s.Close();
            }

            MessageBox.Show($"Successfully exported outfit {currentOutfit.outfitName}!");
        }

        private void btnImportOutfit_Click(object sender, EventArgs e) {
            using (ImportOutfits io = new ImportOutfits()) {
                io.FormClosed += Io_FormClosed;
                io.ShowDialog();
            }
        }

        private void Io_FormClosed(object sender, FormClosedEventArgs e) {
            if (ReferenceEquals(Outfit.outfitToImport, null) || listBox1.SelectedIndex == -1) {
                return;
            }

            if (Outfit.Poke(Outfit.outfitToImport, Outfit.outfitToImport.outfitName, listBox1.SelectedIndex)) {
                MessageBox.Show($"Successfully imported {Outfit.outfitToImport.outfitName} to outfit slot {listBox1.SelectedIndex + 1}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblReload_Click(null, null);
                Outfit.outfitToImport = null;
                
            }
            else {
                MessageBox.Show($"Failed setting {Outfit.outfitToImport.outfitName} to outfit slot {listBox1.SelectedIndex + 1}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tU27X360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jRPCToolStripMenuItem.Visible = true;
            tMAPIToolStripMenuItem.Visible = false;
            cCAPIToolStripMenuItem.Visible = false;
            fileToolStripMenuItem.Text = "Connect to Xbox360";
            toolStripMenuItem126.Checked = false;
            toolStripMenuItem127.Checked = false;
            tU27X360ToolStripMenuItem.Checked = true;
        }

        private void cCAPIToolStripMenuItem_Click(object sender, EventArgs e) {
            ConnectAttach(SelectAPI.ControlConsole);

        }

        private void tMAPIToolStripMenuItem_Click(object sender, EventArgs e) {
            ConnectAttach(SelectAPI.TargetManager);
        }

        private void jRPCToolStripMenuItem_Click(object sender, EventArgs e) {
            ConnectX360();
        }
    }
}
