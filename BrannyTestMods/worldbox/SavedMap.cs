using System;
using System.Collections.Generic;
using Assets.SimpleZip;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000223 RID: 547
[Serializable]
public class SavedMap
{
	// Token: 0x06000C56 RID: 3158 RVA: 0x00079574 File Offset: 0x00077774
	public SavedMap()
	{
		this.width = Config.ZONE_AMOUNT_X_DEFAULT;
		this.height = Config.ZONE_AMOUNT_Y_DEFAULT;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00079616 File Offset: 0x00077816
	public void init()
	{
		this.worldLaws = new WorldLaws();
		this.worldLaws.init();
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0007962E File Offset: 0x0007782E
	public int getTileMapID(string tileString)
	{
		if (!this.tileMap.Contains(tileString))
		{
			this.tileMap.Add(tileString);
		}
		return this.tileMap.IndexOf(tileString);
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00079658 File Offset: 0x00077858
	public void create()
	{
		this.init();
		MapBox instance = MapBox.instance;
		this.width = Config.ZONE_AMOUNT_X;
		this.height = Config.ZONE_AMOUNT_Y;
		this.saveVersion = Config.WORLD_SAVE_VERSION;
		this.mapStats = instance.mapStats;
		this.worldLaws = instance.worldLaws;
		this.mapStats.population = instance.units.Count;
		this.cultures = instance.cultures.save();
		this.kingdoms = instance.kingdoms.save();
		this.relations = instance.kingdoms.diplomacyManager.save();
		foreach (City city in instance.citiesList)
		{
			CityData data = city.data;
			data.storage.save();
			data.zones.Clear();
			foreach (TileZone tileZone in city.zones)
			{
				ZoneData zoneData = new ZoneData
				{
					x = tileZone.x,
					y = tileZone.y
				};
				data.zones.Add(zoneData);
			}
			this.cities.Add(data);
		}
		this.tileMap.Clear();
		this.fire.Clear();
		this.conwayEater.Clear();
		this.conwayCreator.Clear();
		List<int[]> list = new List<int[]>();
		List<int[]> list2 = new List<int[]>();
		string b = string.Empty;
		int num = 0;
		int num2 = 0;
		int num3 = this.width * 64;
		list.Add(new int[num3]);
		list2.Add(new int[num3]);
		int num4 = 0;
		for (int i = 0; i < instance.tilesList.Count; i++)
		{
			WorldTile worldTile = instance.tilesList[i];
			string wholeTileIDForSave = this.getWholeTileIDForSave(worldTile);
			if (wholeTileIDForSave != b || num2 != worldTile.pos.y)
			{
				if (num > 0)
				{
					list2[num2][num4] = num;
					list[num2][num4++] = this.getTileMapID(b);
					num = 0;
				}
				b = wholeTileIDForSave;
				if (num2 != worldTile.pos.y)
				{
					list[num2] = Toolbox.resizeArray<int>(list[num2], num4);
					list2[num2] = Toolbox.resizeArray<int>(list2[num2], num4);
					num2 = worldTile.pos.y;
					list.Add(new int[num3]);
					list2.Add(new int[num3]);
					num4 = 0;
				}
			}
			num++;
			if (worldTile.data.fire)
			{
				this.fire.Add(worldTile.data.tile_id);
			}
			if (worldTile.data.conwayType == ConwayType.Eater)
			{
				this.conwayEater.Add(worldTile.data.tile_id);
			}
			if (worldTile.data.conwayType == ConwayType.Creator)
			{
				this.conwayCreator.Add(worldTile.data.tile_id);
			}
		}
		if (num > 0)
		{
			list2[num2][num4] = num;
			list[num2][num4++] = this.getTileMapID(b);
			list[num2] = Toolbox.resizeArray<int>(list[num2], num4);
			list2[num2] = Toolbox.resizeArray<int>(list2[num2], num4);
		}
		this.tileArray = list.ToArray();
		this.tileAmounts = list2.ToArray();
		list.Clear();
		list2.Clear();
		List<Actor> simpleList = instance.units.getSimpleList();
		for (int j = 0; j < simpleList.Count; j++)
		{
			Actor actor = simpleList[j];
			if (actor.data.alive && !actor.stats.skipSave)
			{
				ActorData actorData = new ActorData
				{
					status = actor.data,
					x = actor.currentTile.pos.x,
					y = actor.currentTile.pos.y,
					inventory = actor.inventory
				};
				if (actor.equipment != null)
				{
					List<ItemData> dataForSave = actor.equipment.getDataForSave();
					if (dataForSave.Count > 0)
					{
						actorData.items = dataForSave;
					}
				}
				if (actor.city != null)
				{
					actorData.cityID = actor.city.data.cityID;
				}
				this.actors.Add(actorData);
			}
		}
		List<Building> simpleList2 = instance.buildings.getSimpleList();
		for (int k = 0; k < simpleList2.Count; k++)
		{
			Building building = simpleList2[k];
			building.prepareForSave();
			this.buildings.Add(building.data);
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00079B80 File Offset: 0x00077D80
	private string getWholeTileIDForSave(WorldTile pTile)
	{
		string text = pTile.main_type.id;
		if (pTile.top_type != null)
		{
			text = text + ":" + pTile.top_type.id;
		}
		return text;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00079BBC File Offset: 0x00077DBC
	public string toJson()
	{
		if (this.worldLaws == null)
		{
			this.create();
		}
		string text = "";
		try
		{
			text = JsonConvert.SerializeObject(this, new JsonSerializerSettings
			{
				DefaultValueHandling = 3
			});
			if (string.IsNullOrEmpty(text) || text.Length < 20)
			{
				text = JsonUtility.ToJson(this);
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			text = JsonUtility.ToJson(this);
		}
		if (string.IsNullOrEmpty(text) || text.Length < 20)
		{
			throw new Exception("Error while creating json");
		}
		return text;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00079C48 File Offset: 0x00077E48
	public byte[] toZip()
	{
		return Zip.Compress(this.toJson());
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00079C58 File Offset: 0x00077E58
	public MapMetaData getMeta()
	{
		MapMetaData mapMetaData = new MapMetaData();
		List<string> list = new List<string>();
		int num = 0;
		int num2 = 0;
		foreach (ActorData actorData in this.actors)
		{
			ActorStats actorStats = AssetManager.unitStats.get(actorData.status.statsID);
			if (actorStats.unit)
			{
				num++;
				if (!list.Contains(actorStats.race))
				{
					list.Add(actorStats.race);
				}
			}
			else
			{
				num2++;
			}
		}
		foreach (CityData cityData in this.cities)
		{
			num += cityData.popPoints.Count;
		}
		mapMetaData.saveVersion = this.saveVersion;
		mapMetaData.width = this.width;
		mapMetaData.height = this.height;
		mapMetaData.mapStats = this.mapStats;
		mapMetaData.cities = this.cities.Count;
		mapMetaData.units = this.actors.Count;
		mapMetaData.population = num;
		mapMetaData.cultures = this.cultures.Count;
		mapMetaData.mobs = num2;
		int num3 = 0;
		int num4 = 0;
		using (List<BuildingData>.Enumerator enumerator3 = this.buildings.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				if (enumerator3.Current.cityID != "")
				{
					num3++;
				}
				num4++;
			}
		}
		mapMetaData.buildings = num3;
		mapMetaData.structures = num4;
		mapMetaData.kingdoms = this.kingdoms.Count;
		mapMetaData.races = list;
		return mapMetaData;
	}

	// Token: 0x04000EC6 RID: 3782
	public int saveVersion;

	// Token: 0x04000EC7 RID: 3783
	public int width;

	// Token: 0x04000EC8 RID: 3784
	public int height;

	// Token: 0x04000EC9 RID: 3785
	public MapStats mapStats;

	// Token: 0x04000ECA RID: 3786
	public WorldLaws worldLaws;

	// Token: 0x04000ECB RID: 3787
	public string tileString;

	// Token: 0x04000ECC RID: 3788
	public List<string> tileMap = new List<string>();

	// Token: 0x04000ECD RID: 3789
	public int[][] tileArray;

	// Token: 0x04000ECE RID: 3790
	public int[][] tileAmounts;

	// Token: 0x04000ECF RID: 3791
	public List<int> fire = new List<int>();

	// Token: 0x04000ED0 RID: 3792
	public List<int> conwayEater = new List<int>();

	// Token: 0x04000ED1 RID: 3793
	public List<int> conwayCreator = new List<int>();

	// Token: 0x04000ED2 RID: 3794
	public List<WorldTileData> tiles = new List<WorldTileData>();

	// Token: 0x04000ED3 RID: 3795
	public List<CityData> cities = new List<CityData>();

	// Token: 0x04000ED4 RID: 3796
	public List<ActorData> actors = new List<ActorData>();

	// Token: 0x04000ED5 RID: 3797
	public List<BuildingData> buildings = new List<BuildingData>();

	// Token: 0x04000ED6 RID: 3798
	public List<Kingdom> kingdoms = new List<Kingdom>();

	// Token: 0x04000ED7 RID: 3799
	public List<DiplomacyRelation> relations = new List<DiplomacyRelation>();

	// Token: 0x04000ED8 RID: 3800
	public List<Culture> cultures = new List<Culture>();
}
