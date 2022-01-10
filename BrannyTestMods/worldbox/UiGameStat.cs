using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000270 RID: 624
public class UiGameStat : MonoBehaviour
{
	// Token: 0x06000DCD RID: 3533 RVA: 0x00082A96 File Offset: 0x00080C96
	private void Awake()
	{
		this.nameText.GetComponent<LocalizedText>().key = this.statName;
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00082AAE File Offset: 0x00080CAE
	private void Start()
	{
		UiGameStat.world = MapBox.instance;
		UiGameStat.stats = MapBox.instance.gameStats;
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x00082ACC File Offset: 0x00080CCC
	private void Update()
	{
		if (!this.onUpdate)
		{
			return;
		}
		if (this.timeout > 0f)
		{
			this.timeout -= Time.deltaTime / Config.timeScale;
			return;
		}
		this.timeout = 0.5f;
		this.updateText();
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00082B1C File Offset: 0x00080D1C
	internal void updateText()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (LocalizedTextManager.instance == null)
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
		UiGameStat.stats = MapBox.instance.gameStats;
		string statistic = StatsHelper.getStatistic(this.statName);
		this.nameText.GetComponent<LocalizedText>().key = this.statName;
		this.nameText.GetComponent<LocalizedText>().updateText(true);
		this.valueText.text = statistic;
		if (LocalizedTextManager.isRTLLang())
		{
			this.nameText.text = ":" + this.nameText.text;
			this.nameText.alignment = 5;
			this.valueText.alignment = 3;
			return;
		}
		this.nameText.text = this.nameText.text + ":";
		this.nameText.alignment = 3;
		this.valueText.alignment = 5;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00082C2D File Offset: 0x00080E2D
	private void OnEnable()
	{
		this.updateText();
	}

	// Token: 0x04001095 RID: 4245
	public Text nameText;

	// Token: 0x04001096 RID: 4246
	public Text valueText;

	// Token: 0x04001097 RID: 4247
	public string statName;

	// Token: 0x04001098 RID: 4248
	public bool onUpdate = true;

	// Token: 0x04001099 RID: 4249
	private static MapBox world;

	// Token: 0x0400109A RID: 4250
	private static GameStats stats;

	// Token: 0x0400109B RID: 4251
	private float timeout;
}
