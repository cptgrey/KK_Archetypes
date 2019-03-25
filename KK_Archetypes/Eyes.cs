using KKAPI.Maker;

namespace KK_Archetypes
{
    class Eyes
    {
        // For documentation, see KK_Archetypes.Hair, these methods are basically the same.

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

        protected static void EyelineWriter(ChaFileFace from, ChaFileFace to)
        {
            to.eyelineUpId = from.eyelineUpId;
            to.eyelineDownId = from.eyelineDownId;
            to.eyelineUpWeight = from.eyelineUpWeight;
            to.eyelineColor = from.eyelineColor;
        }

        internal static void AddEyeColor(ChaFileFace curr)
        {
            ChaFileFace add = new ChaFileFace();
            EyeColorWriter(curr, add);
            KK_Archetypes.Data.Eyecolor.Add(add);
        }

        internal static void AddArchetypeEyeColorFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddEyeColor(curr);
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyeColor()
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeColor(curr);
            Utilities.PlaySound();
        }

        internal static void AddEyeline(ChaFileFace curr)
        {
            ChaFileFace add = new ChaFileFace();
            EyelineWriter(curr, add);
            KK_Archetypes.Data.Eyeline.Add(add);
        }

        internal static void AddArchetypeEyeline()
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeline(curr);
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyelineFromSelected()
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeline(curr);
            Utilities.PlaySound();
        }

        internal static void LoadRandomArchetypeEyeColor()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Eyecolor.Count == 0) return;
            ChaFileFace add = KK_Archetypes.Data.Eyecolor[Utilities.Rand.Next(KK_Archetypes.Data.Eyecolor.Count)];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyeColorWriter(add, curr);
            Utilities.FinalizeLoad();
        }

        internal static void LoadRandomArchetypeEyeline()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Eyeline.Count == 0) return;
            ChaFileFace add = KK_Archetypes.Data.Eyeline[Utilities.Rand.Next(KK_Archetypes.Data.Eyeline.Count)];
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            EyelineWriter(add, curr);
            Utilities.FinalizeLoad();
        }

    }
}
