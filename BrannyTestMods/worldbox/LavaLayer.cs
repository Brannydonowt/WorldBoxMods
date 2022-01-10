using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class LavaLayer : BaseModule
{
	// Token: 0x0600089B RID: 2203 RVA: 0x0005CAD8 File Offset: 0x0005ACD8
	internal override void create()
	{
		base.create();
		this.lavaTypes = new Dictionary<int, TileType>();
		this.lavaTypes.Add(0, TileLibrary.lava0);
		this.lavaTypes.Add(1, TileLibrary.lava1);
		this.lavaTypes.Add(2, TileLibrary.lava2);
		this.lavaTypes.Add(3, TileLibrary.lava3);
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0005CB3A File Offset: 0x0005AD3A
	public void loadLavaTile(WorldTile pTile)
	{
		pTile.lavaTick = 0;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0005CB44 File Offset: 0x0005AD44
	public void increaseLava(WorldTile pTile)
	{
		if (pTile.Type.lavaLevel > this.maxLavaLevel)
		{
			return;
		}
		if (pTile.Type.lavaLevel < this.maxLavaLevel)
		{
			this.changeLavaTile(pTile, this.lavaTypes[pTile.Type.lavaLevel + 1]);
			return;
		}
		this.changeLavaTile(pTile, this.lavaTypes[this.maxLavaLevel]);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0005CBB0 File Offset: 0x0005ADB0
	public void decreaseLava(WorldTile pTile)
	{
		if (pTile.Type.lavaLevel > this.maxLavaLevel)
		{
			return;
		}
		if (pTile.Type.lavaLevel > 0)
		{
			this.changeLavaTile(pTile, this.lavaTypes[pTile.Type.lavaLevel - 1]);
			return;
		}
		this.putOut(pTile);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0005CC08 File Offset: 0x0005AE08
	public void addLava(WorldTile pTile, string pType = "lava3")
	{
		if (pTile.Type.lavaLevel >= this.maxLavaLevel)
		{
			return;
		}
		if (pTile.Type.explodable && pTile.Type.burnable)
		{
			this.stopLavaUpdate = true;
		}
		pTile.setFire(false);
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), AssetManager.terraform.get("lavaDamage"));
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0005CC72 File Offset: 0x0005AE72
	private void changeLavaTile(WorldTile pTile, TileType pType)
	{
		if (pTile.Type == pType)
		{
			return;
		}
		MapAction.terraformMain(pTile, pType, AssetManager.terraform.get("lavaDamage"));
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0005CC94 File Offset: 0x0005AE94
	private void changeLavaTile(WorldTile pTile, string pType)
	{
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), AssetManager.terraform.get("lavaDamage"));
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0005CCB6 File Offset: 0x0005AEB6
	private void lavaEffect(WorldTile pTile)
	{
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0005CCB8 File Offset: 0x0005AEB8
	public override void update(float pElapsed)
	{
		this.updateLava();
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0005CCC0 File Offset: 0x0005AEC0
	private void updateLava()
	{
		if (this.world.isPaused())
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= this.world.elapsed;
			return;
		}
		this.stopLavaUpdate = false;
		this.timer = 0.05f;
		this._lavaIslands.Clear();
		foreach (TileIsland tileIsland in this.world.islandsCalculator.islands)
		{
			if (tileIsland.type == TileLayerType.Lava)
			{
				this._lavaIslands.Add(tileIsland);
			}
		}
		foreach (TileIsland tileIsland2 in this._lavaIslands)
		{
			foreach (WorldTile worldTile in tileIsland2.regions.GetRandom().tiles)
			{
				if (!Toolbox.randomBool())
				{
					if (worldTile.building != null && worldTile.building.stats.affectedByLava)
					{
						worldTile.building.getHit(10f, true, AttackType.Other, null, true);
					}
					if (worldTile.Type.lavaLevel > 1)
					{
						if (worldTile.lavaTick > 0)
						{
							worldTile.lavaTick--;
						}
						else if (Toolbox.randomBool())
						{
							this.moveLava(worldTile);
							if (this.stopLavaUpdate)
							{
								break;
							}
							if (worldTile.flash_state <= 0 && Random.value > 0.995f)
							{
								worldTile.setFire(true);
							}
						}
					}
					else if (worldTile.Type.lavaLevel == 1 && !this.world.worldLaws.world_law_forever_lava.boolVal)
					{
						worldTile.lavaTick++;
						if (worldTile.lavaTick > 5)
						{
							this.changeLavaTile(worldTile, this.lavaTypes[0]);
						}
					}
					else if (worldTile.Type.lavaLevel == 0 && !this.world.worldLaws.world_law_forever_lava.boolVal)
					{
						worldTile.lavaTick++;
						if (worldTile.lavaTick > 10)
						{
							this.changeLavaTile(worldTile, "hills");
						}
					}
					else
					{
						worldTile.lavaTick = 0;
					}
				}
			}
		}
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0005CF7C File Offset: 0x0005B17C
	private bool moveLava(WorldTile pTile)
	{
		WorldTile worldTile = null;
		pTile.neighbours.ShuffleOne<WorldTile>();
		foreach (WorldTile worldTile2 in pTile.neighbours)
		{
			if (!worldTile2.Type.lava)
			{
				if (worldTile2.Type.frozen)
				{
					MapAction.unfreezeTile(worldTile2);
				}
				else
				{
					worldTile2.setFire(false);
				}
			}
			if (worldTile2.Type.render_z <= pTile.Type.render_z && worldTile2.Type.lavaLevel != pTile.Type.lavaLevel && (!worldTile2.Type.lava || worldTile2.Type.lavaLevel < this.maxLavaLevel) && (worldTile == null || worldTile.Type.render_z > worldTile2.Type.render_z))
			{
				worldTile = worldTile2;
			}
		}
		if (worldTile == null)
		{
			pTile.lavaTick = 30;
			return false;
		}
		int lavaLevel = pTile.Type.lavaLevel;
		if (pTile.Type.lavaLevel > 0)
		{
			this.changeLavaTile(pTile, this.lavaTypes[pTile.Type.lavaLevel - 1]);
		}
		if (worldTile.Type.lavaLevel == -1)
		{
			this.changeLavaTile(worldTile, this.lavaTypes[1]);
		}
		else
		{
			this.changeLavaTile(worldTile, this.lavaTypes[worldTile.Type.lavaLevel + 1]);
		}
		worldTile.lavaTick = 0;
		pTile.lavaTick = 0;
		this.world.flashEffects.flashPixel(worldTile, 10, ColorType.White);
		this.lavaEffect(worldTile);
		return true;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0005D124 File Offset: 0x0005B324
	public void putOut(WorldTile pTile)
	{
		MapAction.increaseTile(pTile, "flash");
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0005D131 File Offset: 0x0005B331
	public void removeLava(WorldTile pTile)
	{
		MapAction.decreaseTile(pTile, "flash");
	}

	// Token: 0x04000B06 RID: 2822
	public int maxLavaLevel = 3;

	// Token: 0x04000B07 RID: 2823
	public int maxTicksToRemove = 15;

	// Token: 0x04000B08 RID: 2824
	private bool stopLavaUpdate;

	// Token: 0x04000B09 RID: 2825
	private Dictionary<int, TileType> lavaTypes;

	// Token: 0x04000B0A RID: 2826
	private List<TileIsland> _lavaIslands = new List<TileIsland>();
}
