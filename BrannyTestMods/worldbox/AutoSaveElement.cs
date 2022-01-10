using System;
using System.Globalization;
using System.IO;
using Humanizer;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029A RID: 666
public class AutoSaveElement : MonoBehaviour
{
	// Token: 0x06000EAF RID: 3759 RVA: 0x00088158 File Offset: 0x00086358
	public void load(AutoSaveData pData)
	{
		this._world_path = pData.path;
		byte[] array = File.ReadAllBytes(SaveManager.generatePngSmallPreviewPath(pData.path));
		Texture2D texture2D = new Texture2D(32, 32);
		texture2D.anisoLevel = 0;
		texture2D.filterMode = FilterMode.Point;
		ImageConversion.LoadImage(texture2D, array);
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));
		MapMetaData metaFor = SaveManager.getMetaFor(pData.path);
		this.textName.text = metaFor.mapStats.name;
		this.textKingdoms.text = metaFor.kingdoms.ToString();
		this.textCities.text = metaFor.cities.ToString();
		this.textPopulation.text = metaFor.population.ToString();
		this.textMobs.text = metaFor.mobs.ToString();
		this.textAge.text = metaFor.mapStats.year.ToString();
		this.image.sprite = sprite;
		string text = "";
		string text2 = "";
		try
		{
			DateTime dateTime = Epoch.toDateTime(pData.timestamp);
			text2 = LocalizedTextManager.langToCulture();
			DateTime t = DateTime.UtcNow.AddDays(7.0);
			if (dateTime.Year < 2017)
			{
				text = "GREG";
			}
			else if (dateTime > t)
			{
				text = "DREDD";
			}
			else if (text2 == "")
			{
				text = dateTime.ToShortDateString();
			}
			else
			{
				text = DateHumanizeExtensions.Humanize(dateTime, true, null, new CultureInfo(text2));
			}
		}
		catch (Exception message)
		{
			Debug.Log("failed with " + text2);
			Debug.LogError(message);
		}
		this.textSaveTimeAgo.text = text;
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0008833C File Offset: 0x0008653C
	public void clickLoadAutoSave()
	{
		if (Config.havePremium)
		{
			SaveManager.setCurrentPath(this._world_path);
			ScrollWindow.showWindow("load_world");
			return;
		}
		ScrollWindow.showWindow("premium_menu");
	}

	// Token: 0x0400118D RID: 4493
	public Image image;

	// Token: 0x0400118E RID: 4494
	public Text textName;

	// Token: 0x0400118F RID: 4495
	public Text textSaveTimeAgo;

	// Token: 0x04001190 RID: 4496
	public Text textKingdoms;

	// Token: 0x04001191 RID: 4497
	public Text textCities;

	// Token: 0x04001192 RID: 4498
	public Text textPopulation;

	// Token: 0x04001193 RID: 4499
	public Text textMobs;

	// Token: 0x04001194 RID: 4500
	public Text textAge;

	// Token: 0x04001195 RID: 4501
	private string _world_path;
}
