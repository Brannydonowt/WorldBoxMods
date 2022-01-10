using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class BuildingTower : BaseBuildingComponent
{
	// Token: 0x0600044A RID: 1098 RVA: 0x0003CCA6 File Offset: 0x0003AEA6
	internal override void create()
	{
		base.create();
		this.chunksInRadius = new List<MapChunk>();
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x0003CCBC File Offset: 0x0003AEBC
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.building.data.underConstruction)
		{
			return;
		}
		if (this.testShooting && Input.GetMouseButtonDown(2))
		{
			Vector3 pStart = new Vector3(this.building.currentTile.posV3.x, this.building.currentTile.posV3.y);
			pStart.y += this.building.stats.tower_projectile_offset;
			this.world.stackEffects.startProjectile(pStart, this.world.getMouseTilePos().posV3, this.building.stats.tower_projectile, 0f);
		}
		if (this._shootingActive)
		{
			this.shootAtTarget();
			return;
		}
		if (this.check_enemies_timeout > 0f)
		{
			this.check_enemies_timeout -= pElapsed;
			return;
		}
		this.check_enemies_interval = this.building.stats.tower_projectile_reload;
		this.check_enemies_timeout = this.check_enemies_interval + Toolbox.randomFloat(0f, this.check_enemies_interval);
		this.checkEnemies();
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0003CDE0 File Offset: 0x0003AFE0
	private void shootAtTarget()
	{
		if (this._shootingTarget == null)
		{
			this._shootingActive = false;
			return;
		}
		this._shootingAmount--;
		if (this._shootingAmount <= 0)
		{
			this._shootingActive = false;
		}
		Vector3 pStart = new Vector3(this.building.currentTile.posV3.x, this.building.currentTile.posV3.y);
		pStart.y += this.building.stats.tower_projectile_offset;
		Vector3 posV = this._shootingTarget.currentTile.posV3;
		posV.x += Toolbox.randomFloat(-(this._shootingTarget.curStats.size + 1f), this._shootingTarget.curStats.size + 1f);
		posV.y += Toolbox.randomFloat(-this._shootingTarget.curStats.size, this._shootingTarget.curStats.size);
		float pZ = 0f;
		if (this._shootingTarget.isInAir())
		{
			pZ = this._shootingTarget.getZ();
		}
		Projectile projectile = this.world.stackEffects.startProjectile(pStart, posV, this.building.stats.tower_projectile, pZ);
		if (projectile != null)
		{
			projectile.byWho = this.building;
			projectile.setStats(projectile.byWho.curStats);
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0003CF58 File Offset: 0x0003B158
	private void checkEnemies()
	{
		this._shootingTarget = null;
		this._shootingActive = false;
		this._shootingAmount = 0;
		if (!this.chunksInRadius.Any<MapChunk>())
		{
			this.chunksInRadius.Add(this.building.currentTile.chunk);
			this.chunksInRadius.AddRange(this.building.currentTile.chunk.neighboursAll);
		}
		Toolbox.temp_list_objects_enemies.Clear();
		BaseSimObject baseSimObject = this.building.findEnemyObjectTarget();
		if (baseSimObject == null)
		{
			return;
		}
		this.building.startShake(0.1f);
		this._shootingActive = true;
		this._shootingTarget = baseSimObject;
		this._shootingAmount = this.building.stats.tower_projectile_amount;
	}

	// Token: 0x04000683 RID: 1667
	private float check_enemies_timeout = 1f;

	// Token: 0x04000684 RID: 1668
	private float check_enemies_interval = 3f;

	// Token: 0x04000685 RID: 1669
	private List<MapChunk> chunksInRadius;

	// Token: 0x04000686 RID: 1670
	private bool testShooting;

	// Token: 0x04000687 RID: 1671
	private int _shootingAmount;

	// Token: 0x04000688 RID: 1672
	private bool _shootingActive;

	// Token: 0x04000689 RID: 1673
	private BaseSimObject _shootingTarget;
}
