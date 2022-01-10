using System;
using System.Collections.Generic;

// Token: 0x02000177 RID: 375
public class Heat
{
	// Token: 0x06000875 RID: 2165 RVA: 0x0005B88C File Offset: 0x00059A8C
	internal void clear()
	{
		this.world = MapBox.instance;
		if (this.tiles == null)
		{
			this.tiles = new HashSetWorldTile();
		}
		foreach (WorldTile worldTile in this.tiles)
		{
			worldTile.heat = 0;
		}
		this.tiles.Clear();
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0005B908 File Offset: 0x00059B08
	internal void addTile(WorldTile pTile, int pHeat = 1)
	{
		if (pTile.heat == 0)
		{
			this.tiles.Add(pTile);
		}
		pTile.heat += pHeat;
		if (pTile.heat > 5)
		{
			if (pTile.building != null && pTile.building.data.alive)
			{
				pTile.building.getHit(0f, true, AttackType.Other, null, true);
			}
			if (pTile.Type.layerType == TileLayerType.Ocean)
			{
				MapAction.removeLiquid(pTile);
				this.world.particlesSmoke.spawn(pTile.posV3);
			}
			if (pTile.Type.frozen)
			{
				MapAction.unfreezeTile(pTile);
			}
			pTile.setBurned(-1);
			this.world.setTileDirty(pTile, false);
		}
		if (pTile.heat > 10)
		{
			if (pTile.Type.burnable)
			{
				if (pTile.Type.IsType("tnt") || pTile.Type.IsType("tnt_timed"))
				{
					AchievementLibrary.achievementTntAndHeat.check();
				}
				pTile.setFire(false);
			}
			if (pTile.building != null && pTile.building.data.alive)
			{
				pTile.building.getHit(0f, true, AttackType.Other, null, true);
			}
			if (pTile.building != null)
			{
				pTile.setFire(false);
			}
		}
		if (pTile.heat > 20)
		{
			if (pTile.Type.explodable && pTile.explosionWave == 0)
			{
				this.world.explosionLayer.explodeBomb(pTile, false);
			}
			for (int i = 0; i < pTile.units.Count; i++)
			{
				Actor actor = pTile.units[i];
				if (!actor.stats.very_high_flyer)
				{
					actor.getHit(50f, true, AttackType.Other, null, true);
				}
			}
			if (pTile.building != null)
			{
				pTile.setFire(false);
			}
			if (pTile.top_type != null)
			{
				MapAction.decreaseTile(pTile, "flash");
			}
		}
		if (pTile.heat > 30)
		{
			if (pTile.Type.lava)
			{
				this.world.lavaLayer.addLava(pTile, "lava3");
			}
			if (pTile.Type.IsType("soil_low") || pTile.Type.IsType("soil_high"))
			{
				pTile.setTileType("sand");
			}
			if (pTile.Type.road)
			{
				pTile.setTileType("sand");
			}
		}
		if (pTile.heat > 100 && pTile.Type.IsType("sand"))
		{
			this.world.lavaLayer.addLava(pTile, "lava3");
		}
		if (pTile.heat > 160)
		{
			if (pTile.Type.IsType("mountains"))
			{
				this.world.lavaLayer.addLava(pTile, "lava3");
			}
			if (pTile.Type.IsType("hills"))
			{
				this.world.lavaLayer.addLava(pTile, "lava3");
			}
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0005BC08 File Offset: 0x00059E08
	internal void update(float pElapsed)
	{
		if (this.world.isPaused())
		{
			return;
		}
		if (this.tiles.Count == 0)
		{
			return;
		}
		if (this.tickTimer > 0f)
		{
			this.tickTimer -= pElapsed;
			return;
		}
		this.tickTimer = 1f;
		this.tilesToRemove.Clear();
		foreach (WorldTile worldTile in this.tiles)
		{
			worldTile.heat--;
			if (worldTile.heat <= 0)
			{
				this.tilesToRemove.Add(worldTile);
			}
		}
		for (int i = 0; i < this.tilesToRemove.Count; i++)
		{
			WorldTile worldTile2 = this.tilesToRemove[i];
			this.tiles.Remove(worldTile2);
		}
		this.tilesToRemove.Clear();
	}

	// Token: 0x04000AEC RID: 2796
	private float tickTimer;

	// Token: 0x04000AED RID: 2797
	private List<WorldTile> tilesToRemove = new List<WorldTile>();

	// Token: 0x04000AEE RID: 2798
	private HashSetWorldTile tiles;

	// Token: 0x04000AEF RID: 2799
	private MapBox world;
}
