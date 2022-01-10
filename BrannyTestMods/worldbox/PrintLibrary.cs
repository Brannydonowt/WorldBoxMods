using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class PrintLibrary : MonoBehaviour
{
	// Token: 0x06000342 RID: 834 RVA: 0x000352EC File Offset: 0x000334EC
	private void Awake()
	{
		this.color0 = Toolbox.makeColor("#FFFFFF");
		this.color1 = Toolbox.makeColor("#CCCCCC");
		this.color2 = Toolbox.makeColor("#7F7F7F");
		this.color3 = Toolbox.makeColor("#000000");
		this.dict = new Dictionary<string, PrintTemplate>();
		for (int i = 0; i < this.list.Count; i++)
		{
			PrintTemplate printTemplate = this.list[i];
			this.calcSteps(printTemplate);
			this.dict.Add(printTemplate.name, printTemplate);
			if (printTemplate.name.Contains("quake"))
			{
				this.listQuakes.Add(printTemplate);
				this.addRotatedQuake(printTemplate, 90);
				this.addRotatedQuake(printTemplate, 180);
				this.addRotatedQuake(printTemplate, 360);
				this.addRotatedQuake(printTemplate, -360);
				this.addRotatedQuake(printTemplate, -90);
				this.addRotatedQuake(printTemplate, -180);
				this.addRotatedQuake(printTemplate, -270);
			}
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x000353F4 File Offset: 0x000335F4
	private void addRotatedQuake(PrintTemplate pOrigin, int pRotation)
	{
		PrintTemplate printTemplate = new PrintTemplate();
		printTemplate.name = pOrigin.name + "_" + pRotation.ToString();
		Texture2D originTexture = Object.Instantiate<Texture2D>(pOrigin.graphics);
		printTemplate.graphics = TextureRotator.Rotate(originTexture, pRotation);
		this.calcSteps(printTemplate);
		this.listQuakes.Add(printTemplate);
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00035450 File Offset: 0x00033650
	private void calcSteps(PrintTemplate pPrint)
	{
		pPrint.steps = new List<PrintStep>();
		int width = pPrint.graphics.width;
		int height = pPrint.graphics.height;
		for (int i = 1; i < width - 1; i++)
		{
			for (int j = 1; j < height - 1; j++)
			{
				Color pixel = pPrint.graphics.GetPixel(i, j);
				if (!(pixel == this.color0))
				{
					PrintStep printStep = new PrintStep
					{
						x = i - 1 - width / 2,
						y = j - 1 - height / 2,
						action = 1
					};
					pPrint.steps.Add(printStep);
					if (pixel == this.color2)
					{
						pPrint.steps.Add(printStep);
					}
					else if (pixel == this.color3)
					{
						pPrint.steps.Add(printStep);
						pPrint.steps.Add(printStep);
					}
				}
			}
		}
		pPrint.stepsPerTick = (int)((float)pPrint.steps.Count * 0.005f + 1f);
	}

	// Token: 0x04000553 RID: 1363
	private Color color0;

	// Token: 0x04000554 RID: 1364
	private Color color1;

	// Token: 0x04000555 RID: 1365
	private Color color2;

	// Token: 0x04000556 RID: 1366
	private Color color3;

	// Token: 0x04000557 RID: 1367
	public List<PrintTemplate> list;

	// Token: 0x04000558 RID: 1368
	public Dictionary<string, PrintTemplate> dict;

	// Token: 0x04000559 RID: 1369
	internal List<PrintTemplate> listQuakes = new List<PrintTemplate>();
}
