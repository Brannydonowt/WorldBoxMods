using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C0 RID: 704
public class WorkshopInfoIcons : MonoBehaviour
{
	// Token: 0x06000F6A RID: 3946 RVA: 0x0008A9E0 File Offset: 0x00088BE0
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		WorkshopMapData currentWorkshopMapData = SaveManager.currentWorkshopMapData;
		this.favorites.text = currentWorkshopMapData.workshop_item.NumFavorites.ToString();
		this.upvotes.text = currentWorkshopMapData.workshop_item.VotesUp.ToString();
		this.comments.text = currentWorkshopMapData.workshop_item.NumComments.ToString();
		this.subscription.text = currentWorkshopMapData.workshop_item.NumSubscriptions.ToString();
	}

	// Token: 0x04001249 RID: 4681
	public Text favorites;

	// Token: 0x0400124A RID: 4682
	public Text upvotes;

	// Token: 0x0400124B RID: 4683
	public Text comments;

	// Token: 0x0400124C RID: 4684
	public Text subscription;
}
