using System;
using System.Collections.Generic;

// Token: 0x0200017B RID: 379
public class KingdomManager
{
	// Token: 0x0600088C RID: 2188 RVA: 0x0005C434 File Offset: 0x0005A634
	public KingdomManager(MapBox pWorld)
	{
		this.world = pWorld;
		this.diplomacyManager = new DiplomacyManager(this.world, this);
		this.dict_hidden = new Dictionary<string, Kingdom>();
		this.list = new List<Kingdom>();
		this.list_civs = new List<Kingdom>();
		this.list_hidden = new List<Kingdom>();
		foreach (KingdomAsset pAsset in AssetManager.kingdoms.list)
		{
			this.newHiddenKingdom(pAsset);
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0005C4D8 File Offset: 0x0005A6D8
	public List<Kingdom> save()
	{
		foreach (Kingdom kingdom in this.list_civs)
		{
			kingdom.save();
		}
		return this.list_civs;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0005C530 File Offset: 0x0005A730
	public void setDiplomacyState(string k1, string k2, DiplomacyState pState)
	{
		this.setDiplomacyState(this.getKingdomByID(k1), this.getKingdomByID(k2), pState);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0005C548 File Offset: 0x0005A748
	public void setDiplomacyState(Kingdom k1, Kingdom k2, DiplomacyState pState)
	{
		this.diplomacyManager.getRelation(k1, k2).state = pState;
		k1.setDiplomacyState(k2, DiplomacyState.Clear);
		k2.setDiplomacyState(k1, DiplomacyState.Clear);
		if (pState == DiplomacyState.Ally)
		{
			k1.setDiplomacyState(k2, pState);
			k2.setDiplomacyState(k1, pState);
		}
		else if (pState == DiplomacyState.War)
		{
			k1.setDiplomacyState(k2, pState);
			k2.setDiplomacyState(k1, pState);
		}
		ZoneCalculator zoneCalculator = this.world.zoneCalculator;
		if (zoneCalculator == null)
		{
			return;
		}
		zoneCalculator.setDrawnZonesDirty();
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
	private Kingdom newHiddenKingdom(KingdomAsset pAsset)
	{
		Kingdom kingdom = new Kingdom();
		kingdom.asset = pAsset;
		kingdom.createHidden();
		kingdom.id = pAsset.id;
		kingdom.name = pAsset.id;
		this.addKingdom(kingdom, false);
		return kingdom;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0005C5FC File Offset: 0x0005A7FC
	public void clear()
	{
		this.list_civs.Clear();
		this.list.Clear();
		this.diplomacyManager.clear();
		foreach (Kingdom kingdom in this.list_hidden)
		{
			kingdom.clear();
		}
		this.list.AddRange(this.list_hidden);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0005C680 File Offset: 0x0005A880
	public Kingdom getKingdomByID(string pID)
	{
		if (this.dict_hidden.ContainsKey(pID))
		{
			return this.dict_hidden[pID];
		}
		foreach (Kingdom kingdom in this.list_civs)
		{
			if (kingdom.id == pID)
			{
				return kingdom;
			}
		}
		return null;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0005C6FC File Offset: 0x0005A8FC
	public void checkKingdoms()
	{
		int i = 0;
		while (i < this.list_civs.Count)
		{
			Kingdom kingdom = this.list_civs[i];
			if (kingdom.cities.Count == 0 && kingdom.getPopulationTotal() == 0)
			{
				this.list_civs.Remove(kingdom);
			}
			else
			{
				i++;
			}
		}
		foreach (Kingdom kingdom2 in this.list_civs)
		{
			foreach (Kingdom kingdom3 in this.list_civs)
			{
				if (kingdom2 != kingdom3 && !kingdom2.allies.ContainsKey(kingdom3))
				{
					this.setDiplomacyState(kingdom2, kingdom3, DiplomacyState.War);
				}
			}
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0005C7EC File Offset: 0x0005A9EC
	public Kingdom makeNewCivKingdom(City pCity, Race pRace = null, string pID = null, bool pLog = true)
	{
		Kingdom kingdom = new Kingdom();
		kingdom.createAI();
		if (pCity != null && pRace == null)
		{
			pRace = pCity.race;
		}
		if (pRace != null)
		{
			kingdom.createCiv(pRace, pID);
		}
		if (pCity != null)
		{
			pCity.setKingdom(kingdom);
		}
		this.addKingdom(kingdom, true);
		if (pLog)
		{
			WorldLog.logNewKingdom(kingdom);
		}
		this.diplomacyManager.makeAllFriendlyTo(kingdom);
		return kingdom;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0005C854 File Offset: 0x0005AA54
	public void addKingdom(Kingdom pKingdom, bool pCiv = false)
	{
		this.list.Add(pKingdom);
		pKingdom.hashcode = pKingdom.GetHashCode();
		if (pCiv)
		{
			this.list_civs.Add(pKingdom);
		}
		else
		{
			this.dict_hidden.Add(pKingdom.id, pKingdom);
			this.list_hidden.Add(pKingdom);
		}
		ZoneCalculator zoneCalculator = this.world.zoneCalculator;
		if (zoneCalculator == null)
		{
			return;
		}
		zoneCalculator.setDrawnZonesDirty();
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0005C8C0 File Offset: 0x0005AAC0
	public void update(float pElapsed)
	{
		foreach (Kingdom kingdom in this.list)
		{
			kingdom.update(pElapsed);
		}
		if (this.world.isPaused())
		{
			return;
		}
		this.updateCivKingdoms(pElapsed);
		this.diplomacyManager.update(pElapsed);
		MapMarks.drawArrows();
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0005C938 File Offset: 0x0005AB38
	private void updateCivKingdoms(float pElapsed)
	{
		int i = 0;
		while (i < this.list_civs.Count)
		{
			Kingdom kingdom = this.list_civs[i];
			kingdom.updateCiv(pElapsed);
			if (kingdom.alive)
			{
				i++;
			}
		}
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0005C978 File Offset: 0x0005AB78
	public void updateAge()
	{
		for (int i = 0; i < this.list_civs.Count; i++)
		{
			this.list_civs[i].updateAge();
		}
		this.diplomacyManager.updateAge();
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0005C9B7 File Offset: 0x0005ABB7
	public Kingdom getRandom()
	{
		if (this.list_civs.Count == 0)
		{
			return null;
		}
		this.list_civs.ShuffleOne<Kingdom>();
		return this.list_civs[0];
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0005C9E0 File Offset: 0x0005ABE0
	public void destroyKingdom(Kingdom pKingdom)
	{
		this.diplomacyManager.removeRelations(pKingdom);
		this.list.Remove(pKingdom);
		this.list_civs.Remove(pKingdom);
		if (this.world.isSelectedPower("relations") && Config.selectedKingdom == pKingdom)
		{
			this.world.selectedButtons.unselectAll();
		}
		if (pKingdom.alive)
		{
			pKingdom.alive = false;
			WorldLog.logKingdomDestroyed(pKingdom);
		}
		foreach (Kingdom kingdom in this.list)
		{
			if (kingdom.enemies.ContainsKey(pKingdom))
			{
				kingdom.setDiplomacyState(pKingdom, DiplomacyState.Clear);
			}
			if (kingdom.allies.ContainsKey(pKingdom))
			{
				kingdom.setDiplomacyState(pKingdom, DiplomacyState.Clear);
			}
		}
		pKingdom.makeSurvivorsToNomads();
		ZoneCalculator zoneCalculator = this.world.zoneCalculator;
		if (zoneCalculator == null)
		{
			return;
		}
		zoneCalculator.setDrawnZonesDirty();
	}

	// Token: 0x04000B00 RID: 2816
	public Dictionary<string, Kingdom> dict_hidden;

	// Token: 0x04000B01 RID: 2817
	public readonly List<Kingdom> list;

	// Token: 0x04000B02 RID: 2818
	public readonly List<Kingdom> list_civs;

	// Token: 0x04000B03 RID: 2819
	public readonly List<Kingdom> list_hidden;

	// Token: 0x04000B04 RID: 2820
	private readonly MapBox world;

	// Token: 0x04000B05 RID: 2821
	public DiplomacyManager diplomacyManager;
}
