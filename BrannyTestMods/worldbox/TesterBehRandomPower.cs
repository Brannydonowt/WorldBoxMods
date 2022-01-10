using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class TesterBehRandomPower : BehaviourActionTester
{
	// Token: 0x06000B4F RID: 2895 RVA: 0x0006DCFC File Offset: 0x0006BEFC
	public TesterBehRandomPower()
	{
		if (TesterBehRandomPower.events.Count == 0)
		{
			foreach (GodPower godPower in AssetManager.powers.list)
			{
				if (godPower.id[0] != '_' && godPower.tester_enabled)
				{
					TesterBehRandomPower.events.Add(godPower.id);
					if (godPower.type == PowerActionType.Tile)
					{
						TesterBehRandomPower.events.Add(godPower.id);
						TesterBehRandomPower.events.Add(godPower.id);
						TesterBehRandomPower.events.Add(godPower.id);
					}
				}
			}
		}
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x0006DDC4 File Offset: 0x0006BFC4
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehRandomPower.last_id > TesterBehRandomPower.events.Count - 1)
		{
			TesterBehRandomPower.last_id = 0;
			TesterBehRandomPower.events.Shuffle<string>();
		}
		string text = TesterBehRandomPower.events[TesterBehRandomPower.last_id++];
		int x = Toolbox.randomInt(0, MapBox.width);
		int y = Toolbox.randomInt(0, MapBox.height);
		if (!AssetManager.powers.dict.ContainsKey(text))
		{
			Debug.LogError("TESTER ERROR... " + text);
			return BehResult.Continue;
		}
		GodPower pPower = AssetManager.powers.get(text);
		Config.currentBrush = Brush.getRandom();
		pObject.debugString = "rand_power_" + text;
		BehaviourActionBase<AutoTesterBot>.world.Clicked(new Vector2Int(x, y), pPower, null);
		return base.execute(pObject);
	}

	// Token: 0x04000D91 RID: 3473
	private static List<string> events = new List<string>();

	// Token: 0x04000D92 RID: 3474
	private static int last_id = 0;
}
