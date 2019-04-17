using System;
using System.Linq;
using System.Collections.Generic;
using Harmony;
using ExtensibleSaveFormat;
using KKABMX.Core;
using MessagePack;
using UnityEngine;
using Illusion.Game;
using KKAPI.Maker;
using BepInEx.Logging;
using Logger = BepInEx.Logger;

namespace KK_Archetypes
{
    static class Utilities
    {
        // RNG
        internal static readonly System.Random Rand = new System.Random();

        /// <summary>
        /// Method to finalize loading and play sound.
        /// </summary>
        internal static void FinalizeLoad()
        {
            MakerAPI.GetCharacterControl().Reload();
            PlaySound();
        }

        /// <summary>
        /// Method to shuffle a generic list.
        /// NOTE: Currently unused. Could be used in later implementations.
        /// </summary>
        /// <param list>List to shuffle</param>
        public static void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Utilities.Rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Method to get a random key from a string dictionary.
        /// </summary>
        internal static string GetRandomKey<T>(Dictionary<string, T> dict)
        {
            return dict.Keys.ElementAt(Rand.Next(dict.Count));
        }

        /// <summary>
        /// Method to get a random value from a string dictionary.
        /// </summary>
        internal static T GetRandomValue<T>(Dictionary<string, T> dict)
        {
            return dict[GetRandomKey<T>(dict)];
        }

        /// <summary>
        /// Method to create a new key for a data dict.
        /// </summary>
        /// <param curr>ChaFileControl to generate key from</param>
        internal static string CreateNewKey(ChaFileControl curr)
        {
            return String.Format("{1}{2}->{0}", DateTime.Now.ToLocalTime().ToString("yyMMddhhmmssfff"), curr.parameter.lastname, curr.parameter.firstname);
        }

        /// <summary>
        /// Method to create a new key for a data dict.
        /// </summary>
        /// <param curr>ChaClothesControl to generate key from</param>
        internal static string CreateNewKey(ChaFileCoordinate curr)
        {
            return String.Format("{1}{2}->{0}", DateTime.Now.ToLocalTime().ToString("yyMMddhhmmssfff"), curr.coordinateName, curr.coordinateFileName);
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
            if (selected != null)
            {
                ChaFileControl tmp = new ChaFileControl();
                tmp.LoadCharaFile(selected.info.FullPath, byte.MaxValue);
                return tmp;
            }
            else return null;
        }

        /// <summary>
        /// Returns a ChaFileCoordinate instance from the selected coordinate in CharaMaker.
        /// </summary>
        public static ChaFileCoordinate GetSelectedCoordinate()
        {
            ChaCustom.CustomCoordinateFile clothesFile = Singleton<ChaCustom.CustomCoordinateFile>.Instance;
            ChaCustom.CustomFileListCtrl listCtrl = Traverse.Create(clothesFile).Field("listCtrl").GetValue<ChaCustom.CustomFileListCtrl>();
            ChaCustom.CustomFileInfoComponent selected = listCtrl.GetSelectTopItem();
            if (selected != null)
            {
                string fullPath = selected.info.FullPath;
                ChaFileCoordinate tmp = new ChaFileCoordinate();
                tmp.LoadFile(fullPath);
                return tmp;
            }
            else return null;
        }

        /// <summary>
        /// Returns int index of currently selected coordinate from the character in CharaMaker.
        /// </summary>
        public static int GetCurrentlyAssignedCoordinateType()
        {
            ChaFileStatus chaFileStatus = MakerAPI.GetCharacterControl().fileStatus;
            return chaFileStatus.coordinateType;
        }

        /// <summary>
        /// Used to play a sound when loading / saving has finished.
        /// </summary>
        internal static void PlaySound()
        {
            Utils.Sound.Play(SystemSE.ok_l);
        }

        /// <summary>
        /// Used to increment the selected character / coordinate in the maker.
        /// </summary>
        public static void IncrementSelectIndex()
        {
            if (KK_Archetypes.IncrementFlag)
            {
                // Increment character select index
                if (KK_Archetypes._loadCharaToggle.isOn && (Body.AnyToggles() || Face.AnyToggles() || Hair.AnyToggles()))
                {
                    ChaCustom.CustomFileListCtrl listControl = Singleton<ChaCustom.CustomFileListCtrl>.Instance;
                    List<ChaCustom.CustomFileInfo> fileInfos = Traverse.Create(listControl).Field("lstFileInfo").GetValue<List<ChaCustom.CustomFileInfo>>();
                    if (fileInfos != null)
                    {
                        int nextindex = fileInfos.IndexOf(fileInfos.Find(x => x.index == listControl.GetSelectIndex()[0])) + 1;
                        listControl.ToggleAllOff();
                        if (nextindex < listControl.GetInclusiveCount()) listControl.SelectItem(fileInfos[nextindex].index);
                    }
                }

                // Increment coordinate select index
                if (KK_Archetypes._loadCosToggle.isOn && Clothes._toggleClothes.Value)
                {
                    ChaCustom.CustomCoordinateFile clothesFile = Singleton<ChaCustom.CustomCoordinateFile>.Instance;
                    ChaCustom.CustomFileListCtrl listControl = Traverse.Create(clothesFile).Field("listCtrl").GetValue<ChaCustom.CustomFileListCtrl>();
                    List<ChaCustom.CustomFileInfo> fileInfos = Traverse.Create(listControl).Field("lstFileInfo").GetValue<List<ChaCustom.CustomFileInfo>>();
                    if (fileInfos != null)
                    {
                        int nextindex = fileInfos.IndexOf(fileInfos.Find(x => x.index == listControl.GetSelectIndex()[0])) + 1;
                        listControl.ToggleAllOff();
                        if (nextindex < listControl.GetInclusiveCount()) listControl.SelectItem(fileInfos[nextindex].index);
                    }
                }
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
