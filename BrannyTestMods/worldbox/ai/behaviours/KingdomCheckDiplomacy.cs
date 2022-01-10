using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020003B1 RID: 945
	public class KingdomCheckDiplomacy : BehaviourActionKingdom
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x000A9EF8 File Offset: 0x000A80F8
		public override BehResult execute(Kingdom pKingdom)
		{
			if (!BehaviourActionBase<Kingdom>.world.worldLaws.world_law_diplomacy.boolVal)
			{
				return BehResult.Stop;
			}
			if (pKingdom.cities.Count == 0)
			{
				return BehResult.Stop;
			}
			KingdomCheckDiplomacy._tempKingdoms.Clear();
			this.tryNewPeace(pKingdom);
			this.tryNewWar(pKingdom);
			return BehResult.Continue;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x000A9F48 File Offset: 0x000A8148
		private void tryNewWar(Kingdom pKingdom)
		{
			int num = pKingdom.countArmy();
			if (num < 10)
			{
				return;
			}
			if (!pKingdom.diceAgressionSuccess())
			{
				return;
			}
			Kingdom kingdom = null;
			float num2 = 0f;
			foreach (Kingdom kingdom2 in pKingdom.civs_allies.Keys)
			{
				if (kingdom2 != pKingdom && (num >= kingdom2.countArmy() || kingdom2.civs_enemies.Count != 0))
				{
					DiplomacyRelation relation = BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.getRelation(pKingdom, kingdom2);
					if (relation.stateChanged >= 22)
					{
						relation.recalculate();
						if (BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.getOpinion(pKingdom, kingdom2)._opinion_total <= 0)
						{
							float num3 = Kingdom.distanceBetweenKingdom(pKingdom, kingdom2);
							if (kingdom == null || num3 < num2)
							{
								num2 = num3;
								kingdom = kingdom2;
							}
						}
					}
				}
			}
			if (kingdom == null)
			{
				return;
			}
			this.newDiplomacyEvent(pKingdom, kingdom, DiplomacyState.War);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000AA050 File Offset: 0x000A8250
		private void tryNewPeace(Kingdom pKingdom)
		{
			foreach (Kingdom kingdom in pKingdom.civs_enemies.Keys)
			{
				KingdomCheckDiplomacy._tempKingdoms.Add(kingdom);
			}
			foreach (Kingdom pTarget in KingdomCheckDiplomacy._tempKingdoms)
			{
				this.newDiplomacyEvent(pKingdom, pTarget, DiplomacyState.Ally);
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000AA0F0 File Offset: 0x000A82F0
		private bool isThereActiveCityConquest(Kingdom pKingdom, Kingdom pTarget)
		{
			using (List<City>.Enumerator enumerator = pKingdom.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isGettingCapturedBy(pTarget))
					{
						return true;
					}
				}
			}
			using (List<City>.Enumerator enumerator = pTarget.cities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isGettingCapturedBy(pKingdom))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000AA190 File Offset: 0x000A8390
		private void newDiplomacyEvent(Kingdom pKingdom, Kingdom pTarget, DiplomacyState pNewState)
		{
			if (pKingdom.king == null)
			{
				return;
			}
			if (pTarget.cities.Count == 0)
			{
				return;
			}
			if (pKingdom == pTarget)
			{
				return;
			}
			if (this.isThereActiveCityConquest(pKingdom, pTarget))
			{
				return;
			}
			DiplomacyRelation relation = BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.getRelation(pKingdom, pTarget);
			if (relation.stateChanged < 22)
			{
				return;
			}
			relation.recalculate();
			KingdomOpinion opinion = BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.getOpinion(pKingdom, pTarget);
			int num = 0;
			if (relation.state == DiplomacyState.War)
			{
				num = BehaviourActionBase<Kingdom>.world.mapStats.year - relation.warSince;
			}
			bool flag = false;
			if (opinion._opinion_total < 0)
			{
				flag = true;
			}
			if (pKingdom == DiplomacyManager.kingdom_supreme && pTarget == DiplomacyManager.kingdom_second)
			{
				flag = true;
			}
			if (pNewState == DiplomacyState.War)
			{
				float num2 = pKingdom.king.curStats.personality_aggression;
				if (pKingdom.power > pTarget.power)
				{
					if (pKingdom.cities.Count > pKingdom.getMaxCities())
					{
						num2 -= (float)(pKingdom.cities.Count - pKingdom.getMaxCities()) * 0.1f;
					}
					else if (pKingdom.cities.Count < pKingdom.getMaxCities() && pKingdom.diceAgressionSuccess())
					{
						num2 += 0.1f;
					}
					if (flag)
					{
						num2 += 0.05f;
					}
				}
				if (pKingdom.power < pTarget.power)
				{
					float num3 = (float)(pTarget.power / pKingdom.power);
					num2 -= 0.2f * num3;
				}
				if (pKingdom.civs_enemies.Count > 0)
				{
					num2 -= 0.3f * (float)pKingdom.civs_enemies.Count;
				}
				if (Toolbox.randomChance(num2))
				{
					BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.startWar(pKingdom, pTarget, false);
					return;
				}
			}
			else if (pNewState == DiplomacyState.Ally)
			{
				float num4 = pKingdom.king.curStats.personality_diplomatic;
				if (num > 10)
				{
					num4 += 0.1f;
				}
				if (!pKingdom.diceAgressionSuccess())
				{
					num4 += 0.1f;
				}
				if (pKingdom.power < pTarget.power)
				{
					num4 += 0.05f;
				}
				if (Toolbox.randomChance(num4))
				{
					BehaviourActionBase<Kingdom>.world.kingdoms.diplomacyManager.startPeace(pKingdom, pTarget, false);
				}
			}
		}

		// Token: 0x04001573 RID: 5491
		private static List<Kingdom> _tempKingdoms = new List<Kingdom>();
	}
}
