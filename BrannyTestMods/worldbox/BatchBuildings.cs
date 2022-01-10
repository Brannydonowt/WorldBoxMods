using System;
using System.Collections.Generic;

// Token: 0x020000DF RID: 223
public class BatchBuildings : Batch<Building>
{
	// Token: 0x060004B0 RID: 1200 RVA: 0x0003F3A0 File Offset: 0x0003D5A0
	protected override void createJobs()
	{
		base.addJob(null, new JobUpdater(this.prepare), JobType.Parallel);
		base.createJob(out this.c_main, new JobUpdater(this.updateScale), JobType.Parallel);
		base.createJob(out this.c_resource_shaker, new JobUpdater(this.updateResourceShaker), JobType.Parallel);
		base.createJob(out this.c_sprite_dirty, new JobUpdater(this.checkSpriteConstructor), JobType.Parallel);
		base.addUpdaterOnly(this.c_main, new JobUpdater(this.updateRenderer), JobType.Post);
		base.createJob(out this.c_tiles_dirty, new JobUpdater(this.updateTilesDirty), JobType.Post);
		base.createJob(out this.c_auto_remove, new JobUpdater(this.updateAutoRemove), JobType.Post);
		base.createJob(out this.c_stats_dirty, new JobUpdater(this.updateStatsDirty), JobType.Post);
		base.createJob(out this.c_components, new JobUpdater(this.updateComponents), JobType.Post);
		base.createJob(out this.c_shake, new JobUpdater(this.updateShake), JobType.Post);
		base.createJob(out this.c_position_dirty, new JobUpdater(this.updatePositionsDirty), JobType.Post);
		base.createJob(out this.c_status_effects, new JobUpdater(this.updateStatusEffects), JobType.Post);
		this.main = this.c_main;
		this.applyParallelResults = (JobUpdater)Delegate.Combine(this.applyParallelResults, new JobUpdater(this.applyParallelScale));
		this.clearParallelResults = (JobUpdater)Delegate.Combine(this.clearParallelResults, new JobUpdater(this.clearParallelScale));
		this.applyParallelResults = (JobUpdater)Delegate.Combine(this.applyParallelResults, new JobUpdater(this.applyParallelSprites));
		this.clearParallelResults = (JobUpdater)Delegate.Combine(this.clearParallelResults, new JobUpdater(this.clearParallelSprites));
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0003F56C File Offset: 0x0003D76C
	private void updateScale()
	{
		if (!base.check(this.c_main))
		{
			return;
		}
		for (int i = 0; i < this._list.Count; i++)
		{
			this._building = this._list[i];
			this._building.updateScale(this._elapsed);
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
	private void updateRenderer()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		bool renderBuildings = this.world.qualityChanger.renderBuildings;
		if (this._last_enabled != renderBuildings)
		{
			this._render_dirty = true;
		}
		if (this._render_dirty)
		{
			this._last_enabled = renderBuildings;
			this._render_dirty = false;
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				this._building.spriteRenderer.enabled = renderBuildings;
			}
		}
		this._cur_container.checkAddRemove();
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0003F65C File Offset: 0x0003D85C
	private void applyParallelScale()
	{
		for (int i = 0; i < this.l_parallel_scale.Count; i++)
		{
			this.l_parallel_scale[i].applyScale();
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0003F690 File Offset: 0x0003D890
	private void clearParallelScale()
	{
		this.l_parallel_scale.Clear();
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0003F6A0 File Offset: 0x0003D8A0
	private void updateTilesDirty()
	{
		if (base.check(this._cur_container))
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				this._building.checkDirtyTiles();
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0003F6FC File Offset: 0x0003D8FC
	private void updateAutoRemove()
	{
		if (base.check(this._cur_container))
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				this._building.updateAutoRemove(this._elapsed);
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0003F75C File Offset: 0x0003D95C
	private void updateStatsDirty()
	{
		if (base.check(this._cur_container))
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				this._building.updateStats();
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0003F7B8 File Offset: 0x0003D9B8
	private void checkSpriteConstructor()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		for (int i = 0; i < this._list.Count; i++)
		{
			this._building = this._list[i];
			this._building.checkSpriteConstructor();
		}
		this._cur_container.checkAddRemove();
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0003F814 File Offset: 0x0003DA14
	private void applyParallelSprites()
	{
		for (int i = 0; i < this.l_parallel_update_sprites.Count; i++)
		{
			this.l_parallel_update_sprites[i].applyConstructedSprite();
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0003F848 File Offset: 0x0003DA48
	private void clearParallelSprites()
	{
		this.l_parallel_update_sprites.Clear();
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0003F858 File Offset: 0x0003DA58
	private void updateComponents()
	{
		if (base.check(this._cur_container) && !this.world._isPaused)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				if (this._building.data.alive)
				{
					this._building.updateComponents(this._elapsed);
				}
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0003F8D8 File Offset: 0x0003DAD8
	private void updateResourceShaker()
	{
		if (base.check(this._cur_container) && !this.world._isPaused)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				if (this._building.data.alive)
				{
					this._building.updateTimerShakeResources(this._elapsed);
				}
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0003F958 File Offset: 0x0003DB58
	private void updateShake()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		for (int i = 0; i < this._list.Count; i++)
		{
			this._building = this._list[i];
			this._building.updateShake(this._elapsed);
		}
		this._cur_container.checkAddRemove();
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
	private void updatePositionsDirty()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		for (int i = 0; i < this._list.Count; i++)
		{
			this._building = this._list[i];
			this._building.updatePosition();
		}
		this._cur_container.checkAddRemove();
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0003FA14 File Offset: 0x0003DC14
	private void updateStatusEffects()
	{
		if (base.check(this._cur_container) && !this.world._isPaused)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				this._building = this._list[i];
				this._building.updateStatusEffects(this._elapsed);
			}
			this._cur_container.checkAddRemove();
		}
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0003FA80 File Offset: 0x0003DC80
	internal override void add(Building pBuilding)
	{
		base.add(pBuilding);
		pBuilding.batch = this;
		this._render_dirty = true;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0003FA97 File Offset: 0x0003DC97
	internal override void remove(Building pObject)
	{
		base.remove(pObject);
		pObject.batch = null;
	}

	// Token: 0x040006D6 RID: 1750
	public int zone_id;

	// Token: 0x040006D7 RID: 1751
	public TileZone zone;

	// Token: 0x040006D8 RID: 1752
	public bool dirty;

	// Token: 0x040006D9 RID: 1753
	public ObjectContainer<Building> c_main;

	// Token: 0x040006DA RID: 1754
	public ObjectContainer<Building> c_components;

	// Token: 0x040006DB RID: 1755
	public ObjectContainer<Building> c_resource_shaker;

	// Token: 0x040006DC RID: 1756
	public ObjectContainer<Building> c_shake;

	// Token: 0x040006DD RID: 1757
	public ObjectContainer<Building> c_position_dirty;

	// Token: 0x040006DE RID: 1758
	public ObjectContainer<Building> c_status_effects;

	// Token: 0x040006DF RID: 1759
	public ObjectContainer<Building> c_tiles_dirty;

	// Token: 0x040006E0 RID: 1760
	public ObjectContainer<Building> c_sprite_dirty;

	// Token: 0x040006E1 RID: 1761
	public ObjectContainer<Building> c_stats_dirty;

	// Token: 0x040006E2 RID: 1762
	public ObjectContainer<Building> c_auto_remove;

	// Token: 0x040006E3 RID: 1763
	internal List<Building> l_parallel_scale = new List<Building>();

	// Token: 0x040006E4 RID: 1764
	internal List<Building> l_parallel_update_sprites = new List<Building>();

	// Token: 0x040006E5 RID: 1765
	private Building _building;

	// Token: 0x040006E6 RID: 1766
	private bool _render_dirty;

	// Token: 0x040006E7 RID: 1767
	private bool _last_enabled;
}
