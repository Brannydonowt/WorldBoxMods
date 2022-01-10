using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class MapBorder : BaseEffect
{
	// Token: 0x06000606 RID: 1542 RVA: 0x00047D05 File Offset: 0x00045F05
	internal override void create()
	{
		base.create();
		this.updateTimer = new WorldTimer(0.12f, new Action(this.updateEffect));
		this.alphaTimer = new WorldTimer(0.02f, new Action(this.updateAlpha));
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00047D48 File Offset: 0x00045F48
	internal void generateTexture()
	{
		if (this.curWidth == MapBox.width && this.curHeight == MapBox.height)
		{
			return;
		}
		this.curWidth = MapBox.width;
		this.curHeight = MapBox.height;
		SpriteRenderer component = base.gameObject.GetComponent<SpriteRenderer>();
		Texture2D texture2D = new Texture2D(this.curWidth, this.curHeight, TextureFormat.RGBA32, false);
		texture2D.filterMode = FilterMode.Point;
		int num = texture2D.height * texture2D.width;
		Color32[] array = new Color32[num];
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		list2.Clear();
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			if (num3 == 0 && !list.Contains(i))
			{
				list2.Add(i);
			}
			num2++;
			if (num2 >= this.curWidth)
			{
				num2 = 0;
				num3++;
			}
		}
		list.AddRange(list2);
		list2.Clear();
		num2 = 0;
		num3 = 0;
		for (int j = 0; j < num; j++)
		{
			if (num2 == this.curWidth - 1 && !list.Contains(j))
			{
				list2.Add(j);
			}
			num2++;
			if (num2 >= this.curWidth)
			{
				num2 = 0;
				num3++;
			}
		}
		list.AddRange(list2);
		list2.Clear();
		num2 = 0;
		num3 = 0;
		for (int k = 0; k < num; k++)
		{
			if (num3 == this.curHeight - 1 && !list.Contains(k))
			{
				list2.Add(k);
			}
			num2++;
			if (num2 >= this.curWidth)
			{
				num2 = 0;
				num3++;
			}
		}
		list.AddRange(list2);
		list2.Clear();
		num2 = 0;
		num3 = 0;
		for (int l = 0; l < num; l++)
		{
			if (num2 == 0 && !list.Contains(l))
			{
				list2.Add(l);
			}
			num2++;
			if (num2 >= this.curWidth)
			{
				num2 = 0;
				num3++;
			}
		}
		list2.Reverse();
		list.AddRange(list2);
		int num4 = 0;
		for (int m = 0; m < list.Count; m++)
		{
			int num5 = list[m];
			if (num4 == 0 || num4 == 1 || num4 == 2)
			{
				array[num5] = Color.white;
				num4++;
			}
			else
			{
				num4 = 0;
			}
		}
		component.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 1f);
		texture2D.SetPixels32(array);
		texture2D.Apply();
		base.gameObject.transform.localPosition = new Vector3((float)(this.curWidth / 2), (float)(this.curHeight / 2));
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00048009 File Offset: 0x00046209
	private void Update()
	{
		this.updateTimer.update(-1f);
		this.alphaTimer.update(-1f);
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0004802C File Offset: 0x0004622C
	private void updateAlpha()
	{
		if (this.world.selectedButtons.selectedButton == null)
		{
			this.alpha -= 0.02f;
			if (this.alpha < 0f)
			{
				this.alpha = 0f;
			}
		}
		else
		{
			this.alpha += 0.02f;
			if (this.alpha > 0.42f)
			{
				this.alpha = 0.42f;
			}
		}
		if (this.sprRenderer.color.a == this.alpha)
		{
			return;
		}
		base.setAlpha(this.alpha);
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000480CC File Offset: 0x000462CC
	private void updateEffect()
	{
		if (this.alpha == 0f)
		{
			return;
		}
		this.currentState++;
		if (this.currentState > 3)
		{
			this.currentState = 0;
		}
		switch (this.currentState)
		{
		case 0:
			this.sprRenderer.flipX = false;
			this.sprRenderer.flipY = false;
			return;
		case 1:
			this.sprRenderer.flipX = true;
			this.sprRenderer.flipY = false;
			return;
		case 2:
			this.sprRenderer.flipX = true;
			this.sprRenderer.flipY = true;
			return;
		case 3:
			this.sprRenderer.flipX = false;
			this.sprRenderer.flipY = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x040007E3 RID: 2019
	private int currentState;

	// Token: 0x040007E4 RID: 2020
	private WorldTimer updateTimer;

	// Token: 0x040007E5 RID: 2021
	private WorldTimer alphaTimer;

	// Token: 0x040007E6 RID: 2022
	private int curWidth;

	// Token: 0x040007E7 RID: 2023
	private int curHeight;
}
