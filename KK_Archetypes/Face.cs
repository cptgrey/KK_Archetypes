using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace KK_Archetypes
{
    class Face
    {
        // Current class toggles for Maker menu.
        internal static MakerToggle _toggleEyecolor { get; set; }
        internal static MakerToggle _toggleEyeline { get; set; }
        internal static MakerToggle _toggleEyebrow { get; set; }
        internal static MakerToggle _toggleFace { get; set; }

        /// <summary>
        /// Method to check if any current class toggles are selected.
        /// </summary>
        internal static bool AnyToggles()
        {
            return _toggleEyecolor.Value || _toggleEyeline.Value || _toggleEyebrow.Value || _toggleFace.Value;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void EyeColorWriter(ChaFileFace from, ChaFileFace to)
        {
            for (int j = 0; j < 2; j++)
            {
                to.pupil[j].id = from.pupil[j].id;
                to.pupil[j].baseColor = from.pupil[j].baseColor;
                to.pupil[j].subColor = from.pupil[j].subColor;
                to.pupil[j].gradMaskId = from.pupil[j].gradMaskId;
                to.pupil[j].gradBlend = from.pupil[j].gradBlend;
                to.pupil[j].gradOffsetY = from.pupil[j].gradOffsetY;
                to.pupil[j].gradScale = from.pupil[j].gradScale;
            }
            to.pupilX = from.pupilX;
            to.pupilY = from.pupilY;
            to.pupilHeight = from.pupilHeight;
            to.pupilWidth = from.pupilWidth;
            to.hlUpId = from.hlUpId;
            to.hlUpColor = from.hlUpColor;
            to.hlDownId = from.hlDownId;
            to.whiteId = from.whiteId;
            to.whiteBaseColor = from.whiteBaseColor;
            to.whiteSubColor = from.whiteSubColor;
            to.hlDownColor = from.hlDownColor;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void EyelineWriter(ChaFileFace from, ChaFileFace to)
        {
            to.eyelineUpId = from.eyelineUpId;
            to.eyelineDownId = from.eyelineDownId;
            to.eyelineUpWeight = from.eyelineUpWeight;
            to.eyelineColor = from.eyelineColor;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void EyebrowWriter(ChaFileFace from, ChaFileFace to)
        {
            to.eyebrowId = from.eyebrowId;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void FaceWriter(ChaFileFace from, ChaFileFace to)
        {
            to.foregroundEyebrow = from.foregroundEyebrow;
            to.foregroundEyes = from.foregroundEyes;
            to.doubleTooth = from.doubleTooth;
            for (int i = 0; i < from.shapeValueFace.Length; i++) to.shapeValueFace[i] = from.shapeValueFace[i];
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddEyeColor(ChaFileControl curr)
        {
            if (!_toggleEyecolor.Value) return;
            ChaFileFace add = new ChaFileFace();
            EyeColorWriter(curr.custom.face, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.EyecolorDict.Add(key, add);
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddEyeline(ChaFileControl curr)
        {
            if (!_toggleEyeline.Value) return;
            ChaFileFace add = new ChaFileFace();
            EyelineWriter(curr.custom.face, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.EyelineDict.Add(key, add);
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddEyebrow(ChaFileControl curr)
        {
            if (!_toggleEyebrow.Value) return;
            ChaFileFace add = new ChaFileFace();
            EyebrowWriter(curr.custom.face, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.EyebrowDict.Add(key, add);
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        /// <param fromSelected>Flag to retrieve data from CustomCharaList</param>
        internal static void AddFace(ChaFileControl curr, bool fromSelected = false)
        {
            if (!_toggleFace.Value) return;
            ChaFileFace add = new ChaFileFace();
            FaceWriter(curr.custom.face, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.FaceDict.Add(key, add);
            Bones.AddFaceBones(curr, fromSelected, key);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadEyeColor(string key = null)
        {
            if (!_toggleEyecolor.Value) return;
            if (KK_Archetypes.Data.EyecolorDict.Count == 0) return;
            ChaFileFace add;
            if (key == null) add = Utilities.GetRandomValue(KK_Archetypes.Data.EyecolorDict);
            else add = KK_Archetypes.Data.EyecolorDict[key];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyeColorWriter(add, curr);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadEyeline(string key = null)
        {
            if (!_toggleEyeline.Value) return;
            if (KK_Archetypes.Data.EyelineDict.Count == 0) return;
            ChaFileFace add;
            if (key == null) add = Utilities.GetRandomValue(KK_Archetypes.Data.EyelineDict);
            else add = KK_Archetypes.Data.EyelineDict[key];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyelineWriter(add, curr);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadEyebrow(string key = null)
        {
            if (!_toggleEyebrow.Value) return;
            if (KK_Archetypes.Data.EyebrowDict.Count == 0) return;
            ChaFileFace add;
            if (key == null) add = Utilities.GetRandomValue(KK_Archetypes.Data.EyebrowDict);
            else add = KK_Archetypes.Data.EyebrowDict[key];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyebrowWriter(add, curr);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadFace(string key = null)
        {
            if (!_toggleFace.Value) return;
            if (KK_Archetypes.Data.FaceDict.Count == 0) return;
            key = key == null ? Utilities.GetRandomKey(KK_Archetypes.Data.FaceDict) : key;
            ChaFileFace add = KK_Archetypes.Data.FaceDict[key];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            FaceWriter(add, curr);
            Bones.LoadFaceBones(key);
        }
    }
}
