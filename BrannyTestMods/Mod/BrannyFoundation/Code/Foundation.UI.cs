using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrannyCore
{
    public partial class BrannyFoundation
    {
        public static GameObject brannyCanvas;

        void init_ui()
        {
            brannyCanvas = instance.GetGameObjectFromAssetBundle("BrannyCanvas");
            brannyCanvas.name = "BrannyCanvas";

            
        }
    }
}