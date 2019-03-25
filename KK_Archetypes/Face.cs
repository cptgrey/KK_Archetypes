using KKAPI.Maker;

namespace KK_Archetypes
{
    class Face
    {

        protected static void FaceWriter(ChaFileFace from, ChaFileFace to)
        {
            to.foregroundEyebrow = from.foregroundEyebrow;
            to.foregroundEyes = from.foregroundEyes;
            to.doubleTooth = from.doubleTooth;
            to.shapeValueFace = from.shapeValueFace;
        }

        internal static void AddEyebrow(ChaFileFace curr, KKATData data)
        {
            ChaFileFace add = new ChaFileFace();
            add.eyebrowId = curr.eyebrowId;
            data.Eyebrow.Add(add);
        }

        internal static void AddArchetypeEyebrowFromSelected(KKATData data)
        {
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddEyebrow(curr, data);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyebrow(KKATData data)
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyebrow(curr, data);
            Utilities.PlaySound();
        }

        internal static void AddFace(ChaFileFace curr, KKATData data)
        {
            ChaFileFace add = new ChaFileFace();
            FaceWriter(curr, add);
            data.Face.Add(add);
        }

        internal static void AddArchetypeFaceFromSelected(KKATData data)
        {
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddFace(curr, data);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeFace(KKATData data)
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddFace(curr, data);
            Utilities.PlaySound();
        }

        internal static void GetRandomArchetypeEyebrow(KKATData data)
        {
            if (data.Eyebrow.Count == 0) return;
            ChaFileFace add = data.Eyebrow[Utilities.Rand.Next(data.Eyebrow.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileFace curr = file.custom.face;
            curr.eyebrowId = add.eyebrowId;
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }

        internal static void GetRandomArchetypeFace(KKATData data)
        {
            if (data.Face.Count == 0) return;
            ChaFileFace add = data.Face[Utilities.Rand.Next(data.Face.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileFace curr = file.custom.face;
            FaceWriter(add, curr);
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }
    }
}
