using KKAPI.Maker;

namespace KK_Archetypes
{
    class Face
    {

        // For documentation, see KK_Archetypes.Hair, these methods are basically the same.

        protected static void EyebrowWriter(ChaFileFace from, ChaFileFace to)
        {
            to.eyebrowId = from.eyebrowId;
        }

        protected static void FaceWriter(ChaFileFace from, ChaFileFace to)
        {
            to.foregroundEyebrow = from.foregroundEyebrow;
            to.foregroundEyes = from.foregroundEyes;
            to.doubleTooth = from.doubleTooth;
            to.shapeValueFace = from.shapeValueFace;
        }


        internal static void AddEyebrow(ChaFileFace curr)
        {
            ChaFileFace add = new ChaFileFace();
            EyebrowWriter(curr, add);
            KK_Archetypes.Data.Eyebrow.Add(add);
        }

        internal static void AddArchetypeEyebrowFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddEyebrow(curr);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyebrow()
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyebrow(curr);
            Utilities.PlaySound();
        }

        internal static void AddFace(ChaFileFace curr)
        {
            ChaFileFace add = new ChaFileFace();
            FaceWriter(curr, add);
            KK_Archetypes.Data.Face.Add(add);
        }

        internal static void AddArchetypeFaceFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddFace(curr);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeFace()
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddFace(curr);
            Utilities.PlaySound();
        }

        internal static void LoadRandomArchetypeEyebrow()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Eyebrow.Count == 0) return;
            ChaFileFace add = KK_Archetypes.Data.Eyebrow[Utilities.Rand.Next(KK_Archetypes.Data.Eyebrow.Count)];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyebrowWriter(add, curr);
            Utilities.FinalizeLoad();
        }

        internal static void LoadRandomArchetypeFace()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Face.Count == 0) return;
            ChaFileFace add = KK_Archetypes.Data.Face[Utilities.Rand.Next(KK_Archetypes.Data.Face.Count)];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            FaceWriter(add, curr);
            Utilities.FinalizeLoad();
        }
    }
}
