using System;

// Token: 0x020000D2 RID: 210
public class IceTower : BaseBuildingComponent
{
	// Token: 0x06000461 RID: 1121 RVA: 0x0003D511 File Offset: 0x0003B711
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.freezeTimer > 0f)
		{
			this.freezeTimer -= pElapsed;
			return;
		}
		this.freezeTimer = this.freezeInterval;
		this.freezeRandomTile();
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0003D548 File Offset: 0x0003B748
	private void freezeRandomTile()
	{
		WorldTile currentTile = this.building.currentTile;
		MapRegion region = currentTile.region;
		TileIsland tileIsland = (region != null) ? region.island : null;
		if (tileIsland == null)
		{
			return;
		}
		WorldTile random = tileIsland.regions.GetRandom().tiles.GetRandom<WorldTile>();
		this.freeze(currentTile, random);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0003D596 File Offset: 0x0003B796
	private void freeze(WorldTile pCenter, WorldTile pTile)
	{
		if (Toolbox.DistTile(pCenter, pTile) > 50f)
		{
			return;
		}
		MapAction.freezeTile(pTile);
		MapAction.freezeTile(pTile);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0003D5B4 File Offset: 0x0003B7B4
	private void checkChunk(WorldTile pCenterTile, MapChunk pChunk)
	{
		WorldTile random = pChunk.tiles.GetRandom<WorldTile>();
		if (Toolbox.DistTile(pCenterTile, random) > 10f)
		{
			return;
		}
		MapAction.freezeTile(random);
		MapAction.freezeTile(random);
	}

	// Token: 0x04000690 RID: 1680
	private float freezeInterval = 0.3f;

	// Token: 0x04000691 RID: 1681
	private float freezeTimer;
}
