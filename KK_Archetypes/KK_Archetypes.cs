using BepInEx;
using Harmony;
using UnityEngine;
using UnityEngine.UI;
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
        public const string Version = "1.0.1";

        internal static KKATData Data = KKATData.GetFromFile();
        private static UI _UI = new UI();
        internal static Toggle _loadCharaToggle;
        internal static Toggle _loadCosToggle;
        private static CanvasGroup _parameterGroup;
        private static CanvasGroup _systemGroup;

        public static bool IncrementFlag { get; internal set; }

        [DisplayName("Add All Hotkey")]
        public static SavedKeyboardShortcut AddAllHotkey { get; internal set; }
        [DisplayName("Randomize All Hotkey")]
        public static SavedKeyboardShortcut LoadAllHotkey { get; internal set; }

        void Start()
        {
            // Check for KKABMX version, not really necessary (?).
            AddAllHotkey = new SavedKeyboardShortcut("AddAllHotkey", PluginNameInternal, new KeyboardShortcut(KeyCode.Q, UI.CtrlShift));
            LoadAllHotkey = new SavedKeyboardShortcut("GetAllHotkey", PluginNameInternal, new KeyboardShortcut(KeyCode.E, UI.CtrlShift));

            HarmonyInstance.Create(GUID).PatchAll(typeof(KK_Archetypes));
        }

        /// <summary>
        /// Hook for retrieving instances of CharaMaker components for checking load flags for menus.
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ChaCustom.CustomCharaFile), "Start")]
        public static void InitHook(ChaCustom.CustomCharaFile __instance)
        {
            var st = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/06_SystemTop");
            var pt = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/05_ParameterTop");
            var chat = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/06_SystemTop/tglLoadChara");
            var cost = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/06_SystemTop/tglLoadCos");
            _parameterGroup = pt.transform.GetComponent<CanvasGroup>();
            _systemGroup = st.transform.GetComponent<CanvasGroup>();
            _loadCharaToggle = chat.GetComponent<Toggle>();
            _loadCosToggle = cost.GetComponent<Toggle>();
        }

        /// <summary>
        /// Set menu subcategory callback.
        /// </summary>
        void Main()
        {
            MakerAPI.RegisterCustomSubCategories += _UI.Archetype_Maker_UI_Menu;
        }

        /// <summary>
        /// Calls UI Update function.
        /// </summary>
        void Update()
        {
            _UI.Update();
        }

        /// <summary>
        /// Draws advanced control menu and quick controls menu.
        /// </summary>
        void OnGUI()
        {
            if (MakerAPI.InsideMaker && Manager.Scene.Instance.NowSceneNames[0] == "CustomScene")
            {
                if (UI.showAdvancedGUI && _parameterGroup.interactable)
                {
                    Rect advWindowRect = new Rect(UI._xposAdv, UI._yposAdv, UI._xsizeAdv, UI._ysizeAdv);
                    Rect background = new Rect(GUILayout.Window(3156121, advWindowRect, UI.AdvancedControls, "Advanced Favorite Controls"));
                    UI.DrawSolidWindowBackground(background);
                }
                if (UI.showLoadGUI && _systemGroup.interactable && (_loadCharaToggle.isOn || _loadCosToggle.isOn))
                {
                    Rect quickWindowRect = new Rect(UI._xposQuick, UI._yposQuick, UI._xsizeQuick, UI._ysizeQuick);
                    Rect background = new Rect(GUILayout.Window(3156122, quickWindowRect, UI.QuickControls, "Add to Favorites"));
                    UI.DrawSolidWindowBackground(background);
                }
            }
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
    }
}
