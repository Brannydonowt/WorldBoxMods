using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000395 RID: 917
	public class BehaviourActionActor : BehaviourActionBase<Actor>
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x000A3FE6 File Offset: 0x000A21E6
		public BehResult forceTask(Actor pActor, string pTask, bool pClean = true)
		{
			pActor.ai.setTask(pTask, pClean, false);
			return BehResult.Skip;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000A3FF8 File Offset: 0x000A21F8
		public override bool errorsFound(Actor pObject)
		{
			return pObject.currentTile.region == null || pObject.currentTile.region.island == null || (this.null_check_city && (pObject.city == null || !pObject.city.alive)) || (this.null_check_actor_target && (pObject.beh_actor_target == null || !pObject.beh_actor_target.base_data.alive)) || (this.null_check_tile_target && pObject.beh_tile_target == null) || (this.null_check_building_target && pObject.beh_building_target == null) || (this.check_building_target_non_usable && (pObject.beh_building_target == null || pObject.beh_building_target.isNonUsable())) || base.errorsFound(pObject);
		}

		// Token: 0x0400154A RID: 5450
		public bool null_check_city;

		// Token: 0x0400154B RID: 5451
		public bool null_check_kingdom;

		// Token: 0x0400154C RID: 5452
		public bool null_check_tile_target;

		// Token: 0x0400154D RID: 5453
		public bool null_check_building_target;

		// Token: 0x0400154E RID: 5454
		public bool null_check_actor_target;

		// Token: 0x0400154F RID: 5455
		public bool check_building_target_non_usable;

		// Token: 0x04001550 RID: 5456
		internal bool special_inside_object;

		// Token: 0x04001551 RID: 5457
		internal string forceAnimation;

		// Token: 0x04001552 RID: 5458
		protected static List<Actor> temp_actors = new List<Actor>();

		// Token: 0x04001553 RID: 5459
		protected static List<City> temp_cities = new List<City>();

		// Token: 0x04001554 RID: 5460
		protected static List<string> temp_tasks = new List<string>();

		// Token: 0x04001555 RID: 5461
		protected static List<WorldTile> possible_moves = new List<WorldTile>();
	}
}
