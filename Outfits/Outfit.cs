using System;
using System.Collections.Generic;
using PS3Lib;
using System.IO;
using Newtonsoft.Json;

namespace Outfits
{
    [Serializable]
    public class OutfitStruct
    {
        public string outfitName;

        public int mask;
        public int torso;
        public int pants;
        public int parachute;
        public int shoes;
        public int misc1;
        public int tops1;
        public int armour;
        public int crew;
        public int tops2;
        public int hat;
        public int glasses;
        public int earpiece;

        public int maskTexture;
        public int torsoTexture;
        public int pantsTexture;
        public int parachuteTexture;
        public int shoesTexture;
        public int misc1Texture;
        public int tops1Texture;
        public int armourTexture;
        public int crewTexture;
        public int tops2Texture;
        public int hatTexture;
        public int glassesTexture;
        public int earpieceTexture;

        public static bool operator ==(OutfitStruct outfit1, OutfitStruct outfit2) {
            return (
                outfit1.mask == outfit2.mask &&
                outfit1.torso == outfit2.torso &&
                outfit1.pants == outfit2.pants &&
                outfit1.parachute == outfit2.parachute &&
                outfit1.shoes == outfit2.shoes &&
                outfit1.misc1 == outfit2.misc1 &&
                outfit1.tops1 == outfit2.tops1 &&
                outfit1.armour == outfit2.armour &&
                outfit1.crew == outfit2.crew &&
                outfit1.tops2 == outfit2.tops2 &&
                outfit1.hat == outfit2.hat &&
                outfit1.glasses == outfit2.glasses &&
                outfit1.earpiece == outfit2.earpiece &&

                outfit1.maskTexture == outfit2.maskTexture &&
                outfit1.torsoTexture == outfit2.torsoTexture &&
                outfit1.pantsTexture == outfit2.pantsTexture &&
                outfit1.parachuteTexture == outfit2.parachuteTexture &&
                outfit1.shoesTexture == outfit2.shoesTexture &&
                outfit1.misc1Texture == outfit2.misc1Texture &&
                outfit1.tops1Texture == outfit2.tops1Texture &&
                outfit1.armourTexture == outfit2.armourTexture &&
                outfit1.crewTexture == outfit2.crewTexture &&
                outfit1.tops2Texture == outfit2.tops2Texture &&
                outfit1.hatTexture == outfit2.hatTexture &&
                outfit1.glassesTexture == outfit2.glassesTexture &&
                outfit1.earpieceTexture == outfit2.earpieceTexture
                );
        }
        public static bool operator !=(OutfitStruct outfit1, OutfitStruct outfit2) {
            return !(
                outfit1.mask == outfit2.mask &&
                outfit1.torso == outfit2.torso &&
                outfit1.pants == outfit2.pants &&
                outfit1.parachute == outfit2.parachute &&
                outfit1.shoes == outfit2.shoes &&
                outfit1.misc1 == outfit2.misc1 &&
                outfit1.tops1 == outfit2.tops1 &&
                outfit1.armour == outfit2.armour &&
                outfit1.crew == outfit2.crew &&
                outfit1.tops2 == outfit2.tops2 &&
                outfit1.hat == outfit2.hat &&
                outfit1.glasses == outfit2.glasses &&
                outfit1.earpiece == outfit2.earpiece &&

                outfit1.maskTexture == outfit2.maskTexture &&
                outfit1.torsoTexture == outfit2.torsoTexture &&
                outfit1.pantsTexture == outfit2.pantsTexture &&
                outfit1.parachuteTexture == outfit2.parachuteTexture &&
                outfit1.shoesTexture == outfit2.shoesTexture &&
                outfit1.misc1Texture == outfit2.misc1Texture &&
                outfit1.tops1Texture == outfit2.tops1Texture &&
                outfit1.armourTexture == outfit2.armourTexture &&
                outfit1.crewTexture == outfit2.crewTexture &&
                outfit1.tops2Texture == outfit2.tops2Texture &&
                outfit1.hatTexture == outfit2.hatTexture &&
                outfit1.glassesTexture == outfit2.glassesTexture &&
                outfit1.earpieceTexture == outfit2.earpieceTexture
                );
        }
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
    public class Outfit
    {

        public static uint outfitStructLen = 0x34;
        public static uint outfitNameLen = 0x20;
        public static uint accessoriesStructLen = 0x28;
        public static uint pointerToOutfitStruct = 0x710;
        public static uint pointerToNames = 0x108;
        public static uint pointerToAccessories = 0x2F8;
        public static uint pointerToAccessoriesTextures = 0x164;
        public static uint pointerToOutfitTextures = 0x500;
        public static uint Address;

