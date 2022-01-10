using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Token: 0x02000229 RID: 553
[Serializable]
public class MapMetaData
{
	// Token: 0x06000C64 RID: 3172 RVA: 0x00079F05 File Offset: 0x00078105
	public void prepareForSave()
	{
		this.modded = Config.MODDED;
		this.timestamp = Epoch.Current();
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00079F1D File Offset: 0x0007811D
	public string toJson()
	{
		return JsonConvert.SerializeObject(this, new JsonSerializerSettings
		{
			DefaultValueHandling = 3
		});
	}

	// Token: 0x04000EFC RID: 3836
	[NonSerialized]
	public string temp_date_string = "";

	// Token: 0x04000EFD RID: 3837
	public int saveVersion;

	// Token: 0x04000EFE RID: 3838
	public int width;

	// Token: 0x04000EFF RID: 3839
	public int height;

	// Token: 0x04000F00 RID: 3840
	public MapStats mapStats;

	// Token: 0x04000F01 RID: 3841
	public int cities;

	// Token: 0x04000F02 RID: 3842
	public int units;

	// Token: 0x04000F03 RID: 3843
	public int population;

	// Token: 0x04000F04 RID: 3844
	public int buildings;

	// Token: 0x04000F05 RID: 3845
	public int structures;

	// Token: 0x04000F06 RID: 3846
	public int kingdoms;

	// Token: 0x04000F07 RID: 3847
	public int mobs;

	// Token: 0x04000F08 RID: 3848
	public int cultures;

	// Token: 0x04000F09 RID: 3849
	public bool modded;

	// Token: 0x04000F0A RID: 3850
	public double timestamp;

	// Token: 0x04000F0B RID: 3851
	public List<string> races;
}
