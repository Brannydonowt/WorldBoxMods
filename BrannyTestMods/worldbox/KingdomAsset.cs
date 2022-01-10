using System;
using System.Collections.Generic;

// Token: 0x02000090 RID: 144
public class KingdomAsset : Asset
{
	// Token: 0x0600031D RID: 797 RVA: 0x00033720 File Offset: 0x00031920
	public override void create()
	{
		base.create();
		this.hashCode = this.GetHashCode();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00033734 File Offset: 0x00031934
	public void addTag(string pTag)
	{
		this.tags.Add(pTag);
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00033743 File Offset: 0x00031943
	public void addFriendlyTag(string pTag)
	{
		this.friendly_tags.Add(pTag);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x00033752 File Offset: 0x00031952
	public void addEnemyTag(string pTag)
	{
		this.enemy_tags.Add(pTag);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00033764 File Offset: 0x00031964
	public bool isFoe(KingdomAsset pTarget)
	{
		int num = 0;
		this.cached_enemies.TryGetValue(pTarget.hashCode, ref num);
		if (num != 0)
		{
			return num == 1;
		}
		if (this.nature || pTarget.nature)
		{
			this.cached_enemies.Add(pTarget.hashCode, -1);
			return false;
		}
		if (this == pTarget)
		{
			this.cached_enemies.Add(pTarget.hashCode, this.attack_each_other ? 1 : -1);
			return this.attack_each_other;
		}
		if (this.enemy_tags.Count > 0 && this.enemy_tags.Overlaps(pTarget.tags))
		{
			this.cached_enemies.Add(pTarget.hashCode, 1);
			return true;
		}
		if (this.friendly_tags.Count > 0 && this.friendly_tags.Overlaps(pTarget.tags))
		{
			this.cached_enemies.Add(pTarget.hashCode, -1);
			return false;
		}
		this.cached_enemies.Add(pTarget.hashCode, 1);
		return true;
	}

	// Token: 0x04000516 RID: 1302
	public bool civ;

	// Token: 0x04000517 RID: 1303
	public bool nomads;

	// Token: 0x04000518 RID: 1304
	public bool nature;

	// Token: 0x04000519 RID: 1305
	public bool attack_each_other;

	// Token: 0x0400051A RID: 1306
	public bool mobs;

	// Token: 0x0400051B RID: 1307
	public bool mad;

	// Token: 0x0400051C RID: 1308
	public bool brain;

	// Token: 0x0400051D RID: 1309
	public HashSet<string> friendly_tags = new HashSet<string>();

	// Token: 0x0400051E RID: 1310
	public HashSet<string> enemy_tags = new HashSet<string>();

	// Token: 0x0400051F RID: 1311
	public HashSet<string> tags = new HashSet<string>();

	// Token: 0x04000520 RID: 1312
	[NonSerialized]
	public Dictionary<int, int> cached_enemies = new Dictionary<int, int>();

	// Token: 0x04000521 RID: 1313
	[NonSerialized]
	public int hashCode;

	// Token: 0x04000522 RID: 1314
	public KingdomColor default_kingdom_color;
}
