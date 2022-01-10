using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
public class MapText : MonoBehaviour
{
	// Token: 0x06000015 RID: 21 RVA: 0x0000379E File Offset: 0x0000199E
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
		this.textRect = this.text.GetComponent<RectTransform>();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000037C0 File Offset: 0x000019C0
	internal void update(float pElapsed)
	{
		if (this.showing)
		{
			this.tweenScale += Time.deltaTime * 0.6f;
			if (this.tweenScale > this.maxScale)
			{
				this.tweenScale = this.maxScale;
			}
			base.transform.localScale = new Vector3(this.maxScale, iTween.easeOutBack(0f, 1f, this.tweenScale * 2f) * 0.5f, this.maxScale);
			return;
		}
		this.tweenScale = 0f;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00003850 File Offset: 0x00001A50
	public void setShowing(bool pVal)
	{
		this.showing = pVal;
		if (!pVal)
		{
			base.gameObject.transform.localPosition = Globals.POINT_IN_VOID;
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00003874 File Offset: 0x00001A74
	public void clear()
	{
		base.transform.position = Globals.POINT_IN_VOID;
		this.priority_capital = false;
		this.priority_population = 0;
		this.city = null;
		this.Kingdom = null;
		this.setShowing(false);
		this.conquerContainer.localPosition = Globals.POINT_IN_VOID;
		this.capitalContainer.localPosition = Globals.POINT_IN_VOID;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000038D4 File Offset: 0x00001AD4
	internal void showTextKingdom(Kingdom pKingdom)
	{
		this.text.color = pKingdom.kingdomColor.colorBorderOut;
		string pNewText = pKingdom.name + "  " + pKingdom.getPopulationTotal().ToString();
		this.setText(pNewText, pKingdom.capital.cityCenter);
		this.priority_population = pKingdom.units.Count;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000393C File Offset: 0x00001B3C
	internal void showTextCulture(Culture pCulture)
	{
		this.text.color = pCulture.color32_text;
		string pNewText = pCulture.name + "  " + pCulture.followers.ToString();
		this.setText(pNewText, pCulture.titleCenter);
		this.priority_population = pCulture.followers;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003994 File Offset: 0x00001B94
	internal void showTextCity()
	{
		string pNewText = this.city.data.cityName + "  " + this.city.getPopulationTotal().ToString();
		if (this.text.color != this.city.kingdom.kingdomColor.colorBorderOut)
		{
			this.text.color = this.city.kingdom.kingdomColor.colorBorderOut;
		}
		this.setText(pNewText, this.city.cityCenter);
		if (this.city.kingdom.capital == this.city)
		{
			this.capitalContainer.localPosition = new Vector3(-this._text_width / 2f - 5f, 0f);
		}
		else
		{
			this.capitalContainer.localPosition = Globals.POINT_IN_VOID;
		}
		if (this.city.lastTicks == 0)
		{
			this.conquerContainer.localPosition = Globals.POINT_IN_VOID;
		}
		else
		{
			this.conquerContainer.localPosition = new Vector3(0f, 20f);
			if (this.city.capturingBy != null)
			{
				this.conquerText.color = this.city.capturingBy.kingdomColor.colorBorderOut;
			}
			Text text = this.conquerText;
			int lastTicks = this.city.lastTicks;
			text.text = lastTicks.ToString() + "%";
		}
		this.priority_capital = (this.city.kingdom != null && this.city.kingdom.capital == this.city);
		this.priority_population = this.city.status.population;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003B54 File Offset: 0x00001D54
	private void setText(string pNewText, Vector3 tPos)
	{
		this.rect.anchoredPosition = this.transformPosition(tPos);
		this.racalcScaledOverlapRect(this.backgroundRect, ref this.map_text_rect);
		if (this._old_text == pNewText)
		{
			return;
		}
		this._old_text = pNewText;
		this.text.text = pNewText;
		this._text_width = LayoutUtility.GetPreferredWidth(this.textRect) + 20f;
		if (this._text_width < 70f)
		{
			this._text_width = 70f;
		}
		this.backgroundRect.sizeDelta = new Vector2(this._text_width, this.backgroundRect.sizeDelta.y);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003BFC File Offset: 0x00001DFC
	public bool Overlaps(MapText pText)
	{
		return this.map_text_rect.Overlaps(pText.map_text_rect);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003C10 File Offset: 0x00001E10
	public void racalcScaledOverlapRect(RectTransform pFrom, ref Rect pTo)
	{
		float scaleFactor = this.manager.canvasScaler.scaleFactor;
		Vector2 sizeDelta = pFrom.sizeDelta;
		float num = sizeDelta.x * 0.55f * scaleFactor;
		float num2 = sizeDelta.y * 0.55f * scaleFactor;
		Vector3 position = pFrom.position;
		pTo.x = position.x - num * 0.5f;
		pTo.y = position.y - num2 * 0.5f;
		pTo.width = num;
		pTo.height = num2;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00003C90 File Offset: 0x00001E90
	private Vector2 transformPosition(Vector3 pVec)
	{
		Vector2 vector = this.manager.world.camera.WorldToViewportPoint(pVec);
		return new Vector2(vector.x * this.manager.canvasRect.sizeDelta.x - this.manager.canvasRect.sizeDelta.x * 0.5f, vector.y * this.manager.canvasRect.sizeDelta.y - this.manager.canvasRect.sizeDelta.y * 0.5f);
	}

	// Token: 0x04000014 RID: 20
	private float maxScale = 0.5f;

	// Token: 0x04000015 RID: 21
	internal MapNamesManager manager;

	// Token: 0x04000016 RID: 22
	internal City city;

	// Token: 0x04000017 RID: 23
	internal Kingdom Kingdom;

	// Token: 0x04000018 RID: 24
	public Image capitalIcon;

	// Token: 0x04000019 RID: 25
	public RectTransform capitalContainer;

	// Token: 0x0400001A RID: 26
	public RectTransform backgroundRect;

	// Token: 0x0400001B RID: 27
	public Text text;

	// Token: 0x0400001C RID: 28
	internal RectTransform rect;

	// Token: 0x0400001D RID: 29
	internal RectTransform textRect;

	// Token: 0x0400001E RID: 30
	public RectTransform conquerContainer;

	// Token: 0x0400001F RID: 31
	public Text conquerText;

	// Token: 0x04000020 RID: 32
	public bool showing;

	// Token: 0x04000021 RID: 33
	internal float tweenScale;

	// Token: 0x04000022 RID: 34
	public bool priority_capital;

	// Token: 0x04000023 RID: 35
	public int priority_population;

	// Token: 0x04000024 RID: 36
	internal Rect map_text_rect;

	// Token: 0x04000025 RID: 37
	private string _old_text;

	// Token: 0x04000026 RID: 38
	private float _text_width;
}
