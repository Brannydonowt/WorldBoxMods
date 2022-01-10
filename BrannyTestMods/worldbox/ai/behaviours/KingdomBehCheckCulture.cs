using System;

namespace ai.behaviours
{
	// Token: 0x020003AC RID: 940
	public class KingdomBehCheckCulture : BehaviourActionKingdom
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x000A989C File Offset: 0x000A7A9C
		public override BehResult execute(Kingdom pKingdom)
		{
			this.recalcMainCulture(pKingdom);
			return BehResult.Continue;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000A98A8 File Offset: 0x000A7AA8
		private void recalcMainCulture(Kingdom pKingdom)
		{
			if (pKingdom.capital == null)
			{
				pKingdom.cultureID = string.Empty;
				return;
			}
			Culture culture = pKingdom.capital.getCulture();
			if (culture == null)
			{
				return;
			}
			pKingdom.cultureID = culture.id;
		}
	}
}
