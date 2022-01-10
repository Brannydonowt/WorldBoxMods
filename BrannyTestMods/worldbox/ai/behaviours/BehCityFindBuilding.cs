using System;

namespace ai.behaviours
{
	// Token: 0x0200034A RID: 842
	public class BehCityFindBuilding : BehCity
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x000A04DC File Offset: 0x0009E6DC
		public BehCityFindBuilding(string pType)
		{
			this.type = pType;
			if (pType.Contains(","))
			{
				this.types = pType.Split(new char[]
				{
					','
				});
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000A050F File Offset: 0x0009E70F
		public override BehResult execute(Actor pActor)
		{
			if (this.types != null)
			{
				this.type = this.types.GetRandom<string>();
			}
			pActor.beh_building_target = ActorTool.findNewBuildingTarget(pActor, this.type);
			return BehResult.Continue;
		}

		// Token: 0x04001527 RID: 5415
		private string type;

		// Token: 0x04001528 RID: 5416
		private string[] types;
	}
}
