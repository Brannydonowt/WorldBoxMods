using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class WorkshopHelpers : MonoBehaviour
{
	// Token: 0x06000F66 RID: 3942 RVA: 0x0008A97C File Offset: 0x00088B7C
	public void openCurrentMapInWorkshop()
	{
		Application.OpenURL("steam://url/CommunityFilePage/" + SaveManager.currentWorkshopMapData.workshop_item.Id.ToString());
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x0008A9B5 File Offset: 0x00088BB5
	public void openUploadWorld()
	{
		SaveManager.clearCurrentSelectedWorld();
		ScrollWindow.showWindow("steam_workshop_upload_world");
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0008A9C6 File Offset: 0x00088BC6
	public void openBrowseWorlds()
	{
		SaveManager.clearCurrentSelectedWorld();
		ScrollWindow.showWindow("steam_workshop_browse");
	}

	// Token: 0x04001247 RID: 4679
	public const string color_own_map = "#3DDEFF";

	// Token: 0x04001248 RID: 4680
	public const string color_other_map = "#FF9B1C";
}
