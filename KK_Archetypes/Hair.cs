using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace KK_Archetypes
{
    class Hair
    {
        // Current class toggles for Maker menu.
        internal static MakerToggle _toggleHairstyle { get; set; }
        internal static MakerToggle _toggleHaircolor { get; set; }

        /// <summary>
        /// Method to check if any current class toggles are selected.
        /// </summary>
        internal static bool AnyToggles()
        {
            return _toggleHairstyle.Value || _toggleHaircolor.Value;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void HairStyleWriter(ChaFileHair from, ChaFileHair to)
        {
            for (int j = 0; j < 4; j++)
            {
                to.parts[j].id = from.parts[j].id;
                to.parts[j].length = from.parts[j].length;
            }
            to.kind = from.kind;
            to.glossId = from.glossId;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void HairColorWriter(ChaFileHair from, ChaFileHair to)
        {
            for (int j = 0; j < 4; j++)
            {
                to.parts[j].baseColor = from.parts[j].baseColor;
                to.parts[j].startColor = from.parts[j].startColor;
                to.parts[j].endColor = from.parts[j].endColor;
            }
            to.parts[0].acsColor = from.parts[0].acsColor;
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddHairStyle(ChaFileControl curr)
        {
            if (!_toggleHairstyle.Value) return;
            ChaFileHair add = new ChaFileHair();
            HairStyleWriter(curr.custom.hair, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.HairstyleDict.Add(key, add);
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddHairColor(ChaFileControl curr)
        {
            if (!_toggleHaircolor.Value) return;
            ChaFileHair add = new ChaFileHair();
            HairColorWriter(curr.custom.hair, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.HaircolorDict.Add(key, add);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadHairStyle(string key = null)
        {
            if (!_toggleHairstyle.Value) return;
            if (KK_Archetypes.Data.HairstyleDict.Count == 0) return;
            ChaFileHair add;
            if (key == null) add = Utilities.GetRandomValue(KK_Archetypes.Data.HairstyleDict);
            else add = KK_Archetypes.Data.HairstyleDict[key];
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            HairStyleWriter(add, curr);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadHairColor(string key = null)
        {
            if (!_toggleHaircolor.Value) return;
            if (KK_Archetypes.Data.HaircolorDict.Count == 0) return;
            ChaFileHair add;
            if (key == null) add = Utilities.GetRandomValue(KK_Archetypes.Data.HaircolorDict);
            else add = KK_Archetypes.Data.HaircolorDict[key];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair curr = file.custom.hair;
            HairColorWriter(add, curr);
            file.custom.face.eyebrowColor = Utilities.GetSlightlyDarkerColor(curr.parts[0].baseColor);
            file.custom.body.underhairColor = file.custom.face.eyebrowColor;
        }
    }
}
