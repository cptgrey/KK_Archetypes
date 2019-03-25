using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using UnityEngine;

namespace KK_Archetypes
{
    class UI
    {
        private KeyCode[] CtrlShift = { KeyCode.LeftControl, KeyCode.LeftShift };

        internal UI()
        {
            KK_Archetypes.IncrementFlag =         new ConfigWrapper<bool>("Increment Character Selector", KK_Archetypes.PluginNameInternal, false);
            KK_Archetypes.AddHairstyleHotkey =    new SavedKeyboardShortcut("AddHairstyleHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.A, CtrlShift));
            KK_Archetypes.AddHaircolorHotkey =    new SavedKeyboardShortcut("AddHaircolorHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.S, CtrlShift));
            KK_Archetypes.AddEyebrowHotkey =      new SavedKeyboardShortcut("AddEyebrowHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.D, CtrlShift));
            KK_Archetypes.AddEyelineHotkey =      new SavedKeyboardShortcut("AddEyelineHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.F, CtrlShift));
            KK_Archetypes.AddEyecolorHotkey =     new SavedKeyboardShortcut("AddEyecolorHotkey",  KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.G, CtrlShift));
            KK_Archetypes.AddFaceHotkey =         new SavedKeyboardShortcut("AddFaceHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.H, CtrlShift));
            KK_Archetypes.AddSkinHotkey =         new SavedKeyboardShortcut("AddSkinHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.J, CtrlShift));
            KK_Archetypes.AddBodyHotkey =         new SavedKeyboardShortcut("AddBodyHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.K, CtrlShift));
            KK_Archetypes.AddAllHeadHotkey =      new SavedKeyboardShortcut("AddAllHeadHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.Q, CtrlShift));
            KK_Archetypes.AddAllBodyHotkey =      new SavedKeyboardShortcut("AddAllBodyHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.W, CtrlShift));

            KK_Archetypes.LoadHairstyleHotkey =    new SavedKeyboardShortcut("GetHairstyleHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.Z, CtrlShift));
            KK_Archetypes.LoadHaircolorHotkey =    new SavedKeyboardShortcut("GetHaircolorHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.X, CtrlShift));
            KK_Archetypes.LoadEyebrowHotkey =      new SavedKeyboardShortcut("GetEyebrowHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.C, CtrlShift));
            KK_Archetypes.LoadEyelineHotkey =      new SavedKeyboardShortcut("GetEyelineHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.V, CtrlShift));
            KK_Archetypes.LoadEyecolorHotkey =     new SavedKeyboardShortcut("GetEyecolorHotkey",  KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.B, CtrlShift));
            KK_Archetypes.LoadFaceHotkey =         new SavedKeyboardShortcut("GetFaceHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.N, CtrlShift));
            KK_Archetypes.LoadSkinHotkey =         new SavedKeyboardShortcut("GetSkinHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.M, CtrlShift));
            KK_Archetypes.LoadBodyHotkey =         new SavedKeyboardShortcut("GetBodyHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.Comma, CtrlShift));
            KK_Archetypes.LoadAllHotkey =          new SavedKeyboardShortcut("GetAllHotkey",       KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.E, CtrlShift));
        }

        internal void Update()
        {
            if (KK_Archetypes.AddHairstyleHotkey.IsDown()) Hair.AddArchetypeHairStyleFromSelected();
            if (KK_Archetypes.AddHaircolorHotkey.IsDown()) Hair.AddArchetypeHairColorFromSelected();
            if (KK_Archetypes.AddEyelineHotkey.IsDown()) Eyes.AddArchetypeEyelineFromSelected();
            if (KK_Archetypes.AddEyecolorHotkey.IsDown()) Eyes.AddArchetypeEyeColorFromSelected();
            if (KK_Archetypes.AddEyebrowHotkey.IsDown()) Face.AddArchetypeEyebrowFromSelected();
            if (KK_Archetypes.AddFaceHotkey.IsDown()) Face.AddArchetypeFaceFromSelected();
            if (KK_Archetypes.AddSkinHotkey.IsDown()) Body.AddArchetypeSkinFromSelected();
            if (KK_Archetypes.AddBodyHotkey.IsDown()) Body.AddArchetypeBodyFromSelected();

            if (KK_Archetypes.LoadHairstyleHotkey.IsDown()) Hair.LoadRandomArchetypeHairStyle();
            if (KK_Archetypes.LoadHaircolorHotkey.IsDown()) Hair.LoadRandomArchetypeHairColor();
            if (KK_Archetypes.LoadEyelineHotkey.IsDown()) Eyes.LoadRandomArchetypeEyeline();
            if (KK_Archetypes.LoadEyecolorHotkey.IsDown()) Eyes.LoadRandomArchetypeEyeColor();
            if (KK_Archetypes.LoadEyebrowHotkey.IsDown()) Face.LoadRandomArchetypeEyebrow();
            if (KK_Archetypes.LoadFaceHotkey.IsDown()) Face.LoadRandomArchetypeFace();
            if (KK_Archetypes.LoadSkinHotkey.IsDown()) Body.LoadRandomArchetypeSkin();
            if (KK_Archetypes.LoadBodyHotkey.IsDown()) Body.LoadRandomArchetypeBody();

            if (KK_Archetypes.AddAllHeadHotkey.IsDown()) KK_Archetypes.AddAllArchetypesHead(true);
            if (KK_Archetypes.AddAllBodyHotkey.IsDown()) KK_Archetypes.AddAllArchetypesBody(true);
            if (KK_Archetypes.LoadAllHotkey.IsDown()) KK_Archetypes.LoadAllArchetypes();
        }

        public void Archetype_Maker_UI_Menu(object sender, RegisterSubCategoriesEvent e)
        {
            KK_Archetypes KKAT_instance = Singleton<KK_Archetypes>.Instance;
            // Face Menu
            e.AddControl(new MakerText("Add Favorites To List", MakerConstants.Face.All, KKAT_instance));
            e.AddControl(new MakerButton("Add Hair Style", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.AddArchetypeHairStyle(); });
            e.AddControl(new MakerButton("Add Hair Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.AddArchetypeHairColor(); });
            e.AddControl(new MakerButton("Add Eye Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.AddArchetypeEyeColor(); });
            e.AddControl(new MakerButton("Add Eyeline", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.AddArchetypeEyeline(); });
            e.AddControl(new MakerButton("Add Eyebrow", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.AddArchetypeEyebrow(); });
            e.AddControl(new MakerButton("Add Face", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.AddArchetypeFace(); });

            e.AddControl(new MakerText("Randomize Favorite Styles", MakerConstants.Face.All, KKAT_instance));
            e.AddControl(new MakerButton("Get Hair Style", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.LoadRandomArchetypeHairStyle(); });
            e.AddControl(new MakerButton("Get Hair Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.LoadRandomArchetypeHairColor(); });
            e.AddControl(new MakerButton("Get Eye Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.LoadRandomArchetypeEyeColor(); });
            e.AddControl(new MakerButton("Get Eyeline", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.LoadRandomArchetypeEyeline(); });
            e.AddControl(new MakerButton("Get Eyebrow", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.LoadRandomArchetypeEyebrow(); });
            e.AddControl(new MakerButton("Get Face", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.LoadRandomArchetypeFace(); });

            // Body Menu
            e.AddControl(new MakerText("Add Favorites To List", MakerConstants.Body.All, KKAT_instance));
            e.AddControl(new MakerButton("Add Skin", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.AddArchetypeSkin(); });
            e.AddControl(new MakerButton("Add Body", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.AddArchetypeBody(); });

            e.AddControl(new MakerText("Randomize Favorite Styles", MakerConstants.Body.All, KKAT_instance));
            e.AddControl(new MakerButton("Get Skin", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.LoadRandomArchetypeSkin(); });
            e.AddControl(new MakerButton("Get Body", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.LoadRandomArchetypeBody(); });

            // Parameter Menu
            e.AddControl(new MakerText("Archetype Control", MakerConstants.Parameter.Character, KKAT_instance));
            e.AddControl(new MakerButton("Add Head To All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.AddAllArchetypesHead(); });
            e.AddControl(new MakerButton("Add Body To All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.AddAllArchetypesBody(); });
            e.AddControl(new MakerButton("Randomize All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.LoadAllArchetypes(); });

            e.AddControl(new MakerText("Save/Reset Favorites", MakerConstants.Parameter.Character, KKAT_instance));
            e.AddControl(new MakerButton("Save List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.Data.SaveToFile(); });
            e.AddControl(new MakerButton("Reset List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.ClearData(); });
            e.AddControl(new MakerButton("Reload List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.ReloadData(); });
        }
    }
}
