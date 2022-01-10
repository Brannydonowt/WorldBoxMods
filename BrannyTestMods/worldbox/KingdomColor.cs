using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[Serializable]
public class KingdomColor
{
	// Token: 0x06000150 RID: 336 RVA: 0x000173A4 File Offset: 0x000155A4
	public KingdomColor(string pColor1, string pColor2, string pColor3)
	{
		this.colorBorderInside = Toolbox.makeColor(pColor1);
		this.colorBorderOut = Toolbox.makeColor(pColor2);
		this.colorBorderBannerIcon = Toolbox.makeColor(pColor3);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000173DC File Offset: 0x000155DC
	public void initColor()
	{
		this.id = KingdomColor.last_id++;
		this.colorBorderInsideAlpha = new Color(this.colorBorderInside.r, this.colorBorderInside.g, this.colorBorderInside.b);
		this.colorBorderInsideAlpha.a = 0.7f;
		this.colorBorderInside_dark = default(Color);
		this.colorBorderInside_dark.r = this.colorBorderInside.r * 0.8f;
		this.colorBorderInside_dark.g = this.colorBorderInside.g * 0.8f;
		this.colorBorderInside_dark.b = this.colorBorderInside.b * 0.8f;
		this.colorBorderInside_dark.a = 1f;
		this.colorBorderInside_dark32 = this.colorBorderInside_dark;
		this.colorBorderInside32 = this.colorBorderInside;
		this.color32_unit = Color.Lerp(this.colorBorderInside32, Color.white, 0.3f);
		this.color32_unit.a = byte.MaxValue;
		this.k_color_0 = this.colorBorderInside32;
		this.k_color_1 = Color.Lerp(this.k_color_0, Color.black, 0.13f);
		this.k_color_2 = Color.Lerp(this.k_color_0, Color.black, 0.35000002f);
		this.k_color_3 = Color.Lerp(this.k_color_0, Color.black, 0.51f);
		this.k_color_4 = Color.Lerp(this.k_color_0, Color.black, 0.65999997f);
	}

	// Token: 0x04000139 RID: 313
	public static int last_id;

	// Token: 0x0400013A RID: 314
	[NonSerialized]
	public int id;

	// Token: 0x0400013B RID: 315
	public string name = "not_set";

	// Token: 0x0400013C RID: 316
	public Color colorBorderInside;

	// Token: 0x0400013D RID: 317
	[NonSerialized]
	public Color32 k_color_0;

	// Token: 0x0400013E RID: 318
	[NonSerialized]
	public Color32 k_color_1;

	// Token: 0x0400013F RID: 319
	[NonSerialized]
	public Color32 k_color_2;

	// Token: 0x04000140 RID: 320
	[NonSerialized]
	public Color32 k_color_3;

	// Token: 0x04000141 RID: 321
	[NonSerialized]
	public Color32 k_color_4;

	// Token: 0x04000142 RID: 322
	[NonSerialized]
	public Color32 color32_unit;

	// Token: 0x04000143 RID: 323
	[NonSerialized]
	public Color32 colorBorderInside32;

	// Token: 0x04000144 RID: 324
	[NonSerialized]
	public Color colorBorderInside_dark;

	// Token: 0x04000145 RID: 325
	[NonSerialized]
	public Color32 colorBorderInside_dark32;

	// Token: 0x04000146 RID: 326
	[NonSerialized]
	public Color colorBorderInsideAlpha;

	// Token: 0x04000147 RID: 327
	public Color colorBorderOut;

	// Token: 0x04000148 RID: 328
	public Color colorBorderBannerIcon;
}
