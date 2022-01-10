using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020001F2 RID: 498
public class TesterBehCreateBuildings : BehaviourActionTester
{
	// Token: 0x06000B52 RID: 2898 RVA: 0x0006DE9C File Offset: 0x0006C09C
	public TesterBehCreateBuildings()
	{
		if (TesterBehCreateBuildings.assets.Count == 0)
		{
			TesterBehCreateBuildings.assets.Add("tree");
			TesterBehCreateBuildings.assets.Add("stone");
			TesterBehCreateBuildings.assets.Add("ore_deposit");
			TesterBehCreateBuildings.assets.Add("gold");
			TesterBehCreateBuildings.assets.Add("fruit_bush");
			TesterBehCreateBuildings.assets.Add("palm");
			TesterBehCreateBuildings.assets.Add("pine");
			TesterBehCreateBuildings.assets.Add("tumor");
			TesterBehCreateBuildings.assets.Add("goldenBrain");
			TesterBehCreateBuildings.assets.Add("corruptedBrain");
			TesterBehCreateBuildings.assets.Add("beehive");
			TesterBehCreateBuildings.assets.Add("iceTower");
			TesterBehCreateBuildings.assets.Add("flameTower");
			TesterBehCreateBuildings.assets.Add("volcano");
			TesterBehCreateBuildings.assets.Add("geyserAcid");
			TesterBehCreateBuildings.assets.Add("geyser");
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0006DFB0 File Offset: 0x0006C1B0
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehCreateBuildings.last_id > TesterBehCreateBuildings.assets.Count - 1)
		{
			TesterBehCreateBuildings.last_id = 0;
			TesterBehCreateBuildings.assets.Shuffle<string>();
		}
		string pID = TesterBehCreateBuildings.assets[TesterBehCreateBuildings.last_id++];
		for (int i = 0; i < 3; i++)
		{
			TileZone random = BehaviourActionBase<AutoTesterBot>.world.zoneCalculator.zones.GetRandom<TileZone>();
			BehaviourActionBase<AutoTesterBot>.world.addBuilding(pID, random.centerTile, null, true, false, BuildPlacingType.New);
		}
		return base.execute(pObject);
	}

	// Token: 0x04000D93 RID: 3475
	private static List<string> assets = new List<string>();

	// Token: 0x04000D94 RID: 3476
	private static int last_id = 0;
}
