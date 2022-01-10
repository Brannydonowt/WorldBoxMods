using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class ZoneCamera
{
	// Token: 0x06000AC5 RID: 2757 RVA: 0x0006B862 File Offset: 0x00069A62
	public ZoneCamera()
	{
		this.world = MapBox.instance;
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0006B880 File Offset: 0x00069A80
	private void clear()
	{
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.visible = false;
		}
		this.zones.Clear();
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0006B8DC File Offset: 0x00069ADC
	internal void update()
	{
		this.clear();
		TileZone zoneOnCamera = this.getZoneOnCamera(0, 0, 0f);
		TileZone zoneOnCamera2 = this.getZoneOnCamera(1, 0, 0f);
		TileZone zoneOnCamera3 = this.getZoneOnCamera(0, 1, 0f);
		TileZone zoneOnCamera4 = this.getZoneOnCamera(1, 1, 0f);
		int x;
		if (zoneOnCamera3.x > zoneOnCamera.x)
		{
			x = zoneOnCamera3.x;
		}
		else
		{
			x = zoneOnCamera.x;
		}
		int y;
		if (zoneOnCamera2.y > zoneOnCamera.y)
		{
			y = zoneOnCamera2.y;
		}
		else
		{
			y = zoneOnCamera.y;
		}
		int num = zoneOnCamera4.x - x;
		int num2 = zoneOnCamera4.y - y;
		if (zoneOnCamera2.x < zoneOnCamera4.x)
		{
			num = zoneOnCamera2.x - x;
		}
		if (zoneOnCamera3.y < zoneOnCamera4.y)
		{
			num2 = zoneOnCamera3.y - y;
		}
		for (int i = 0; i <= num; i++)
		{
			for (int j = 0; j <= num2; j++)
			{
				TileZone zone = this.world.zoneCalculator.getZone(x + i, y + j);
				if (zone != null)
				{
					this.zones.Add(zone);
					zone.visible = true;
				}
			}
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0006BA08 File Offset: 0x00069C08
	private TileZone getZoneOnCamera(int pX, int pY, float bonusY = 0f)
	{
		Vector3 vector = this.world.camera.ViewportToWorldPoint(new Vector3((float)pX, (float)pY, this.world.camera.nearClipPlane));
		int x = (int)vector.x;
		int y = (int)vector.y + (int)(bonusY * 8f);
		WorldTile tile = this.world.GetTile(x, y);
		if (tile != null)
		{
			return tile.zone;
		}
		if (pX == 0 && pY == 0)
		{
			return this.world.zoneCalculator.getZone(0, 0);
		}
		if (pX == 1 && pY == 1)
		{
			return this.world.zoneCalculator.getZone(this.world.zoneCalculator.totalZonesX - 1, this.world.zoneCalculator.totalZonesY - 1);
		}
		if (pX == 0 && pY == 1)
		{
			return this.world.zoneCalculator.getZone(0, this.world.zoneCalculator.totalZonesY - 1);
		}
		if (pX == 1 && pY == 0)
		{
			return this.world.zoneCalculator.getZone(this.world.zoneCalculator.totalZonesX - 1, 0);
		}
		return null;
	}

	// Token: 0x04000D4A RID: 3402
	internal HashSet<TileZone> zones = new HashSet<TileZone>();

	// Token: 0x04000D4B RID: 3403
	private MapBox world;
}
