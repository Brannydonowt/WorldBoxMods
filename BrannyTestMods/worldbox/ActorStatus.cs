using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000071 RID: 113
[Serializable]
public class ActorStatus : BaseObjectData
{
	// Token: 0x0600028E RID: 654 RVA: 0x0002D3FC File Offset: 0x0002B5FC
	public void generateTraits(ActorStats pStats, Race pRace)
	{
		if (pStats.traits != null)
		{
			for (int i = 0; i < pStats.traits.Count; i++)
			{
				string pTrait = pStats.traits[i];
				this.addTrait(pTrait);
			}
		}
		if (pRace.civilization)
		{
			this.generateCivUnitTraits();
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0002D449 File Offset: 0x0002B649
	public void addTrait(string pTrait)
	{
		if (this.traits.Contains(pTrait))
		{
			return;
		}
		this.traits.Add(pTrait);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0002D468 File Offset: 0x0002B668
	internal void inheritTraits(List<string> pTraits)
	{
		for (int i = 0; i < pTraits.Count; i++)
		{
			string pID = pTraits[i];
			ActorTrait actorTrait = AssetManager.traits.get(pID);
			if (actorTrait != null && actorTrait.inherit != 0f)
			{
				float num = Toolbox.randomFloat(0f, 100f);
				if (actorTrait.inherit >= num && !this.traits.Contains(actorTrait.id) && !this.haveOppositeTrait(actorTrait))
				{
					this.addTrait(actorTrait.id);
				}
			}
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0002D4EC File Offset: 0x0002B6EC
	private void generateCivUnitTraits()
	{
		for (int i = 0; i < AssetManager.traits.list.Count; i++)
		{
			ActorTrait actorTrait = AssetManager.traits.list[i];
			if (actorTrait.birth != 0f)
			{
				float num = Toolbox.randomFloat(0f, 100f);
				if (actorTrait.birth >= num && !this.traits.Contains(actorTrait.id) && !this.haveOppositeTrait(actorTrait))
				{
					this.addTrait(actorTrait.id);
				}
			}
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0002D574 File Offset: 0x0002B774
	internal bool haveOppositeTrait(ActorTrait pTraitMain)
	{
		if (pTraitMain == null)
		{
			return false;
		}
		if (pTraitMain.oppositeArr == null)
		{
			return false;
		}
		for (int i = 0; i < pTraitMain.oppositeArr.Length; i++)
		{
			string text = pTraitMain.oppositeArr[i];
			if (this.traits.Contains(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
	internal bool updateAge(Race pRace)
	{
		this.age++;
		ActorStats actorStats = AssetManager.unitStats.get(this.statsID);
		this.updateAttributes(actorStats, pRace, false);
		if (!MapBox.instance.worldLaws.world_law_old_age.boolVal)
		{
			return true;
		}
		int num = actorStats.maxAge;
		Culture culture = MapBox.instance.cultures.get(this.culture);
		if (culture != null)
		{
			num += culture.getMaxAgeBonus();
		}
		return num == 0 || num > this.age || !Toolbox.randomChance(0.15f);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0002D658 File Offset: 0x0002B858
	internal void updateAttributes(ActorStats pStats, Race pRace, bool pForce = false)
	{
		if (!pStats.unit)
		{
			return;
		}
		if ((this.age % 3 == 0 && this.age <= 100) || pForce)
		{
			string random = pRace.preferred_attribute.GetRandom<string>();
			if (random != null)
			{
				if (random == "intelligence")
				{
					this.intelligence++;
					return;
				}
				if (random == "diplomacy")
				{
					this.diplomacy++;
					return;
				}
				if (random == "warfare")
				{
					this.warfare++;
					return;
				}
				if (!(random == "stewardship"))
				{
					return;
				}
				this.stewardship++;
			}
		}
	}

	// Token: 0x04000364 RID: 868
	[DefaultValue("")]
	public string actorID = "";

	// Token: 0x04000365 RID: 869
	public string firstName = "firstName";

	// Token: 0x04000366 RID: 870
	[DefaultValue("")]
	public string favoriteFood = "";

	// Token: 0x04000367 RID: 871
	[DefaultValue("")]
	public string mood = "";

	// Token: 0x04000368 RID: 872
	[DefaultValue("")]
	public string transportID = "";

	// Token: 0x04000369 RID: 873
	[DefaultValue(ActorGender.Unknown)]
	public ActorGender gender;

	// Token: 0x0400036A RID: 874
	[DefaultValue(-1)]
	public int head = -1;

	// Token: 0x0400036B RID: 875
	[DefaultValue(-1)]
	public int skin = -1;

	// Token: 0x0400036C RID: 876
	[DefaultValue(-1)]
	public int skin_set = -1;

	// Token: 0x0400036D RID: 877
	[DefaultValue("")]
	public string culture = string.Empty;

	// Token: 0x0400036E RID: 878
	[DefaultValue("")]
	public string homeBuildingID = "";

	// Token: 0x0400036F RID: 879
	public string statsID = "?";

	// Token: 0x04000370 RID: 880
	public UnitProfession profession;

	// Token: 0x04000371 RID: 881
	public int kills;

	// Token: 0x04000372 RID: 882
	public int age = 1;

	// Token: 0x04000373 RID: 883
	public int bornTime;

	// Token: 0x04000374 RID: 884
	[DefaultValue(0)]
	public int children;

	// Token: 0x04000375 RID: 885
	[DefaultValue(0)]
	public int hunger;

	// Token: 0x04000376 RID: 886
	[DefaultValue(1)]
	public int level = 1;

	// Token: 0x04000377 RID: 887
	[DefaultValue(0)]
	public int experience;

	// Token: 0x04000378 RID: 888
	[DefaultValue(0)]
	public int diplomacy;

	// Token: 0x04000379 RID: 889
	[DefaultValue(0)]
	public int intelligence;

	// Token: 0x0400037A RID: 890
	[DefaultValue(0)]
	public int stewardship;

	// Token: 0x0400037B RID: 891
	[DefaultValue(0)]
	public int warfare;

	// Token: 0x0400037C RID: 892
	[DefaultValue("")]
	public string special_graphics = string.Empty;

	// Token: 0x0400037D RID: 893
	public bool favorite;

	// Token: 0x0400037E RID: 894
	public List<string> traits = new List<string>();
}
