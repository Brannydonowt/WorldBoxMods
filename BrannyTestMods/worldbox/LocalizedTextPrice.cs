using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// Token: 0x020001BE RID: 446
public class LocalizedTextPrice : MonoBehaviour
{
	// Token: 0x06000A0F RID: 2575 RVA: 0x00067308 File Offset: 0x00065508
	internal void updateText(bool pCheckText = true)
	{
		if (!string.IsNullOrEmpty(LocalizedTextPrice.discount))
		{
			this.showDiscount(LocalizedTextPrice.discount);
		}
		string text = "";
		if (InAppManager.instance != null)
		{
			InAppManager instance = InAppManager.instance;
			bool flag;
			if (instance == null)
			{
				flag = (null != null);
			}
			else
			{
				IStoreController controller = instance.controller;
				flag = (((controller != null) ? controller.products : null) != null);
			}
			if (flag)
			{
				Product product = InAppManager.instance.controller.products.WithID(this.inAppId);
				if (product != null)
				{
					text = product.metadata.localizedPriceString;
					goto IL_7B;
				}
				goto IL_7B;
			}
		}
		text = LocalizedTextPrice.price_current;
		IL_7B:
		this.text_current_price.text = text;
		if (!string.IsNullOrEmpty(LocalizedTextPrice.price_old))
		{
			this.text_old_price.text = LocalizedTextPrice.price_old;
			this.text_old_price.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000673C9 File Offset: 0x000655C9
	private void showDiscount(string pString)
	{
		this.text_percent.text = pString;
		this.discount_bg.gameObject.SetActive(true);
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000673E8 File Offset: 0x000655E8
	private void setDefault()
	{
		this.discount_bg.gameObject.SetActive(false);
		this.text_current_price.gameObject.SetActive(true);
		this.text_current_price.text = "??";
		this.text_old_price.gameObject.SetActive(false);
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00067438 File Offset: 0x00065638
	private void OnEnable()
	{
		this.setDefault();
		this.updateText(true);
	}

	// Token: 0x04000C97 RID: 3223
	public static string price_current = "???";

	// Token: 0x04000C98 RID: 3224
	public static string price_old = string.Empty;

	// Token: 0x04000C99 RID: 3225
	public static string discount = string.Empty;

	// Token: 0x04000C9A RID: 3226
	public Text text_old_price;

	// Token: 0x04000C9B RID: 3227
	public Text text_current_price;

	// Token: 0x04000C9C RID: 3228
	public GameObject discount_bg;

	// Token: 0x04000C9D RID: 3229
	public Text text_percent;

	// Token: 0x04000C9E RID: 3230
	private string inAppId = "premium";
}
