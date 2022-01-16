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

namespace BrannyCore
{
    public partial class BrannyFoundation
    {
        static AssetBundle loadedAssetBundle;

        public static bool assets_initialised;

        void init_assets() 
        {
            instance.LoadAssetBundle();
        }

        void LoadAssetBundle()
        {
            string bundlename = "brannywbox";
            loadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundle", bundlename));
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

            var prefab = loadedAssetBundle.LoadAsset<GameObject>(name);
            GameObject result = Instantiate(prefab) as GameObject;
            result.SetActive(false);
            return result;
        }
    }
}
