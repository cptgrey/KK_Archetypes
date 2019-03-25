using KKAPI.Maker;

namespace KK_Archetypes
{
    class Hair
    {
        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void HairStyleWriter(ChaFileHair from, ChaFileHair to)
        {
            for (int j = 0; j < 4; j++)
            {
                to.parts[j].id = from.parts[j].id;
                to.parts[j].length = from.parts[j].length;
            }
            to.kind = from.kind;
            to.glossId = from.glossId;
        }

        /// <summary>
        /// Method to copy data to/from characters.
        /// </summary>
        /// <param from>File to copy from</param>
        /// <param to>File to copy to</param>
        protected static void HairColorWriter(ChaFileHair from, ChaFileHair to)
        {
            for (int j = 0; j < 4; j++)
            {
                to.parts[j].baseColor = from.parts[j].baseColor;
                to.parts[j].startColor = from.parts[j].startColor;
                to.parts[j].endColor = from.parts[j].endColor;
            }
            to.parts[0].acsColor = from.parts[0].acsColor;
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddHairStyle(ChaFileHair curr)
        {
            ChaFileHair add = new ChaFileHair();
            HairStyleWriter(curr, add);
            KK_Archetypes.Data.Hairstyle.Add(add);
        }

        /// <summary>
        /// Method to add data directly from selected card in CharaMaker.
        /// </summary>
        internal static void AddArchetypeHairStyleFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileHair curr = Utilities.GetSelectedCharacter().custom.hair;
            AddHairStyle(curr);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        /// <summary>
        /// Method to add data from current character.
        /// </summary>
        internal static void AddArchetypeHairStyle()
        {
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            AddHairStyle(curr);
            Utilities.PlaySound();
        }

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        internal static void AddHairColor(ChaFileHair curr)
        {
            ChaFileHair add = new ChaFileHair();
            HairColorWriter(curr, add);
            KK_Archetypes.Data.Haircolor.Add(add);
        }

        /// <summary>
        /// Method to add data directly from selected card in CharaMaker.
        /// </summary>
        internal static void AddArchetypeHairColorFromSelected()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            ChaFileHair curr = Utilities.GetSelectedCharacter().custom.hair;
            AddHairColor(curr);
            Utilities.IncrementSelectIndex();
            Utilities.PlaySound();
        }

        /// <summary>
        /// Method to add data from current character.
        /// </summary>
        internal static void AddArchetypeHairColor()
        {
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            AddHairColor(curr);
            Utilities.PlaySound();
        }

        /// <summary>
        /// Method to load hairstyle data from KKATData.
        /// </summary>
        internal static void LoadRandomArchetypeHairStyle()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Hairstyle.Count == 0) return;
            ChaFileHair add = KK_Archetypes.Data.Hairstyle[Utilities.Rand.Next(KK_Archetypes.Data.Hairstyle.Count)];
            ChaFileHair curr = MakerAPI.GetCharacterControl().chaFile.custom.hair;
            HairStyleWriter(add, curr);
            Utilities.FinalizeLoad();
        }

        /// <summary>
        /// Method to load hair color data from KKATData.
        /// </summary>
        internal static void LoadRandomArchetypeHairColor()
        {
            if (!MakerAPI.InsideAndLoaded) return;
            if (KK_Archetypes.Data.Haircolor.Count == 0) return;
            ChaFileHair add = KK_Archetypes.Data.Haircolor[Utilities.Rand.Next(KK_Archetypes.Data.Haircolor.Count)];
            ChaFile file = MakerAPI.GetCharacterControl().chaFile;
            ChaFileHair curr = file.custom.hair;
            HairColorWriter(add, curr);
            file.custom.face.eyebrowColor = Utilities.GetSlightlyDarkerColor(curr.parts[0].baseColor);
            file.custom.body.underhairColor = file.custom.face.eyebrowColor;
            Utilities.FinalizeLoad();
        }
    }
}
