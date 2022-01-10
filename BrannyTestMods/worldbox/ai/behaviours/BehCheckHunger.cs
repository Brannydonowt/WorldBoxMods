using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000342 RID: 834
	public class BehCheckHunger : BehaviourActionActor
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x000A023C File Offset: 0x0009E43C
		public override BehResult execute(Actor pActor)
		{
			if (!MapBox.instance.worldLaws.world_law_hunger.boolVal)
			{
				return BehResult.Stop;
			}
			if (pActor.data.hunger > pActor.stats.maxHunger / 2)
			{
				return BehResult.Stop;
			}
			BehaviourActionActor.temp_tasks.Clear();
			if (pActor.stats.diet_berries)
			{
				BehaviourActionActor.temp_tasks.Add("eat_fruits");
			}
			if (pActor.stats.diet_flowers)
			{
				BehaviourActionActor.temp_tasks.Add("eat_flowers");
			}
			if (pActor.stats.diet_crops)
			{
				BehaviourActionActor.temp_tasks.Add("eat_crops");
			}
			if (pActor.stats.diet_grass)
			{
				BehaviourActionActor.temp_tasks.Add("eat_grass");
			}
			if (pActor.stats.diet_meat || pActor.stats.diet_meat_insect)
			{
				BehaviourActionActor.temp_tasks.Add("hunting_attack");
			}
			if (BehaviourActionActor.temp_tasks.Count > 0)
			{
				if (pActor.diet_index >= BehaviourActionActor.temp_tasks.Count)
				{
					pActor.diet_index = 0;
				}
				List<string> temp_tasks = BehaviourActionActor.temp_tasks;
				int diet_index = pActor.diet_index;
				pActor.diet_index = diet_index + 1;
				return base.forceTask(pActor, temp_tasks[diet_index], true);
			}
			return BehResult.Continue;
		}
	}
}
