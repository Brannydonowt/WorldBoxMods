using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class ExplosionsEffects : MapLayer
{
	// Token: 0x060005DD RID: 1501 RVA: 0x00046BA4 File Offset: 0x00044DA4
	internal override void create()
	{
		this.colorValues = new Color(1f, 1f, 1f, 1f);
		this.colors_amount = 60;
		this.explosionQueue = new List<WorldTile>();
		this.explosionQueueCurrent = new List<WorldTile>();
		this.explosionDict = new Dictionary<WorldTile, TileTypeBase>();
		this.explosionDictCurrent = new Dictionary<WorldTile, TileTypeBase>();
		this.tileDictionary = new TileDictionary();
		base.create();
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00046C18 File Offset: 0x00044E18
	internal override void clear()
	{
		this.explosionQueue.Clear();
		this.explosionQueueCurrent.Clear();
		this.explosionDict.Clear();
		this.explosionDictCurrent.Clear();
		this.tileDictionary.clear();
		this.timedBombs.Clear();
		base.clear();
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00046C6D File Offset: 0x00044E6D
	internal void activateDelayedBomb(WorldTile pBomb)
	{
		if (!this.delayedBombs.Contains(pBomb))
		{
			this.delayedBombs.Add(pBomb);
			pBomb.delayedBombType = pBomb.Type.id;
			pBomb.delayedTimerBomb = 0.09f;
		}
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00046CA5 File Offset: 0x00044EA5
	internal void addTimedTnt(WorldTile pTile)
	{
		if (this.timedBombs.Contains(pTile))
		{
			return;
		}
		pTile.delayedTimerBomb = 5f;
		this.timedBombs.Add(pTile);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00046CD0 File Offset: 0x00044ED0
	internal void explodeBomb(WorldTile pBombTile, bool pForce = false)
	{
		if (this.bombDict.contains(pBombTile))
		{
			return;
		}
		if (pBombTile.Type.explodableDelayed && !pForce)
		{
			this.activateDelayedBomb(pBombTile);
			return;
		}
		this.world.startShake(0.3f, 0.01f, 2f, true, true);
		this.nextWave.Clear();
		this.nextWave.Enqueue(pBombTile);
		while (this.nextWave.Count > 0)
		{
			WorldTile worldTile = this.nextWave.Dequeue();
			this.bombDict.add(worldTile);
			if (worldTile.Type.explodable && !worldTile.Type.explodableDelayed)
			{
				worldTile.explosionWave = worldTile.Type.explodeRange;
				worldTile.explosionPower = worldTile.Type.explodeRange;
			}
			if (worldTile.explosionWave > 0)
			{
				for (int i = 0; i < worldTile.neighbours.Count; i++)
				{
					WorldTile worldTile2 = worldTile.neighbours[i];
					if (worldTile2.explosionWave > 0 && this.bombDict.contains(worldTile2))
					{
						if (worldTile2.explosionWave < worldTile.explosionWave && !worldTile2.Type.explodable)
						{
						}
					}
					else
					{
						this.bombDict.add(worldTile2);
						worldTile2.explosionWave = worldTile.explosionWave - 1;
						worldTile2.explosionPower = worldTile.explosionPower;
						this.nextWave.Enqueue(worldTile2);
					}
				}
			}
		}
		MapAction.pixelLimit = 0;
		if (this.bombDict.dict.Keys.Count < 20)
		{
			Sfx.play("explosion small", true, -1f, -1f);
		}
		else
		{
			Sfx.play("explosion medium", true, -1f, -1f);
		}
		foreach (WorldTile worldTile3 in this.bombDict.dict.Keys)
		{
			MapAction.explodeTile(worldTile3, (float)((worldTile3.explosionPower - worldTile3.explosionWave) * 10), (float)(worldTile3.explosionPower * 10), pBombTile, AssetManager.terraform.get("bomb"));
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00046F04 File Offset: 0x00045104
	public void prepareNewExplosion(WorldTile pTile)
	{
		if (!this.explosionDict.ContainsKey(pTile))
		{
			this.explosionQueue.Add(pTile);
			this.explosionDict.Add(pTile, pTile.Type);
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00046F34 File Offset: 0x00045134
	private void updateExplosionQueue()
	{
		if (this.timerExplosionQueue > 0f)
		{
			this.timerExplosionQueue -= this.world.elapsed;
			return;
		}
		this.timerExplosionQueue = 0.1f;
		if (this.explosionQueue.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.explosionQueue.Count; i++)
		{
			WorldTile worldTile = this.explosionQueue[i];
			this.explosionQueueCurrent.Add(worldTile);
			this.explosionDictCurrent.Add(worldTile, this.explosionDict[worldTile]);
		}
		this.explosionQueue.Clear();
		this.explosionDict.Clear();
		for (int j = 0; j < this.explosionQueueCurrent.Count; j++)
		{
			WorldTile worldTile2 = this.explosionQueueCurrent[j];
			MapAction.damageWorld(worldTile2, this.explosionDictCurrent[worldTile2].explodeRange, AssetManager.terraform.get("bomb"));
			Sfx.play("explosion medium", true, (float)worldTile2.pos.x, (float)worldTile2.pos.y);
		}
		this.explosionQueueCurrent.Clear();
		this.explosionDictCurrent.Clear();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00047068 File Offset: 0x00045268
	public override void update(float pElapsed)
	{
		this.checkAutoDisable();
		if (this.sprRnd || this.delayedBombs.Count > 0)
		{
			this.UpdateDirty(pElapsed);
		}
		if (this.timedBombs.Count > 0 && !this.world.isPaused())
		{
			int i = 0;
			while (i < this.timedBombs.Count)
			{
				WorldTile worldTile = this.timedBombs[i];
				if (worldTile.delayedTimerBomb > 0f)
				{
					worldTile.delayedTimerBomb -= pElapsed;
					i++;
				}
				else
				{
					this.timedBombs.RemoveAt(i);
					if (worldTile.Type.explodableTimed)
					{
						MapAction.damageWorld(worldTile, worldTile.Type.explodeRange, AssetManager.terraform.get("bomb"));
						Sfx.play("explosion medium", true, (float)worldTile.pos.x, (float)worldTile.pos.y);
					}
				}
			}
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00047168 File Offset: 0x00045368
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.delayedBombs.Count > 0)
		{
			int i = 0;
			while (i < this.delayedBombs.Count)
			{
				WorldTile worldTile = this.delayedBombs[i];
				worldTile.delayedTimerBomb -= this.world.elapsed;
				if (worldTile.delayedTimerBomb <= 0f)
				{
					worldTile.delayedTimerBomb = -100f;
					this.delayedBombs.Remove(worldTile);
					TileTypeBase tileTypeBase;
					if (!string.IsNullOrEmpty(worldTile.delayedBombType))
					{
						tileTypeBase = AssetManager.topTiles.get(worldTile.delayedBombType);
					}
					else
					{
						tileTypeBase = TopTileLibrary.tnt_timed;
					}
					MapAction.damageWorld(worldTile, tileTypeBase.explodeRange, AssetManager.terraform.get("bomb"));
					Sfx.play("explosion medium", true, (float)worldTile.pos.x, (float)worldTile.pos.y);
				}
				else
				{
					i++;
				}
			}
		}
		if (this.bombDict.Count() > 0)
		{
			foreach (WorldTile worldTile2 in this.bombDict.dict.Keys)
			{
				worldTile2.explosionWave = 0;
				worldTile2.explosionPower = 0;
			}
			this.bombDict.clear();
		}
		if (this.timer > 0f)
		{
			this.timer -= this.world.elapsed;
			return;
		}
		this.timer = this.interval;
		if (this.tileDictionary.Count() == 0)
		{
			return;
		}
		this.tilesToRemove.Clear();
		foreach (WorldTile worldTile3 in this.tileDictionary.dict.Keys)
		{
			if (worldTile3.explosionFxStage > 0)
			{
				if (Random.value > 0.5f)
				{
					this.pixels[worldTile3.data.tile_id] = Toolbox.clear;
				}
				else
				{
					this.pixels[worldTile3.data.tile_id] = this.colors[worldTile3.explosionFxStage - 1];
				}
				worldTile3.explosionFxStage--;
				if (worldTile3.explosionFxStage <= 0)
				{
					worldTile3.explosionFxStage = 0;
					this.tilesToRemove.Add(worldTile3);
				}
			}
		}
		if (this.tilesToRemove.Count > 0)
		{
			for (int j = 0; j < this.tilesToRemove.Count; j++)
			{
				WorldTile pTile = this.tilesToRemove[j];
				this.tileDictionary.remove(pTile, true);
			}
		}
		base.updatePixels();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00047440 File Offset: 0x00045640
	internal void setDirty(WorldTile pTile, float pDist, float pRadius)
	{
		int num = (int)(60f * (1f - pDist / pRadius));
		if (num == 0)
		{
			return;
		}
		if (num < pTile.explosionFxStage)
		{
			return;
		}
		this.tileDictionary.add(pTile);
		pTile.explosionFxStage = num;
	}

	// Token: 0x040007C3 RID: 1987
	private Dictionary<WorldTile, TileTypeBase> explosionDict;

	// Token: 0x040007C4 RID: 1988
	private Dictionary<WorldTile, TileTypeBase> explosionDictCurrent;

	// Token: 0x040007C5 RID: 1989
	public List<WorldTile> explosionQueue;

	// Token: 0x040007C6 RID: 1990
	private List<WorldTile> explosionQueueCurrent;

	// Token: 0x040007C7 RID: 1991
	private float timerExplosionQueue;

	// Token: 0x040007C8 RID: 1992
	public float interval = 0.01f;

	// Token: 0x040007C9 RID: 1993
	internal Queue<WorldTile> nextWave = new Queue<WorldTile>();

	// Token: 0x040007CA RID: 1994
	internal TileDictionary bombDict = new TileDictionary();

	// Token: 0x040007CB RID: 1995
	internal List<WorldTile> delayedBombs = new List<WorldTile>();

	// Token: 0x040007CC RID: 1996
	internal List<WorldTile> timedBombs = new List<WorldTile>();

	// Token: 0x040007CD RID: 1997
	private List<WorldTile> tilesToRemove = new List<WorldTile>();
}