        private static uint pointer127 = 0x02223918;
        private static uint pointer126 = 0x02223518;
        private static PS3API PS3;
        private static OutfitStruct currentOutfit = new OutfitStruct();
        public static OutfitStruct outfitToImport { get; set; }



        public static List<OutfitStruct> currentOutfits = new List<OutfitStruct>();
        public static List<OutfitStruct> customOutfits = new List<OutfitStruct>();

        public static bool Init(PS3API ps3) {
            PS3 = ps3;
            Address = ps3.Extension.ReadUInt32(Properties.Settings.Default.gtaVersion == "126" ? pointer126 : pointer127);
            return (Address != 0);

        }
        public static OutfitStruct Fetch(int outfitIndex) {
            OutfitStruct tempOutfit = new OutfitStruct();
            uint outfit_struct = (Address - pointerToOutfitStruct) + ((uint)outfitIndex * outfitStructLen) + 4;
            uint accessory_struct = (Address - pointerToAccessories) + ((uint)outfitIndex * accessoriesStructLen);
            uint accessory_textures = (Address - pointerToAccessoriesTextures) + ((uint)outfitIndex * accessoriesStructLen);
            uint outfit_textures = (Address - pointerToOutfitTextures) + ((uint)outfitIndex * outfitStructLen);
            tempOutfit.outfitName = GetOutfitName(outfitIndex);
            tempOutfit.mask = PS3.Extension.ReadInt32(outfit_struct);
            //i think 0x04 is hair but i didnt test it
            tempOutfit.torso = PS3.Extension.ReadInt32(outfit_struct + 0x08);
            tempOutfit.pants = PS3.Extension.ReadInt32(outfit_struct + 0x0C);
            tempOutfit.parachute = PS3.Extension.ReadInt32(outfit_struct + 0x10);
            tempOutfit.shoes = PS3.Extension.ReadInt32(outfit_struct + 0x14);
            tempOutfit.misc1 = PS3.Extension.ReadInt32(outfit_struct + 0x18);
            tempOutfit.tops1 = PS3.Extension.ReadInt32(outfit_struct + 0x1C);
            tempOutfit.armour = PS3.Extension.ReadInt32(outfit_struct + 0x20);
            tempOutfit.crew = PS3.Extension.ReadInt32(outfit_struct + 0x24);
            tempOutfit.tops2 = PS3.Extension.ReadInt32(outfit_struct + 0x28);

            tempOutfit.maskTexture = PS3.Extension.ReadInt32(outfit_textures);
            //i think 0x04 is hair but i didnt test it
            tempOutfit.torsoTexture = PS3.Extension.ReadInt32(outfit_textures + 0x08);
            tempOutfit.pantsTexture = PS3.Extension.ReadInt32(outfit_textures + 0x0C);
            tempOutfit.parachuteTexture = PS3.Extension.ReadInt32(outfit_textures + 0x10);
            tempOutfit.shoesTexture = PS3.Extension.ReadInt32(outfit_textures + 0x14);
            tempOutfit.misc1Texture = PS3.Extension.ReadInt32(outfit_textures + 0x18);
            tempOutfit.tops1Texture = PS3.Extension.ReadInt32(outfit_textures + 0x1C);
            tempOutfit.armourTexture = PS3.Extension.ReadInt32(outfit_textures + 0x20);
            tempOutfit.crewTexture = PS3.Extension.ReadInt32(outfit_textures + 0x24);
            tempOutfit.tops2Texture = PS3.Extension.ReadInt32(outfit_textures + 0x28);

            tempOutfit.hat = PS3.Extension.ReadInt32(accessory_struct);
            tempOutfit.glasses = PS3.Extension.ReadInt32(accessory_struct + 0x04);
            tempOutfit.earpiece = PS3.Extension.ReadInt32(accessory_struct + 0x08);

            tempOutfit.hatTexture = PS3.Extension.ReadInt32(accessory_textures);
            tempOutfit.glassesTexture = PS3.Extension.ReadInt32(accessory_textures + 0x04);
            tempOutfit.earpieceTexture = PS3.Extension.ReadInt32(accessory_textures + 0x08);

            return tempOutfit;
        }
        public static bool Poke(OutfitStruct o, string outfitName, int outfitIndex) {
            uint outfit_struct = (Address - pointerToOutfitStruct) + ((uint)outfitIndex * outfitStructLen) + 4;
            uint accessory_struct = (Address - pointerToAccessories) + ((uint)outfitIndex * accessoriesStructLen);
            uint accessory_textures = (Address - pointerToAccessoriesTextures) + ((uint)outfitIndex * accessoriesStructLen);
            uint outfit_textures = (Address - pointerToOutfitTextures) + ((uint)outfitIndex * outfitStructLen);
            uint name = (Address + pointerToNames) + ((uint)outfitIndex * outfitNameLen);
            //if (PS3.Extension.ReadByte(name) == 00)
            // return false;
            if (outfitName.Length >= (int)Outfit.outfitNameLen)
                outfitName = outfitName.Substring(0, (int)Outfit.outfitNameLen - 1);
            if (!string.Equals(PS3.Extension.ReadString(name), outfitName)) {
                PS3.Extension.WriteString(name, outfitName);
            }
            PS3.Extension.WriteInt32(outfit_struct, o.mask);
            PS3.Extension.WriteInt32(outfit_struct + 0x08, o.torso);
            PS3.Extension.WriteInt32(outfit_struct + 0x0C, o.pants);
            PS3.Extension.WriteInt32(outfit_struct + 0x10, o.parachute);
            PS3.Extension.WriteInt32(outfit_struct + 0x14, o.shoes);
            PS3.Extension.WriteInt32(outfit_struct + 0x18, o.misc1);
            PS3.Extension.WriteInt32(outfit_struct + 0x1C, o.tops1);
            PS3.Extension.WriteInt32(outfit_struct + 0x20, o.armour);
            PS3.Extension.WriteInt32(outfit_struct + 0x24, o.crew);
            PS3.Extension.WriteInt32(outfit_struct + 0x28, o.tops2);

            PS3.Extension.WriteInt32(outfit_textures, o.maskTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x08, o.torsoTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x0C, o.pantsTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x10, o.parachuteTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x14, o.shoesTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x18, o.misc1Texture);
            PS3.Extension.WriteInt32(outfit_textures + 0x1C, o.tops1Texture);
            PS3.Extension.WriteInt32(outfit_textures + 0x20, o.armourTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x24, o.crewTexture);
            PS3.Extension.WriteInt32(outfit_textures + 0x28, o.tops2Texture);

            PS3.Extension.WriteInt32(accessory_struct, o.hat);
            PS3.Extension.WriteInt32(accessory_struct + 0x04, o.glasses);
            PS3.Extension.WriteInt32(accessory_struct + 0x08, o.earpiece);

            PS3.Extension.WriteInt32(accessory_textures, o.hatTexture);
            PS3.Extension.WriteInt32(accessory_textures + 0x04, o.glassesTexture);
            PS3.Extension.WriteInt32(accessory_textures + 0x08, o.earpieceTexture);

            return true;
        }
        public static List<string> FetchOutfitNames() {
            List<string> output = new List<string>(10);
            for (int i = 0; i < 10; i++) {
                uint a = (Address + pointerToNames) + ((uint)i * outfitNameLen);
                if (PS3.Extension.ReadByte(a) == 00)
                    break;
                output.Add(PS3.Extension.ReadString(a));
            }
            return output;
        }
        private static int GetOutfitsCount() {
            return FetchOutfitNames().Count;
        }

