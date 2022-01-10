using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000277 RID: 631
public class WorldTip : MonoBehaviour
{
	// Token: 0x06000DE7 RID: 3559 RVA: 0x000835C3 File Offset: 0x000817C3
	private void Awake()
	{
		this.status = TipStatus.Hidden;
		this.canvasGroup.alpha = 0f;
		WorldTip.instance = this;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000835E4 File Offset: 0x000817E4
	public void show(string pText, bool pTranslate = true, string pPosition = "center", float pTime = 3f)
	{
		if (WorldTip.instance == null)
		{
			return;
		}
		if (pTranslate)
		{
			WorldTip.instance.text.text = LocalizedTextManager.getText(pText, null);
			if (WorldTip.replacementDict != null)
			{
				WorldTip.instance.text.text = WorldTip.replaceWords(WorldTip.instance.text.text);
			}
			WorldTip.instance.text.GetComponent<LocalizedText>().checkSpecialLanguages();
		}
		else
		{
			WorldTip.instance.text.text = pText;
		}
		DiscordTracker.showText(WorldTip.instance.text.text);
		this.updateTextWidth();
		base.transform.SetParent(this.transform_main);
		WorldTip.instance.startShow(pTime);
		if (pPosition == "center")
		{
			base.transform.position = Vector3.zero;
			return;
		}
		if (pPosition == "top")
		{
			base.transform.position = this.transform_positionTop.position;
			return;
		}
		WorldTip.instance.transform.position = Input.mousePosition;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x000836F5 File Offset: 0x000818F5
	public static void showNowCursor(string pText)
	{
		WorldTip.showNow(pText, true, "cursor", 3f);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00083708 File Offset: 0x00081908
	public static void showNowCenter(string pText)
	{
		WorldTip.showNow(pText, true, "center", 3f);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0008371B File Offset: 0x0008191B
	public static void showNowTop(string pText)
	{
		WorldTip.showNow(pText, true, "top", 3f);
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0008372E File Offset: 0x0008192E
	public static void addWordReplacement(string key, string value)
	{
		if (WorldTip.replacementDict == null)
		{
			WorldTip.replacementDict = new Dictionary<string, string>();
		}
		WorldTip.replacementDict[key] = value;
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00083750 File Offset: 0x00081950
	public static string replaceWords(string text)
	{
		foreach (string text2 in WorldTip.replacementDict.Keys)
		{
			text = text.Replace(text2, WorldTip.replacementDict[text2]);
		}
		WorldTip.replacementDict = null;
		return text;
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x000837BC File Offset: 0x000819BC
	public static void showNow(string pText, bool pTranslate = true, string pPosition = "center", float pTime = 3f)
	{
		WorldTip.instance.show(pText, pTranslate, pPosition, pTime);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x000837CC File Offset: 0x000819CC
	public void showToolbarText(string pText)
	{
		if (Config.isComputer)
		{
			return;
		}
		this.text.text = pText;
		if (this.text.GetComponent<LocalizedText>() != null)
		{
			this.text.GetComponent<LocalizedText>().checkSpecialLanguages();
		}
		this.updateTextWidth();
		this.startShow(3f);
		base.transform.position = this.transform_toolbar.position;
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00083838 File Offset: 0x00081A38
	public void showToolbarText(GodPower pPower)
	{
		this.text.text = LocalizedTextManager.getText(pPower.name, null) + "\n" + LocalizedTextManager.getText(pPower.name + " Description", null);
		if (this.text.GetComponent<LocalizedText>() != null)
		{
			this.text.GetComponent<LocalizedText>().checkSpecialLanguages();
		}
		this.updateTextWidth();
		this.startShow(3f);
		base.transform.position = this.transform_toolbar.position;
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000838C8 File Offset: 0x00081AC8
	public void setText(string pText, bool pAddSKip = false)
	{
		this.text.text = LocalizedTextManager.getText(pText, null);
		this.text.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (pAddSKip)
		{
			this.text.text = "\n" + this.text.text;
		}
		this.updateTextWidth();
		base.transform.position = this.transform_toolbar.position;
		this.startShow(3f);
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x00083941 File Offset: 0x00081B41
	private void updateTextWidth()
	{
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00083943 File Offset: 0x00081B43
	private void startShow(float pTime = 3f)
	{
		this.status = TipStatus.Shown;
		this.timeout = pTime;
		this.scale = 1.5f;
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0008395E File Offset: 0x00081B5E
	public static void hideNow()
	{
		if (WorldTip.instance == null)
		{
			return;
		}
		if (!WorldTip.instance.gameObject.activeSelf)
		{
			return;
		}
		WorldTip.instance.startHide();
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0008398A File Offset: 0x00081B8A
	internal void startHide()
	{
		this.status = TipStatus.Hidden;
		this.timeout = 0f;
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x000839A0 File Offset: 0x00081BA0
	private void Update()
	{
		if (this.scale > 1f)
		{
			this.scale -= Time.deltaTime * 3f;
			if (this.scale < 1f)
			{
				this.scale = 1f;
			}
			base.transform.localScale = new Vector3(this.scale, this.scale, 1f);
		}
		TipStatus tipStatus = this.status;
		if (tipStatus != TipStatus.Hidden)
		{
			if (tipStatus != TipStatus.Shown)
			{
				return;
			}
			if (this.canvasGroup.alpha < 1f)
			{
				this.canvasGroup.alpha += Time.deltaTime * 3f;
			}
			if (this.canvasGroup.alpha == 1f)
			{
				this.timeout -= Time.deltaTime;
				if (this.timeout <= 0f)
				{
					this.startHide();
				}
			}
		}
		else if (this.canvasGroup.alpha > 0f)
		{
			this.canvasGroup.alpha -= Time.deltaTime * 2f;
			return;
		}
	}

	// Token: 0x040010BE RID: 4286
	public Transform transform_toolbar;

	// Token: 0x040010BF RID: 4287
	public Transform transform_main;

	// Token: 0x040010C0 RID: 4288
	public Transform transform_positionTop;

	// Token: 0x040010C1 RID: 4289
	public static WorldTip instance;

	// Token: 0x040010C2 RID: 4290
	public Canvas canvas;

	// Token: 0x040010C3 RID: 4291
	public Text text;

	// Token: 0x040010C4 RID: 4292
	public TipStatus status;

	// Token: 0x040010C5 RID: 4293
	public CanvasGroup canvasGroup;

	// Token: 0x040010C6 RID: 4294
	private float timeout;

	// Token: 0x040010C7 RID: 4295
	private float scale = 1f;

	// Token: 0x040010C8 RID: 4296
	public static Dictionary<string, string> replacementDict;
}
