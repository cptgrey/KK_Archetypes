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

            KK_Archetypes.GetHairstyleHotkey =    new SavedKeyboardShortcut("GetHairstyleHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.Z, CtrlShift));
            KK_Archetypes.GetHaircolorHotkey =    new SavedKeyboardShortcut("GetHaircolorHotkey", KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.X, CtrlShift));
            KK_Archetypes.GetEyebrowHotkey =      new SavedKeyboardShortcut("GetEyebrowHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.C, CtrlShift));
            KK_Archetypes.GetEyelineHotkey =      new SavedKeyboardShortcut("GetEyelineHotkey",   KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.V, CtrlShift));
            KK_Archetypes.GetEyecolorHotkey =     new SavedKeyboardShortcut("GetEyecolorHotkey",  KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.B, CtrlShift));
            KK_Archetypes.GetFaceHotkey =         new SavedKeyboardShortcut("GetFaceHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.N, CtrlShift));
            KK_Archetypes.GetSkinHotkey =         new SavedKeyboardShortcut("GetSkinHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.M, CtrlShift));
            KK_Archetypes.GetBodyHotkey =         new SavedKeyboardShortcut("GetBodyHotkey",      KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.Comma, CtrlShift));
            KK_Archetypes.GetAllHotkey =          new SavedKeyboardShortcut("GetAllHotkey",       KK_Archetypes.PluginNameInternal, new KeyboardShortcut(KeyCode.E, CtrlShift));
        }

        internal void Update()
        {
            if (KK_Archetypes.AddHairstyleHotkey.IsDown()) Hair.AddArchetypeHairStyleFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddHaircolorHotkey.IsDown()) Hair.AddArchetypeHairColorFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddEyelineHotkey.IsDown()) Eyes.AddArchetypeEyelineFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddEyecolorHotkey.IsDown()) Eyes.AddArchetypeEyeColorFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddEyebrowHotkey.IsDown()) Face.AddArchetypeEyebrowFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddFaceHotkey.IsDown()) Face.AddArchetypeFaceFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddSkinHotkey.IsDown()) Body.AddArchetypeSkinFromSelected(KK_Archetypes.Data);
            if (KK_Archetypes.AddBodyHotkey.IsDown()) Body.AddArchetypeBodyFromSelected(KK_Archetypes.Data);

            if (KK_Archetypes.GetHairstyleHotkey.IsDown()) Hair.GetRandomArchetypeHairStyle(KK_Archetypes.Data);
            if (KK_Archetypes.GetHaircolorHotkey.IsDown()) Hair.GetRandomArchetypeHairColor(KK_Archetypes.Data);
            if (KK_Archetypes.GetEyelineHotkey.IsDown()) Eyes.GetRandomArchetypeEyeline(KK_Archetypes.Data);
            if (KK_Archetypes.GetEyecolorHotkey.IsDown()) Eyes.GetRandomArchetypeEyeColor(KK_Archetypes.Data);
            if (KK_Archetypes.GetEyebrowHotkey.IsDown()) Face.GetRandomArchetypeEyebrow(KK_Archetypes.Data);
            if (KK_Archetypes.GetFaceHotkey.IsDown()) Face.GetRandomArchetypeFace(KK_Archetypes.Data);
            if (KK_Archetypes.GetSkinHotkey.IsDown()) Body.GetRandomArchetypeSkin(KK_Archetypes.Data);
            if (KK_Archetypes.GetBodyHotkey.IsDown()) Body.GetRandomArchetypeBody(KK_Archetypes.Data);

            if (KK_Archetypes.AddAllHeadHotkey.IsDown()) KK_Archetypes.AddAllArchetypesHead(true);
            if (KK_Archetypes.AddAllBodyHotkey.IsDown()) KK_Archetypes.AddAllArchetypesBody(true);
            if (KK_Archetypes.GetAllHotkey.IsDown()) KK_Archetypes.GetAllArchetypes();
        }

        public void Archetype_Maker_UI_Menu(object sender, RegisterSubCategoriesEvent e)
        {
            KK_Archetypes KKAT_instance = Singleton<KK_Archetypes>.Instance;
            // Face Menu
            e.AddControl(new MakerText("Add Favorites To List", MakerConstants.Face.All, KKAT_instance));
            e.AddControl(new MakerButton("Add Hair Style", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.AddArchetypeHairStyle(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Hair Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.AddArchetypeHairColor(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Eye Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.AddArchetypeEyeColor(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Eyeline", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.AddArchetypeEyeline(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Eyebrow", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.AddArchetypeEyebrow(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Face", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.AddArchetypeFace(KK_Archetypes.Data); });

            e.AddControl(new MakerText("Randomize Favorite Styles", MakerConstants.Face.All, KKAT_instance));
            e.AddControl(new MakerButton("Get Hair Style", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.GetRandomArchetypeHairStyle(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Hair Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Hair.GetRandomArchetypeHairColor(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Eye Color", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.GetRandomArchetypeEyeColor(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Eyeline", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Eyes.GetRandomArchetypeEyeline(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Eyebrow", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.GetRandomArchetypeEyebrow(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Face", MakerConstants.Face.All, KKAT_instance)).OnClick.AddListener(delegate { Face.GetRandomArchetypeFace(KK_Archetypes.Data); });

            // Body Menu
            e.AddControl(new MakerText("Add Favorites To List", MakerConstants.Body.All, KKAT_instance));
            e.AddControl(new MakerButton("Add Skin", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.AddArchetypeSkin(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Add Body", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.AddArchetypeBody(KK_Archetypes.Data); });

            e.AddControl(new MakerText("Randomize Favorite Styles", MakerConstants.Body.All, KKAT_instance));
            e.AddControl(new MakerButton("Get Skin", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.GetRandomArchetypeSkin(KK_Archetypes.Data); });
            e.AddControl(new MakerButton("Get Body", MakerConstants.Body.All, KKAT_instance)).OnClick.AddListener(delegate { Body.GetRandomArchetypeBody(KK_Archetypes.Data); });

            // Parameter Menu
            e.AddControl(new MakerText("Archetype Control", MakerConstants.Parameter.Character, KKAT_instance));
            e.AddControl(new MakerButton("Add Head To All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.AddAllArchetypesHead(); });
            e.AddControl(new MakerButton("Add Body To All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.AddAllArchetypesBody(); });
            e.AddControl(new MakerButton("Randomize All", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.GetAllArchetypes(); });

            e.AddControl(new MakerText("Save/Reset Favorites", MakerConstants.Parameter.Character, KKAT_instance));
            e.AddControl(new MakerButton("Save List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.Data.Save(); });
            e.AddControl(new MakerButton("Reset List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.ResetData(); });
            e.AddControl(new MakerButton("Reload List", MakerConstants.Parameter.Character, KKAT_instance)).OnClick.AddListener(delegate { KK_Archetypes.ReloadData(); });
        }
    }
}
