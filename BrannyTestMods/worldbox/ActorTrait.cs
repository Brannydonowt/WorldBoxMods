using System;

// Token: 0x02000019 RID: 25
[Serializable]
public class ActorTrait : Asset
{
	// Token: 0x04000068 RID: 104
	public string icon;

	// Token: 0x04000069 RID: 105
	public float birth;

	// Token: 0x0400006A RID: 106
	public float inherit;

	// Token: 0x0400006B RID: 107
	public string opposite;

	// Token: 0x0400006C RID: 108
	public string[] oppositeArr;

	// Token: 0x0400006D RID: 109
	public bool transformationTrait;

	// Token: 0x0400006E RID: 110
	public BaseStats baseStats = new BaseStats();

	// Token: 0x0400006F RID: 111
	public int sameTraitMod;

	// Token: 0x04000070 RID: 112
	public int oppositeTraitMod;

	// Token: 0x04000071 RID: 113
	public TraitGroup group = TraitGroup.Other;

	// Token: 0x04000072 RID: 114
	public TraitType type = TraitType.Other;

	// Token: 0x04000073 RID: 115
	public WorldAction action_death;

	// Token: 0x04000074 RID: 116
	public WorldAction action_special_effect;

	// Token: 0x04000075 RID: 117
	public bool can_be_cured;
}
