using System;
using System.Collections.Generic;

// Token: 0x020000D3 RID: 211
public class UnitSpawner : BaseBuildingComponent
{
	// Token: 0x06000466 RID: 1126 RVA: 0x0003D5FB File Offset: 0x0003B7FB
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.spawnTimer > 0f)
		{
			this.spawnTimer -= pElapsed;
			return;
		}
		this.spawnTimer = this.spawnInterval;
		this.spawnUnit();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0003D634 File Offset: 0x0003B834
	private void spawnUnit()
	{
		if (this.units_current >= this.units_limit)
		{
			return;
		}
		string spawnUnits_asset = this.building.stats.spawnUnits_asset;
		Actor unitFromHere = this.world.createNewUnit(spawnUnits_asset, this.building.currentTile, null, 0f, null);
		this.setUnitFromHere(unitFromHere);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0003D688 File Offset: 0x0003B888
	public void setUnitFromHere(Actor pActor)
	{
		pActor.setHomeBuilding(this.building);
		pActor.callbacks_on_death.Add(new BaseActionActor(this.callbackUnitDied));
		this.units_current++;
		for (int i = 0; i < this.spawnActions.Count; i++)
		{
			this.spawnActions[i](pActor);
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0003D6EE File Offset: 0x0003B8EE
	public void callbackUnitDied(Actor pActor)
	{
		this.units_current--;
	}

	// Token: 0x04000692 RID: 1682
	public int units_current;

	// Token: 0x04000693 RID: 1683
	public int units_limit = 5;

	// Token: 0x04000694 RID: 1684
	public float spawnInterval = 10f;

	// Token: 0x04000695 RID: 1685
	public float spawnTimer = 1f;

	// Token: 0x04000696 RID: 1686
	public List<BaseActionActor> spawnActions = new List<BaseActionActor>();
}
