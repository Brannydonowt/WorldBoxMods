using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class NukeFlash : BaseEffect
{
	// Token: 0x06000618 RID: 1560 RVA: 0x00048635 File Offset: 0x00046835
	internal void spawnFlash(WorldTile pTile, string pBomb)
	{
		this.tile = pTile;
		this.godPower = AssetManager.powers.get(pBomb);
		this.bombSpawned = false;
		this.bomb = pBomb;
		this.killing = false;
		this.prepare(pTile, 0.1f);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00048670 File Offset: 0x00046870
	private void Update()
	{
		if (base.transform.localScale.x < 1f && !this.killing)
		{
			Config.greyGooDamaged = false;
			Vector3 localScale = base.transform.localScale;
			localScale.x += this.world.elapsed * 2.5f;
			if (localScale.x >= 0.8f && !this.bombSpawned)
			{
				this.bombSpawned = true;
				this.bombAction();
			}
			if (!AchievementLibrary.isUnlocked(AchievementLibrary.achievementFinalResolution) && Config.greyGooDamaged)
			{
				AchievementLibrary.achievementFinalResolution.check();
			}
			if (localScale.x >= 1f)
			{
				localScale.x = 1f;
				this.killing = true;
			}
			localScale.y = localScale.x;
			base.transform.localScale = localScale;
			return;
		}
		if (this.killing)
		{
			Vector3 localScale2 = base.transform.localScale;
			localScale2.x -= this.world.elapsed * 2.5f;
			localScale2.y = localScale2.x;
			if (localScale2.x <= 0f)
			{
				localScale2.x = 0f;
				base.kill();
			}
			base.transform.localScale = localScale2;
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x000487B6 File Offset: 0x000469B6
	private void bombAction()
	{
		if (this.bomb == "atomicBomb")
		{
			this.atomicBombAction();
			return;
		}
		if (this.bomb == "czarBomba")
		{
			this.czarBombaAction();
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x000487EC File Offset: 0x000469EC
	private void atomicBombAction()
	{
		this.world.stackEffects.get("explosionNuke").spawnAtRandomScale(this.tile, 0.8f, 0.9f);
		if (ExplosionChecker.instance.checkNearby(this.tile, 30))
		{
			return;
		}
		MapAction.damageWorld(this.tile, 30, TerraformLibrary.atomicBomb);
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0004884C File Offset: 0x00046A4C
	private void czarBombaAction()
	{
		this.world.stackEffects.get("explosionNuke").spawnAtRandomScale(this.tile, 1.4f, 1.6f);
		if (ExplosionChecker.instance.checkNearby(this.tile, 70))
		{
			return;
		}
		MapAction.damageWorld(this.tile, 70, TerraformLibrary.czarBomba);
	}

	// Token: 0x040007F6 RID: 2038
	private bool killing;

	// Token: 0x040007F7 RID: 2039
	private bool bombSpawned;

	// Token: 0x040007F8 RID: 2040
	private string bomb;

	// Token: 0x040007F9 RID: 2041
	private GodPower godPower;
}
