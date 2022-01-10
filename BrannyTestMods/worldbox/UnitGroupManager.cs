using System;
using System.Collections.Generic;

// Token: 0x02000146 RID: 326
public class UnitGroupManager
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x00055B7A File Offset: 0x00053D7A
	public UnitGroupManager(MapBox pWorld)
	{
		this.world = pWorld;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00055BAC File Offset: 0x00053DAC
	public void update(float pElapsed)
	{
		if (this.timer_check_dead > 0f)
		{
			this.timer_check_dead -= pElapsed;
			return;
		}
		this.timer_check_dead = this.interval_check_dead;
		int i = 0;
		while (i < this.groups.Count)
		{
			UnitGroup unitGroup = this.groups[i];
			unitGroup.update(pElapsed);
			if (!unitGroup.city.alive)
			{
				unitGroup.disband();
				this.groups.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00055C2C File Offset: 0x00053E2C
	public UnitGroup createNewGroup(City pCity)
	{
		UnitGroup unitGroup = new UnitGroup(pCity);
		UnitGroup unitGroup2 = unitGroup;
		int num = this.last_id;
		this.last_id = num + 1;
		unitGroup2.id = num;
		this.groups.Add(unitGroup);
		return unitGroup;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00055C64 File Offset: 0x00053E64
	public void clear()
	{
		foreach (UnitGroup unitGroup in this.groups)
		{
			unitGroup.clear();
		}
		this.groups.Clear();
	}

	// Token: 0x04000A2F RID: 2607
	private int last_id;

	// Token: 0x04000A30 RID: 2608
	private float interval_check_dead = 1f;

	// Token: 0x04000A31 RID: 2609
	private float timer_check_dead = 1f;

	// Token: 0x04000A32 RID: 2610
	public List<UnitGroup> groups = new List<UnitGroup>();

	// Token: 0x04000A33 RID: 2611
	private MapBox world;
}
