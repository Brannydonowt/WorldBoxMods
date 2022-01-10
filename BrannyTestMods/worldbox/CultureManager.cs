using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class CultureManager
{
	// Token: 0x06000825 RID: 2085 RVA: 0x0005966D File Offset: 0x0005786D
	public CultureManager(MapBox pWorld)
	{
		this.list = new List<Culture>();
		CultureManager.instance = this;
		this.dict = new Dictionary<string, Culture>();
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000596A8 File Offset: 0x000578A8
	public void testCulture()
	{
		Culture culture = new Culture();
		Race pRace = AssetManager.raceLibrary.get("elf");
		culture.create(pRace, null);
		for (int i = 0; i < 100; i++)
		{
			culture.researchNewTechTest();
		}
		string text = "";
		for (int j = 0; j < culture.list_tech_ids.Count; j++)
		{
			text = text + culture.list_tech_ids[j] + ",";
		}
		Debug.Log(text);
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00059728 File Offset: 0x00057928
	public void updateProgress()
	{
		foreach (Culture culture in this.list)
		{
			culture.updateProgress();
		}
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00059778 File Offset: 0x00057978
	public void update(float pElapsed)
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].update(pElapsed);
		}
		this.updateRecalcValues(pElapsed);
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x000597B4 File Offset: 0x000579B4
	private void updateRecalcValues(float pElapsed)
	{
		if (this._timer_recalc_values > 0f)
		{
			this._timer_recalc_values -= pElapsed;
			return;
		}
		this.recalcCultureValues();
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000597D8 File Offset: 0x000579D8
	private void removeDeadCultures()
	{
		this._to_remove.Clear();
		foreach (Culture culture in this.list)
		{
			if (culture.followers == 0 && culture.cities == 0 && culture.kingdoms == 0)
			{
				this._to_remove.Add(culture);
			}
		}
		foreach (Culture culture2 in this._to_remove)
		{
			culture2.clearZones();
			this.list.Remove(culture2);
			this.dict.Remove(culture2.id);
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x000598B4 File Offset: 0x00057AB4
	public void recalcCultureValues()
	{
		this._timer_recalc_values = 3f;
		foreach (Culture culture in this.list)
		{
			culture.reset();
		}
		foreach (Kingdom kingdom in this.world.kingdoms.list)
		{
			if (!string.IsNullOrEmpty(kingdom.cultureID))
			{
				this.countKingdom(kingdom);
			}
			foreach (City city in kingdom.cities)
			{
				this.countCity(city);
				foreach (ActorData actorData in city.data.popPoints)
				{
					this.countUnit(actorData.status.culture);
				}
			}
		}
		foreach (Actor actor in this.world.units)
		{
			this.countUnit(actor.data.culture);
		}
		foreach (Culture culture2 in this.list)
		{
			culture2.calculateKnowledgeGain();
		}
		this.removeDeadCultures();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00059AA0 File Offset: 0x00057CA0
	private void countKingdom(Kingdom pKingdom)
	{
		Culture culture = this.get(pKingdom.cultureID);
		if (culture == null)
		{
			return;
		}
		culture.kingdoms++;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00059ACC File Offset: 0x00057CCC
	private void countCity(City pCity)
	{
		Culture culture = this.get(pCity.data.culture);
		if (culture == null)
		{
			return;
		}
		culture.cities++;
		culture._list_cities.Add(pCity);
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00059B0C File Offset: 0x00057D0C
	private void countUnit(string pID)
	{
		Culture culture = this.get(pID);
		if (culture == null)
		{
			return;
		}
		culture.followers++;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00059B34 File Offset: 0x00057D34
	public Culture get(string pID)
	{
		if (string.IsNullOrEmpty(pID))
		{
			return null;
		}
		Culture result = null;
		this.dict.TryGetValue(pID, ref result);
		return result;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00059B60 File Offset: 0x00057D60
	public string getCultureName(string pID)
	{
		string empty = string.Empty;
		if (string.IsNullOrEmpty(pID))
		{
			return empty;
		}
		return this.get(pID).name;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00059B8C File Offset: 0x00057D8C
	public List<Culture> save()
	{
		foreach (Culture culture in this.list)
		{
			culture.prepareForSave();
		}
		return this.list;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00059BE4 File Offset: 0x00057DE4
	public Culture newCulture(Race pRace, City pCity)
	{
		Culture culture = new Culture();
		culture.create(pRace, pCity);
		this.add(culture);
		return culture;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00059C07 File Offset: 0x00057E07
	public void loadCulture(Culture pCulture)
	{
		pCulture.load();
		this.add(pCulture);
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00059C16 File Offset: 0x00057E16
	public void clear()
	{
		this.dict.Clear();
		this.list.Clear();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00059C2E File Offset: 0x00057E2E
	private void add(Culture pCulture)
	{
		this.list.Add(pCulture);
		this.dict.Add(pCulture.id, pCulture);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00059C50 File Offset: 0x00057E50
	public bool isColorUsed(string pColor)
	{
		using (List<Culture>.Enumerator enumerator = this.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.color == pColor)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04000AA0 RID: 2720
	public static CultureManager instance;

	// Token: 0x04000AA1 RID: 2721
	private readonly MapBox world = MapBox.instance;

	// Token: 0x04000AA2 RID: 2722
	public readonly List<Culture> list;

	// Token: 0x04000AA3 RID: 2723
	public Dictionary<string, Culture> dict;

	// Token: 0x04000AA4 RID: 2724
	private float _timer_recalc_values;

	// Token: 0x04000AA5 RID: 2725
	private List<Culture> _to_remove = new List<Culture>();
}
