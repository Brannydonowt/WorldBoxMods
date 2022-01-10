using System;
using System.Collections.Generic;

// Token: 0x020000C5 RID: 197
public class BuildingCreepHUB : BaseBuildingComponent
{
	// Token: 0x06000432 RID: 1074 RVA: 0x0003C288 File Offset: 0x0003A488
	internal override void create()
	{
		base.create();
		for (int i = 0; i < this.building.stats.grow_creep_workers; i++)
		{
			this._workers.Add(new BuildingCreepWorker(this));
		}
		this._interval = this.building.stats.grow_creep_step_inteval;
		this._timer = this._interval;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x0003C2EC File Offset: 0x0003A4EC
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._timer > 0f)
		{
			this._timer -= pElapsed;
			return;
		}
		this._timer = this._interval;
		for (int i = 0; i < this._workers.Count; i++)
		{
			this._workers[i].update();
		}
	}

	// Token: 0x04000657 RID: 1623
	private float _interval = 0.1f;

	// Token: 0x04000658 RID: 1624
	private float _timer;

	// Token: 0x04000659 RID: 1625
	private List<BuildingCreepWorker> _workers = new List<BuildingCreepWorker>();
}
