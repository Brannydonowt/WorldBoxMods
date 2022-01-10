using System;
using System.Collections.Generic;

// Token: 0x020000A4 RID: 164
public class TerraformOptions : Asset
{
	// Token: 0x0400058A RID: 1418
	public bool spawnPixels;

	// Token: 0x0400058B RID: 1419
	public bool removeTreesFully;

	// Token: 0x0400058C RID: 1420
	public bool destroyBuildings;

	// Token: 0x0400058D RID: 1421
	public bool removeBurned;

	// Token: 0x0400058E RID: 1422
	public bool removeTornado;

	// Token: 0x0400058F RID: 1423
	public bool addBurned;

	// Token: 0x04000590 RID: 1424
	public int addHeat;

	// Token: 0x04000591 RID: 1425
	public bool flash;

	// Token: 0x04000592 RID: 1426
	public bool removeBorders;

	// Token: 0x04000593 RID: 1427
	public bool removeBuilding;

	// Token: 0x04000594 RID: 1428
	public bool removeTopTile;

	// Token: 0x04000595 RID: 1429
	public bool removeRoads;

	// Token: 0x04000596 RID: 1430
	public bool removeFrozen;

	// Token: 0x04000597 RID: 1431
	public bool removeWater;

	// Token: 0x04000598 RID: 1432
	public bool removeRuins;

	// Token: 0x04000599 RID: 1433
	public bool lightningEffect;

	// Token: 0x0400059A RID: 1434
	public bool removeLava;

	// Token: 0x0400059B RID: 1435
	public bool damageBuildings;

	// Token: 0x0400059C RID: 1436
	public int damage;

	// Token: 0x0400059D RID: 1437
	public bool setFire;

	// Token: 0x0400059E RID: 1438
	public bool transformToWasteland;

	// Token: 0x0400059F RID: 1439
	public bool applyForce;

	// Token: 0x040005A0 RID: 1440
	public float force_power = 1.5f;

	// Token: 0x040005A1 RID: 1441
	public bool explode_tile;

	// Token: 0x040005A2 RID: 1442
	public bool explosion_pixel_effect = true;

	// Token: 0x040005A3 RID: 1443
	public bool explode_and_set_random_fire;

	// Token: 0x040005A4 RID: 1444
	public int explode_strength;

	// Token: 0x040005A5 RID: 1445
	public bool applies_to_high_flyers;

	// Token: 0x040005A6 RID: 1446
	public bool shake;

	// Token: 0x040005A7 RID: 1447
	public float shake_duration = 0.3f;

	// Token: 0x040005A8 RID: 1448
	public float shake_interval = 0.01f;

	// Token: 0x040005A9 RID: 1449
	public float shake_intensity = 2f;

	// Token: 0x040005AA RID: 1450
	public string addTrait = string.Empty;

	// Token: 0x040005AB RID: 1451
	public string[] ignoreKingdoms;

	// Token: 0x040005AC RID: 1452
	public List<string> destroyOnly;

	// Token: 0x040005AD RID: 1453
	public List<string> ignoreBuildings;
}
