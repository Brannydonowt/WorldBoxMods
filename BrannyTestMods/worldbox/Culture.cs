using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000134 RID: 308
[Serializable]
public class Culture
{
	// Token: 0x06000718 RID: 1816 RVA: 0x00051004 File Offset: 0x0004F204
	public void create(Race pRace, City pCity)
	{
		this.race = pRace.id;
		this.list_tech_ids = new List<string>();
		this.id = MapBox.instance.mapStats.getNextId("culture");
		NameGeneratorAsset pAsset = AssetManager.nameGenerator.get(pRace.name_template_culture);
		this.name = NameGenerator.generateNameFromTemplate(pAsset);
		if (pCity != null)
		{
			this.village_origin = pCity.data.cityName;
		}
		else
		{
			this.village_origin = "??";
		}
		this.year = MapBox.instance.mapStats.year;
		this.prepare();
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000510A1 File Offset: 0x0004F2A1
	private void prepare()
	{
		this.createSkins();
		this.setDirty();
		if (this.color32 == Color.clear)
		{
			this.createGameColors();
		}
		this.hash = this.GetHashCode();
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000510D8 File Offset: 0x0004F2D8
	public void addZone(TileZone pZone)
	{
		if (pZone.culture != null && pZone.culture != this)
		{
			pZone.culture.removeZone(pZone);
		}
		this.zones.Add(pZone);
		pZone.setCulture(this);
		this._zones_dirty = true;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00051112 File Offset: 0x0004F312
	public void removeZone(TileZone pZone)
	{
		this.zones.Remove(pZone);
		pZone.removeCulture();
		this._zones_dirty = true;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00051130 File Offset: 0x0004F330
	private void createSkins()
	{
		if (string.IsNullOrEmpty(this.race))
		{
			this.race = "human";
		}
		if (!string.IsNullOrEmpty(this.skin_citizen_female))
		{
			return;
		}
		Race race = AssetManager.raceLibrary.get(this.race);
		int num = Toolbox.randomInt(0, race.skin_citizen_female.Count);
		this.skin_citizen_female = race.skin_citizen_female[num];
		this.skin_citizen_male = race.skin_citizen_male[num];
		this.skin_warrior = race.skin_warrior[num];
		if (string.IsNullOrEmpty(this.icon_element))
		{
			this.icon_element = race.culture_elements.GetRandom<string>();
			this.icon_decor = race.culture_decors.GetRandom<string>();
			race.culture_colors.Shuffle<string>();
			this.color = string.Empty;
			for (int i = 0; i < race.culture_colors.Count; i++)
			{
				if (!MapBox.instance.cultures.isColorUsed(race.culture_colors[i]))
				{
					this.color = race.culture_colors[i];
					break;
				}
			}
			if (string.IsNullOrEmpty(this.color))
			{
				this.color = race.culture_colors.GetRandom<string>();
			}
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00051269 File Offset: 0x0004F469
	public void reset()
	{
		this.kingdoms = 0;
		this.cities = 0;
		this.followers = 0;
		this.knowledge_gain = 0f;
		this._list_cities.Clear();
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00051298 File Offset: 0x0004F498
	public void calculateKnowledgeGain()
	{
		Race race = AssetManager.raceLibrary.get(this.race);
		this.knowledge_gain = (float)race.culture_knowledge_gain_base;
		for (int i = 0; i < this._list_cities.Count; i++)
		{
			Actor leader = this._list_cities[i].leader;
			if (!(leader == null) && !(leader.data.culture != this.id))
			{
				float num = (float)(leader.curStats.intelligence + 1) * race.culture_knowledge_gain_per_intelligence;
				this.knowledge_gain += num * race.culture_knowledge_gain_rate;
			}
		}
		this.knowledge_gain += this.stats.knowledge_gain.value;
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00051354 File Offset: 0x0004F554
	public void prepareForSave()
	{
		this.list_zone_ids = new List<int>();
		foreach (TileZone tileZone in this.zones)
		{
			this.list_zone_ids.Add(tileZone.id);
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000513BC File Offset: 0x0004F5BC
	public void load()
	{
		this.prepare();
		if (this.list_zone_ids != null)
		{
			foreach (int pID in this.list_zone_ids)
			{
				this.addZone(MapBox.instance.zoneCalculator.getZoneByID(pID));
			}
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0005142C File Offset: 0x0004F62C
	public void setDirty()
	{
		this.dirty = true;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00051435 File Offset: 0x0004F635
	public int getCurrentLevel()
	{
		return this.list_tech_ids.Count;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00051444 File Offset: 0x0004F644
	public void updateProgress()
	{
		if (string.IsNullOrEmpty(this.researching_tech))
		{
			this.researching_tech = this.findNextTechToResearch();
			this.research_progress = 0f;
			return;
		}
		this.research_progress += this.knowledge_gain;
		if (DebugConfig.isOn(DebugOption.FastCultures))
		{
			this.research_progress = this.getKnowledgeCostForResearch();
		}
		if (this.research_progress >= this.getKnowledgeCostForResearch())
		{
			this.addFinishedTech(this.researching_tech);
			this.researching_tech = string.Empty;
			this.research_progress = 0f;
			this.researching_tech = this.findNextTechToResearch();
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x000514DC File Offset: 0x0004F6DC
	public float getKnowledgeCostForResearch()
	{
		if (string.IsNullOrEmpty(this.researching_tech))
		{
			return 0f;
		}
		CultureTechAsset cultureTechAsset = AssetManager.culture_tech.get(this.researching_tech);
		float num = cultureTechAsset.knowledge_cost * 100f + (float)(25 * this.getCurrentLevel());
		if (cultureTechAsset.type == TechType.Rare)
		{
			num *= 2f;
		}
		return num;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0005153B File Offset: 0x0004F73B
	public void addFinishedTech(string pTech)
	{
		this.list_tech_ids.Add(pTech);
		this.setDirty();
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00051550 File Offset: 0x0004F750
	private void updateSpread(float pElapsed)
	{
		if (this.timer_spread > 0f)
		{
			this.timer_spread -= pElapsed;
			return;
		}
		this.timer_spread = this.stats.culture_spread_speed.value;
		if (this.followers == 0)
		{
			return;
		}
		if (!this.zones.Any<TileZone>())
		{
			return;
		}
		Culture._list_zones.Clear();
		foreach (TileZone tileZone in this.zones)
		{
			Culture._list_zones.Add(tileZone);
		}
		TileZone random = Culture._list_zones.GetRandom<TileZone>();
		this.spreadAround(random);
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0005160C File Offset: 0x0004F80C
	private void spreadAround(TileZone pZone)
	{
		TileZone bestZoneToSpreadFrom = this.getBestZoneToSpreadFrom(pZone);
		Culture culture = pZone.culture;
		if (bestZoneToSpreadFrom == null)
		{
			return;
		}
		List<Actor> list = new List<Actor>();
		if (bestZoneToSpreadFrom.culture == null)
		{
			this.spreadOn(bestZoneToSpreadFrom);
			return;
		}
		int num = 0;
		for (int i = 0; i < bestZoneToSpreadFrom.neighbours.Count; i++)
		{
			if (bestZoneToSpreadFrom.neighbours[i].culture == culture)
			{
				num++;
			}
		}
		float num2 = 0f;
		list.Clear();
		Toolbox.fillListWithUnitsFromChunk(bestZoneToSpreadFrom.centerTile.chunk, list);
		for (int j = 0; j < list.Count; j++)
		{
			if (!(list[j].data.culture != this.id))
			{
				num2 += 0.05f;
			}
		}
		float num3 = this.stats.culture_spread_convert_chance.value * (float)num + num2;
		if (bestZoneToSpreadFrom.culture.followers > this.followers)
		{
			float num4 = (float)((this.followers + 1) / (bestZoneToSpreadFrom.culture.followers + 1));
			num3 *= num4;
		}
		if (Toolbox.randomChance(num3))
		{
			this.spreadOn(bestZoneToSpreadFrom);
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00051730 File Offset: 0x0004F930
	private TileZone getBestZoneToSpreadFrom(TileZone pZone)
	{
		Culture._list_zones_to_spread.Clear();
		Culture._list_zones_to_spread.AddRange(pZone.neighbours);
		TileZone tileZone = null;
		TileZone tileZone2 = null;
		Culture culture = pZone.culture;
		for (int i = 0; i < Culture._list_zones_to_spread.Count; i++)
		{
			Culture._list_zones_to_spread.ShuffleOne(i);
			TileZone tileZone3 = Culture._list_zones_to_spread[i];
			if (!(tileZone3.city == null) && tileZone3.culture != this && !(tileZone3.city.race.id != culture.race))
			{
				if (tileZone2 == null)
				{
					tileZone2 = tileZone3;
				}
				if (tileZone == null && tileZone3.tilesWithGround == 64)
				{
					tileZone = tileZone3;
					break;
				}
			}
		}
		TileZone result;
		if (tileZone != null)
		{
			result = tileZone;
		}
		else
		{
			result = tileZone2;
		}
		return result;
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x000517F1 File Offset: 0x0004F9F1
	private void spreadOn(TileZone pZone)
	{
		this.addZone(pZone);
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x000517FC File Offset: 0x0004F9FC
	private void updateDirty(float pElapsed)
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		this._list_tech.Clear();
		this.stats.clear();
		Race race = AssetManager.raceLibrary.get(this.race);
		this.stats.addStats(race.stats);
		for (int i = 0; i < this.list_tech_ids.Count; i++)
		{
			string pID = this.list_tech_ids[i];
			CultureTechAsset cultureTechAsset = AssetManager.culture_tech.get(pID);
			if (cultureTechAsset != null)
			{
				this._list_tech.Add(cultureTechAsset);
				this.stats.addStats(cultureTechAsset.stats);
			}
		}
		this.calculateKnowledgeGain();
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x000518A6 File Offset: 0x0004FAA6
	public void update(float pElapsed)
	{
		if (this._zones_dirty)
		{
			this._zones_dirty = false;
			this.updateTitleCenter();
		}
		if (!MapBox.instance._isPaused)
		{
			this.updateSpread(pElapsed);
		}
		this.updateDirty(pElapsed);
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x000518D8 File Offset: 0x0004FAD8
	public string findNextTechToResearch()
	{
		if (this._maximum_level_reached)
		{
			return string.Empty;
		}
		string empty = string.Empty;
		List<CultureTechAsset> list = new List<CultureTechAsset>();
		List<CultureTechAsset> list2 = new List<CultureTechAsset>();
		List<CultureTechAsset> list3 = new List<CultureTechAsset>();
		list3.AddRange(AssetManager.culture_tech.list);
		list3.Shuffle<CultureTechAsset>();
		int currentLevel = this.getCurrentLevel();
		int num = this.countCurrentRareTech();
		Race race = AssetManager.raceLibrary.get(this.race);
		for (int i = 0; i < list3.Count; i++)
		{
			CultureTechAsset cultureTechAsset = list3[i];
			if (cultureTechAsset.enabled && cultureTechAsset.required_level <= currentLevel && !this.haveTech(cultureTechAsset.id) && this.haveRequiredTechFor(cultureTechAsset) && (cultureTechAsset.type != TechType.Rare || num != race.culture_rate_tech_limit) && (!race.culture_forbidden_tech.Any<string>() || !race.culture_forbidden_tech.Contains(cultureTechAsset.id)))
			{
				if (cultureTechAsset.priority)
				{
					list.Add(cultureTechAsset);
				}
				list2.Add(cultureTechAsset);
			}
		}
		if (list.Any<CultureTechAsset>())
		{
			list2.Clear();
			list2.AddRange(list);
		}
		if (!list2.Any<CultureTechAsset>())
		{
			this._maximum_level_reached = true;
			return string.Empty;
		}
		return list2.GetRandom<CultureTechAsset>().id;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00051A1C File Offset: 0x0004FC1C
	internal int countCurrentRareTech()
	{
		int num = 0;
		for (int i = 0; i < this.list_tech_ids.Count; i++)
		{
			string pID = this.list_tech_ids[i];
			CultureTechAsset cultureTechAsset = AssetManager.culture_tech.get(pID);
			if (cultureTechAsset != null && cultureTechAsset.type == TechType.Rare)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00051A6C File Offset: 0x0004FC6C
	public void debug(DebugTool pTool)
	{
		pTool.setText("id:", this.id);
		pTool.setText("name:", this.name);
		pTool.setText("followers:", this.followers);
		pTool.setText("cities:", this.cities);
		pTool.setText("list_tech_ids:", this.list_tech_ids.Count);
		pTool.setText("researching_tech:", this.researching_tech);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00051AF3 File Offset: 0x0004FCF3
	public bool haveTech(string pTech)
	{
		return this.list_tech_ids.Contains(pTech);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00051B04 File Offset: 0x0004FD04
	public bool haveRequiredTechFor(CultureTechAsset pTech)
	{
		if (pTech.requirements == null)
		{
			return true;
		}
		if (!pTech.requirements.Any<string>())
		{
			return true;
		}
		for (int i = 0; i < pTech.requirements.Count; i++)
		{
			string text = pTech.requirements[i];
			if (!this.list_tech_ids.Contains(text))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00051B60 File Offset: 0x0004FD60
	private void updateTitleCenter()
	{
		if (this.zones.Count == 0)
		{
			this.titleCenter = Globals.POINT_IN_VOID;
			return;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		TileZone tileZone = null;
		foreach (TileZone tileZone2 in this.zones)
		{
			num += tileZone2.centerTile.posV3.x;
			num2 += tileZone2.centerTile.posV3.y;
		}
		this.titleCenter.x = num / (float)this.zones.Count;
		this.titleCenter.y = num2 / (float)this.zones.Count;
		foreach (TileZone tileZone3 in this.zones)
		{
			float num4 = Toolbox.Dist((float)tileZone3.centerTile.x, (float)tileZone3.centerTile.y, this.titleCenter.x, this.titleCenter.y);
			if (tileZone == null || num4 < num3)
			{
				tileZone = tileZone3;
				num3 = num4;
			}
		}
		this.titleCenter.x = tileZone.centerTile.posV3.x;
		this.titleCenter.y = tileZone.centerTile.posV3.y + 2f;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00051CFC File Offset: 0x0004FEFC
	public void createGameColors()
	{
		this.color32 = Toolbox.makeColor(this.color);
		this.color32.a = 140;
		this.color32_border = Toolbox.makeDarkerColor(this.color32, 0.7f);
		this.color32_border.a = 230;
		this.color32_text = new Color32(this.color32.r, this.color32.g, this.color32.b, this.color32.a);
		this.color32_text = this.convertToLight(this.color32_text);
		this.color32_text.a = byte.MaxValue;
		this.color32_unit = Color.Lerp(this.color32, Color.white, 0.3f);
		this.color32_unit.a = byte.MaxValue;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00051DF8 File Offset: 0x0004FFF8
	private Color convertToLight(Color pColor)
	{
		if ((pColor.r + pColor.b + pColor.g) / 3f < 0.33333334f)
		{
			float num = 1.6666666f;
			return new Color(pColor.r * num, pColor.b * num, pColor.g * num);
		}
		return pColor;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00051E4C File Offset: 0x0005004C
	public void clearZones()
	{
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.removeCulture();
		}
		this.zones.Clear();
		this._list_tech.Clear();
		this._list_cities.Clear();
		this.list_tech_ids.Clear();
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00051EC8 File Offset: 0x000500C8
	public int getBornLevel()
	{
		return 1 + (int)this.stats.bonus_born_level.value;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00051EDD File Offset: 0x000500DD
	public int getMaxAgeBonus()
	{
		return (int)this.stats.bonus_age.value;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00051EF0 File Offset: 0x000500F0
	public int getMaxLevelBonus()
	{
		return (int)this.stats.bonus_max_unit_level.value;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00051F04 File Offset: 0x00050104
	public void researchNewTechTest()
	{
		string text = this.findNextTechToResearch();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		this.addFinishedTech(text);
	}

	// Token: 0x04000967 RID: 2407
	[NonSerialized]
	public int hash;

	// Token: 0x04000968 RID: 2408
	public string id;

	// Token: 0x04000969 RID: 2409
	public string name;

	// Token: 0x0400096A RID: 2410
	[NonSerialized]
	public Color32 color32 = Color.clear;

	// Token: 0x0400096B RID: 2411
	[NonSerialized]
	public Color32 color32_border = Color.clear;

	// Token: 0x0400096C RID: 2412
	[NonSerialized]
	public Color32 color32_text = Color.clear;

	// Token: 0x0400096D RID: 2413
	[NonSerialized]
	public Color32 color32_unit = Color.clear;

	// Token: 0x0400096E RID: 2414
	public string color = string.Empty;

	// Token: 0x0400096F RID: 2415
	public string icon_element = string.Empty;

	// Token: 0x04000970 RID: 2416
	public string icon_decor = string.Empty;

	// Token: 0x04000971 RID: 2417
	public int year;

	// Token: 0x04000972 RID: 2418
	public string village_origin = string.Empty;

	// Token: 0x04000973 RID: 2419
	public string race = string.Empty;

	// Token: 0x04000974 RID: 2420
	public string skin_citizen_male = string.Empty;

	// Token: 0x04000975 RID: 2421
	public string skin_citizen_female = string.Empty;

	// Token: 0x04000976 RID: 2422
	public string skin_warrior = string.Empty;

	// Token: 0x04000977 RID: 2423
	public List<int> list_zone_ids;

	// Token: 0x04000978 RID: 2424
	public List<string> list_tech_ids;

	// Token: 0x04000979 RID: 2425
	public string researching_tech = string.Empty;

	// Token: 0x0400097A RID: 2426
	public float research_progress;

	// Token: 0x0400097B RID: 2427
	[NonSerialized]
	private List<CultureTechAsset> _list_tech = new List<CultureTechAsset>();

	// Token: 0x0400097C RID: 2428
	[NonSerialized]
	public List<City> _list_cities = new List<City>();

	// Token: 0x0400097D RID: 2429
	[NonSerialized]
	public KingdomStats stats = new KingdomStats();

	// Token: 0x0400097E RID: 2430
	[NonSerialized]
	private bool dirty;

	// Token: 0x0400097F RID: 2431
	[NonSerialized]
	public int followers;

	// Token: 0x04000980 RID: 2432
	[NonSerialized]
	public int cities;

	// Token: 0x04000981 RID: 2433
	[NonSerialized]
	public int kingdoms;

	// Token: 0x04000982 RID: 2434
	[NonSerialized]
	public float knowledge_gain;

	// Token: 0x04000983 RID: 2435
	[NonSerialized]
	public HashSetTileZone zones = new HashSetTileZone();

	// Token: 0x04000984 RID: 2436
	[NonSerialized]
	private bool _zones_dirty;

	// Token: 0x04000985 RID: 2437
	private static List<TileZone> _list_zones = new List<TileZone>();

	// Token: 0x04000986 RID: 2438
	private static List<TileZone> _list_zones_to_spread = new List<TileZone>();

	// Token: 0x04000987 RID: 2439
	private float timer_spread;

	// Token: 0x04000988 RID: 2440
	private bool _maximum_level_reached;

	// Token: 0x04000989 RID: 2441
	internal Vector3 titleCenter = Globals.POINT_IN_VOID;
}
