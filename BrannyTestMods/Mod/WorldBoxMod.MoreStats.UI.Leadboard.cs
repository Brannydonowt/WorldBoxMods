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

        private void Start()
        {
            // We want our transform to cover the height of our first child (the stat title)
            baseHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        }

        public void TogglePanel()
        {
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
            myRect.sizeDelta = new Vector2(myRect.rect.width, height);
        }

        public void UnfoldPanel()
        {
            Debug.Log("Doing the thing");
            numChildren = itemParent.childCount;

            float totalHeight = GetChildHeight() * numChildren;
            totalHeight += GetAdditionalHeight();

            SetTransformHeight(totalHeight + baseHeight);
        }

        public float GetChildHeight()
        {
            RectTransform entry = itemParent.GetChild(0).GetComponent<RectTransform>();
            float height = entry.rect.height;
            return height;
        }

        public float GetAdditionalHeight()
        {
            float result = 0;

            result += layoutGroup.padding.top;
            result += layoutGroup.padding.bottom;

            result += (layoutGroup.spacing * numChildren);

            return result;
        }
    }
}