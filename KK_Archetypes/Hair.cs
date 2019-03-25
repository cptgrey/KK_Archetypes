using KKAPI.Maker;

namespace KK_Archetypes
{
    class Hair
    {
        internal static void AddHairStyle(ChaFileHair curr, KKATData data)
        {
            ChaFileHair add = new ChaFileHair();
            for (int j = 0; j < 4; j++)
            {
                add.parts[j].id = curr.parts[j].id;
                add.parts[j].length = curr.parts[j].length;
            }
            add.kind = curr.kind;
            add.glossId = curr.glossId;
            data.Hairstyle.Add(add);
        }

        internal static void AddArchetypeHairStyleFromSelected(KKATData data)
        {
            ChaFileHair curr = Utilities.GetSelectedCharacter().custom.hair;
            AddHairStyle(curr, data);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeHairStyle(KKATData data)
        {
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            AddHairStyle(curr, data);
            Utilities.PlaySound();
        }

        internal static void AddHairColor(ChaFileHair curr, KKATData data)
        {
            ChaFileHair add = new ChaFileHair();
            for (int j = 0; j < 4; j++)
            {
                add.parts[j].baseColor = curr.parts[j].baseColor;
                add.parts[j].startColor = curr.parts[j].startColor;
                add.parts[j].endColor = curr.parts[j].endColor;
            }
            add.parts[0].acsColor = curr.parts[0].acsColor;
            data.Haircolor.Add(add);
        }

        internal static void AddArchetypeHairColorFromSelected(KKATData data)
        {
            ChaFileHair curr = Utilities.GetSelectedCharacter().custom.hair;
            AddHairColor(curr, data);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        internal static void AddArchetypeHairColor(KKATData data)
        {
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            AddHairColor(curr, data);
            Utilities.PlaySound();
        }

        internal static void GetRandomArchetypeHairStyle(KKATData data)
        {
            if (data.Hairstyle.Count == 0) return;
            ChaFileHair add = data.Hairstyle[Utilities.Rand.Next(data.Hairstyle.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair curr = file.custom.hair;
            for (int j = 0; j < 4; j++)
            {
                curr.parts[j].id = add.parts[j].id;
                curr.parts[j].length = add.parts[j].length;
            }
            curr.kind = add.kind;
            curr.glossId = add.glossId;
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }

        internal static void GetRandomArchetypeHairColor(KKATData data)
        {
            if (data.Haircolor.Count == 0) return;
            ChaFileHair add = data.Haircolor[Utilities.Rand.Next(data.Haircolor.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair curr = file.custom.hair;
            for (int j = 0; j < 4; j++)
            {
                curr.parts[j].baseColor = add.parts[j].baseColor;
                curr.parts[j].startColor = add.parts[j].startColor;
                curr.parts[j].endColor = add.parts[j].endColor;
            }
            curr.parts[0].acsColor = add.parts[0].acsColor;
            file.custom.face.eyebrowColor = Utilities.GetSlightlyDarkerColor(curr.parts[0].baseColor);
            file.custom.body.underhairColor = file.custom.face.eyebrowColor;
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                Utilities.PlaySound();
            }
        }
    }
}
