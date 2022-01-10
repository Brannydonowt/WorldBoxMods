using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000250 RID: 592
public class ButtonsViewer : MonoBehaviour
{
	// Token: 0x06000CE0 RID: 3296 RVA: 0x0007B9F4 File Offset: 0x00079BF4
	private void Start()
	{
		this.content = base.transform.parent;
		this.canvas = CanvasMain.instance.canvas_ui;
		this.buttons = new List<PowerButton>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject.GetComponent<PowerButton>() != null && gameObject.activeSelf)
			{
				this.buttons.Add(gameObject.GetComponent<PowerButton>());
			}
			else if (!(gameObject.GetComponent<Image>() != null) || !gameObject.activeSelf)
			{
				Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0007BAA8 File Offset: 0x00079CA8
	private void Update()
	{
		if (this.lastX == this.content.position.x && this.lastY == this.content.position.y)
		{
			return;
		}
		this.lastX = this.content.position.x;
		this.lastY = this.content.position.y;
		int num = 0;
		int num2 = 0;
		bool flag = false;
		for (int i = 0; i < this.buttons.Count; i++)
		{
			PowerButton powerButton = this.buttons[i];
			if (flag)
			{
				num2++;
				powerButton.gameObject.SetActive(false);
			}
			else
			{
				num++;
				Vector3[] array = new Vector3[4];
				powerButton.rectTransform.GetWorldCorners(array);
				float num3 = Mathf.Max(new float[]
				{
					array[0].x,
					array[1].x,
					array[2].x,
					array[3].x
				});
				float num4 = Mathf.Min(new float[]
				{
					array[0].x,
					array[1].x,
					array[2].x,
					array[3].x
				});
				if (num3 < 0f || num4 > (float)Screen.width)
				{
					powerButton.gameObject.SetActive(false);
					if (num4 > (float)Screen.width)
					{
						flag = true;
					}
				}
				else
				{
					powerButton.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x04000FB5 RID: 4021
	private List<PowerButton> buttons;

	// Token: 0x04000FB6 RID: 4022
	private Transform content;

	// Token: 0x04000FB7 RID: 4023
	private float lastX;

	// Token: 0x04000FB8 RID: 4024
	private float lastY;

	// Token: 0x04000FB9 RID: 4025
	private Canvas canvas;
}
