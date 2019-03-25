using KKAPI.Maker;
using BepInEx.Logging;
using Logger = BepInEx.Logger;

namespace KK_Archetypes
{
    class Eyes
    {
        internal static void AddEyeColor(ChaFileFace curr, KKATData data)
        {
            ChaFileFace add = new ChaFileFace();
            for (int j = 0; j < 2; j++)
            {
                add.pupil[j].id = curr.pupil[j].id;
                add.pupil[j].baseColor = curr.pupil[j].baseColor;
                add.pupil[j].subColor = curr.pupil[j].subColor;
                add.pupil[j].gradMaskId = curr.pupil[j].gradMaskId;
                add.pupil[j].gradBlend = curr.pupil[j].gradBlend;
                add.pupil[j].gradOffsetY = curr.pupil[j].gradOffsetY;
                add.pupil[j].gradScale = curr.pupil[j].gradScale;
            }
            add.pupilX = curr.pupilX;
            add.pupilY = curr.pupilY;
            add.pupilHeight = curr.pupilHeight;
            add.pupilWidth = curr.pupilWidth;
            add.hlUpId = curr.hlUpId;
            add.hlUpColor = curr.hlUpColor;
            add.hlDownId = curr.hlDownId;
            add.whiteId = curr.whiteId;
            add.whiteBaseColor = curr.whiteBaseColor;
            add.whiteSubColor = curr.whiteSubColor;
            add.hlDownColor = curr.hlDownColor;
            data.Eyecolor.Add(add);
        }

        internal static void AddArchetypeEyeColorFromSelected(KKATData data)
        {
            ChaFileFace curr = Utilities.GetSelectedCharacter().custom.face;
            AddEyeColor(curr, data);
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyeColor(KKATData data)
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeColor(curr, data);
            Utilities.PlaySound();
        }

        internal static void AddEyeline(ChaFileFace curr, KKATData data)
        {
            ChaFileFace add = new ChaFileFace();
            add.eyelineColor = curr.eyelineColor;
            if (add.eyelineColor != curr.eyelineColor) Logger.Log(LogLevel.Error, "[KK_Archetypes] Error in copying eyeline color");
            add.eyelineUpId = curr.eyelineUpId;
            add.eyelineDownId = curr.eyelineDownId;
            add.eyelineUpWeight = curr.eyelineUpWeight;
            data.Eyeline.Add(add);
        }

        internal static void AddArchetypeEyeline(KKATData data)
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeline(curr, data);
            Utilities.PlaySound();
        }

        internal static void AddArchetypeEyelineFromSelected(KKATData data)
        {
            ChaFileFace curr = MakerAPI.GetCharacterControl().chaFile.custom.face;
            AddEyeline(curr, data);
            Utilities.PlaySound();
        }

        internal static void GetRandomArchetypeEyeColor(KKATData data)
        {
            if (data.Eyecolor.Count == 0) return;
            ChaFileFace add = data.Eyecolor[Utilities.Rand.Next(data.Eyecolor.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileFace curr = file.custom.face;
            for (int j = 0; j < 2; j++)
            {
                curr.pupil[j].id = add.pupil[j].id;
                curr.pupil[j].baseColor = add.pupil[j].baseColor;
                curr.pupil[j].subColor = add.pupil[j].subColor;
                curr.pupil[j].gradMaskId = add.pupil[j].gradMaskId;
                curr.pupil[j].gradBlend = add.pupil[j].gradBlend;
                curr.pupil[j].gradOffsetY = add.pupil[j].gradOffsetY;
                curr.pupil[j].gradScale = add.pupil[j].gradScale;
            }
            curr.hlUpId = add.hlUpId;
            curr.hlUpColor = add.hlUpColor;
            curr.hlDownId = add.hlDownId;
            curr.whiteId = add.whiteId;
            curr.whiteBaseColor = add.whiteBaseColor;
            curr.whiteSubColor = add.whiteSubColor;
            curr.hlDownColor = add.hlDownColor;
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }

        internal static void GetRandomArchetypeEyeline(KKATData data)
        {
            if (data.Eyeline.Count == 0) return;
            ChaFileFace add = data.Eyeline[Utilities.Rand.Next(data.Eyeline.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileFace curr = file.custom.face;
            curr.eyelineUpId = add.eyelineUpId;
            curr.eyelineDownId = add.eyelineDownId;
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }

    }
}
