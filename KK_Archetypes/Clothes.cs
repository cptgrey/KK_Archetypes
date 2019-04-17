using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace KK_Archetypes
{
    internal class Clothes
    {
        // Current class toggles for Maker menu.
        internal static MakerToggle _toggleClothes { get; set; }
        internal static int _coordinateToggle = 0;

        /// <summary>
        /// Method to add data to KKATData.
        /// </summary>
        /// <param curr>Current file to copy from</param>
        /// <param key>Specific key to add, used for adding keys by character cards</param>
        internal static void AddClothes(ChaFileCoordinate curr, string key = null)
        {
            if (!_toggleClothes.Value) return;
            key = key == null ? Utilities.CreateNewKey(curr) : key;
            KK_Archetypes.Data.ClothesDict.Add(key, curr.clothes);
            KK_Archetypes.Data.AccessoryDict.Add(key, curr.accessory);
        }

        /// <summary>
        /// Method to load data from KKATData.
        /// </summary>
        /// <param key>Key to load</param>
        internal static void LoadClothes(string key = null)
        {
            if (!_toggleClothes.Value) return;
            if (KK_Archetypes.Data.ClothesDict.Count == 0) return;
            ChaFileClothes addclothes;
            ChaFileAccessory addaccessory;
            if (key == null)
            {
                key = Utilities.GetRandomKey(KK_Archetypes.Data.ClothesDict);
            }
            addclothes = KK_Archetypes.Data.ClothesDict[key];
            addaccessory = KK_Archetypes.Data.AccessoryDict[key];
            MakerAPI.GetCharacterControl().nowCoordinate.clothes = addclothes;
            MakerAPI.GetCharacterControl().nowCoordinate.accessory = addaccessory;
        }
    }

    // NOTE: These methods are currently unused, but might be implemented in a later release.

    // Methods to generate semi random aestethically pleasing color palettes for coordinates.
    //internal static List<Color> _palette = GetRandomColors();
    //public static List<Color> GetRandomColors()
    //{
    //    List<Color> colors = new List<Color>();
    //    List<float> hues = new List<float> { 0, 0, 0, 0, 0 };
    //    List<float> sats = new List<float> { 0, 0, 0, 0, 0 };
    //    List<float> vals = new List<float> { .4f, .6f, .7f, .8f, .95f };

    //    hues[0] = (float)Utilities.Rand.NextDouble();
    //    sats[0] = (float)(Utilities.Rand.NextDouble() * 0.7 + 0.15);
    //    float angle = (float)Utilities.Rand.NextDouble() / 2;
    //    hues[1] = hues[0] + angle <= 1 ? hues[0] + angle : hues[0] + angle - 1;
    //    hues[3] = hues[0] - angle >= 0 ? hues[0] - angle : hues[0] - angle + 1;
    //    hues[2] = hues[1];
    //    hues[4] = hues[3];
    //    float satalt = sats[0] >= .5f ? sats[0] - .3f : sats[0] + .3f;
    //    sats[1] = sats[0];
    //    sats[3] = sats[0];
    //    sats[2] = satalt;
    //    sats[4] = satalt;
    //    Utilities.ShuffleList(vals);
    //    for (int i = 0; i < 5; i++) colors.Add(Color.HSVToRGB(hues[i], sats[i], vals[i]));
    //    return colors;
    //}
    //protected static void ApplyColors(ChaFileClothes clothes)
    //{
    //    _palette = GetRandomColors();
    //    Utilities.ShuffleList(_palette);
    //    for (int i=0; i<clothes.parts.Length; i++)
    //    {
    //        for (int j = 0; j < clothes.parts[i].colorInfo.Length; j++)
    //        {
    //            clothes.parts[i].colorInfo[j].baseColor = _palette[j];
    //            clothes.parts[i].colorInfo[j].patternColor = Utilities.GetSlightlyDarkerColor(_palette[j+1]);
    //        }
    //    }
    //}

}
