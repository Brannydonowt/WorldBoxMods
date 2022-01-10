using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class ZonesInfo : BaseMapObject
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x0006BB20 File Offset: 0x00069D20
	public void init()
	{
		base.create();
		this.initiated = true;
		foreach (TileZone tileZone in this.world.zoneCalculator.zones)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.infoTextPrefab).gameObject;
			tileZone.infoText = gameObject.GetComponent<InfoText>();
			gameObject.transform.position = new Vector3((float)(tileZone.centerTile.pos.x - 1), (float)(tileZone.centerTile.pos.y - 1 - 20), gameObject.transform.position.z);
			gameObject.transform.parent = base.transform;
		}
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0006BC08 File Offset: 0x00069E08
	private void Update()
	{
		if (!Config.showZonesInfo)
		{
			return;
		}
		if (!this.initiated)
		{
			this.init();
		}
		foreach (TileZone tileZone in this.world.zoneCalculator.zones)
		{
			string text = "fires: " + tileZone.tilesOnFire.ToString() + "\n";
			text = string.Concat(new string[]
			{
				text,
				"water: ",
				tileZone.tilesWithLiquid.ToString(),
				" | ground: ",
				tileZone.tilesWithGround.ToString(),
				"\n"
			});
			text = text + "fire: " + tileZone.tilesOnFire.ToString();
			tileZone.infoText.setText(text);
		}
	}

	// Token: 0x04000D4C RID: 3404
	public GameObject infoTextPrefab;

	// Token: 0x04000D4D RID: 3405
	private bool initiated;
}
