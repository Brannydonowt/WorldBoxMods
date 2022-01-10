using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class WorkshopOpenSteamWorkshop : MonoBehaviour
{
	// Token: 0x06000F7B RID: 3963 RVA: 0x0008AFC3 File Offset: 0x000891C3
	public void playWorkShopMap()
	{
		Application.OpenURL("steam://url/SteamWorkshopPage/1206560");
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0008AFCF File Offset: 0x000891CF
	public void openWorkShopAgreement()
	{
		Application.OpenURL("steam://url/CommunityFilePage/" + WorkshopOpenSteamWorkshop.fileID);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001269 RID: 4713
	public static string fileID;
}
