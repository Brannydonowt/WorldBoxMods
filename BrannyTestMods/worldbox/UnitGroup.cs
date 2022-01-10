using System;
using System.Collections.Generic;

// Token: 0x02000145 RID: 325
public class UnitGroup
{
	// Token: 0x0600079C RID: 1948 RVA: 0x00055818 File Offset: 0x00053A18
	public UnitGroup(City pCity)
	{
		this.city = pCity;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00055839 File Offset: 0x00053A39
	public void clear()
	{
		this.units.Clear();
		this.setGroupLeader(null);
		this.alive = false;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00055854 File Offset: 0x00053A54
	public void disband()
	{
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			simpleList[i].removeFromGroup();
		}
		this.clear();
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00055890 File Offset: 0x00053A90
	private void setGroupLeader(Actor pActor)
	{
		if (pActor == null && this.groupLeader != null)
		{
			this.groupLeader.setGroupLeader(false);
		}
		this.groupLeader = pActor;
		if (this.groupLeader != null)
		{
			this.groupLeader.setGroupLeader(true);
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000558E4 File Offset: 0x00053AE4
	public void update(float pElapsed)
	{
		this.units.checkAddRemove();
		if (this.groupLeader != null && !this.groupLeader.data.alive)
		{
			this._prev_leader_position = this.groupLeader.currentTile;
			this.setGroupLeader(null);
		}
		this.findGroupLeader();
		this.checkDeadUnits();
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00055940 File Offset: 0x00053B40
	private void findGroupLeader()
	{
		if (this.groupLeader != null)
		{
			if (this.groupLeader.kingdom.isCiv())
			{
				return;
			}
			this.setGroupLeader(null);
		}
		if (this.units.Count == 0)
		{
			return;
		}
		if (this._prev_leader_position == null)
		{
			this.setGroupLeader(this.units.GetRandom());
			return;
		}
		Actor x = null;
		float num = 0f;
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.data.alive)
			{
				float num2 = Toolbox.DistTile(actor.currentTile, this._prev_leader_position);
				if (x == null || num2 < num)
				{
					x = actor;
					num = num2;
				}
			}
		}
		if (x != null)
		{
			this.setGroupLeader(x);
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00055A1C File Offset: 0x00053C1C
	public void checkDeadUnits()
	{
		UnitGroup._units_to_remove.Clear();
		List<Actor> simpleList = this.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (!actor.data.alive)
			{
				UnitGroup._units_to_remove.Add(actor);
			}
		}
		for (int j = 0; j < UnitGroup._units_to_remove.Count; j++)
		{
			Actor pObject = UnitGroup._units_to_remove[j];
			this.units.Remove(pObject);
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00055A9F File Offset: 0x00053C9F
	public bool isAlive()
	{
		return this.alive;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00055AA7 File Offset: 0x00053CA7
	public void addUnit(Actor pActor)
	{
		this.units.Add(pActor);
		pActor.unitGroup = this;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00055ABC File Offset: 0x00053CBC
	public void removeUnit(Actor pActor)
	{
		this.units.Remove(pActor);
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00055ACA File Offset: 0x00053CCA
	public int countUnits()
	{
		return this.units.Count;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00055AD8 File Offset: 0x00053CD8
	public string getDebug()
	{
		string text = this.units.Count.ToString() ?? "";
		if (this.groupLeader != null)
		{
			text = string.Concat(new string[]
			{
				text,
				" ",
				this.groupLeader.data.firstName,
				"(",
				this.groupLeader.data.age.ToString(),
				")"
			});
		}
		return text;
	}

	// Token: 0x04000A27 RID: 2599
	private static List<WorldTile> _tiles = new List<WorldTile>();

	// Token: 0x04000A28 RID: 2600
	private static List<Actor> _units_to_remove = new List<Actor>();

	// Token: 0x04000A29 RID: 2601
	private ActorContainer units = new ActorContainer();

	// Token: 0x04000A2A RID: 2602
	public int id;

	// Token: 0x04000A2B RID: 2603
	public Actor groupLeader;

	// Token: 0x04000A2C RID: 2604
	private WorldTile _prev_leader_position;

	// Token: 0x04000A2D RID: 2605
	private bool alive = true;

	// Token: 0x04000A2E RID: 2606
	public City city;
}
