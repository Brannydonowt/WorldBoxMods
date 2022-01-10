using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public struct WorldLogMessage
{
	// Token: 0x06000388 RID: 904 RVA: 0x000379B4 File Offset: 0x00035BB4
	public WorldLogMessage(string pText, string pSpecial1 = null, string pSpecial2 = null, string pSpecial3 = null)
	{
		this.text = pText;
		this.special1 = pSpecial1;
		this.special2 = pSpecial2;
		this.special3 = pSpecial3;
		this.color_special1 = Color.clear;
		this.color_special2 = Color.clear;
		this.color_special3 = Color.clear;
		this.date = "y:" + (MapBox.instance.mapStats.year + 1).ToString() + ", m:" + (MapBox.instance.mapStats.month + 1).ToString();
		this.icon = "";
		this.unit = null;
		this.kingdom = null;
		this.city = null;
		this.location = Vector3.zero;
	}

	// Token: 0x040005D0 RID: 1488
	public string text;

	// Token: 0x040005D1 RID: 1489
	public string special1;

	// Token: 0x040005D2 RID: 1490
	public string special2;

	// Token: 0x040005D3 RID: 1491
	public string special3;

	// Token: 0x040005D4 RID: 1492
	public Color color_special1;

	// Token: 0x040005D5 RID: 1493
	public Color color_special2;

	// Token: 0x040005D6 RID: 1494
	public Color color_special3;

	// Token: 0x040005D7 RID: 1495
	public string date;

	// Token: 0x040005D8 RID: 1496
	public string icon;

	// Token: 0x040005D9 RID: 1497
	public City city;

	// Token: 0x040005DA RID: 1498
	public Kingdom kingdom;

	// Token: 0x040005DB RID: 1499
	public Actor unit;

	// Token: 0x040005DC RID: 1500
	public Vector3 location;
}
