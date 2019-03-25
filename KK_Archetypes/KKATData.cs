using System;
using System.IO;
using MessagePack;
using KKABMX.Core;
using BepInEx.Logging;
using System.Collections.Generic;
using Logger = BepInEx.Logger;

namespace KK_Archetypes
{
    [MessagePackObject]
    public class KKATData
    {
        [Key(0)]
        public List<ChaFileHair> Hairstyle { get; set; }
        [Key(1)]
        public List<ChaFileHair> Haircolor { get; set; }
        [Key(2)]
        public List<ChaFileFace> Eyecolor { get; set; }
        [Key(3)]
        public List<ChaFileFace> Eyeline { get; set; }
        [Key(4)]
        public List<ChaFileFace> Eyebrow { get; set; }
        [Key(5)]
        public List<ChaFileFace> Face { get; set; }
        [Key(6)]
        public List<ChaFileBody> SkinBody { get; set; }
        [Key(7)]
        public List<ChaFileFace> SkinFace { get; set; }
        [Key(8)]
        public List<ChaFileBody> Body { get; set; }
        [Key(9)]
        public List<List<BoneModifier>> Bones { get; set; }
        [Key(10)]
        public string Version { get; set; }

        public KKATData()
        {
            Hairstyle = new List<ChaFileHair>();
            Haircolor = new List<ChaFileHair>();
            Eyecolor = new List<ChaFileFace>();
            Eyeline = new List<ChaFileFace>();
            Eyebrow = new List<ChaFileFace>();
            Face = new List<ChaFileFace>();
            SkinBody = new List<ChaFileBody>();
            SkinFace = new List<ChaFileFace>();
            Body = new List<ChaFileBody>();
            Bones = new List<List<BoneModifier>>();
            Version = KK_Archetypes.Version;
        }

        public void Save()
        {
            if (!Directory.Exists("./BepInEx/KKAT/")) Directory.CreateDirectory("./BepInEx/KKAT/");
            using (var stream = new FileStream("./BepInEx/KKAT/KKAT_Data.xml", FileMode.Create))
            {
                LZ4MessagePackSerializer.Serialize<KKATData>(stream, this);
            }
        }

        public static KKATData LoadFromFile()
        {
            if (!File.Exists("./BepInEx/KKAT/KKAT_Data.xml")) return new KKATData();
            try
            {
                using (var stream = new FileStream("./BepInEx/KKAT/KKAT_Data.xml", FileMode.Open))
                {
                    return LZ4MessagePackSerializer.Deserialize<KKATData>(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, "[KK_Archetypes] Invalid data format in KKATData - " + ex);
                return new KKATData();
            }
        }

    }
}
