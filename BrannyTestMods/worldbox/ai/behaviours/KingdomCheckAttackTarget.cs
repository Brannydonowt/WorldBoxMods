using System;

namespace ai.behaviours
{
	// Token: 0x020003AF RID: 943
	public class KingdomCheckAttackTarget : BehaviourActionKingdom
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x000A9D80 File Offset: 0x000A7F80
		public override BehResult execute(Kingdom pKingdom)
		{
			if (pKingdom.timer_attack_target > 0f)
			{
				return BehResult.Continue;
			}
			pKingdom.timer_attack_target = 10f;
			if (pKingdom.civs_enemies.Count == 0)
			{
				return BehResult.Continue;
			}
			if (pKingdom.king == null)
			{
				return BehResult.Continue;
			}
			if (pKingdom.target_kingdom != null && pKingdom.target_kingdom.isEnemy(pKingdom))
			{
				return BehResult.Continue;
			}
			if (!pKingdom.diceAgressionSuccess())
			{
				pKingdom.target_kingdom = null;
				return BehResult.Continue;
			}
			Kingdom kingdom = null;
			float num = 0f;
			foreach (Kingdom kingdom2 in pKingdom.civs_enemies.Keys)
			{
				if (kingdom2.cities.Count != 0)
				{
					float num2 = Kingdom.distanceBetweenKingdom(pKingdom, kingdom2);
					if (kingdom == null || num2 < num)
					{
						kingdom = kingdom2;
						num = num2;
					}
				}
			}
			if (kingdom == null)
			{
				return BehResult.Continue;
			}
			pKingdom.timer_attack_target = 300f + Toolbox.randomFloat(0f, 200f);
			this.startAttackOn(pKingdom, kingdom);
			return BehResult.Continue;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x000A9E90 File Offset: 0x000A8090
		internal void startAttackOn(Kingdom pKingdom, Kingdom pTargetKingdom)
		{
			if (pTargetKingdom.cities.Count == 0)
			{
				return;
			}
			if (pKingdom.capital == null)
			{
				return;
			}
			pKingdom.eventState = KingdomEventState.Wait;
			pKingdom.target_kingdom = pTargetKingdom;
			pKingdom.findCityTarget();
		}
	}
}
