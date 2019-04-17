using System.Linq;
using System.Collections.Generic;
using KKABMX.Core;
using KKABMX.GUI;
using KKAPI.Maker;

namespace KK_Archetypes
{
    internal class Bones
    {
        // List of categories for face bones, used to separate bones in BoneModifier List
        protected static List<MakerCategory> makerFaceCategories = new List<MakerCategory>
            {
                InterfaceData.FaceAll,
                InterfaceData.FaceHead,
                InterfaceData.FaceEar,
                InterfaceData.FaceChin,
                InterfaceData.FaceCheek,
                InterfaceData.FaceEyebrow,
                InterfaceData.FaceEye01,
                InterfaceData.FaceEyes2,
                InterfaceData.FaceEyelashUp,
                InterfaceData.FaceEyelashDn,
                InterfaceData.FaceNose,
                InterfaceData.FaceMouth,
                InterfaceData.FaceMouth2,
            };

        // List of categories for body bones, used to separate bones in BoneModifier List
        protected static List<MakerCategory> makerBodyCategories = new List<MakerCategory>
            {
                InterfaceData.BodyAll,
                InterfaceData.BodyBreast,
                InterfaceData.BodyBreast2,
                InterfaceData.BodyNipples,
                InterfaceData.BodyUpper,
                InterfaceData.BodyUpper2,
                InterfaceData.BodyLeg,
                InterfaceData.BodyLower,
                InterfaceData.BodyLower2,
                InterfaceData.BodyArm,
                InterfaceData.BodyArm2,
                InterfaceData.BodyForearms,
                InterfaceData.BodyHands,
                InterfaceData.BodyThighs,
                InterfaceData.BodyFeet,
                InterfaceData.BodyBot,
                InterfaceData.BodySkirtScl,
                InterfaceData.BodyUnderhair,
                InterfaceData.BodyGenitals,
            };

        /// <summary>
        /// Method to retrieve face bones from BoneModifier list.
        /// </summary>
        /// <param from>List of BoneModifiers to extract face bones from</param>
        protected static List<BoneModifier> GetFaceBones(List<BoneModifier> from)
        {
            List<BoneModifier> to = new List<BoneModifier>();
            foreach (BoneMeta boneMeta in InterfaceData.BoneControls.Where(x => makerFaceCategories.Contains(x.Category)))
            {
                BoneModifier addbone = from.Find(x => x.BoneName == boneMeta.BoneName);
                if (addbone != null) to.Add(addbone);
                if (boneMeta.RightBoneName != "")
                {
                    addbone = from.Find(x => x.BoneName == boneMeta.RightBoneName);
                    if (addbone != null) to.Add(addbone);
                }
            }
            return to;
        }

        /// <summary>
        /// Method to retrieve body bones from BoneModifier list.
        /// </summary>
        /// <param from>List of BoneModifiers to extract body bones from</param>
        protected static List<BoneModifier> GetBodyBones(List<BoneModifier> from)
        {
            List<BoneModifier> to = new List<BoneModifier>();
            foreach (BoneMeta boneMeta in InterfaceData.BoneControls.Where(x => makerBodyCategories.Contains(x.Category)))
            {
                BoneModifier addbone = from.Find(x => x.BoneName == boneMeta.BoneName);
                if (addbone != null) to.Add(addbone);
                if (boneMeta.RightBoneName != "")
                {
                    addbone = from.Find(x => x.BoneName == boneMeta.RightBoneName);
                    if (addbone != null) to.Add(addbone);
                }
            }
            return to;
        }

        /// <summary>
        /// Method to add face bones to favorites list.
        /// </summary>
        /// <param curr>File to add bones from</param>
        /// <param fromSelected>Flag to select bones from CustomCharaList</param>
        /// <param key>Specifies key for KKATData entry</param>
        public static void AddFaceBones(ChaFileControl curr, bool fromSelected = false, string key = null)
        {
            List<BoneModifier> modifiers = fromSelected ?
                GetFaceBones(Utilities.GetBoneModifiersFromCard()) :
                GetFaceBones(MakerAPI.GetCharacterControl().GetComponent<BoneController>().Modifiers);
            key = key == null ? Utilities.CreateNewKey(curr) : key;
            KK_Archetypes.Data.FaceBonesDict.Add(key, modifiers);
        }

        /// <summary>
        /// Method to add body bones to favorites list.
        /// </summary>
        /// <param curr>File to add bones from</param>
        /// <param fromSelected>Flag to select bones from CustomCharaList</param>
        /// <param key>Specifies key for KKATData entry</param>
        public static void AddBodyBones(ChaFileControl curr, bool fromSelected = false, string key = null)
        {
            List<BoneModifier> modifiers = fromSelected ?
                GetBodyBones(Utilities.GetBoneModifiersFromCard()) :
                GetBodyBones(MakerAPI.GetCharacterControl().GetComponent<BoneController>().Modifiers);
            key = key == null ? Utilities.CreateNewKey(curr) : key;
            KK_Archetypes.Data.BodyBonesDict.Add(key, modifiers);
        }

        /// <summary>
        /// Method to load face bones from favorites list.
        /// </summary>
        /// <param key>Specifies key to load</param>
        public static void LoadFaceBones(string key = null)
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.FaceBonesDict.Count == 0) return;
            key = key == null ? Utilities.GetRandomKey(KK_Archetypes.Data.BodyBonesDict) : key;
            var controller = MakerAPI.GetCharacterControl().GetComponent<BoneController>();
            List<BoneModifier> addbones = KK_Archetypes.Data.FaceBonesDict[key];
            List<BoneModifier> bodybones = GetBodyBones(controller.Modifiers);
            while (controller.Modifiers.Count > 0)
            {
                controller.Modifiers[controller.Modifiers.Count - 1].Reset();
                controller.Modifiers.RemoveAt(controller.Modifiers.Count - 1);
            }
            for (int i = 0; i < addbones.Count; i++)
            {
                controller.AddModifier(addbones[i]);
            }
            for (int i = 0; i < bodybones.Count; i++)
            {
                controller.AddModifier(bodybones[i]);
            }
        }

        /// <summary>
        /// Method to load body bones from favorites list.
        /// </summary>
        /// <param key>Specifies key to load</param>
        public static void LoadBodyBones(string key = null)
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.BodyBonesDict.Count == 0) return;
            key = key == null ? Utilities.GetRandomKey(KK_Archetypes.Data.BodyBonesDict) : key;
            var controller = MakerAPI.GetCharacterControl().GetComponent<BoneController>();
            List<BoneModifier> addbones = KK_Archetypes.Data.BodyBonesDict[key];
            List<BoneModifier> facebones = GetFaceBones(controller.Modifiers);
            while (controller.Modifiers.Count > 0)
            {
                controller.Modifiers[controller.Modifiers.Count - 1].Reset();
                controller.Modifiers.RemoveAt(controller.Modifiers.Count - 1);
            }
            for (int i = 0; i < addbones.Count; i++)
            {
                controller.AddModifier(addbones[i]);
            }
            for (int i = 0; i < facebones.Count; i++)
            {
                controller.AddModifier(facebones[i]);
            }
        }
    }
}
