using System;
using System.Collections.Generic;
using System.IO;
using ai.behaviours;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class AssetManager
{
	// Token: 0x060000D6 RID: 214 RVA: 0x00011E85 File Offset: 0x00010085
	public static void init()
	{
		AssetManager.instance = new AssetManager();
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00011E94 File Offset: 0x00010094
	public AssetManager()
	{
		this.list = new List<BaseAssetLibrary>();
		this.dict = new Dictionary<string, BaseAssetLibrary>();
		this.assetgv = Config.gv;
		this.add(AssetManager.tiles = new TileLibrary(), "tiles");
		this.add(AssetManager.topTiles = new TopTileLibrary(), "cap_tiles");
		this.add(AssetManager.culture_tech = new CultureTechLibrary(), "culture_tech");
		this.add(AssetManager.professions = new ProfessionLibrary(), "professions");
		this.add(AssetManager.personalities = new PersonalityLibrary(), "personalities");
		this.add(AssetManager.moods = new MoodLibrary(), "moods");
		this.add(AssetManager.drops = new DropsLibrary(), "drops");
		this.add(AssetManager.buildings = new BuildingLibrary(), "buildings");
		this.add(AssetManager.status = new StatusLibrary(), "status");
		this.add(AssetManager.spells = new SpellLibrary(), "spells");
		this.add(AssetManager.tasks_actor = new BehaviourTaskActorLibrary(), "beh");
		this.add(AssetManager.tasks_city = new BehaviourTaskCityLibrary(), "beh_city");
		this.add(AssetManager.tasks_kingdom = new BehaviourTaskKingdomLibrary(), "beh_kingdom");
		this.add(AssetManager.kingdoms = new KingdomLibrary(), "kingdoms");
		this.add(AssetManager.raceLibrary = new RaceLibrary(), "race");
		this.add(AssetManager.unitStats = new ActorStatsLibrary(), "units");
		this.add(AssetManager.nameGenerator = new NameGeneratorLibrary(), "name_generator");
		this.add(AssetManager.disasters = new DisasterLibrary(), "disasters");
		this.add(AssetManager.traits = new ActorTraitLibrary(), "traits");
		this.add(AssetManager.job_actor = new ActorJobLibrary(), "jobs_actor");
		this.add(AssetManager.job_city = new CityJobLibrary(), "jobs_city");
		this.add(AssetManager.job_kingdom = new KingdomJobLibrary(), "jobs_kingdom");
		this.add(AssetManager.powers = new PowerLibrary(), "powers");
		this.add(AssetManager.items = new ItemLibrary(), "items");
		this.add(AssetManager.items_prefix = new ItemPrefixLibrary(), "items_prefix");
		this.add(AssetManager.items_suffix = new ItemSuffixLibrary(), "items_suffix");
		this.add(AssetManager.items_material_weapon = new ItemWeaponMaterialLibrary(), "items_material_weapon");
		this.add(AssetManager.items_material_armor = new ItemArmorMaterialLibrary(), "items_material_armor");
		this.add(AssetManager.items_material_accessory = new ItemAccessoryMaterialLibrary(), "items_material_jewelry");
		this.add(AssetManager.resources = new ResourceLibrary(), "resources");
		this.add(AssetManager.terraform = new TerraformLibrary(), "terraform");
		this.add(AssetManager.projectiles = new ProjectileLibrary(), "projectiles");
		this.add(AssetManager.achievements = new AchievementLibrary(), "achievements");
		this.add(AssetManager.achievementGroups = new AchievementGroupLibrary(), "achievementGroups");
		this.add(AssetManager.tester_jobs = new TesterJobLibrary(), "tester_jobs");
		this.add(AssetManager.tester_tasks = new TesterBehTaskLibrary(), "tester_tasks");
		ItemGenerator.init();
		BannerGenerator.loadBannersFromResources();
		foreach (BaseAssetLibrary baseAssetLibrary in this.list)
		{
			baseAssetLibrary.checkCache();
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00012230 File Offset: 0x00010430
	private void loadModules()
	{
		string text = Application.streamingAssetsPath + "/modules/";
		if (Config.isAndroid)
		{
			text = Application.dataPath + "/Resources/modules/";
		}
		string[] directories = Directory.GetDirectories(text);
		Debug.Log("LOAD MODULES " + text + " " + directories.Length.ToString());
		foreach (string pPath in directories)
		{
			this.checkModule(pPath);
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000122A8 File Offset: 0x000104A8
	private void checkModule(string pPath)
	{
		if (pPath.Contains(".meta"))
		{
			return;
		}
		Debug.Log("CheckDir: " + pPath);
		string[] files = Directory.GetFiles(pPath);
		foreach (string text in Directory.GetDirectories(pPath))
		{
			if (text.Contains("banners"))
			{
				BannerGenerator.loadBanners(text);
			}
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00012308 File Offset: 0x00010508
	private void checkFile(string pPath)
	{
		if (pPath.Contains(".json.meta"))
		{
			return;
		}
		if (!pPath.Contains(".json"))
		{
			return;
		}
		Debug.Log("file: " + pPath);
		string text = File.ReadAllText(pPath);
		pPath.Contains("units.json");
		if (pPath.Contains("traits.json"))
		{
			foreach (ActorTrait actorTrait in JsonUtility.FromJson<ActorTraitLibrary>(text).list)
			{
				Debug.Log("TRAIT LOADED " + actorTrait.id);
				AssetManager.traits.add(actorTrait);
			}
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x000123C8 File Offset: 0x000105C8
	private void add(BaseAssetLibrary pLibrary, string pId)
	{
		if (this.assetgv[0] != '0')
		{
			return;
		}
		pLibrary.init();
		this.list.Add(pLibrary);
		this.dict.Add(pId, pLibrary);
		pLibrary.id = pId;
	}

	// Token: 0x0400007A RID: 122
	public static KingdomJobLibrary job_kingdom;

	// Token: 0x0400007B RID: 123
	public static BehaviourTaskKingdomLibrary tasks_kingdom;

	// Token: 0x0400007C RID: 124
	public static CityJobLibrary job_city;

	// Token: 0x0400007D RID: 125
	public static BehaviourTaskCityLibrary tasks_city;

	// Token: 0x0400007E RID: 126
	public static ActorJobLibrary job_actor;

	// Token: 0x0400007F RID: 127
	public static BehaviourTaskActorLibrary tasks_actor;

	// Token: 0x04000080 RID: 128
	public static CultureTechLibrary culture_tech;

	// Token: 0x04000081 RID: 129
	public static MoodLibrary moods;

	// Token: 0x04000082 RID: 130
	public static PersonalityLibrary personalities;

	// Token: 0x04000083 RID: 131
	public static ProfessionLibrary professions;

	// Token: 0x04000084 RID: 132
	public static DropsLibrary drops;

	// Token: 0x04000085 RID: 133
	public static BuildingLibrary buildings;

	// Token: 0x04000086 RID: 134
	public static ActorStatsLibrary unitStats;

	// Token: 0x04000087 RID: 135
	public static ActorTraitLibrary traits;

	// Token: 0x04000088 RID: 136
	public static KingdomLibrary kingdoms;

	// Token: 0x04000089 RID: 137
	public static NameGeneratorLibrary nameGenerator;

	// Token: 0x0400008A RID: 138
	public static DisasterLibrary disasters;

	// Token: 0x0400008B RID: 139
	public static RaceLibrary raceLibrary;

	// Token: 0x0400008C RID: 140
	public static ResourceLibrary resources;

	// Token: 0x0400008D RID: 141
	public static ItemLibrary items;

	// Token: 0x0400008E RID: 142
	public static ItemPrefixLibrary items_prefix;

	// Token: 0x0400008F RID: 143
	public static ItemSuffixLibrary items_suffix;

	// Token: 0x04000090 RID: 144
	public static ItemWeaponMaterialLibrary items_material_weapon;

	// Token: 0x04000091 RID: 145
	public static ItemArmorMaterialLibrary items_material_armor;

	// Token: 0x04000092 RID: 146
	public static ItemAccessoryMaterialLibrary items_material_accessory;

	// Token: 0x04000093 RID: 147
	public static ProjectileLibrary projectiles;

	// Token: 0x04000094 RID: 148
	public static TileLibrary tiles;

	// Token: 0x04000095 RID: 149
	public static TopTileLibrary topTiles;

	// Token: 0x04000096 RID: 150
	public static TerraformLibrary terraform;

	// Token: 0x04000097 RID: 151
	public static PowerLibrary powers;

	// Token: 0x04000098 RID: 152
	public static SpellLibrary spells;

	// Token: 0x04000099 RID: 153
	public static StatusLibrary status;

	// Token: 0x0400009A RID: 154
	public static TesterJobLibrary tester_jobs;

	// Token: 0x0400009B RID: 155
	public static TesterBehTaskLibrary tester_tasks;

	// Token: 0x0400009C RID: 156
	public static AchievementLibrary achievements;

	// Token: 0x0400009D RID: 157
	public static AchievementGroupLibrary achievementGroups;

	// Token: 0x0400009E RID: 158
	public static AssetManager instance;

	// Token: 0x0400009F RID: 159
	public List<BaseAssetLibrary> list;

	// Token: 0x040000A0 RID: 160
	public Dictionary<string, BaseAssetLibrary> dict;

	// Token: 0x040000A1 RID: 161
	private string assetgv;
}
