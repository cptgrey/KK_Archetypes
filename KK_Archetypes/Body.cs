using System.Collections.Generic;
using KKABMX.Core;
using KKAPI.Maker;

namespace KK_Archetypes
{
    internal class Body
    {

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

        internal static void AddSkin(ChaFileBody currBody, ChaFileFace currFace, KKATData data)
        {
            ChaFileBody addBody = new ChaFileBody();
            ChaFileFace addFace = new ChaFileFace();
            SkinWriter(currBody, currFace, addBody, addFace);
            data.SkinBody.Add(addBody);
            data.SkinFace.Add(addFace);
        }

        internal static void AddArchetypeSkinFromSelected(KKATData data)
        {
            ChaFileCustom custom = Utilities.GetSelectedCharacter().custom;
            ChaFileBody currBody = custom.body;
            ChaFileFace currFace = custom.face;
            AddSkin(currBody, currFace, data);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeSkin(KKATData data)
        {
            ChaFileCustom custom = MakerAPI.GetCharacterControl().chaFile.custom;
            ChaFileBody currBody = custom.body;
            ChaFileFace currFace = custom.face;
            AddSkin(currBody, currFace, data);
            Utilities.PlaySound();
        }

        internal static void AddBody(ChaFileBody curr, KKATData data, bool fromSelected = false)
        {
            ChaFileBody add = new ChaFileBody();
            List<BoneModifier> addbones = new List<BoneModifier>();
            List<BoneModifier> modifiers = fromSelected ? Utilities.GetBoneModifiersFromCard() : MakerAPI.GetCharacterControl().GetComponent<BoneController>().Modifiers;
            for (int i = 0; i < modifiers.Count; i++) addbones.Add(modifiers[i]);
            BodyWriter(curr, add);
            data.Body.Add(add);
            data.Bones.Add(addbones);
        }

        internal static void AddArchetypeBodyFromSelected(KKATData data)
        {
            ChaFileBody curr = Utilities.GetSelectedCharacter().custom.body;
            AddBody(curr, data, true);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeBody(KKATData data)
        {
            ChaFileBody curr = MakerAPI.GetCharacterControl().chaFile.custom.body;
            AddBody(curr, data);
            Utilities.PlaySound();
        }

        internal static void GetRandomArchetypeSkin(KKATData data)
        {
            if (data.SkinBody.Count != data.SkinFace.Count || data.SkinFace.Count == 0) return;
            int randidx = Utilities.Rand.Next(data.SkinFace.Count);
            ChaFileBody addBody = data.SkinBody[randidx];
            ChaFileFace addFace = data.SkinFace[randidx];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileBody currBody = file.custom.body;
            ChaFileFace currFace = file.custom.face;
            SkinWriter(addBody, addFace, currBody, currFace);
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }

        internal static void GetRandomArchetypeBody(KKATData data)
        {
            if (data.Body.Count == 0) return;
            int randidx = Utilities.Rand.Next(data.Body.Count);
            ChaFileBody add = data.Body[randidx];
            List<BoneModifier> addbones = data.Bones[randidx];
            ChaControl chaControl = MakerAPI.GetCharacterControl();
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
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
            ChaFileBody curr = file.custom.body;
            BodyWriter(add, curr);
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }
    }
}
