using System;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.Purchasing;

// Token: 0x020001AB RID: 427
internal static class Discounts
{
	// Token: 0x060009B1 RID: 2481 RVA: 0x000652F8 File Offset: 0x000634F8
	internal static void checkDiscounts()
	{
		try
		{
			Discounts.checkPlatform();
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
					Product product = InAppManager.instance.controller.products.WithID("premium");
					if (product != null)
					{
						Discounts.discountRequest(product.metadata);
						goto IL_71;
					}
					Debug.Log("DC:no req/prod");
					goto IL_71;
				}
			}
			Debug.Log("DC:np");
			IL_71:;
		}
		catch (Exception message)
		{
			Debug.Log("DC:err");
			Debug.Log(message);
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0006539C File Offset: 0x0006359C
	private static void discountRequest(ProductMetadata pProductMeta)
	{
		if (Discounts.platform.Length < 2)
		{
			return;
		}
		string url = "https://currency.superworldbox.com/discounts/" + Discounts.platform + ".json?" + Toolbox.cacheBuster();
		string text = JsonConvert.SerializeObject(pProductMeta, new JsonSerializerSettings
		{
			DefaultValueHandling = 3
		});
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		if (text == "{}")
		{
			return;
		}
		RestClient.Post(url, text).Then(delegate(ResponseHelper response)
		{
			if (string.IsNullOrEmpty(response.Text))
			{
				return;
			}
			Debug.Log(response.Text);
			DiscountData discountData = JsonConvert.DeserializeObject<DiscountData>(response.Text);
			Debug.Log("DS:Setting");
			if (!string.IsNullOrEmpty(discountData.discount) && !string.IsNullOrEmpty(discountData.price_current) && !string.IsNullOrEmpty(discountData.price_old))
			{
				LocalizedTextPrice.discount = discountData.discount;
				LocalizedTextPrice.price_current = discountData.price_current;
				LocalizedTextPrice.price_old = discountData.price_old;
				Debug.Log("DS:Set");
				return;
			}
			Debug.Log("DS:NSet");
		}).Catch(delegate(Exception err)
		{
			Debug.Log("DS:err");
			Debug.Log(err.Message);
		});
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0006544C File Offset: 0x0006364C
	private static void checkPlatform()
	{
		RuntimePlatform runtimePlatform = Application.platform;
		if (runtimePlatform <= RuntimePlatform.Android)
		{
			switch (runtimePlatform)
			{
			case RuntimePlatform.OSXEditor:
				Discounts.platform = "mac";
				return;
			case RuntimePlatform.OSXPlayer:
				Discounts.platform = "mac";
				return;
			case RuntimePlatform.WindowsPlayer:
				Discounts.platform = "pc";
				return;
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.WindowsWebPlayer:
			case (RuntimePlatform)6:
				break;
			case RuntimePlatform.WindowsEditor:
				Discounts.platform = "pc";
				return;
			case RuntimePlatform.IPhonePlayer:
				Discounts.platform = "ios";
				return;
			default:
				if (runtimePlatform == RuntimePlatform.Android)
				{
					Discounts.platform = "android";
					return;
				}
				break;
			}
		}
		else
		{
			if (runtimePlatform == RuntimePlatform.LinuxPlayer)
			{
				Discounts.platform = "linux";
				return;
			}
			if (runtimePlatform == RuntimePlatform.LinuxEditor)
			{
				Discounts.platform = "linux";
				return;
			}
		}
		Discounts.platform = "unknown";
	}

	// Token: 0x04000C5D RID: 3165
	private static ProductMetadata localPriceData;

	// Token: 0x04000C5E RID: 3166
	private static string platform;
}
