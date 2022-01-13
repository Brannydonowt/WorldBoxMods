using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrannyTestMods
{
    public class UnfoldList : MonoBehaviour
    {
        public static RectTransform myRect = null;
        public static RectTransform parentRect = null;

        public static float baseHeight;

        public static Transform itemParent;

        public static VerticalLayoutGroup layoutGroup;

        public static int numChildren;

        public bool open = false;

        public void Setup()
        {
            // Setup the component, grab references
            myRect = GetComponent<RectTransform>();
            parentRect = transform.parent.GetComponent<RectTransform>();
            baseHeight = myRect.rect.height;
            itemParent = transform.parent.GetChild(1);
            layoutGroup = itemParent.GetComponent<VerticalLayoutGroup>();
        }

        public void OnInteract() 
        {
            TogglePanel();
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
            parentRect.sizeDelta = new Vector2(myRect.rect.width, height);
        }

        public void UnfoldPanel()
        {
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