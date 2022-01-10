using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000224 RID: 548
[Serializable]
public class CityData
{
	// Token: 0x04000ED9 RID: 3801
	public string cityName = "NEW_CITY";

	// Token: 0x04000EDA RID: 3802
	public CityStorage storage = new CityStorage();

	// Token: 0x04000EDB RID: 3803
	[DefaultValue("")]
	public string cityID = "";

	// Token: 0x04000EDC RID: 3804
	[DefaultValue("")]
	public string kingdomID = "";

	// Token: 0x04000EDD RID: 3805
	[DefaultValue("")]
	public string leaderID = "";

	// Token: 0x04000EDE RID: 3806
	public List<ZoneData> zones = new List<ZoneData>();

	// Token: 0x04000EDF RID: 3807
	[DefaultValue("")]
	public string culture = string.Empty;

	// Token: 0x04000EE0 RID: 3808
	public string race = "null";

	// Token: 0x04000EE1 RID: 3809
	public int age;

	// Token: 0x04000EE2 RID: 3810
	public int deaths;

	// Token: 0x04000EE3 RID: 3811
	public int born;

	// Token: 0x04000EE4 RID: 3812
	public float timer_supply;

	// Token: 0x04000EE5 RID: 3813
	public float timer_trade;

	// Token: 0x04000EE6 RID: 3814
	public float timer_revolt;

	// Token: 0x04000EE7 RID: 3815
	public List<ActorData> popPoints = new List<ActorData>();
}
