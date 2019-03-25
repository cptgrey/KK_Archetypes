using BepInEx;
using Harmony;
using KKAPI;
using KKAPI.Maker;
using KKABMX.Core;
using System.ComponentModel;

namespace KK_Archetypes
{
    [BepInDependency(KoikatuAPI.GUID)]
    [BepInDependency(KKABMX_Core.GUID)]
    [BepInPlugin(GUID, PluginName, Version)]
    public class KK_Archetypes : BaseUnityPlugin
    {
        public const string GUID = "com.cptgrey.bepinex.archetypes";
        public const string PluginName = "KK Character Archetypes";
        public const string PluginNameInternal = "KK_Archetypes";
        public const string Version = "0.4";

        internal static KKATData Data = KKATData.LoadFromFile();
        internal static bool AllFlag = false;
        private static UI _UI = new UI();

        [DisplayName("Increment Character Selector")]
        [Description("Increment the selected character in the maker when using hotkeys.\n" +
            "NOTE: The incrementation follows an alphabetical descending order,\n" +
             "not necessarily the order the characters are in on screen! (Slightly buggy)")]
        public static ConfigWrapper<bool> IncrementFlag { get; internal set; }

        [DisplayName("Add Hairstyle Hotkey")]
        public static SavedKeyboardShortcut AddHairstyleHotkey { get; internal set; }
        [DisplayName("Add Haircolor Hotkey")]
        public static SavedKeyboardShortcut AddHaircolorHotkey { get; internal set; }
        [DisplayName("Add Eyecolor Hotkey")]
        public static SavedKeyboardShortcut AddEyecolorHotkey { get; internal set; }
        [DisplayName("Add Eyeline Hotkey")]
        public static SavedKeyboardShortcut AddEyelineHotkey { get; internal set; }
        [DisplayName("Add Eyebrow Hotkey")]
        public static SavedKeyboardShortcut AddEyebrowHotkey { get; internal set; }
        [DisplayName("Add Face Hotkey")]
        public static SavedKeyboardShortcut AddFaceHotkey { get; internal set; }
        [DisplayName("Add Skin Hotkey")]
        public static SavedKeyboardShortcut AddSkinHotkey { get; internal set; }
        [DisplayName("Add Body Hotkey")]
        public static SavedKeyboardShortcut AddBodyHotkey { get; internal set; }
        [DisplayName("Add All Head Hotkey")]
        public static SavedKeyboardShortcut AddAllHeadHotkey { get; internal set; }
        [DisplayName("Add All Body Hotkey")]
        public static SavedKeyboardShortcut AddAllBodyHotkey { get; internal set; }


        [DisplayName("Get Hairstyle Hotkey")]
        public static SavedKeyboardShortcut GetHairstyleHotkey { get; internal set; }
        [DisplayName("Get Haircolor Hotkey")]
        public static SavedKeyboardShortcut GetHaircolorHotkey { get; internal set; }
        [DisplayName("Get Eyecolor Hotkey")]
        public static SavedKeyboardShortcut GetEyecolorHotkey { get; internal set; }
        [DisplayName("Get Eyeline Hotkey")]
        public static SavedKeyboardShortcut GetEyelineHotkey { get; internal set; }
        [DisplayName("Get Eyebrow Hotkey")]
        public static SavedKeyboardShortcut GetEyebrowHotkey { get; internal set; }
        [DisplayName("Get Face Hotkey")]
        public static SavedKeyboardShortcut GetFaceHotkey { get; internal set; }
        [DisplayName("Get Skin Hotkey")]
        public static SavedKeyboardShortcut GetSkinHotkey { get; internal set; }
        [DisplayName("Get Body Hotkey")]
        public static SavedKeyboardShortcut GetBodyHotkey { get; internal set; }
        [DisplayName("Get All Hotkey")]
        public static SavedKeyboardShortcut GetAllHotkey { get; internal set; }



        void Main()
        {
            var harmony = HarmonyInstance.Create(GUID);
            harmony.PatchAll(typeof(KK_Archetypes));

            // Set menu subcategory callback
            MakerAPI.RegisterCustomSubCategories += _UI.Archetype_Maker_UI_Menu;
        }

        void Update()
        {
            _UI.Update();
        }

        internal static void ResetData()
        {
            Data = new KKATData();
            Utilities.PlaySound();
        }

        internal static void ReloadData()
        {
            Data = KKATData.LoadFromFile();
            Utilities.PlaySound();
        }

        internal static void AddAllArchetypesHead(bool fromSelected = false)
        {
            ChaFile file = fromSelected ? Utilities.GetSelectedCharacter() : MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair hair = file.custom.hair;
            ChaFileFace face = file.custom.face;
            Hair.AddHairStyle(hair, Data);
            Hair.AddHairColor(hair, Data);
            Eyes.AddEyeColor(face, Data);
            Eyes.AddEyeline(face, Data);
            Face.AddEyebrow(face, Data);
            Face.AddFace(face, Data);
            if (fromSelected)
            {
                Utilities.IncrementSelectIndex();
            }
            Utilities.PlaySound();
        }

        internal static void AddAllArchetypesBody(bool fromSelected = false)
        {
            ChaFile file = fromSelected ? Utilities.GetSelectedCharacter() : MakerAPI.GetCharacterControl().chaFile;
            ChaFileBody body = file.custom.body;
            ChaFileFace face = file.custom.face;
            Body.AddSkin(body, face, Data);
            Body.AddBody(body, Data, fromSelected);
            if (fromSelected)
            {
                Utilities.IncrementSelectIndex();
            }
            Utilities.PlaySound();
        }

        internal static void GetAllArchetypes()
        {
            AllFlag = true;
            Hair.GetRandomArchetypeHairStyle(Data);
            Hair.GetRandomArchetypeHairColor(Data);
            Eyes.GetRandomArchetypeEyeColor(Data);
            Eyes.GetRandomArchetypeEyeline(Data);
            Face.GetRandomArchetypeEyebrow(Data);
            Face.GetRandomArchetypeFace(Data);
            Body.GetRandomArchetypeSkin(Data);
            Body.GetRandomArchetypeBody(Data);
            MakerAPI.GetCharacterControl().Reload();
            AllFlag = false;
            Utilities.PlaySound();
        }
    }
}
