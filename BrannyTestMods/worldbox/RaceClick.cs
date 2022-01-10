using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class RaceClick : MonoBehaviour
{
	// Token: 0x06000E63 RID: 3683 RVA: 0x00085EE8 File Offset: 0x000840E8
	public void click()
	{
		Race race = null;
		if (Config.selectedUnit != null)
		{
			race = Config.selectedUnit.race;
		}
		else if (Config.selectedCity != null)
		{
			race = Config.selectedCity.race;
		}
		else if (Config.selectedKingdom != null)
		{
			race = Config.selectedKingdom.race;
		}
		if (race != null)
		{
			int num = Toolbox.randomInt(1, 3);
			string text = "click_" + race.id + "_" + num.ToString();
			if (!LocalizedTextManager.instance.contains(text))
			{
				return;
			}
			WorldTip.showNow(text, true, "cursor", 3f);
		}
	}
}
