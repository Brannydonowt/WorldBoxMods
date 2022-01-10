using System;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

// Token: 0x020001B6 RID: 438
[ObfuscateLiterals]
public class InAppManager : MonoBehaviour, IStoreListener
{
	// Token: 0x060009C5 RID: 2501 RVA: 0x000656F3 File Offset: 0x000638F3
	private void Start()
	{
		InAppManager.instance = this;
		if (PlayerConfig.instance != null && !PlayerConfig.instance.data.pPossible0133)
		{
			return;
		}
		InAppManager.activatePrem(false);
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0006571A File Offset: 0x0006391A
	private static bool checkGoogleAccount()
	{
		if (!InAppManager.googleAccount)
		{
			ErrorWindow.errorMessage = "A Google Account is missing or you're not logged in with one.";
			ScrollWindow.get("error_with_reason").clickShow();
		}
		return InAppManager.googleAccount;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00065744 File Offset: 0x00063944
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void InitializePurchasing(bool pForce = false)
	{
		if (!pForce && this.IsInitialized())
		{
			return;
		}
		InAppManager.instance = this;
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), Array.Empty<IPurchasingModule>());
		ConfigurationBuilder configurationBuilder2 = configurationBuilder;
		string text = "premium";
		ProductType productType = 1;
		IDs ds = new IDs();
		ds.Add("premium", new string[]
		{
			"GooglePlay"
		});
		ds.Add("premium", new string[]
		{
			"AppleAppStore"
		});
		configurationBuilder2.AddProduct(text, productType, ds);
		try
		{
			InAppManager.validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
			Debug.Log("validator assigned");
		}
		catch (NotImplementedException ex)
		{
			Debug.Log("validator not assigned");
			string str = "Cross Platform Validator Not Implemented: ";
			NotImplementedException ex2 = ex;
			Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
		}
		catch (Exception ex3)
		{
			Debug.Log("validator not assigned");
			string str2 = "Cross Platform Validator Not Implemented: ";
			Exception ex4 = ex3;
			Debug.LogError(str2 + ((ex4 != null) ? ex4.ToString() : null));
		}
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x00065854 File Offset: 0x00063A54
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
		if (this.apple == null)
		{
			this.apple = extensions.GetExtension<IAppleExtensions>();
		}
		if (this.googleplay == null)
		{
			this.googleplay = extensions.GetExtension<IGooglePlayStoreExtensions>();
		}
		this.apple.RegisterPurchaseDeferredListener(new Action<Product>(this.OnAskToBuy));
		this.checkPremium();
		this.checkWorldnet();
		Discounts.checkDiscounts();
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000658C0 File Offset: 0x00063AC0
	public void checkPremium()
	{
		bool flag = true;
		bool flag2 = false;
		Product product = this.controller.products.WithID("premium");
		Config.lockGameControls = false;
		if (!flag && flag2)
		{
			return;
		}
		if ((flag && product != null && product.hasReceipt) || PlayerConfig.instance.data.premium)
		{
			InAppManager.activatePrem(false);
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0006591B File Offset: 0x00063B1B
	public void checkWorldnet()
	{
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0006591D File Offset: 0x00063B1D
	public static void consumePremium()
	{
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0006591F File Offset: 0x00063B1F
	private void OnAskToBuy(Product item)
	{
		Debug.Log("Purchase deferred: " + item.definition.id);
		Config.lockGameControls = false;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00065944 File Offset: 0x00063B44
	public string getDebugInfo()
	{
		string str = "";
		Product product = this.controller.products.WithID("premium");
		return str + "hasReceipt: " + product.hasReceipt.ToString() + "\nreceipt: " + product.receipt + "\nprem? " + PlayerConfig.instance.data.premium.ToString();
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000659B4 File Offset: 0x00063BB4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void activatePrem(bool pShowWindowUnlocked = false)
	{
		PlayerConfig.instance.data.premium = true;
		PlayerConfig.saveData();
		Config.havePremium = true;
		PremiumElementsChecker.checkElements();
		PlayerConfig.setFirebaseProp("have_premium", "yes");
		if (pShowWindowUnlocked)
		{
			if (!PlayerConfig.instance.data.tutorialFinished)
			{
				ScrollWindow.queueWindow("premium_unlocked");
				return;
			}
			ScrollWindow.hideAllEvent(false);
			ScrollWindow.showWindow("premium_unlocked");
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00065A1F File Offset: 0x00063C1F
	private void activateSub(bool pShowWindowUnlocked = false)
	{
		if (pShowWindowUnlocked && PlayerConfig.instance.data.tutorialFinished)
		{
			ScrollWindow.hideAllEvent(false);
			ScrollWindow.showWindow("worldnet_sub");
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00065A45 File Offset: 0x00063C45
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Config.lockGameControls = false;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00065A50 File Offset: 0x00063C50
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		Config.lockGameControls = true;
		Debug.Log("Purchase OK: " + args.purchasedProduct.definition.id);
		Debug.Log("Receipt: " + args.purchasedProduct.receipt);
		string str = "Args: ";
		Product purchasedProduct = args.purchasedProduct;
		Debug.Log(str + ((purchasedProduct != null) ? purchasedProduct.ToString() : null));
		if (args.purchasedProduct.definition.id == "premium")
		{
			Debug.Log("process premium");
			return this.ProcessPremium(args);
		}
		if (args.purchasedProduct.definition.id == "worldnet")
		{
			Debug.Log("process worldnet");
			return this.ProcessWorldnet(args);
		}
		Debug.Log("process nothing to be done");
		return 0;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x00065B24 File Offset: 0x00063D24
	public PurchaseProcessingResult ProcessPremium(PurchaseEventArgs args)
	{
		bool flag = true;
		bool flag2 = false;
		if (!flag && flag2)
		{
			Config.lockGameControls = false;
			return 1;
		}
		if (flag && string.Equals(args.purchasedProduct.definition.id, "premium", StringComparison.Ordinal))
		{
			InAppManager.activatePrem(true);
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		}
		Config.lockGameControls = false;
		return 0;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00065B94 File Offset: 0x00063D94
	public PurchaseProcessingResult ProcessWorldnet(PurchaseEventArgs args)
	{
		bool flag = true;
		bool flag2 = false;
		PlayerConfig.instance.data.worldnet = "";
		if (!flag && flag2)
		{
			Debug.Log("purchase pending");
			Config.lockGameControls = false;
			return 1;
		}
		Debug.Log("check if valid");
		Config.lockGameControls = false;
		if (flag && string.Equals(args.purchasedProduct.definition.id, "worldnet", StringComparison.Ordinal))
		{
			Debug.Log("valid!");
			this.setWorldnetSubscription(args.purchasedProduct.transactionID);
			this.activateSub(true);
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		}
		Debug.Log("we are here");
		if (Config.lockGameControls)
		{
			Debug.Log("lockgamecontrosl locked");
		}
		else
		{
			Debug.Log("lockgamecontrosl not locked");
		}
		return 0;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00065C6C File Offset: 0x00063E6C
	public void setWorldnetSubscription(string pTransactionID)
	{
		PlayerConfig.instance.data.worldnet = pTransactionID;
		PlayerConfig.saveData();
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00065C83 File Offset: 0x00063E83
	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		Debug.Log("WORLDBOX PURCHASE FAILED  " + p.ToString());
		Config.lockGameControls = false;
		ScrollWindow.showWindow("premium_purchase_error");
		this.InitializePurchasing(true);
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x00065CBC File Offset: 0x00063EBC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void checkPremiumReceipt(string pReceipt, ref bool validPurchase, ref bool purchasePending)
	{
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00065CBE File Offset: 0x00063EBE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void checkWorldnetReceipt(string pReceipt, ref bool validPurchase, ref bool purchasePending)
	{
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x00065CC0 File Offset: 0x00063EC0
	public bool buyPremium()
	{
		if (!Config.isMobile)
		{
			InAppManager.activatePrem(true);
			Config.lockGameControls = false;
			return true;
		}
		if (!this.IsInitialized())
		{
			this.InitializePurchasing(true);
			ScrollWindow.showWindow("premium_purchase_error");
			Config.lockGameControls = false;
			return false;
		}
		return this.BuyProductID("premium");
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00065D0E File Offset: 0x00063F0E
	public bool buyWorldNet()
	{
		return false;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00065D14 File Offset: 0x00063F14
	private bool BuyProductID(string productId)
	{
		if (!this.IsInitialized())
		{
			ScrollWindow.showWindow(productId + "_purchase_error");
			Debug.Log("BuyProductID FAIL. Not initialized.");
			Config.lockGameControls = false;
			return false;
		}
		Product product = this.controller.products.WithID(productId);
		if (product != null && product.availableToPurchase)
		{
			Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
			Config.lockGameControls = true;
			this.controller.InitiatePurchase(product);
			if (ScrollWindow.windowLoaded("premium_menu") && ScrollWindow.get("premium_menu").currentWindow)
			{
				ScrollWindow.get("premium_menu").clickHide("right");
			}
			return true;
		}
		ScrollWindow.showWindow(productId + "_purchase_error");
		Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
		Config.lockGameControls = false;
		return false;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00065DEC File Offset: 0x00063FEC
	public void RestorePurchases()
	{
		if (!this.IsInitialized())
		{
			this.InitializePurchasing(true);
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");
			this.apple.RestoreTransactions(delegate(bool result)
			{
				Debug.Log("RestorePurchases continuing: " + result.ToString() + ". If no further messages, no purchases available to restore.");
				if (!result)
				{
					Application.OpenURL("https://www.superworldbox.com/faq#i-purchased-the-premium-on-ios-and-later-got-a-new-apple-device-how-do-i-restore-premium");
				}
			});
			return;
		}
		Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform.ToString());
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00065E7A File Offset: 0x0006407A
	private bool IsInitialized()
	{
		return this.controller != null && this.extensions != null;
	}

	// Token: 0x04000C64 RID: 3172
	public static InAppManager instance;

	// Token: 0x04000C65 RID: 3173
	private IAppleExtensions apple;

	// Token: 0x04000C66 RID: 3174
	private IGooglePlayStoreExtensions googleplay;

	// Token: 0x04000C67 RID: 3175
	internal IStoreController controller;

	// Token: 0x04000C68 RID: 3176
	private IExtensionProvider extensions;

	// Token: 0x04000C69 RID: 3177
	private static CrossPlatformValidator validator;

	// Token: 0x04000C6A RID: 3178
	private static bool googleAccount = true;
}
