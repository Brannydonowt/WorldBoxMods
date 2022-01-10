using System;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class WorkshopMapData
{
	// Token: 0x06000F6C RID: 3948 RVA: 0x0008AA7C File Offset: 0x00088C7C
	public static WorkshopMapData currentMapToWorkshop()
	{
		WorkshopMapData workshopMapData = new WorkshopMapData();
		string text = SaveManager.generateWorkshopPath("");
		SavedMap savedMap = SaveManager.saveWorldToDirectory(text, true, true);
		workshopMapData.meta_data_map = savedMap.getMeta();
		workshopMapData.preview_image_path = text + "preview.png";
		workshopMapData.main_path = text;
		return workshopMapData;
	}

	// Token: 0x0400124D RID: 4685
	public string main_path;

	// Token: 0x0400124E RID: 4686
	public string preview_image_path;

	// Token: 0x0400124F RID: 4687
	public Sprite sprite_small_preview;

	// Token: 0x04001250 RID: 4688
	public MapMetaData meta_data_map;

	// Token: 0x04001251 RID: 4689
	public WorkshopMapMetaData meta_data_workshop;

	// Token: 0x04001252 RID: 4690
	public Item workshop_item;
}
