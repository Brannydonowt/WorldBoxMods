using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020001F3 RID: 499
public class TesterBehCreateUnits : BehaviourActionTester
{
	// Token: 0x06000B55 RID: 2901 RVA: 0x0006E048 File Offset: 0x0006C248
	public TesterBehCreateUnits()
	{
		if (TesterBehCreateUnits.assets.Count == 0)
		{
			TesterBehCreateUnits.assets.Add("unit_human");
			TesterBehCreateUnits.assets.Add("unit_elf");
			TesterBehCreateUnits.assets.Add("unit_orc");
			TesterBehCreateUnits.assets.Add("unit_dwarf");
			TesterBehCreateUnits.assets.Add("baby_human");
			TesterBehCreateUnits.assets.Add("baby_elf");
			TesterBehCreateUnits.assets.Add("baby_orc");
			TesterBehCreateUnits.assets.Add("baby_dwarf");
			TesterBehCreateUnits.assets.Add("antBlack");
			TesterBehCreateUnits.assets.Add("antGreen");
			TesterBehCreateUnits.assets.Add("antBlue");
			TesterBehCreateUnits.assets.Add("antRed");
			TesterBehCreateUnits.assets.Add("sandSpider");
			TesterBehCreateUnits.assets.Add("worm");
			TesterBehCreateUnits.assets.Add("printer");
			TesterBehCreateUnits.assets.Add("dragon");
			TesterBehCreateUnits.assets.Add("zombie_dragon");
			TesterBehCreateUnits.assets.Add("UFO");
			TesterBehCreateUnits.assets.Add("tornado");
			TesterBehCreateUnits.assets.Add("godFinger");
			TesterBehCreateUnits.assets.Add("santa");
			TesterBehCreateUnits.assets.Add("sheep");
			TesterBehCreateUnits.assets.Add("cow");
			TesterBehCreateUnits.assets.Add("penguin");
			TesterBehCreateUnits.assets.Add("turtle");
			TesterBehCreateUnits.assets.Add("crab");
			TesterBehCreateUnits.assets.Add("fairy");
			TesterBehCreateUnits.assets.Add("bee");
			TesterBehCreateUnits.assets.Add("fly");
			TesterBehCreateUnits.assets.Add("butterfly");
			TesterBehCreateUnits.assets.Add("grasshopper");
			TesterBehCreateUnits.assets.Add("beetle");
			TesterBehCreateUnits.assets.Add("egg_chicken");
			TesterBehCreateUnits.assets.Add("egg_crab");
			TesterBehCreateUnits.assets.Add("egg_turtle");
			TesterBehCreateUnits.assets.Add("chicken");
			TesterBehCreateUnits.assets.Add("chick");
			TesterBehCreateUnits.assets.Add("rat");
			TesterBehCreateUnits.assets.Add("zombie_rat");
			TesterBehCreateUnits.assets.Add("ratKing");
			TesterBehCreateUnits.assets.Add("zombie_ratKing");
			TesterBehCreateUnits.assets.Add("cat");
			TesterBehCreateUnits.assets.Add("rabbit");
			TesterBehCreateUnits.assets.Add("piranha");
			TesterBehCreateUnits.assets.Add("bandit");
			TesterBehCreateUnits.assets.Add("snowman");
			TesterBehCreateUnits.assets.Add("bear");
			TesterBehCreateUnits.assets.Add("wolf");
			TesterBehCreateUnits.assets.Add("demon");
			TesterBehCreateUnits.assets.Add("zombie");
			TesterBehCreateUnits.assets.Add("necromancer");
			TesterBehCreateUnits.assets.Add("whiteMage");
			TesterBehCreateUnits.assets.Add("evilMage");
			TesterBehCreateUnits.assets.Add("skeleton");
			TesterBehCreateUnits.assets.Add("skeleton_cursed");
			TesterBehCreateUnits.assets.Add("walker");
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x0006E3B4 File Offset: 0x0006C5B4
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehCreateUnits.last_id > TesterBehCreateUnits.assets.Count - 1)
		{
			TesterBehCreateUnits.last_id = 0;
			TesterBehCreateUnits.assets.Shuffle<string>();
		}
		string pStatsID = TesterBehCreateUnits.assets[TesterBehCreateUnits.last_id++];
		for (int i = 0; i < 10; i++)
		{
			TileZone random = BehaviourActionBase<AutoTesterBot>.world.zoneCalculator.zones.GetRandom<TileZone>();
			BehaviourActionBase<AutoTesterBot>.world.spawnNewUnit(pStatsID, random.centerTile, "", 6f);
		}
		return base.execute(pObject);
	}

	// Token: 0x04000D95 RID: 3477
	private static List<string> assets = new List<string>();

	// Token: 0x04000D96 RID: 3478
	private static int last_id = 0;
}
