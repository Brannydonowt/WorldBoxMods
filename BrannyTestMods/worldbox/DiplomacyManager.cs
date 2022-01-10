using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000171 RID: 369
public class DiplomacyManager
{
	// Token: 0x0600084A RID: 2122 RVA: 0x0005A74C File Offset: 0x0005894C
	public DiplomacyManager(MapBox pWorld, KingdomManager pKingdoms)
	{
		this.world = pWorld;
		this.kingdoms = pKingdoms;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0005A79C File Offset: 0x0005899C
	public List<DiplomacyRelation> save()
	{
		List<DiplomacyRelation> list = new List<DiplomacyRelation>();
		foreach (DiplomacyRelation diplomacyRelation in this.relations_civs.Values)
		{
			diplomacyRelation.kingdom1 = this.kingdoms.getKingdomByID(diplomacyRelation.kingdom1_id);
			diplomacyRelation.kingdom2 = this.kingdoms.getKingdomByID(diplomacyRelation.kingdom2_id);
			if (diplomacyRelation.kingdom1 != null && diplomacyRelation.kingdom2 != null)
			{
				list.Add(diplomacyRelation);
			}
		}
		return list;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0005A83C File Offset: 0x00058A3C
	public void load(List<DiplomacyRelation> pList)
	{
		foreach (DiplomacyRelation diplomacyRelation in pList)
		{
			diplomacyRelation.kingdom1 = this.kingdoms.getKingdomByID(diplomacyRelation.kingdom1_id);
			diplomacyRelation.kingdom2 = this.kingdoms.getKingdomByID(diplomacyRelation.kingdom2_id);
			if (diplomacyRelation.kingdom1 != null && diplomacyRelation.kingdom2 != null)
			{
				this.addToDicts(diplomacyRelation);
			}
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0005A8C8 File Offset: 0x00058AC8
	public void addToDicts(DiplomacyRelation pRelation)
	{
		if (!this.relations_dict.ContainsKey(pRelation.id))
		{
			this.relations_dict.Add(pRelation.id, pRelation);
		}
		if (pRelation.kingdom1.isCiv() && pRelation.kingdom2.isCiv() && !this.relations_civs.ContainsKey(pRelation.id))
		{
			this.relations_civs.Add(pRelation.id, pRelation);
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0005A939 File Offset: 0x00058B39
	public void update(float pElapsed)
	{
		if (this.diplomacyTick > 0f)
		{
			this.diplomacyTick -= pElapsed;
			return;
		}
		this.diplomacyTick = 2f;
		this.newDiplomacyTick();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0005A968 File Offset: 0x00058B68
	public void updateAge()
	{
		foreach (DiplomacyRelation diplomacyRelation in this.relations_civs.Values)
		{
			diplomacyRelation.stateChanged++;
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0005A9C8 File Offset: 0x00058BC8
	public void newDiplomacyTick()
	{
		this.findSupremeKingdom();
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0005A9D0 File Offset: 0x00058BD0
	private void findSupremeKingdom()
	{
		DiplomacyManager.kingdom_supreme = null;
		if (this.kingdoms.list_civs.Count == 0)
		{
			return;
		}
		foreach (Kingdom kingdom in this.kingdoms.list_civs)
		{
			kingdom.power = kingdom.countArmy() * 2 + kingdom.cities.Count * 5;
		}
		this.kingdoms.list_civs.Sort(new Comparison<Kingdom>(this.sortByPower));
		DiplomacyManager.kingdom_supreme = this.kingdoms.list_civs[0];
		if (this.kingdoms.list_civs.Count > 1)
		{
			DiplomacyManager.kingdom_second = this.kingdoms.list_civs[1];
			return;
		}
		DiplomacyManager.kingdom_second = null;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0005AAB8 File Offset: 0x00058CB8
	public int sortByPower(Kingdom o1, Kingdom o2)
	{
		return o2.power.CompareTo(o1.power);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0005AACB File Offset: 0x00058CCB
	internal void startWar(Kingdom pKingdom, Kingdom tTarget, bool pForced = false)
	{
		if (!pForced)
		{
			WorldLog.logNewWar(pKingdom, tTarget);
		}
		this.kingdoms.setDiplomacyState(pKingdom, tTarget, DiplomacyState.War);
		DiplomacyRelation relation = this.getRelation(pKingdom, tTarget);
		relation.warSince = this.world.mapStats.year;
		relation.stateChanged = 0;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0005AB0C File Offset: 0x00058D0C
	internal void startPeace(Kingdom pKingdom, Kingdom tTarget, bool pForced = false)
	{
		if (!pForced)
		{
			WorldLog.logNewPeace(pKingdom, tTarget);
			MapBox.instance.spawnPeaceFireworks(pKingdom);
			MapBox.instance.spawnPeaceFireworks(tTarget);
		}
		this.kingdoms.setDiplomacyState(pKingdom, tTarget, DiplomacyState.Ally);
		DiplomacyRelation relation = this.getRelation(pKingdom, tTarget);
		relation.peaceSince = this.world.mapStats.year;
		relation.stateChanged = 0;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0005AB6C File Offset: 0x00058D6C
	public void eventSpite(Kingdom pKingdom)
	{
		if (pKingdom.civs_allies.Count == 0)
		{
			return;
		}
		this._tempKingdoms.Clear();
		foreach (Kingdom kingdom in pKingdom.civs_allies.Keys)
		{
			this._tempKingdoms.Add(kingdom);
		}
		this.startWar(pKingdom, this._tempKingdoms.GetRandom<Kingdom>(), false);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0005ABF8 File Offset: 0x00058DF8
	public void eventFriendship(Kingdom pKingdom)
	{
		if (pKingdom.civs_enemies.Count == 0)
		{
			return;
		}
		this._tempKingdoms.Clear();
		foreach (Kingdom kingdom in pKingdom.civs_enemies.Keys)
		{
			this._tempKingdoms.Add(kingdom);
		}
		this.startPeace(pKingdom, this._tempKingdoms.GetRandom<Kingdom>(), false);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0005AC84 File Offset: 0x00058E84
	public KingdomOpinion getOpinion(Kingdom k1, Kingdom k2)
	{
		return this.getRelation(k1, k2).getOpinion(k1, k2);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0005AC98 File Offset: 0x00058E98
	public DiplomacyRelation getRelation(Kingdom k1, Kingdom k2)
	{
		this.temp_arr[0] = k1;
		this.temp_arr[1] = k2;
		Array.Sort<Kingdom>(this.temp_arr, (Kingdom x, Kingdom y) => string.Compare(x.id, y.id));
		string text = this.temp_arr[0].id + "_" + this.temp_arr[1].id;
		if (this.relations_dict.ContainsKey(text))
		{
			return this.relations_dict[text];
		}
		DiplomacyRelation diplomacyRelation = new DiplomacyRelation();
		diplomacyRelation.id = text;
		diplomacyRelation.kingdom1_id = this.temp_arr[0].id;
		diplomacyRelation.kingdom2_id = this.temp_arr[1].id;
		diplomacyRelation.kingdom1 = k1;
		diplomacyRelation.kingdom2 = k2;
		this.addToDicts(diplomacyRelation);
		return diplomacyRelation;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0005AD6C File Offset: 0x00058F6C
	public void makeAllFriendlyTo(Kingdom pKingdom)
	{
		foreach (Kingdom kingdom in this.kingdoms.list_civs)
		{
			if (kingdom != pKingdom && pKingdom.isEnemy(kingdom))
			{
				this.startPeace(pKingdom, kingdom, true);
			}
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0005ADD4 File Offset: 0x00058FD4
	public void allPeace()
	{
		foreach (Kingdom pKingdom in this.kingdoms.list_civs)
		{
			this.makeAllFriendlyTo(pKingdom);
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0005AE2C File Offset: 0x0005902C
	public void clear()
	{
		this.relations_dict.Clear();
		this.relations_civs.Clear();
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0005AE44 File Offset: 0x00059044
	public void removeRelations(Kingdom pKingdom)
	{
		int i = 0;
		List<DiplomacyRelation> list = Enumerable.ToList<DiplomacyRelation>(this.relations_dict.Values);
		while (i < list.Count)
		{
			DiplomacyRelation diplomacyRelation = list[i];
			if (diplomacyRelation.kingdom1 == pKingdom || diplomacyRelation.kingdom2 == pKingdom)
			{
				this.relations_dict.Remove(diplomacyRelation.id);
				this.relations_civs.Remove(diplomacyRelation.id);
			}
			i++;
		}
	}

	// Token: 0x04000ACA RID: 2762
	private readonly KingdomManager kingdoms;

	// Token: 0x04000ACB RID: 2763
	private readonly MapBox world;

	// Token: 0x04000ACC RID: 2764
	private Dictionary<string, DiplomacyRelation> relations_dict = new Dictionary<string, DiplomacyRelation>();

	// Token: 0x04000ACD RID: 2765
	private Dictionary<string, DiplomacyRelation> relations_civs = new Dictionary<string, DiplomacyRelation>();

	// Token: 0x04000ACE RID: 2766
	private List<Kingdom> _tempKingdoms = new List<Kingdom>();

	// Token: 0x04000ACF RID: 2767
	private Kingdom[] temp_arr = new Kingdom[2];

	// Token: 0x04000AD0 RID: 2768
	public static Kingdom kingdom_supreme;

	// Token: 0x04000AD1 RID: 2769
	public static Kingdom kingdom_second;

	// Token: 0x04000AD2 RID: 2770
	private float diplomacyTick;
}
