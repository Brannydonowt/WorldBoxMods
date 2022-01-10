using System;
using System.Collections.Generic;

// Token: 0x02000222 RID: 546
[Serializable]
public class WorldLaws
{
	// Token: 0x06000C52 RID: 3154 RVA: 0x000792C8 File Offset: 0x000774C8
	public PlayerOptionData add(PlayerOptionData pData)
	{
		foreach (PlayerOptionData playerOptionData in this.list)
		{
			if (string.Equals(pData.name, playerOptionData.name))
			{
				this.dict.Add(playerOptionData.name, playerOptionData);
				return playerOptionData;
			}
		}
		this.list.Add(pData);
		this.dict.Add(pData.name, pData);
		return pData;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00079360 File Offset: 0x00077560
	public void check()
	{
		this.init();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00079368 File Offset: 0x00077568
	public void init()
	{
		if (this.list == null)
		{
			this.list = new List<PlayerOptionData>();
		}
		if (this.dict == null)
		{
			this.dict = new Dictionary<string, PlayerOptionData>();
		}
		this.world_law_diplomacy = this.add(new PlayerOptionData("world_law_diplomacy")
		{
			boolVal = true
		});
		this.world_law_peaceful_monsters = this.add(new PlayerOptionData("world_law_peaceful_monsters")
		{
			boolVal = false
		});
		this.world_law_hunger = this.add(new PlayerOptionData("world_law_hunger")
		{
			boolVal = true
		});
		this.world_law_grow_trees = this.add(new PlayerOptionData("world_law_grow_trees")
		{
			boolVal = true
		});
		this.world_law_grow_grass = this.add(new PlayerOptionData("world_law_grow_grass")
		{
			boolVal = true
		});
		this.world_law_biome_overgrowth = this.add(new PlayerOptionData("world_law_biome_overgrowth")
		{
			boolVal = true
		});
		this.world_law_kingdom_expansion = this.add(new PlayerOptionData("world_law_kingdom_expansion")
		{
			boolVal = true
		});
		this.world_law_old_age = this.add(new PlayerOptionData("world_law_old_age")
		{
			boolVal = true
		});
		this.world_law_animals_spawn = this.add(new PlayerOptionData("world_law_animals_spawn")
		{
			boolVal = true
		});
		this.world_law_rebellions = this.add(new PlayerOptionData("world_law_rebellions")
		{
			boolVal = true
		});
		this.world_law_border_stealing = this.add(new PlayerOptionData("world_law_border_stealing")
		{
			boolVal = true
		});
		this.world_law_erosion = this.add(new PlayerOptionData("world_law_erosion")
		{
			boolVal = true
		});
		this.world_law_forever_lava = this.add(new PlayerOptionData("world_law_forever_lava")
		{
			boolVal = false
		});
		this.world_law_disasters_nature = this.add(new PlayerOptionData("world_law_disasters_nature")
		{
			boolVal = false
		});
		this.world_law_disasters_other = this.add(new PlayerOptionData("world_law_disasters_other")
		{
			boolVal = false
		});
		this.world_law_angry_civilians = this.add(new PlayerOptionData("world_law_angry_civilians")
		{
			boolVal = false
		});
	}

	// Token: 0x04000EB4 RID: 3764
	[NonSerialized]
	public PlayerOptionData world_law_diplomacy;

	// Token: 0x04000EB5 RID: 3765
	[NonSerialized]
	public PlayerOptionData world_law_peaceful_monsters;

	// Token: 0x04000EB6 RID: 3766
	[NonSerialized]
	public PlayerOptionData world_law_hunger;

	// Token: 0x04000EB7 RID: 3767
	[NonSerialized]
	public PlayerOptionData world_law_grow_trees;

	// Token: 0x04000EB8 RID: 3768
	[NonSerialized]
	public PlayerOptionData world_law_grow_grass;

	// Token: 0x04000EB9 RID: 3769
	[NonSerialized]
	public PlayerOptionData world_law_biome_overgrowth;

	// Token: 0x04000EBA RID: 3770
	[NonSerialized]
	public PlayerOptionData world_law_kingdom_expansion;

	// Token: 0x04000EBB RID: 3771
	[NonSerialized]
	public PlayerOptionData world_law_old_age;

	// Token: 0x04000EBC RID: 3772
	[NonSerialized]
	public PlayerOptionData world_law_animals_spawn;

	// Token: 0x04000EBD RID: 3773
	[NonSerialized]
	public PlayerOptionData world_law_rebellions;

	// Token: 0x04000EBE RID: 3774
	[NonSerialized]
	public PlayerOptionData world_law_border_stealing;

	// Token: 0x04000EBF RID: 3775
	[NonSerialized]
	public PlayerOptionData world_law_erosion;

	// Token: 0x04000EC0 RID: 3776
	[NonSerialized]
	public PlayerOptionData world_law_forever_lava;

	// Token: 0x04000EC1 RID: 3777
	[NonSerialized]
	public PlayerOptionData world_law_disasters_nature;

	// Token: 0x04000EC2 RID: 3778
	[NonSerialized]
	public PlayerOptionData world_law_disasters_other;

	// Token: 0x04000EC3 RID: 3779
	[NonSerialized]
	public PlayerOptionData world_law_angry_civilians;

	// Token: 0x04000EC4 RID: 3780
	public List<PlayerOptionData> list;

	// Token: 0x04000EC5 RID: 3781
	[NonSerialized]
	public Dictionary<string, PlayerOptionData> dict;
}
