using BepInEx;
using Harmony;
using KKAPI;
using KKAPI.Maker;
using ExtensibleSaveFormat;
using KKABMX.Core;
using System.ComponentModel;

namespace KK_Archetypes
{
    [BepInDependency(KoikatuAPI.GUID)]
    [BepInDependency(KKABMX_Core.GUID)]
    [BepInDependency(ExtendedSave.GUID)]
    [BepInPlugin(GUID, PluginName, Version)]
    public class KK_Archetypes : BaseUnityPlugin
    {
        public const string GUID = "com.cptgrey.bepinex.archetypes";
        public const string PluginName = "KK Character Archetypes";
        public const string PluginNameInternal = "KK_Archetypes";
        public const string Version = "0.9";

        internal static KKATData Data = KKATData.GetFromFile();
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
        public static SavedKeyboardShortcut LoadHairstyleHotkey { get; internal set; }
        [DisplayName("Get Haircolor Hotkey")]
        public static SavedKeyboardShortcut LoadHaircolorHotkey { get; internal set; }
        [DisplayName("Get Eyecolor Hotkey")]
        public static SavedKeyboardShortcut LoadEyecolorHotkey { get; internal set; }
        [DisplayName("Get Eyeline Hotkey")]
        public static SavedKeyboardShortcut LoadEyelineHotkey { get; internal set; }
        [DisplayName("Get Eyebrow Hotkey")]
        public static SavedKeyboardShortcut LoadEyebrowHotkey { get; internal set; }
        [DisplayName("Get Face Hotkey")]
        public static SavedKeyboardShortcut LoadFaceHotkey { get; internal set; }
        [DisplayName("Get Skin Hotkey")]
        public static SavedKeyboardShortcut LoadSkinHotkey { get; internal set; }
        [DisplayName("Get Body Hotkey")]
        public static SavedKeyboardShortcut LoadBodyHotkey { get; internal set; }
        [DisplayName("Get All Hotkey")]
        public static SavedKeyboardShortcut LoadAllHotkey { get; internal set; }

        void Start()
        {
            // Check for KKABMX version, not really necessary (?).
            // if (KoikatuAPI.CheckRequiredPlugin(this, KKABMX_Core.GUID, new Version(KoikatuAPI.VersionConst), LogLevel.Warning)) return;
            var harmony = HarmonyInstance.Create(GUID);
            harmony.PatchAll(typeof(KK_Archetypes));
        }

        void Main()
        {
            // Set menu subcategory callback
            MakerAPI.RegisterCustomSubCategories += _UI.Archetype_Maker_UI_Menu;
        }

        void Update()
        {
            _UI.Update();
        }

        /// <summary>
        /// Clear current KKATData.
        /// </summary>
        internal static void ClearData()
        {
            Data = new KKATData();
            Utilities.PlaySound();
        }

        /// <summary>
        /// Reload previously saved KKATData.
        /// </summary>
        internal static void ReloadData()
        {
            Data = KKATData.GetFromFile();
            Utilities.PlaySound();
        }

        /// <summary>
        /// Add all parts from the selected / current head.
        /// </summary>
        /// <param fromSelected=false>Flag for loading by hotkeys in character list</param>
        internal static void AddAllArchetypesHead(bool fromSelected = false)
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFile file = fromSelected ? Utilities.GetSelectedCharacter() : MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair hair = file.custom.hair;
            ChaFileFace face = file.custom.face;
            Hair.AddHairStyle(hair);
            Hair.AddHairColor(hair);
            Eyes.AddEyeColor(face);
            Eyes.AddEyeline(face);
            Face.AddEyebrow(face);
            Face.AddFace(face);
            if (fromSelected)
            {
                Utilities.IncrementSelectIndex();
            }
            Utilities.PlaySound();
        }

        /// <summary>
        /// Add all parts from the selected / current body.
        /// </summary>
        /// <param fromSelected=false>Flag for loading by hotkeys in character list</param>
        internal static void AddAllArchetypesBody(bool fromSelected = false)
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFile file = fromSelected ? Utilities.GetSelectedCharacter() : MakerAPI.GetCharacterControl().chaFile;
            ChaFileBody body = file.custom.body;
            ChaFileFace face = file.custom.face;
            Body.AddSkin(body, face);
            Body.AddBody(body, fromSelected);
            if (fromSelected)
            {
                Utilities.IncrementSelectIndex();
            }
            Utilities.PlaySound();
        }

        /// <summary>
        /// Load completely randomized character from KKATData.
        /// </summary>
        internal static void LoadAllArchetypes()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            AllFlag = true;
            Hair.LoadRandomArchetypeHairStyle();
            Hair.LoadRandomArchetypeHairColor();
            Eyes.LoadRandomArchetypeEyeColor();
            Eyes.LoadRandomArchetypeEyeline();
            Face.LoadRandomArchetypeEyebrow();
            Face.LoadRandomArchetypeFace();
            Body.LoadRandomArchetypeSkin();
            Body.LoadRandomArchetypeBody();
            MakerAPI.GetCharacterControl().Reload();
            AllFlag = false;
            Utilities.PlaySound();
        }
    }
}
