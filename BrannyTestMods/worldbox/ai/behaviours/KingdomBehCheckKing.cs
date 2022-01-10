using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020003AD RID: 941
	public class KingdomBehCheckKing : BehaviourActionKingdom
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x000A98F4 File Offset: 0x000A7AF4
		public override BehResult execute(Kingdom pKingdom)
		{
			if (pKingdom.timer_new_king > 0f)
			{
				return BehResult.Continue;
			}
			if (pKingdom.king != null && pKingdom.king.data.alive)
			{
				this.tryToGiveGoldenTooth(pKingdom.king);
				return BehResult.Continue;
			}
			this._new_kingdoms.Clear();
			pKingdom.clearKingData();
			this._units.Clear();
			this.findKing(pKingdom);
			if (pKingdom.capital != null && pKingdom.capital.leader != null)
			{
				this._units.Remove(pKingdom.capital.leader);
			}
			this.checkIndependants(pKingdom);
			return BehResult.Continue;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x000A99A1 File Offset: 0x000A7BA1
		private void tryToGiveGoldenTooth(Actor pActor)
		{
			if (pActor.data.age > 45 && Toolbox.randomChance(0.05f))
			{
				pActor.addTrait("golden_tooth");
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000A99CC File Offset: 0x000A7BCC
		private void findKing(Kingdom pKingdom)
		{
			foreach (City city in pKingdom.cities)
			{
				if (!(city.leader == null) && city.leader.data.alive)
				{
					this._units.Add(city.leader);
				}
			}
			if (this._units.Count == 0)
			{
				return;
			}
			this._units.Shuffle<Actor>();
			Actor actor = null;
			int num = 0;
			foreach (Actor actor2 in this._units)
			{
				int num2 = ActorTool.attributeDice(actor2, 2);
				if (actor == null || num2 > num)
				{
					num = num2;
					actor = actor2;
				}
			}
			if (actor == null)
			{
				return;
			}
			this._units.Remove(actor);
			if (actor.city.leader == actor)
			{
				actor.city.removeLeader();
			}
			pKingdom.setKing(actor);
			WorldLog.logNewKing(pKingdom);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000A9B04 File Offset: 0x000A7D04
		private void checkIndependants(Kingdom pKingdom)
		{
			if (!MapBox.instance.worldLaws.world_law_rebellions.boolVal)
			{
				return;
			}
			if (!MapBox.instance.worldLaws.world_law_diplomacy.boolVal)
			{
				return;
			}
			if (!pKingdom.check_for_independence)
			{
				return;
			}
			pKingdom.check_for_independence = false;
			if (this._units.Count == 0)
			{
				return;
			}
			if (pKingdom.king == null)
			{
				return;
			}
			int num = ActorTool.attributeDice(pKingdom.king, 3);
			foreach (Actor actor in this._units)
			{
				int num2 = ActorTool.attributeDice(actor, 2);
				if (num <= num2)
				{
					this.makeOrJoinKingdom(actor);
				}
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000A9BD0 File Offset: 0x000A7DD0
		private void makeOrJoinKingdom(Actor pLeader)
		{
			if (this._new_kingdoms.Count == 0)
			{
				this.makeOwnKingdom(pLeader);
				return;
			}
			if (pLeader.city != null && pLeader.city.data.timer_revolt > 0f)
			{
				return;
			}
			this._new_kingdoms.Shuffle<Kingdom>();
			foreach (Kingdom kingdom in this._new_kingdoms)
			{
				if (kingdom.king == null)
				{
					Debug.Log("HUH");
				}
				int num = ActorTool.attributeDice(kingdom.king, 2);
				if (ActorTool.attributeDice(pLeader, 2) <= num)
				{
					foreach (City pB in kingdom.cities)
					{
						if (City.nearbyBorders(pLeader.city, pB))
						{
							pLeader.city.joinAnotherKingdom(kingdom);
							return;
						}
					}
				}
			}
			this.makeOwnKingdom(pLeader);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000A9CF8 File Offset: 0x000A7EF8
		private void makeOwnKingdom(Actor pLeader)
		{
			Kingdom kingdom = pLeader.city.makeOwnKingdom();
			pLeader.city.removeLeader();
			kingdom.setKing(pLeader);
			this._new_kingdoms.Add(kingdom);
		}

		// Token: 0x0400156F RID: 5487
		private List<Actor> _units = new List<Actor>();

		// Token: 0x04001570 RID: 5488
		private List<Kingdom> _new_kingdoms = new List<Kingdom>();
	}
}
