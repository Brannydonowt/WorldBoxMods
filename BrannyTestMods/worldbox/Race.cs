using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[Serializable]
public class Race : Asset
{
	// Token: 0x04000566 RID: 1382
	public string nameLocale;

	// Token: 0x04000567 RID: 1383
	public string icon;

	// Token: 0x04000568 RID: 1384
	public string[] production;

	// Token: 0x04000569 RID: 1385
	public Color color = Color.black;

	// Token: 0x0400056A RID: 1386
	public Color color_abandoned = Color.black;

	// Token: 0x0400056B RID: 1387
	public Color colorBorder = Color.black;

	// Token: 0x0400056C RID: 1388
	public Color colorBorderOut = Color.black;

	// Token: 0x0400056D RID: 1389
	public string name_template_kingdom = string.Empty;

	// Token: 0x0400056E RID: 1390
	public string name_template_city = string.Empty;

	// Token: 0x0400056F RID: 1391
	public string name_template_culture = string.Empty;

	// Token: 0x04000570 RID: 1392
	public List<string> culture_elements;

	// Token: 0x04000571 RID: 1393
	public List<string> culture_decors;

	// Token: 0x04000572 RID: 1394
	public List<string> culture_colors;

	// Token: 0x04000573 RID: 1395
	public KingdomStats stats = new KingdomStats();

	// Token: 0x04000574 RID: 1396
	public int culture_knowledge_gain_base = 1;

	// Token: 0x04000575 RID: 1397
	public float culture_knowledge_gain_per_intelligence = 1f;

	// Token: 0x04000576 RID: 1398
	public float culture_knowledge_gain_rate = 0.1f;

	// Token: 0x04000577 RID: 1399
	public List<string> skin_citizen_male;

	// Token: 0x04000578 RID: 1400
	public List<string> skin_citizen_female;

	// Token: 0x04000579 RID: 1401
	public List<string> skin_warrior;

	// Token: 0x0400057A RID: 1402
	public string skin_civ_default_male = "unit_male_1";

	// Token: 0x0400057B RID: 1403
	public string skin_civ_default_female = "unit_female_1";

	// Token: 0x0400057C RID: 1404
	public bool nature;

	// Token: 0x0400057D RID: 1405
	public bool civilization;

	// Token: 0x0400057E RID: 1406
	public int civ_baseCities = 1;

	// Token: 0x0400057F RID: 1407
	public int civ_baseZones = 5;

	// Token: 0x04000580 RID: 1408
	public int civ_baseArmy = 1;

	// Token: 0x04000581 RID: 1409
	public BuildingPlacements buildingPlacements;

	// Token: 0x04000582 RID: 1410
	public List<string> preferred_weapons = new List<string>();

	// Token: 0x04000583 RID: 1411
	[NonSerialized]
	public List<string> preferred_food = new List<string>();

	// Token: 0x04000584 RID: 1412
	[NonSerialized]
	public List<string> preferred_attribute = new List<string>();

	// Token: 0x04000585 RID: 1413
	public List<string> culture_forbidden_tech = new List<string>();

	// Token: 0x04000586 RID: 1414
	public int culture_rate_tech_limit = 5;

	// Token: 0x04000587 RID: 1415
	public string hateRaces = "";

	// Token: 0x04000588 RID: 1416
	public string bannerId = "";

	// Token: 0x04000589 RID: 1417
	[NonSerialized]
	internal ActorContainer units = new ActorContainer();
}
