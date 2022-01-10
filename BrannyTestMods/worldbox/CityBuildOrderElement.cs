using System;

// Token: 0x02000020 RID: 32
public class CityBuildOrderElement
{
	// Token: 0x040000CC RID: 204
	public string buildingID;

	// Token: 0x040000CD RID: 205
	public int limitType;

	// Token: 0x040000CE RID: 206
	public int limitID;

	// Token: 0x040000CF RID: 207
	public int priority;

	// Token: 0x040000D0 RID: 208
	public bool addRace;

	// Token: 0x040000D1 RID: 209
	public int requiredPop;

	// Token: 0x040000D2 RID: 210
	public int requiredBuildings;

	// Token: 0x040000D3 RID: 211
	public bool waitForResources = true;

	// Token: 0x040000D4 RID: 212
	public bool usedByRacesCheck;

	// Token: 0x040000D5 RID: 213
	public string usedByRaces = "human,orc,elf,dwarf";

	// Token: 0x040000D6 RID: 214
	public bool checkFullVillage;
}
