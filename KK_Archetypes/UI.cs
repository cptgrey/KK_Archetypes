using KKAPI.Maker;
using KKAPI.Maker.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace KK_Archetypes
{
    class UI
    {
        // Main instance of plugin, used to set ownership of elements in Maker menu for KKAPI
        internal static KK_Archetypes KKAT_instance { get; set;}

        // Subcategory menu for plugin
        internal static MakerCategory FavoritesSubCat;

        // Flags for showing UI menus
        internal static bool showAvancedGUI { get; set; }
        internal static bool showLoadGUI { get; set; } // Remnant from implementation of LoadToggles. Left for possible later implementation.

        // Quick reference to Ctrl+Shift hotkey combination
        internal static KeyCode[] CtrlShift = { KeyCode.LeftControl, KeyCode.LeftShift };

        // Parameters for selectable entries in advanced control menu
        private static KKATData.Category _currCategory = KKATData.Category.Hairstyle;
        private static string _selected = null;
        private static string _renameEntry = "";
        private static GUIStyle _selectStyle = new GUIStyle();
        private static GUIStyle _normalStyle = new GUIStyle();

        // GUI Menu parameters
        internal static Vector2 _scrollPos;
        internal static Rect _advWindowRect;
        internal static Rect _quickWindowRect;

        private static float _xsizeAdv = Screen.width * .237f;
        private static float _ysizeAdv = Screen.height * .32f;
        private static float _xposAdv = Screen.width * .078f;
        private static float _yposAdv = Screen.height * .66f;

        private static float _xsizeQuick = Screen.width * .125f;
        private static float _ysizeQuick = Screen.height * .17f;
        private static float _xposQuick = Screen.width * .004f;
        private static float _yposQuick = Screen.height * .28f;

        private static float _quickToggleWidth = (_xsizeQuick - 18) / 2;

        // Flag for selecting / deselecting all toggles
        private static MakerToggle AllToggle;
        private static string[] _currOutfit = { "School", "Going Home", "Gym", "Swimsuit", "Club", "Casual", "Sleepwear" };

        /// <summary>
        /// Initializer for Favorites UI.
        /// </summary>
        internal UI()
        {
            KKAT_instance = Singleton<KK_Archetypes>.Instance;
            showAvancedGUI = false;
            showLoadGUI = true;
            _advWindowRect = new Rect(_xposAdv, _yposAdv, _xsizeAdv, _ysizeAdv);
            _quickWindowRect = new Rect(_xposQuick, _yposQuick, _xsizeQuick, _ysizeQuick);

            _selectStyle.normal.background = new Texture2D(1,1);
            _selectStyle.normal.background.SetPixel(1, 1, new Color(1, 1, 1, .4f));
            _selectStyle.normal.background.Apply();
            _selectStyle.active.background = _selectStyle.normal.background;
            _selectStyle.focused.background = _selectStyle.normal.background;
            _selectStyle.hover.background = _selectStyle.normal.background;

            _normalStyle.normal.textColor = Color.white;
            _normalStyle.active.textColor = Color.white;
            _normalStyle.hover.textColor = Color.white;
            _normalStyle.focused.textColor = Color.white;

            FavoritesSubCat = new MakerCategory(MakerConstants.Parameter.Character.CategoryName, "Archetypes");
            AllToggle = new MakerToggle(FavoritesSubCat, "Select/Deselect All", KKAT_instance);
        }

        /// <summary>
        /// Update function, called by main class for hotkey purposes.
        /// </summary>
        internal void Update()
        {
            if (KK_Archetypes.AddAllHotkey.IsDown())
            {
                if (MakerAPI.InsideAndLoaded)
                    AddSelected(true);
            }
            if (KK_Archetypes.LoadAllHotkey.IsDown())
            {
                LoadSelected(true);
                Utilities.FinalizeLoad();
            }
        }

        /// <summary>
        /// Initializer for Favorites subcategory under preferences in Maker Menu.
        /// </summary>
        /// <param sender>Sender object to event handler</param>
        /// <param e>Event handler for subcategories</param>
        public void Archetype_Maker_UI_Menu(object sender, RegisterSubCategoriesEvent e)
        {
            // Register subcategory
            e.AddSubCategory(FavoritesSubCat);

            e.AddControl(new MakerText("Select parts to add/load from favorites and use the buttons to apply your choice.", FavoritesSubCat, KKAT_instance));
            e.AddControl(new MakerSeparator(FavoritesSubCat, KKAT_instance));

            // Make controls for custom subcategory
            Hair._toggleHairstyle = new MakerToggle(FavoritesSubCat, "Haircolor", KKAT_instance);
            Hair._toggleHaircolor = new MakerToggle(FavoritesSubCat, "Hairstyle", KKAT_instance);
            e.AddControl(Hair._toggleHairstyle);
            e.AddControl(Hair._toggleHaircolor);

            e.AddControl(new MakerSeparator(FavoritesSubCat, KKAT_instance));

            Face._toggleEyecolor = new MakerToggle(FavoritesSubCat, "Eyecolor", KKAT_instance);
            Face._toggleEyeline = new MakerToggle(FavoritesSubCat, "Eyeline", KKAT_instance);
            Face._toggleEyebrow = new MakerToggle(FavoritesSubCat, "Eyebrow", KKAT_instance);
            Face._toggleFace = new MakerToggle(FavoritesSubCat, "Face", KKAT_instance);
            e.AddControl(Face._toggleEyecolor);
            e.AddControl(Face._toggleEyeline);
            e.AddControl(Face._toggleEyebrow);
            e.AddControl(Face._toggleFace);

            e.AddControl(new MakerSeparator(FavoritesSubCat, KKAT_instance));

            Body._toggleSkin = new MakerToggle(FavoritesSubCat, "Skin", KKAT_instance);
            Body._toggleBody = new MakerToggle(FavoritesSubCat, "Body", KKAT_instance);
            e.AddControl(Body._toggleSkin);
            e.AddControl(Body._toggleBody);

            e.AddControl(new MakerSeparator(FavoritesSubCat, KKAT_instance));

            Clothes._toggleClothes = new MakerToggle(FavoritesSubCat, "Clothes", KKAT_instance);
            e.AddControl(Clothes._toggleClothes);

            e.AddControl(new MakerSeparator(FavoritesSubCat, KKAT_instance));

            e.AddControl(AllToggle).ValueChanged.Subscribe(b => ToggleAll(b));

            e.AddControl(new MakerButton("Add Selection To Favorites", FavoritesSubCat, KKAT_instance)).OnClick.AddListener(delegate { AddSelected(); });
            e.AddControl(new MakerButton("Get Random From Favorites", FavoritesSubCat, KKAT_instance)).OnClick.AddListener(delegate { LoadSelected(); });

            e.AddControl(new MakerToggle(FavoritesSubCat, "Show Advanced Favorite Controls", KKAT_instance)).ValueChanged.Subscribe(b => showAvancedGUI = b);
        }

        /// <summary>
        /// Method to add data to favorite list.
        /// NOTE: This could possibly be moved to the main plugin class
        /// </summary>
        /// <param fromSelected>Flag for adding from CustomFileList or from current character</param>
        private static void AddSelected(bool fromSelected = false)
        {
            bool change = false;
            if (!KK_Archetypes._loadCosToggle.isOn) // Do not add character data if in Coordinate Load menu
            {
                ChaFileControl file = fromSelected ? Utilities.GetSelectedCharacter() : MakerAPI.GetCharacterControl().chaFile;
                if (file != null)
                {
                    Hair.AddHairStyle(file);
                    Hair.AddHairColor(file);
                    Face.AddEyeColor(file);
                    Face.AddEyeline(file);
                    Face.AddEyebrow(file);
                    Face.AddFace(file, fromSelected);
                    Body.AddSkin(file);
                    Body.AddBody(file, fromSelected);
                }
            }
            ChaFileCoordinate clothes;
            string key; // Create key in case user is adding coordinate from character instead of coordinate file
            if (fromSelected)
            {
                if (KK_Archetypes._loadCosToggle.isOn)
                {
                    clothes = Utilities.GetSelectedCoordinate();
                    key = null;
                }
                else
                {
                    clothes = Utilities.GetSelectedCharacter().coordinate[Clothes._coordinateToggle];
                    key = Utilities.CreateNewKey(Utilities.GetSelectedCharacter());
                }
            }
            else
            {
                clothes = MakerAPI.GetCharacterControl().nowCoordinate;
                key = Utilities.CreateNewKey(Utilities.GetSelectedCharacter());
            }
            if (clothes != null)
            {
                Clothes.AddClothes(clothes, key);
                if (KK_Archetypes.IncrementFlag) Utilities.IncrementSelectIndex();
                Utilities.PlaySound();
            }
        }

        /// <summary>
        /// Method to load (random) data from favorite list.
        /// NOTE: This could possibly be moved to the main plugin class
        /// </summary>
        /// <param combine>Flag for checking if data is loaded in combination</param>
        private static void LoadSelected(bool combine = false)
        {
            Hair.LoadHairStyle();
            Hair.LoadHairColor();
            Face.LoadEyeColor();
            Face.LoadEyeline();
            Face.LoadEyebrow();
            Face.LoadFace();
            Body.LoadSkin();
            Body.LoadBody();
            Clothes.LoadClothes();
            if (!combine) Utilities.FinalizeLoad();
        }

        /// <summary>
        /// Method to draw buttons for advanced control menu.
        /// </summary>
        public static void DrawAdvCtrButtons()
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save")) KK_Archetypes.Data.SaveToFile();
                if (GUILayout.Button("Clear")) KK_Archetypes.ClearData();
                if (GUILayout.Button("Reload")) KK_Archetypes.ReloadData();
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Method to draw toggles for advanced control menu.
        /// </summary>
        public static void DrawTogglesAdv()
        {
            GUILayout.BeginVertical(GUI.skin.box);
            {
                GUILayout.BeginHorizontal();
                {
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Hairstyle, "Hairstyle") ? KKATData.Category.Hairstyle : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Haircolor, "Haircolor") ? KKATData.Category.Haircolor : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Eyeline, "Eyeline") ? KKATData.Category.Eyeline : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Eyecolor, "Eyecolor") ? KKATData.Category.Eyecolor : _currCategory;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Eyebrow, "Eyebrow") ? KKATData.Category.Eyebrow : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Face, "Face") ? KKATData.Category.Face : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Skin, "Skin") ? KKATData.Category.Skin : _currCategory;
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Body, "Body") ? KKATData.Category.Body : _currCategory;
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    _currCategory = GUILayout.Toggle(_currCategory == KKATData.Category.Clothes, "Clothes") ? KKATData.Category.Clothes : _currCategory;
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Method to draw toggles for advanced control menu.
        /// </summary>
        public static void DrawScrollList()
        {
            DrawAdvCtrButtons();
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.MaxWidth(_xsizeAdv - 8), GUILayout.MaxHeight(_ysizeAdv - 148));
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(_ysizeAdv - 188));
            {
                foreach (string key in KK_Archetypes.Data.GetKeys(_currCategory))
                {
                    string showKey = key.Remove(key.IndexOf("->"), key.Length - key.IndexOf("->"));
                    if (_selected != key)
                    {
                        if (GUILayout.Button(showKey, _normalStyle))
                        {
                            _selected = key;
                            _renameEntry = showKey;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(showKey, _selectStyle))
                        {
                            _selected = key;
                            _renameEntry = showKey;
                        }
                    }
                }
                if (KK_Archetypes.Data.GetKeys(_currCategory).Count == 0) GUILayout.Label("");
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// Method to draw toggles for quick control menu.
        /// </summary>
        public static void DrawTogglesQuick()
        {
            // Check if currently in chara load screen and disable inactive toggles
            GUI.enabled = KK_Archetypes._loadCharaToggle.isOn;
            {
                // Cooridnate select button
                GUILayout.BeginHorizontal(GUI.skin.box);
                {
                    GUILayout.Label("Outfit:");
                    if (GUILayout.Button(_currOutfit[Clothes._coordinateToggle])) Clothes._coordinateToggle = (Clothes._coordinateToggle + 1) % 7;
                }
                GUILayout.EndHorizontal();

                // Hair toggles
                GUILayout.BeginHorizontal();
                {
                    Hair._toggleHairstyle.Value = GUILayout.Toggle(Hair._toggleHairstyle.Value, "Hairstyle", GUILayout.Width(_quickToggleWidth));
                    Hair._toggleHaircolor.Value = GUILayout.Toggle(Hair._toggleHaircolor.Value, "Haircolor");
                }
                GUILayout.EndHorizontal();

                // Eye toggles
                GUILayout.BeginHorizontal();
                {
                    Face._toggleEyecolor.Value = GUILayout.Toggle(Face._toggleEyecolor.Value, "Eyeline", GUILayout.Width(_quickToggleWidth));
                    Face._toggleEyeline.Value = GUILayout.Toggle(Face._toggleEyeline.Value, "Eyecolor");
                }
                GUILayout.EndHorizontal();

                // Face toggles
                GUILayout.BeginHorizontal();
                {
                    Face._toggleEyebrow.Value = GUILayout.Toggle(Face._toggleEyebrow.Value, "Brow", GUILayout.Width(_quickToggleWidth));
                    Face._toggleFace.Value = GUILayout.Toggle(Face._toggleFace.Value, "Face");
                }
                GUILayout.EndHorizontal();

                // Body toggles
                GUILayout.BeginHorizontal();
                {
                    Body._toggleSkin.Value = GUILayout.Toggle(Body._toggleSkin.Value, "Skin", GUILayout.Width(_quickToggleWidth));
                    Body._toggleBody.Value = GUILayout.Toggle(Body._toggleBody.Value, "Body");
                }
                GUILayout.EndHorizontal();
            }
            GUI.enabled = true; // Reset active GUI

            // Clothes / Select All toggles
            GUILayout.BeginHorizontal();
            {
                Clothes._toggleClothes.Value = GUILayout.Toggle(Clothes._toggleClothes.Value, "Clothes", GUILayout.Width(_quickToggleWidth));

                GUI.enabled = KK_Archetypes._loadCharaToggle.isOn; // Disable GUI for select all when not in Character menu
                {
                    bool _allChanged = AllToggle.Value;
                    AllToggle.Value = GUILayout.Toggle(AllToggle.Value, "Select All");
                    if (_allChanged != AllToggle.Value) ToggleAll(AllToggle.Value);
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

        private static void ToggleAll(bool val)
        {
            Hair._toggleHairstyle.Value = val;
            Hair._toggleHaircolor.Value = val;
            Face._toggleEyecolor.Value = val;
            Face._toggleEyeline.Value = val;
            Face._toggleEyebrow.Value = val;
            Face._toggleFace.Value = val;
            Body._toggleSkin.Value = val;
            Body._toggleBody.Value = val;
            Clothes._toggleClothes.Value = val;
        }

        /// <summary>
        /// Method to draw advanced control menu.
        /// </summary>
        /// <param id>Unity window ID</param>
        public static void AdvancedControls(int id)
        {

            GUILayout.BeginVertical(GUILayout.Width(_xsizeAdv-8));
            {
                DrawScrollList();
                DrawTogglesAdv();
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Load Part")) KK_Archetypes.Data.LoadEntry(_currCategory, _selected);
                    if (GUILayout.Button("Delete")) KK_Archetypes.Data.DeleteEntry(_currCategory, _selected);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    _renameEntry = GUILayout.TextField(_renameEntry, GUILayout.Width((_xsizeAdv-8) * 0.6f));
                    if (GUILayout.Button("Rename"))
                    {
                        KK_Archetypes.Data.RenameEntry(_currCategory, _selected, _renameEntry);
                        _selected = _renameEntry + _selected.Substring(_selected.IndexOf("->"));
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Method to draw quick control menu.
        /// </summary>
        /// <param id>Unity window ID</param>
        public static void QuickControls(int id)
        {
            DrawTogglesQuick();
            if (GUILayout.Button("Add from Selected")) AddSelected(true);
            KK_Archetypes.IncrementFlag = GUILayout.Toggle(KK_Archetypes.IncrementFlag, "Jump to next");
        }
    }
}
