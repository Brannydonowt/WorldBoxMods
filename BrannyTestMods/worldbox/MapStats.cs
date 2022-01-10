using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
[Serializable]
public class MapStats
{
	// Token: 0x06000BFD RID: 3069 RVA: 0x00076B20 File Offset: 0x00074D20
	internal void updateAge(float pElapsed)
	{
		this.worldTime += pElapsed;
		if (this.worldTime > 3f)
		{
			this.month++;
			this.worldTime = 0f;
			MapBox.instance.updateCultures();
		}
		if (this.month > 11)
		{
			this.year++;
			this.month = 0;
			MapBox.instance.updateObjectAge();
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00076B94 File Offset: 0x00074D94
	public string getNextId(string pType)
	{
		string result = "";
		if (pType != null)
		{
			if (pType == "culture")
			{
				string str = "c_";
				long num = this.id_culture;
				this.id_culture = num + 1L;
				return str + num.ToString();
			}
			if (pType == "unit")
			{
				string str2 = "u_";
				long num = this.id_unit;
				this.id_unit = num + 1L;
				return str2 + num.ToString();
			}
			if (pType == "building")
			{
				string str3 = "b_";
				long num = this.id_building;
				this.id_building = num + 1L;
				return str3 + num.ToString();
			}
			if (pType == "kingdom")
			{
				string str4 = "k_";
				long num = this.id_kingdom;
				this.id_kingdom = num + 1L;
				return str4 + num.ToString();
			}
			if (pType == "city")
			{
				string str5 = "c_";
				long num = this.id_city;
				this.id_city = num + 1L;
				return str5 + num.ToString();
			}
		}
		Debug.LogError("NO pType for id " + pType);
		return result;
	}

	// Token: 0x04000E6D RID: 3693
	public string name = "WorldBox";

	// Token: 0x04000E6E RID: 3694
	public string description = "";

	// Token: 0x04000E6F RID: 3695
	public int month;

	// Token: 0x04000E70 RID: 3696
	public int year;

	// Token: 0x04000E71 RID: 3697
	public float worldTime;

	// Token: 0x04000E72 RID: 3698
	public int deaths;

	// Token: 0x04000E73 RID: 3699
	public int deaths_age;

	// Token: 0x04000E74 RID: 3700
	public int deaths_hunger;

	// Token: 0x04000E75 RID: 3701
	public int deaths_eaten;

	// Token: 0x04000E76 RID: 3702
	public int deaths_plague;

	// Token: 0x04000E77 RID: 3703
	public int deaths_other;

	// Token: 0x04000E78 RID: 3704
	public int housesDestroyed;

	// Token: 0x04000E79 RID: 3705
	public int population;

	// Token: 0x04000E7A RID: 3706
	public long id_unit;

	// Token: 0x04000E7B RID: 3707
	public long id_building;

	// Token: 0x04000E7C RID: 3708
	public long id_kingdom;

	// Token: 0x04000E7D RID: 3709
	public long id_city;

	// Token: 0x04000E7E RID: 3710
	public long id_culture;
}
