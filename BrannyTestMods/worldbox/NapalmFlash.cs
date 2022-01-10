using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class NapalmFlash : BaseEffect
{
	// Token: 0x06000614 RID: 1556 RVA: 0x0004849F File Offset: 0x0004669F
	internal void spawnFlash(WorldTile pTile, string pBomb)
	{
		this.tile = pTile;
		this.godPower = AssetManager.powers.get(pBomb);
		this.bombSpawned = false;
		this.bomb = pBomb;
		this.killing = false;
		this.prepare(pTile, 0.1f);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000484DA File Offset: 0x000466DA
	public static bool napalmEffect(WorldTile pTile, string pPowerID)
	{
		pTile.setFire(true);
		return true;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000484E8 File Offset: 0x000466E8
	private void Update()
	{
		if (base.transform.localScale.x < 1f && !this.killing)
		{
			Vector3 localScale = base.transform.localScale;
			localScale.x += this.world.elapsed * 0.7f;
			if (localScale.x >= 0.6f && !this.bombSpawned)
			{
				this.bombSpawned = true;
				MapBox.instance.loopWithBrush(this.tile, Brush.get(12, "circ_"), new PowerActionWithID(NapalmFlash.napalmEffect), "napalm");
			}
			if (localScale.x >= 0.7f)
			{
				localScale.x = 0.7f;
				this.killing = true;
			}
			localScale.y = localScale.x;
			base.transform.localScale = localScale;
			return;
		}
		if (this.killing)
		{
			Vector3 localScale2 = base.transform.localScale;
			localScale2.x -= this.world.elapsed * 1.5f;
			localScale2.y = localScale2.x;
			if (localScale2.x <= 0f)
			{
				localScale2.x = 0f;
				base.kill();
			}
			base.transform.localScale = localScale2;
		}
	}

	// Token: 0x040007F2 RID: 2034
	private bool killing;

	// Token: 0x040007F3 RID: 2035
	private bool bombSpawned;

	// Token: 0x040007F4 RID: 2036
	private string bomb;

	// Token: 0x040007F5 RID: 2037
	private GodPower godPower;
}
