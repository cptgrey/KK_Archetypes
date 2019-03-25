using System;
using ExtensibleSaveFormat;
using KKABMX.Core;
using MessagePack;
using System.Collections.Generic;
using UnityEngine;
using Illusion.Game;
using KKAPI.Maker;
using BepInEx.Logging;
using Logger = BepInEx.Logger;

namespace KK_Archetypes
{
    static class Utilities
    {
        internal static readonly System.Random Rand = new System.Random();

        /// <summary>
        /// Method to finalize loading and play sound.
        /// </summary>
        internal static void FinalizeLoad()
        {
            if (!KK_Archetypes.AllFlag)
            {
                MakerAPI.GetCharacterControl().Reload();
                PlaySound();
            }
        }

        /// <summary>
        /// Gets a slightly darker color. Used to set Eyebrow colors from hair.baseColor.
        /// </summary>
        /// <param incolor>Color to darken</param>
        public static Color GetSlightlyDarkerColor(Color incolor)
        {
            Color.RGBToHSV(incolor, out float h, out float s, out float v);
            v = v > 0.08f ? v - 0.08f : 0;
            return Color.HSVToRGB(h, s, v);
        }

        /// <summary>
        /// Returns a ChaFileControl instance from the selected character in CharaMaker.
        /// </summary>
        public static ChaFileControl GetSelectedCharacter()
        {
            ChaCustom.CustomFileInfoComponent selected = Singleton<ChaCustom.CustomFileListCtrl>.Instance.GetSelectTopItem();
            ChaFileControl tmp = new ChaFileControl();
            tmp.LoadCharaFile(selected.info.FullPath, byte.MaxValue);
            return tmp;
        }

        /// <summary>
        /// Used to play a sound when loading / saving has finished.
        /// </summary>
        internal static void PlaySound()
        {
            Utils.Sound.Play(SystemSE.ok_l);
        }

        /// <summary>
        /// Used to increment the selected character in the maker. NOTE: This is quite buggy atm, need to have a look at this later. 
        /// </summary>
        public static void IncrementSelectIndex()
        {
            ChaCustom.CustomFileListCtrl customFileList = Singleton<ChaCustom.CustomFileListCtrl>.Instance;
            int nextindex = customFileList.GetSelectIndex()[0] + 1;
            if (KK_Archetypes.IncrementFlag.Value)
            {
                customFileList.ToggleAllOff();
                if (nextindex < customFileList.GetInclusiveCount()) customFileList.SelectItem(nextindex);
            }
        }

        /// <summary>
        /// Method to retrieve a list of BoneModifiers from a selected card in the maker, without loading a character. 
        /// </summary>
        public static List<BoneModifier> GetBoneModifiersFromCard()
        {
            ChaFile file = Utilities.GetSelectedCharacter();
            PluginData bonedata = ExtendedSave.GetExtendedDataById(file, "KKABMPlugin.ABMData");
            List<BoneModifier> modifiers = new List<BoneModifier>();
            if (bonedata != null)
            {
                try
                {
                    switch (bonedata.version)
                    {
                        // Only support for version 2
                        case 2:
                            modifiers = LZ4MessagePackSerializer.Deserialize<List<BoneModifier>>((byte[])bonedata.data["boneData"]);
                            break;

                        default:
                            throw new NotSupportedException($"Save version {bonedata.version} is not supported");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, "[KK_Archetypes] Failed to load KKABMX extended data - " + ex);
                }
            }
            return modifiers;
        }

    }
}
