using System.Collections.Generic;
using KKABMX.Core;
using KKAPI.Maker;

namespace KK_Archetypes
{
    internal class Body
    {

        // For documentation, see KK_Archetypes.Hair, these methods are basically the same.

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

        protected static void BodyWriter(ChaFileBody from, ChaFileBody to)
        {
            to.skinId = from.skinId;
            to.detailId = from.detailId;
            to.detailPower = from.detailPower;
            to.bustSoftness = from.bustSoftness;
            to.bustWeight = from.bustWeight;
            to.areolaSize = from.areolaSize;
            to.shapeValueBody = from.shapeValueBody;
        }

        internal static void AddSkin(ChaFileBody currBody, ChaFileFace currFace)
        {
            ChaFileBody addBody = new ChaFileBody();
            ChaFileFace addFace = new ChaFileFace();
            SkinWriter(currBody, currFace, addBody, addFace);
            KK_Archetypes.Data.SkinBody.Add(addBody);
            KK_Archetypes.Data.SkinFace.Add(addFace);
        }

        internal static void AddArchetypeSkinFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileCustom custom = Utilities.GetSelectedCharacter().custom;
            ChaFileBody currBody = custom.body;
            ChaFileFace currFace = custom.face;
            AddSkin(currBody, currFace);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeSkin()
        {
            ChaFileCustom custom = MakerAPI.GetCharacterControl().chaFile.custom;
            ChaFileBody currBody = custom.body;
            ChaFileFace currFace = custom.face;
            AddSkin(currBody, currFace);
            Utilities.PlaySound();
        }

        internal static void AddBody(ChaFileBody curr, bool fromSelected = false)
        {
            ChaFileBody add = new ChaFileBody();
            List<BoneModifier> addbones = new List<BoneModifier>();
            List<BoneModifier> modifiers = fromSelected ? Utilities.GetBoneModifiersFromCard() : MakerAPI.GetCharacterControl().GetComponent<BoneController>().Modifiers;
            for (int i = 0; i < modifiers.Count; i++) addbones.Add(modifiers[i]);
            BodyWriter(curr, add);
            KK_Archetypes.Data.Body.Add(add);
            KK_Archetypes.Data.Bones.Add(addbones);
        }

        internal static void AddArchetypeBodyFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileBody curr = Utilities.GetSelectedCharacter().custom.body;
            AddBody(curr, true);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeBody()
        {
            ChaFileBody curr = MakerAPI.GetCharacterControl().chaFile.custom.body;
            AddBody(curr);
            Utilities.PlaySound();
        }

        internal static void LoadRandomArchetypeSkin()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.SkinBody.Count != KK_Archetypes.Data.SkinFace.Count || KK_Archetypes.Data.SkinFace.Count == 0) return;
            int randidx = Utilities.Rand.Next(KK_Archetypes.Data.SkinFace.Count);
            ChaFileBody addBody = KK_Archetypes.Data.SkinBody[randidx];
            ChaFileFace addFace = KK_Archetypes.Data.SkinFace[randidx];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileBody currBody = file.custom.body;
            ChaFileFace currFace = file.custom.face;
            SkinWriter(addBody, addFace, currBody, currFace);
            Utilities.FinalizeLoad();
        }

        internal static void LoadRandomArchetypeBody()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Body.Count == 0) return;
            int randidx = Utilities.Rand.Next(KK_Archetypes.Data.Body.Count);
            ChaFileBody add = KK_Archetypes.Data.Body[randidx];
            List<BoneModifier> addbones = KK_Archetypes.Data.Bones[randidx];
            ChaFileBody curr = MakerAPI.GetCharacterControl().chaFile.custom.body;
            var controller = MakerAPI.GetCharacterControl().GetComponent<BoneController>();
            while (controller.Modifiers.Count > 0)
            {
                controller.Modifiers[controller.Modifiers.Count - 1].Reset();
                controller.Modifiers.RemoveAt(controller.Modifiers.Count - 1);
            }
            for (int i = 0; i < addbones.Count; i++)
            {
                controller.AddModifier(addbones[i]);
            }
            BodyWriter(add, curr);
            Utilities.FinalizeLoad();
        }
    }
}
