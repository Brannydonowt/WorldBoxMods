using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class ButtonPremium : MonoBehaviour
{
	// Token: 0x06000FB0 RID: 4016 RVA: 0x0008BF2E File Offset: 0x0008A12E
	public void clickPremium()
	{
		PlayerConfig.setFirebaseProp("clicked_buy_premium", "yes");
		Analytics.LogEvent("clicked_buy_premium", true, true);
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ScrollWindow.showWindow("premium_purchase_error");
			return;
		}
		InAppManager.instance.buyPremium();
	}
}
