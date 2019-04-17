using System.Collections.Generic;
using KKABMX.Core;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace KK_Archetypes
{
    internal class Body
    {
        // Current class toggles for Maker menu.
        internal static MakerToggle _toggleSkin { get; set; }
        internal static MakerToggle _toggleBody { get; set; }

        /// <summary>
        /// Method to check if any current class toggles are selected.
        /// </summary>
        internal static bool AnyToggles()
        {
            return _toggleSkin.Value || _toggleBody.Value;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void SkinWriter(ChaFileBody fromBody, ChaFileFace fromFace, ChaFileBody toBody, ChaFileFace toFace)
        {
            toBody.skinMainColor = fromBody.skinMainColor;
            toBody.skinSubColor = fromBody.skinSubColor;
            toBody.sunburnId = fromBody.sunburnId;
            toBody.sunburnColor = fromBody.sunburnColor;
            toBody.skinGlossPower = fromBody.skinGlossPower;
            toBody.nipColor = fromBody.nipColor;
            toBody.nailColor = fromBody.nailColor;
            toBody.nipId = fromBody.nipId;
            toBody.paintColor = fromBody.paintColor;
            toBody.paintId = fromBody.paintId;
            toBody.paintLayout = fromBody.paintLayout;
            toBody.paintLayoutId = fromBody.paintLayoutId;

            toFace.baseMakeup = fromFace.baseMakeup;
            toFace.cheekGlossPower = fromFace.cheekGlossPower;
            toFace.detailId = fromFace.detailId;
            toFace.detailPower = fromFace.detailPower;
            toFace.headId = fromFace.headId;
            toFace.lipGlossPower = fromFace.lipGlossPower;
            toFace.lipLineColor = fromFace.lipLineColor;
            toFace.lipLineId = fromFace.lipLineId;
            toFace.moleColor = fromFace.moleColor;
            toFace.moleId = fromFace.moleId;
            toFace.moleLayout = fromFace.moleLayout;
            toFace.noseId = fromFace.noseId;
            toFace.skinId = fromFace.skinId;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void BodyWriter(ChaFileBody from, ChaFileBody to)
        {
            to.skinId = from.skinId;
            to.detailId = from.detailId;
            to.detailPower = from.detailPower;
            to.bustSoftness = from.bustSoftness;
            to.bustWeight = from.bustWeight;
            to.areolaSize = from.areolaSize;
            for (int i=0;i<from.shapeValueBody.Length;i++) to.shapeValueBody[i] = from.shapeValueBody[i];
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddSkin(ChaFileControl curr)
        {
            if (!_toggleSkin.Value) return;
            ChaFileBody addBody = new ChaFileBody();
            ChaFileFace addFace = new ChaFileFace();
            SkinWriter(curr.custom.body, curr.custom.face, addBody, addFace);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.SkinBodyDict.Add(key, addBody);
            KK_Archetypes.Data.SkinFaceDict.Add(key, addFace);
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        /// <param fromSelected>Flag to retrieve data from CustomCharaList</param>
        internal static void AddBody(ChaFileControl curr, bool fromSelected = false)
        {
            if (!_toggleBody.Value) return;
            ChaFileBody add = new ChaFileBody();
            List<BoneModifier> addbones = new List<BoneModifier>();
            BodyWriter(curr.custom.body, add);
            string key = Utilities.CreateNewKey(curr);
            KK_Archetypes.Data.BodyDict.Add(key, add);
            Bones.AddBodyBones(curr, fromSelected, key);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadSkin(string key = null)
        {
            if (!_toggleSkin.Value) return;
            if (KK_Archetypes.Data.SkinBodyDict.Count != KK_Archetypes.Data.SkinFaceDict.Count || KK_Archetypes.Data.SkinFaceDict.Count == 0) return;
            key = key == null ? Utilities.GetRandomKey(KK_Archetypes.Data.SkinBodyDict) : key;
            ChaFileBody addBody = KK_Archetypes.Data.SkinBodyDict[key];
            ChaFileFace addFace = KK_Archetypes.Data.SkinFaceDict[key];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileBody currBody = file.custom.body;
            ChaFileFace currFace = file.custom.face;
            SkinWriter(addBody, addFace, currBody, currFace);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadBody(string key = null)
        {
            if (!_toggleBody.Value) return;
            if (KK_Archetypes.Data.BodyDict.Count == 0) return;
            key = key == null ? Utilities.GetRandomKey(KK_Archetypes.Data.BodyDict) : key;
            ChaFileBody add = KK_Archetypes.Data.BodyDict[key];
            ChaFileBody curr = MakerAPI.GetCharacterControl().chaFile.custom.body;
            BodyWriter(add, curr);
            Bones.LoadBodyBones(key);
        }
    }
}
