using System;

namespace ai.behaviours
{
	// Token: 0x0200036F RID: 879
	public class BehGetResourcesFromMine : BehaviourActionActor
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x000A2538 File Offset: 0x000A0738
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000A2548 File Offset: 0x000A0748
		public override BehResult execute(Actor pActor)
		{
			ResourceAsset random = AssetManager.resources.poolMine.GetRandom<ResourceAsset>();
			if (pActor.haveTrait("miner") && random.id == "stone")
			{
				random = AssetManager.resources.poolMine.GetRandom<ResourceAsset>();
			}
			pActor.inventory.add(random.id, 1);
			return BehResult.Continue;
		}
	}
}
