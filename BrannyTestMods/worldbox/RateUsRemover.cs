using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class RateUsRemover : MonoBehaviour
{
	// Token: 0x06000357 RID: 855 RVA: 0x00036520 File Offset: 0x00034720
	public void clickedRateUs()
	{
		PlayerConfig.instance.data.lastRateID = 11;
		base.gameObject.SetActive(false);
		PlayerConfig.saveData();
	}
}