        private static string GetOutfitName(int index) {
            uint a = (Address + pointerToNames) + ((uint)index * outfitNameLen);
            if (PS3.Extension.ReadByte(a) == 00)
                return null;
            return PS3.Extension.ReadString(a);
        }

        public static List<OutfitStruct> FetchAllOutfits() {
            List<OutfitStruct> outfitList = new List<OutfitStruct>();
            int outfitCount = GetOutfitsCount();
            for (int i = 0; i < outfitCount; i++) {
                OutfitStruct o = Fetch(i);
                outfitList.Add(o);
            }
            return outfitList;
        }

        public static List<OutfitStruct> FetchOutfitsFromFile(string filename) {
            var o = new List<OutfitStruct>();
            if (!File.Exists(filename)) {
                return o;
            }
            using (Stream s = new FileStream(filename, FileMode.Open))
            using (StreamReader sr = new StreamReader(s)) {
                o = JsonConvert.DeserializeObject<List<OutfitStruct>>(sr.ReadToEnd());
                sr.Close();
                s.Close();
            }
            return o ?? new List<OutfitStruct>();
        }

        public static void SaveCustomOutfitsToFile() {
            using (Stream s = new FileStream("outfits.json", FileMode.OpenOrCreate | FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(s)) {
                sw.Write(JsonConvert.SerializeObject(Outfit.customOutfits, Formatting.Indented));
                sw.Close();
                s.Close();
            }
        }

    }
}
