//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using HarmonyLib;
using static Config;

namespace BrannyTestMods
{
    public partial class WorldBoxMod
    {
        static AssetBundle loadedAssetBundle;

        static bool assets_initialised;

        void init_assets() 
        {
            LoadAssetBundle();
        }

        public static void LoadAssetBundle()
        {
            string bundlename = "brannywbox";
            loadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundlename));
            if (loadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle");
                assets_initialised = false;
                return;
            }

            assets_initialised = true;
        }

        public GameObject GetGameObjectFromAssetBundle(string name) 
        {
            if (!assets_initialised)
            {
                Debug.Log("No AssetBundle Yet.");
                init_assets();
                return null;
            }

            Debug.Log("Have asset bundle, pulling asset");
            var prefab = loadedAssetBundle.LoadAsset<GameObject>(name);
            GameObject result = Instantiate(prefab) as GameObject;
            result.SetActive(false);
            return result;
        }
    }
}
