using System;

namespace ai.behaviours
{
	// Token: 0x02000389 RID: 905
	public class BehSitInsideBoat : BehaviourActionActor
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x000A31A7 File Offset: 0x000A13A7
		public override void create()
		{
			base.create();
			this.special_inside_object = true;
		}
	}
}
