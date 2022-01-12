//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.IO;
using System.Collections;
using UnityEngine;
using HarmonyLib;
using static Config;

namespace BrannyTestMods
{
    class AssetLoader : MonoBehaviour
    {
        public void LoadAssetBundle()
        {
            var loadedAssetBundle = AssetBundle.LoadFromFile("testbundle");
            if (loadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle");
                return;
            }

            var prefab = loadedAssetBundle.LoadAsset<GameObject>("BrannyCanvas");
            Instantiate(prefab);

            loadedAssetBundle.Unload(false);
        }
    }
}
