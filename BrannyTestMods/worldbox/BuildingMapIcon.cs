using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class BuildingMapIcon
{
	// Token: 0x06000445 RID: 1093 RVA: 0x0003C940 File Offset: 0x0003AB40
	public BuildingMapIcon(Sprite sprite)
	{
		this.width = sprite.texture.width;
		this.height = sprite.texture.height;
		this.tex = new BuildingColorPixel[this.height][];
		for (int i = 0; i < this.height; i++)
		{
			BuildingColorPixel[] array = new BuildingColorPixel[this.width];
			for (int j = 0; j < this.width; j++)
			{
				Color32 color = sprite.texture.GetPixel(j, i);
				if (color.a == 0)
				{
					color = Toolbox.clear;
				}
				array[j] = new BuildingColorPixel();
				array[j].color = color;
				if (color.a == 0)
				{
					array[j].color_ruin = Toolbox.clear;
					array[j].color_abandoned = Toolbox.clear;
				}
				else
				{
					array[j].color_abandoned = Toolbox.makeDarkerColor(color, 0.9f);
					array[j].color_ruin = Toolbox.makeDarkerColor(color, 0.6f);
				}
			}
			this.tex[i] = array;
		}
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0003CA5C File Offset: 0x0003AC5C
	internal Color32 getColor(int pX, int pY, Building pBuilding)
	{
		if (pX >= this.width || pY >= this.height)
		{
			return Toolbox.clear;
		}
		Color32 color = this.tex[pY][pX].color;
		bool flag = false;
		if (pBuilding.kingdom.kingdomColor != null)
		{
			if (Toolbox.areColorsEqual(color, Toolbox.color_magenta_0))
			{
				color = pBuilding.kingdom.kingdomColor.k_color_0;
				flag = true;
			}
			else if (Toolbox.areColorsEqual(color, Toolbox.color_magenta_1))
			{
				color = pBuilding.kingdom.kingdomColor.k_color_1;
				flag = true;
			}
			else if (Toolbox.areColorsEqual(color, Toolbox.color_magenta_2))
			{
				color = pBuilding.kingdom.kingdomColor.k_color_2;
				flag = true;
			}
			else if (Toolbox.areColorsEqual(color, Toolbox.color_magenta_3))
			{
				color = pBuilding.kingdom.kingdomColor.k_color_3;
				flag = true;
			}
			else if (Toolbox.areColorsEqual(color, Toolbox.color_magenta_4))
			{
				color = pBuilding.kingdom.kingdomColor.k_color_4;
				flag = true;
			}
		}
		if (!flag)
		{
			if (pBuilding.isAbandoned())
			{
				color = this.tex[pY][pX].color_abandoned;
			}
			else if (pBuilding.isRuin())
			{
				color = this.tex[pY][pX].color_ruin;
			}
		}
		return color;
	}

	// Token: 0x0400067E RID: 1662
	private BuildingColorPixel[][] tex;

	// Token: 0x0400067F RID: 1663
	internal int width;

	// Token: 0x04000680 RID: 1664
	internal int height;
}
