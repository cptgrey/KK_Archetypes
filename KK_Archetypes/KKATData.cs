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
        // Category enum for accessing / writing to KKATData
        internal enum Category
        {
            Hairstyle,
            Haircolor,
            Eyecolor,
            Eyeline,
            Eyebrow,
            Face,
            Skin,
            Body,
            Clothes
        }

        // Serializable data for saving favorite lists
        [Key(0)]
        public Dictionary<string, ChaFileHair> HairstyleDict { get; set; }
        [Key(1)]
        public Dictionary<string, ChaFileHair> HaircolorDict { get; set; }
        [Key(2)]
        public Dictionary<string, ChaFileFace> EyecolorDict { get; set; }
        [Key(3)]
        public Dictionary<string, ChaFileFace> EyelineDict { get; set; }
        [Key(4)]
        public Dictionary<string, ChaFileFace> EyebrowDict { get; set; }
        [Key(5)]
        public Dictionary<string, ChaFileFace> FaceDict { get; set; }
        [Key(6)]
        public Dictionary<string, ChaFileBody> SkinBodyDict { get; set; }
        [Key(7)]
        public Dictionary<string, ChaFileFace> SkinFaceDict { get; set; }
        [Key(8)]
        public Dictionary<string, ChaFileBody> BodyDict { get; set; }
        [Key(9)]
        public Dictionary<string, List<BoneModifier>> FaceBonesDict { get; set; }
        [Key(10)]
        public Dictionary<string, List<BoneModifier>> BodyBonesDict { get; set; }
        [Key(11)]
        public Dictionary<string, ChaFileClothes> ClothesDict { get; set; }
        [Key(12)]
        public Dictionary<string, ChaFileAccessory> AccessoryDict { get; set; }
        [Key(13)]
        public string Version { get; set; }

        /// <summary>
        /// Initializer for KKATData.
        /// </summary>
        public KKATData()
        {
            HairstyleDict = new Dictionary<string, ChaFileHair>();
            HaircolorDict = new Dictionary<string, ChaFileHair>();
            EyecolorDict = new Dictionary<string, ChaFileFace>();
            EyelineDict = new Dictionary<string, ChaFileFace>();
            EyebrowDict = new Dictionary<string, ChaFileFace>();
            FaceDict = new Dictionary<string, ChaFileFace>();
            SkinBodyDict = new Dictionary<string, ChaFileBody>();
            SkinFaceDict = new Dictionary<string, ChaFileFace>();
            BodyDict = new Dictionary<string, ChaFileBody>();
            FaceBonesDict = new Dictionary<string, List<BoneModifier>>();
            BodyBonesDict = new Dictionary<string, List<BoneModifier>>();
            ClothesDict = new Dictionary<string, ChaFileClothes>(); // Adding clothes + acc separately, ChaFileCoordinate isn't serializable with default resolver
            AccessoryDict = new Dictionary<string, ChaFileAccessory>();
            Version = KK_Archetypes.Version;
        }

        /// <summary>
        /// Method to retrieve list of keys from KKATData for selectable entries in advanced menu.
        /// </summary>
        /// <param category>Category of key to be retrieved</param>
        internal List<string> GetKeys(Category category)
        {
            List<string> tmp = new List<string>();
            switch (category)
            {
                case Category.Hairstyle:
                    foreach (string key in HairstyleDict.Keys) tmp.Add(key);
                    break;
                case Category.Haircolor:
                    foreach (string key in HaircolorDict.Keys) tmp.Add(key);
                    break;
                case Category.Eyecolor:
                    foreach (string key in EyecolorDict.Keys) tmp.Add(key);
                    break;
                case Category.Eyeline:
                    foreach (string key in EyelineDict.Keys) tmp.Add(key);
                    break;
                case Category.Eyebrow:
                    foreach (string key in EyebrowDict.Keys) tmp.Add(key);
                    break;
                case Category.Face:
                    foreach (string key in FaceDict.Keys) tmp.Add(key);
                    break;
                case Category.Skin:
                    foreach (string key in SkinBodyDict.Keys) tmp.Add(key);
                    break;
                case Category.Body:
                    foreach (string key in BodyDict.Keys) tmp.Add(key);
                    break;
                case Category.Clothes:
                    foreach (string key in ClothesDict.Keys) tmp.Add(key);
                    break;
            }
            return tmp;
        }

        /// <summary>
        /// Method to load a specific key from KKATData.
        /// </summary>
        /// <param category>Category of key to be retrieved</param>
        internal void LoadEntry(Category category, string key)
        {
            switch (category)
            {
                case Category.Hairstyle:
                    Hair.LoadHairStyle(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Haircolor:
                    Hair.LoadHairColor(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Eyecolor:
                    Face.LoadEyeColor(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Eyeline:
                    Face.LoadEyeline(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Eyebrow:
                    Face.LoadEyebrow(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Face:
                    Face.LoadFace(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Skin:
                    Body.LoadSkin(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Body:
                    Body.LoadBody(key);
                    Utilities.FinalizeLoad();
                    break;
                case Category.Clothes:
                    Clothes.LoadClothes(key);
                    Utilities.FinalizeLoad();
                    break;
            }
        }

        /// <summary>
        /// Method to remove a specific key from KKATData.
        /// </summary>
        /// <param category>Category of key to be retrieved</param>
        internal void DeleteEntry(Category category, string key)
        {
            switch (category)
            {
                case Category.Hairstyle: HairstyleDict.Remove(key); break;
                case Category.Haircolor: HaircolorDict.Remove(key); break;
                case Category.Eyecolor: EyecolorDict.Remove(key); break;
                case Category.Eyeline: EyelineDict.Remove(key); break;
                case Category.Eyebrow: EyebrowDict.Remove(key); break;
                case Category.Face:
                    FaceDict.Remove(key);
                    FaceBonesDict.Remove(key);
                    break;
                case Category.Skin:
                    SkinFaceDict.Remove(key);
                    SkinBodyDict.Remove(key);
                    break;
                case Category.Body:
                    BodyDict.Remove(key);
                    BodyBonesDict.Remove(key);
                    break;
                case Category.Clothes:
                    ClothesDict.Remove(key);
                    AccessoryDict.Remove(key);
                    break;
            }
        }

        /// <summary>
        /// Method to rename a specific key from KKATData.
        /// </summary>
        /// <param category>Category of key to be retrieved</param>
        internal void RenameEntry(Category category, string key, string newkey)
        {
            newkey += key.Substring(key.IndexOf("->"));
            switch (category)
            {
                case Category.Hairstyle:
                    var hairstyle = HairstyleDict[key];
                    HairstyleDict.Remove(key);
                    HairstyleDict.Add(newkey, hairstyle);
                    break;
                case Category.Haircolor:
                    var haircolor = HaircolorDict[key];
                    HaircolorDict.Remove(key);
                    HaircolorDict.Add(newkey, haircolor);
                    break;
                case Category.Eyecolor:
                    var eyecolor = EyecolorDict[key];
                    EyecolorDict.Remove(key);
                    EyecolorDict.Add(newkey, eyecolor);
                    break;
                case Category.Eyeline:
                    var eyeline = EyelineDict[key];
                    EyelineDict.Remove(key);
                    EyelineDict.Add(newkey, eyeline);
                    break;
                case Category.Eyebrow:
                    var eyebrow = EyebrowDict[key];
                    EyebrowDict.Remove(key);
                    EyebrowDict.Add(newkey, eyebrow);
                    break;
                case Category.Face:
                    var face = FaceDict[key];
                    var facebones = FaceBonesDict[key];
                    FaceDict.Remove(key);
                    FaceBonesDict.Remove(key);
                    FaceDict.Add(newkey, face);
                    FaceBonesDict.Add(newkey, facebones);
                    break;
                case Category.Skin:
                    var skin = SkinFaceDict[key];
                    var skinbody = SkinBodyDict[key];
                    SkinFaceDict.Remove(key);
                    SkinBodyDict.Remove(key);
                    SkinFaceDict.Add(newkey, skin);
                    SkinBodyDict.Add(newkey, skinbody);
                    break;
                case Category.Body:
                    var body = BodyDict[key];
                    var bodybones = BodyBonesDict[key];
                    BodyDict.Remove(key);
                    BodyBonesDict.Remove(key);
                    BodyDict.Add(newkey, body);
                    BodyBonesDict.Add(newkey, bodybones);
                    break;
                case Category.Clothes:
                    var clothes = ClothesDict[key];
                    var accessory = AccessoryDict[key];
                    ClothesDict.Remove(key);
                    AccessoryDict.Remove(key);
                    ClothesDict.Add(newkey, clothes);
                    AccessoryDict.Add(newkey, accessory);
                    break;
            }
        }

        /// <summary>
        /// Method to serialize data.
        /// </summary>
        public void SaveToFile()
        {
            if (!Directory.Exists("./BepInEx/KKAT/")) Directory.CreateDirectory("./BepInEx/KKAT/");
            using (var stream = new FileStream("./BepInEx/KKAT/KKAT_Data.xml", FileMode.Create))
            {
                LZ4MessagePackSerializer.Serialize<KKATData>(stream, this);
            }
            Logger.Log(LogLevel.Message, "Successfully saved Archetype Favorite Data!");
            Utilities.PlaySound();
        }

        /// <summary>
        /// Method to deserialize data.
        /// </summary>
        public static KKATData GetFromFile()
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
