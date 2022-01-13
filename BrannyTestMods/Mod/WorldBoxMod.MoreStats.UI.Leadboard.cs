using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrannyTestMods
{
    public class UnfoldList : MonoBehaviour
    {
        public static RectTransform myRect = null;

        public static float baseHeight;

        public static Transform itemParent;

        public static VerticalLayoutGroup layoutGroup;

        public static int numChildren;

        static bool open = false;

        public void Setup()
        {
            // We want our transform to cover the height of our first child (the stat title)
            Debug.Log("Getting base height");
            baseHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
            Debug.Log("Getting my rect transform");
            myRect = GetComponent<RectTransform>();
            Debug.Log("Getting item parent");
            itemParent = transform.GetChild(1);
            Debug.Log("Getting layout group");
            layoutGroup = itemParent.GetComponent<VerticalLayoutGroup>();
        }

        public void TogglePanel()
        {
            Debug.Log("Toggling panel");
            // if open, set the height to the base height
            if (open)
            {
                SetTransformHeight(baseHeight);
                open = false;
            }
            // Set the height to the num children + spacing + padding
            else
            {
                UnfoldPanel();
                open = true;
            }
        }

        public void SetTransformHeight(float height)
        {
            Debug.Log("Setting height");
            myRect.sizeDelta = new Vector2(myRect.rect.width, height);
        }

        public void UnfoldPanel()
        {
            Debug.Log("Showing panel");
            numChildren = itemParent.childCount;

            float totalHeight = GetChildHeight() * numChildren;
            totalHeight += GetAdditionalHeight();

            SetTransformHeight(totalHeight + baseHeight);
        }

        public float GetChildHeight()
        {
            Debug.Log("Getting child height");
            RectTransform entry = itemParent.GetChild(0).GetComponent<RectTransform>();
            float height = entry.rect.height;
            return height;
        }

        public float GetAdditionalHeight()
        {
            Debug.Log("Getting addtional height");
            float result = 0;

            result += layoutGroup.padding.top;
            result += layoutGroup.padding.bottom;

            result += (layoutGroup.spacing * numChildren);

            return result;
        }
    }
}