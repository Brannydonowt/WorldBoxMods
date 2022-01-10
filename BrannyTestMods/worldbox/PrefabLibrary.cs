using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009B RID: 155
public class PrefabLibrary : MonoBehaviour
{
	// Token: 0x0600033E RID: 830 RVA: 0x000352C5 File Offset: 0x000334C5
	private void Awake()
	{
		PrefabLibrary.instance = this;
	}

	// Token: 0x0400053E RID: 1342
	internal static PrefabLibrary instance;

	// Token: 0x0400053F RID: 1343
	public GameObject graphy;

	// Token: 0x04000540 RID: 1344
	public DebugTool debugTool;

	// Token: 0x04000541 RID: 1345
	public Building building;

	// Token: 0x04000542 RID: 1346
	public DragonAsset dragonAsset;

	// Token: 0x04000543 RID: 1347
	public DragonAsset zombieDragonAsset;

	// Token: 0x04000544 RID: 1348
	public Giantzilla crab;

	// Token: 0x04000545 RID: 1349
	public Image iconLock;

	// Token: 0x04000546 RID: 1350
	public Sprite pixel;

	// Token: 0x04000547 RID: 1351
	public Texture2D button_unlocked;

	// Token: 0x04000548 RID: 1352
	public Texture2D button_unlocked_new;

	// Token: 0x04000549 RID: 1353
	public Texture2D button_unlocked_flash;

	// Token: 0x0400054A RID: 1354
	public Transform favoriteUnit;

	// Token: 0x0400054B RID: 1355
	public MapObjectShadow shadow;
}
