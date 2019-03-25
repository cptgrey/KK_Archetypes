using System;
using ExtensibleSaveFormat;
using KKABMX.Core;
using MessagePack;
using System.Collections.Generic;
using UnityEngine;
using BepInEx.Logging;
using BepInEx;
using Illusion.Game;
using Logger = BepInEx.Logger;

namespace KK_Archetypes
{
    static class Utilities
    {
        internal static readonly System.Random Rand = new System.Random();

        public static Color GetSlightlyDarkerColor(Color incolor)
        // Used to set Eyebrow colors from hair.baseColor
        {
            Color.RGBToHSV(incolor, out float h, out float s, out float v);
            v = v > 0.08f ? v - 0.08f : 0;
            return Color.HSVToRGB(h, s, v);
        }

        public static ChaFileControl GetSelectedCharacter()
        {
            ChaCustom.CustomFileInfoComponent selected = Singleton<ChaCustom.CustomFileListCtrl>.Instance.GetSelectTopItem();
            ChaFileControl tmp = new ChaFileControl();
            tmp.LoadCharaFile(selected.info.FullPath, byte.MaxValue);
            return tmp;
        }

        internal static void PlaySound()
        {
            Utils.Sound.Play(SystemSE.ok_l);
        }

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
