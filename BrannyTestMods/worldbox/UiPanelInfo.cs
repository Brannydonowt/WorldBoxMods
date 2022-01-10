using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000273 RID: 627
public class UiPanelInfo : MonoBehaviour
{
	// Token: 0x06000DDD RID: 3549 RVA: 0x00082DC4 File Offset: 0x00080FC4
	private void OnEnable()
	{
		this.gameTime_name = this.gameTime.transform.Find("Name").GetComponent<Text>();
		this.gameTime_value = this.gameTime.transform.Find("Value").GetComponent<Text>();
		this.population_name = this.population.transform.Find("Name").GetComponent<Text>();
		this.population_value = this.population.transform.Find("Value").GetComponent<Text>();
		this.beasts_name = this.beasts.transform.Find("Name").GetComponent<Text>();
		this.beasts_value = this.beasts.transform.Find("Value").GetComponent<Text>();
		this.infected_name = this.infected.transform.Find("Name").GetComponent<Text>();
		this.infected_value = this.infected.transform.Find("Value").GetComponent<Text>();
		this.deaths_name = this.deaths.transform.Find("Name").GetComponent<Text>();
		this.deaths_value = this.deaths.transform.Find("Value").GetComponent<Text>();
		this.buildings_name = this.buildings.transform.Find("Name").GetComponent<Text>();
		this.buildings_value = this.buildings.transform.Find("Value").GetComponent<Text>();
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00082F54 File Offset: 0x00081154
	private void Update()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (MapBox.instance == null)
		{
			return;
		}
		if (MapBox.instance.mapStats == null)
		{
			return;
		}
		if (MapBox.instance.gameStats == null)
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			return;
		}
		this.timer = this.interval;
		MapBox instance = MapBox.instance;
		int num = 0;
		int num2 = 0;
		foreach (Actor actor in instance.units)
		{
			if (actor.isInfectedWithAnything())
			{
				num++;
			}
			if (actor.stats.countAsUnit && !actor.stats.unit)
			{
				num2++;
			}
		}
		int num3 = 0;
		foreach (Building building in instance.buildings)
		{
			if (building.data.alive && building.stats.cityBuilding)
			{
				num3++;
			}
		}
		if (LocalizedTextManager.isRTLLang() != this.lastRTL)
		{
			this.lastRTL = LocalizedTextManager.isRTLLang();
			if (this.lastRTL)
			{
				this.gameTime_value.alignment = 3;
				this.population_value.alignment = 3;
				this.beasts_value.alignment = 3;
				this.infected_value.alignment = 3;
				this.deaths_value.alignment = 3;
				this.buildings_value.alignment = 3;
				this.gameTime_name.alignment = 5;
				this.population_name.alignment = 5;
				this.beasts_name.alignment = 5;
				this.infected_name.alignment = 5;
				this.deaths_name.alignment = 5;
				this.buildings_name.alignment = 5;
			}
			else
			{
				this.gameTime_value.alignment = 5;
				this.population_value.alignment = 5;
				this.beasts_value.alignment = 5;
				this.infected_value.alignment = 5;
				this.deaths_value.alignment = 5;
				this.buildings_value.alignment = 5;
				this.gameTime_name.alignment = 3;
				this.population_name.alignment = 3;
				this.beasts_name.alignment = 3;
				this.infected_name.alignment = 3;
				this.deaths_name.alignment = 3;
				this.buildings_name.alignment = 3;
			}
		}
		this.gameTime_value.text = "y:" + (instance.mapStats.year + 1).ToString() + ", m:" + (instance.mapStats.month + 1).ToString();
		this.population_value.text = (instance.getCivWorldPopulation().ToString() ?? "");
		this.beasts_value.text = (num2.ToString() ?? "");
		this.infected_value.text = (num.ToString() ?? "");
		this.deaths_value.text = (instance.mapStats.deaths.ToString() ?? "");
		this.buildings_value.text = (num3.ToString() ?? "");
		this.gameTime_value.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.population_value.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.beasts_value.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.infected_value.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.deaths_value.GetComponent<LocalizedText>().checkSpecialLanguages();
		this.buildings_value.GetComponent<LocalizedText>().checkSpecialLanguages();
	}

	// Token: 0x040010A2 RID: 4258
	public GameObject gameTime;

	// Token: 0x040010A3 RID: 4259
	public GameObject population;

	// Token: 0x040010A4 RID: 4260
	public GameObject beasts;

	// Token: 0x040010A5 RID: 4261
	public GameObject deaths;

	// Token: 0x040010A6 RID: 4262
	public GameObject infected;

	// Token: 0x040010A7 RID: 4263
	public GameObject buildings;

	// Token: 0x040010A8 RID: 4264
	private float interval = 1f;

	// Token: 0x040010A9 RID: 4265
	private float timer;

	// Token: 0x040010AA RID: 4266
	private Text gameTime_name;

	// Token: 0x040010AB RID: 4267
	private Text gameTime_value;

	// Token: 0x040010AC RID: 4268
	private Text population_name;

	// Token: 0x040010AD RID: 4269
	private Text population_value;

	// Token: 0x040010AE RID: 4270
	private Text beasts_name;

	// Token: 0x040010AF RID: 4271
	private Text beasts_value;

	// Token: 0x040010B0 RID: 4272
	private Text deaths_name;

	// Token: 0x040010B1 RID: 4273
	private Text deaths_value;

	// Token: 0x040010B2 RID: 4274
	private Text infected_name;

	// Token: 0x040010B3 RID: 4275
	private Text infected_value;

	// Token: 0x040010B4 RID: 4276
	private Text buildings_name;

	// Token: 0x040010B5 RID: 4277
	private Text buildings_value;

	// Token: 0x040010B6 RID: 4278
	private bool lastRTL;
}
