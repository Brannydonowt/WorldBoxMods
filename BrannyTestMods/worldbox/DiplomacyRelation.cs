using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000170 RID: 368
[Serializable]
public class DiplomacyRelation
{
	// Token: 0x06000847 RID: 2119 RVA: 0x0005A580 File Offset: 0x00058780
	public KingdomOpinion getOpinion(Kingdom pMain, Kingdom pTarget)
	{
		if (this.opinion_k1.target == pTarget)
		{
			return this.opinion_k1;
		}
		return this.opinion_k2;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0005A5A0 File Offset: 0x000587A0
	public void recalculate()
	{
		if (this.opinion_k1 == null)
		{
			this.opinion_k1 = new KingdomOpinion(this.kingdom1, this.kingdom2);
			this.opinion_k2 = new KingdomOpinion(this.kingdom2, this.kingdom1);
		}
		try
		{
			this.opinion_k1.calculate(this.kingdom1, this.kingdom2, this);
			this.opinion_k2.calculate(this.kingdom2, this.kingdom1, this);
		}
		catch (Exception ex)
		{
			string str = "kingdom1 ";
			Kingdom kingdom = this.kingdom1;
			string a = str + ((kingdom != null) ? kingdom.ToString() : null);
			string str2 = " ";
			Kingdom kingdom2 = this.kingdom1;
			int? num;
			if (kingdom2 == null)
			{
				num = null;
			}
			else
			{
				Dictionary<Kingdom, bool> civs_allies = kingdom2.civs_allies;
				num = ((civs_allies != null) ? new int?(civs_allies.Count) : null);
			}
			int? num2 = num;
			Debug.LogError(a == str2 + num2.ToString());
			string str3 = "kingdom2 ";
			Kingdom kingdom3 = this.kingdom2;
			string a2 = str3 + ((kingdom3 != null) ? kingdom3.ToString() : null);
			string str4 = " ";
			Kingdom kingdom4 = this.kingdom2;
			int? num3;
			if (kingdom4 == null)
			{
				num3 = null;
			}
			else
			{
				Dictionary<Kingdom, bool> civs_allies2 = kingdom4.civs_allies;
				num3 = ((civs_allies2 != null) ? new int?(civs_allies2.Count) : null);
			}
			num2 = num3;
			Debug.LogError(a2 == str4 + num2.ToString());
			Debug.LogError(JsonUtility.ToJson(this.kingdom1));
			Debug.LogError(JsonUtility.ToJson(this.kingdom2));
			throw ex;
		}
	}

	// Token: 0x04000ABF RID: 2751
	public string id;

	// Token: 0x04000AC0 RID: 2752
	[NonSerialized]
	public Kingdom kingdom1;

	// Token: 0x04000AC1 RID: 2753
	[NonSerialized]
	public Kingdom kingdom2;

	// Token: 0x04000AC2 RID: 2754
	public string kingdom1_id;

	// Token: 0x04000AC3 RID: 2755
	public string kingdom2_id;

	// Token: 0x04000AC4 RID: 2756
	public int stateChanged;

	// Token: 0x04000AC5 RID: 2757
	public int peaceSince;

	// Token: 0x04000AC6 RID: 2758
	public int warSince;

	// Token: 0x04000AC7 RID: 2759
	internal KingdomOpinion opinion_k1;

	// Token: 0x04000AC8 RID: 2760
	internal KingdomOpinion opinion_k2;

	// Token: 0x04000AC9 RID: 2761
	public DiplomacyState state;
}
