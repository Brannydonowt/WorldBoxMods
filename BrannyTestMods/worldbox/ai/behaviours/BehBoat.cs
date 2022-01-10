using System;

namespace ai.behaviours
{
	// Token: 0x02000329 RID: 809
	public class BehBoat : BehaviourActionActor
	{
		// Token: 0x060012B4 RID: 4788 RVA: 0x0009F6F4 File Offset: 0x0009D8F4
		internal void checkHomeDocks(Actor pActor)
		{
			ActorTool.checkHomeDocks(pActor);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0009F6FC File Offset: 0x0009D8FC
		public override BehResult execute(Actor pActor)
		{
			return BehResult.Continue;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0009F6FF File Offset: 0x0009D8FF
		public Boat getBoat(Actor pActor)
		{
			return pActor.GetComponent<Boat>();
		}
	}
}
