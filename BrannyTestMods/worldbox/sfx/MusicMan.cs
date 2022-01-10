using System;
using System.Collections.Generic;
using UnityEngine;

namespace sfx
{
	// Token: 0x02000306 RID: 774
	public class MusicMan : MonoBehaviour
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x0009B4E0 File Offset: 0x000996E0
		public static void clear()
		{
			if (MusicMan.races.Count == 0)
			{
				MusicMan.music_world = new MusicPlayBox();
				MusicMan.music_god = new MusicPlayBox();
				MusicMan.races.Add("orc", new MusicRaceContainer());
				MusicMan.races.Add("elf", new MusicRaceContainer());
				MusicMan.races.Add("human", new MusicRaceContainer());
				MusicMan.races.Add("dwarf", new MusicRaceContainer());
				MusicMan.dict_values.Add("forest", new MusicValueContainer());
				MusicMan.dict_values.Add("grassland", new MusicValueContainer());
				MusicMan.dict_values.Add("tropical", new MusicValueContainer());
				MusicMan.dict_values.Add("desert", new MusicValueContainer());
				MusicMan.dict_values.Add("snow", new MusicValueContainer());
				MusicMan.dict_values.Add("mountain", new MusicValueContainer());
				foreach (MusicValueContainer musicValueContainer in MusicMan.dict_values.Values)
				{
					MusicMan.list_values.Add(musicValueContainer);
				}
			}
			foreach (MusicRaceContainer musicRaceContainer in MusicMan.races.Values)
			{
				musicRaceContainer.clear();
			}
			MusicMan.godView = MapBox.instance.qualityChanger.lowRes;
			MusicMan.world_size = MapBox.instance.kingdoms.list_civs.Count;
			foreach (MusicValueContainer musicValueContainer2 in MusicMan.dict_values.Values)
			{
				musicValueContainer2.amount = 0;
			}
			foreach (Kingdom kingdom in MapBox.instance.kingdoms.list_civs)
			{
				if (MusicMan.races.ContainsKey(kingdom.race.id))
				{
					MusicMan.races[kingdom.race.id].kingdom_exists = true;
				}
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0009B754 File Offset: 0x00099954
		public static void finishCount()
		{
			MusicMan.list_values.Sort(new Comparison<MusicValueContainer>(MusicMan.sorter));
			for (int i = 0; i < MusicMan.list_values.Count; i++)
			{
				MusicMan.list_values[i].enabled = true;
			}
			for (int j = 0; j < MusicMan.list_values.Count; j++)
			{
				if (j > 0)
				{
					MusicMan.list_values[j].enabled = false;
				}
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0009B7C8 File Offset: 0x000999C8
		public static void count(Building pBuilding)
		{
			if (pBuilding.kingdom == null)
			{
				return;
			}
			if (pBuilding.kingdom.isNature() || pBuilding.kingdom.isCiv())
			{
				if (pBuilding.currentTile.Type.frozen)
				{
					MusicMan.dict_values["snow"].amount++;
				}
				if (pBuilding.currentTile.Type.trees)
				{
					MusicMan.dict_values["forest"].amount++;
				}
				else if (pBuilding.currentTile.Type.grass)
				{
					MusicMan.dict_values["grassland"].amount++;
				}
				if (pBuilding.currentTile.Type.sand)
				{
					MusicMan.dict_values["desert"].amount++;
				}
			}
			if (pBuilding.kingdom.isCiv())
			{
				MusicRaceContainer musicRaceContainer = MusicMan.races[pBuilding.kingdom.race.id];
				musicRaceContainer.buildings++;
				if (pBuilding.stats.upgradeLevel > musicRaceContainer.advancements)
				{
					musicRaceContainer.advancements = pBuilding.stats.upgradeLevel;
				}
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0009B90F File Offset: 0x00099B0F
		public static int sorter(MusicValueContainer v1, MusicValueContainer v2)
		{
			return v2.amount.CompareTo(v1.amount);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0009B924 File Offset: 0x00099B24
		private void Start()
		{
			if (!DebugConfig.isOn(DebugOption.SystemMusic))
			{
				return;
			}
			foreach (AudioClip audioClip in Resources.LoadAll<AudioClip>("music/"))
			{
				MusicLayer musicLayer = Object.Instantiate<MusicLayer>(this.prefabLayer, base.transform);
				musicLayer.create(audioClip.name, audioClip);
				MusicMan.list_layers.Add(musicLayer);
				MusicMan.dict_layers.Add(musicLayer.id, musicLayer);
			}
			this.get("m_god_base").godView = true;
			this.get("m_god_race_elf").godView = true;
			this.get("m_god_race_elf").setRace("elf");
			this.get("m_god_race_orc").godView = true;
			this.get("m_god_race_orc").setRace("orc");
			this.get("m_god_worldsize_small").setWorldSize(0, 5);
			this.get("m_god_worldsize_med").setWorldSize(5, 10);
			this.get("m_god_worldsize_large").setWorldSize(10, 99999);
			this.get("m_village_elf_adv_basic").setAdvancement("elf", 0, 2);
			this.get("m_village_elf_adv_int").setAdvancement("elf", 2, 4);
			this.get("m_village_elf_adv_adv").setAdvancement("elf", 4, 5);
			this.get("m_village_elf_size_small").setRaceSize("elf", 0, 20);
			this.get("m_village_elf_size_med").setRaceSize("elf", 20, 50);
			this.get("m_village_elf_size_large").setRaceSize("elf", 50, 150);
			this.get("m_village_elf_size_huge").setRaceSize("elf", 150, 99999);
			this.get("m_village_orc_adv_basic").setAdvancement("orc", 0, 2);
			this.get("m_village_orc_adv_int").setAdvancement("orc", 2, 4);
			this.get("m_village_orc_adv_adv").setAdvancement("orc", 4, 5);
			this.get("m_village_orc_size_small").setRaceSize("orc", 0, 20);
			this.get("m_village_orc_size_med").setRaceSize("orc", 20, 50);
			this.get("m_village_orc_size_large").setRaceSize("orc", 50, 150);
			this.get("m_village_orc_size_huge").setRaceSize("orc", 150, 99999);
			this.get("m_village_dwarf_adv_basic").setAdvancement("dwarf", 0, 2);
			this.get("m_village_dwarf_adv_int").setAdvancement("dwarf", 2, 4);
			this.get("m_village_dwarf_adv_adv").setAdvancement("dwarf", 4, 5);
			this.get("m_village_dwarf_size_small").setRaceSize("dwarf", 0, 20);
			this.get("m_village_dwarf_size_med").setRaceSize("dwarf", 20, 50);
			this.get("m_village_dwarf_size_large").setRaceSize("dwarf", 50, 150);
			this.get("m_village_dwarf_size_huge").setRaceSize("dwarf", 150, 99999);
			this.get("m_village_human_adv_basic").setAdvancement("human", 0, 2);
			this.get("m_village_human_adv_int").setAdvancement("human", 2, 4);
			this.get("m_village_human_adv_adv").setAdvancement("human", 4, 5);
			this.get("m_village_human_size_small").setRaceSize("human", 0, 20);
			this.get("m_village_human_size_med").setRaceSize("human", 20, 50);
			this.get("m_village_human_size_large").setRaceSize("human", 50, 150);
			this.get("m_village_human_size_huge").setRaceSize("human", 150, 99999);
			this.get("m_village_env_desert").environment = true;
			this.get("m_village_env_desert").environmentID = "desert";
			this.get("m_village_env_forest").environment = true;
			this.get("m_village_env_forest").environmentID = "forest";
			this.get("m_village_env_grassland").environment = true;
			this.get("m_village_env_grassland").environmentID = "grassland";
			this.get("m_village_env_mountain").environment = true;
			this.get("m_village_env_mountain").environmentID = "mountain";
			this.get("m_village_env_snow").environment = true;
			this.get("m_village_env_snow").environmentID = "tropical";
			this.get("m_village_env_tropical").environment = true;
			this.get("m_village_env_tropical").environmentID = "snow";
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0009BDD4 File Offset: 0x00099FD4
		internal MusicLayer get(string pID)
		{
			return MusicMan.dict_layers[pID];
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0009BDE4 File Offset: 0x00099FE4
		private void Update()
		{
			if (!Config.gameLoaded)
			{
				return;
			}
			if (!DebugConfig.isOn(DebugOption.SystemMusic))
			{
				return;
			}
			MusicMan.music_god.update();
			MusicMan.music_world.update();
			foreach (MusicLayer musicLayer in MusicMan.list_layers)
			{
				musicLayer.update();
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0009BE5C File Offset: 0x0009A05C
		public static void play(string pID)
		{
			MusicLayer musicLayer = MusicMan.dict_layers[pID];
			if (musicLayer.gameObject.activeSelf)
			{
				return;
			}
			if (musicLayer.godView)
			{
				MusicMan.music_god.play(musicLayer);
				return;
			}
			if (!musicLayer.godView)
			{
				MusicMan.music_world.play(musicLayer);
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0009BEAC File Offset: 0x0009A0AC
		public static void stopping(string pID)
		{
			MusicLayer musicLayer = MusicMan.dict_layers[pID];
			if (!musicLayer.gameObject.activeSelf)
			{
				return;
			}
			musicLayer.isPlaying = false;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0009BEDC File Offset: 0x0009A0DC
		public static void stop(string pID)
		{
			MusicLayer musicLayer = MusicMan.dict_layers[pID];
			if (musicLayer.godView)
			{
				MusicMan.music_god.stop(musicLayer);
				return;
			}
			if (!musicLayer.godView)
			{
				MusicMan.music_world.stop(musicLayer);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0009BF1C File Offset: 0x0009A11C
		public static void update()
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0009BF20 File Offset: 0x0009A120
		public static void debug(DebugTool pTool)
		{
			pTool.setText("godView:", MusicMan.godView);
			pTool.setText("worldSize:", MusicMan.world_size);
			pTool.setText("orc:", MusicMan.races["orc"].buildings);
			pTool.setText("elf:", MusicMan.races["elf"].buildings);
			pTool.setText("human:", MusicMan.races["human"].buildings);
			pTool.setText("dwarf:", MusicMan.races["dwarf"].buildings);
			pTool.setText("forest:", MusicMan.dict_values["forest"].amount);
			pTool.setText("tropical:", MusicMan.dict_values["tropical"].amount);
			pTool.setText("grassland:", MusicMan.dict_values["grassland"].amount);
			pTool.setText("desert:", MusicMan.dict_values["desert"].amount);
			pTool.setText("", "");
			pTool.setText("cur_time_god:", MusicMan.music_god.currentTime);
			foreach (MusicLayer musicLayer in MusicMan.music_god.active)
			{
				pTool.setText(musicLayer.id + " t|v", musicLayer.s.time.ToString() + " " + musicLayer.s.volume.ToString());
			}
			pTool.setText("", "");
			pTool.setText("", "");
			pTool.setText("cur_time_world:", MusicMan.music_world.currentTime);
			foreach (MusicLayer musicLayer2 in MusicMan.music_world.active)
			{
				pTool.setText(musicLayer2.id + " t|v", musicLayer2.s.time.ToString() + " " + musicLayer2.s.volume.ToString());
			}
		}

		// Token: 0x040014CC RID: 5324
		public MusicLayer prefabLayer;

		// Token: 0x040014CD RID: 5325
		private static List<MusicLayer> list_layers = new List<MusicLayer>();

		// Token: 0x040014CE RID: 5326
		private static Dictionary<string, MusicLayer> dict_layers = new Dictionary<string, MusicLayer>();

		// Token: 0x040014CF RID: 5327
		public static List<MusicValueContainer> list_values = new List<MusicValueContainer>();

		// Token: 0x040014D0 RID: 5328
		public static Dictionary<string, MusicValueContainer> dict_values = new Dictionary<string, MusicValueContainer>();

		// Token: 0x040014D1 RID: 5329
		public static Dictionary<string, MusicRaceContainer> races = new Dictionary<string, MusicRaceContainer>();

		// Token: 0x040014D2 RID: 5330
		public static MusicPlayBox music_world;

		// Token: 0x040014D3 RID: 5331
		public static MusicPlayBox music_god;

		// Token: 0x040014D4 RID: 5332
		public static bool godView;

		// Token: 0x040014D5 RID: 5333
		public static int world_size;
	}
}
