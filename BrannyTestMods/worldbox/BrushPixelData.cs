using System;

// Token: 0x020001A3 RID: 419
public class BrushPixelData
{
	// Token: 0x06000998 RID: 2456 RVA: 0x00064C4B File Offset: 0x00062E4B
	public BrushPixelData(int pX, int pY, float pRadius, float pDist)
	{
		this.x = pX;
		this.y = pY;
		this.dist = pDist;
		this.percentMode = pDist / pRadius;
	}

	// Token: 0x04000C42 RID: 3138
	public int x;

	// Token: 0x04000C43 RID: 3139
	public int y;

	// Token: 0x04000C44 RID: 3140
	public float dist;

	// Token: 0x04000C45 RID: 3141
	public float percentMode;
}
