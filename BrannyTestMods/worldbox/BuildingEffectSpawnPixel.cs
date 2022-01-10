using System;

// Token: 0x020000CB RID: 203
public class BuildingEffectSpawnPixel : BaseBuildingComponent
{
	// Token: 0x06000440 RID: 1088 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
	public override void update(float pElapsed)
	{
		if (!this.building.data.spawnPixelActive)
		{
			return;
		}
		if (this.spawnPixelTimer > 0f)
		{
			this.spawnPixelTimer -= pElapsed;
			return;
		}
		this.spawnPixelTimer = this.building.stats.spawnPixelInterval;
		WorldTile worldTile = this.world.GetTile(this.building.currentTile.pos.x, this.building.currentTile.pos.y);
		if (worldTile == null)
		{
			worldTile = this.building.currentTile;
		}
		this.world.dropManager.spawnBurstPixel(worldTile, this.building.stats.spawnDropID, Toolbox.randomFloat(0.5f, 0.5f), Toolbox.randomFloat(1f, 1.3f), this.building.stats.spawnPixelStartZ, -1f);
	}

	// Token: 0x04000679 RID: 1657
	private float spawnPixelTimer;
}
