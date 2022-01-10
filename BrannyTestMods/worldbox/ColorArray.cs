using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class ColorArray
{
	// Token: 0x060005CA RID: 1482 RVA: 0x00046344 File Offset: 0x00044544
	public ColorArray(float pR, float pG, float pB, float pA, float pAmount, float pMod = 1f)
	{
		this.colors = new List<Color32>();
		int num = 0;
		while ((float)num < pAmount)
		{
			float num2;
			if (num > 0)
			{
				num2 = 1f / pAmount * (float)num;
			}
			else
			{
				num2 = 0f;
			}
			Color c = new Color(pR, pG, pB, num2 * 1f * pMod);
			this.colors.Add(c);
			num++;
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x000463AE File Offset: 0x000445AE
	public ColorArray(Color32 pColor, int pAmount) : this((float)pColor.r, (float)pColor.g, (float)pColor.b, (float)pColor.a, (float)pAmount, 1f)
	{
	}

	// Token: 0x040007B4 RID: 1972
	public List<Color32> colors;
}
