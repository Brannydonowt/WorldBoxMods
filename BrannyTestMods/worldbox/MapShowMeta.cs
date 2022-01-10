using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CA RID: 714
public class MapShowMeta : MonoBehaviour
{
	// Token: 0x06000F8C RID: 3980 RVA: 0x0008B464 File Offset: 0x00089664
	private void Update()
	{
		if (this.startSpinning)
		{
			this.angle -= Time.deltaTime * 180f;
			this.iconFavorite.transform.localEulerAngles = new Vector3(0f, 0f, this.angle);
			return;
		}
		if (this.angle != 0f)
		{
			this.angle -= Time.deltaTime * 720f;
			if (this.angle < -360f)
			{
				this.angle = 0f;
			}
			this.iconFavorite.transform.localEulerAngles = new Vector3(0f, 0f, this.angle);
		}
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x0008B519 File Offset: 0x00089719
	public void pressFavorite()
	{
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0008B51B File Offset: 0x0008971B
	public void copyToClipboard()
	{
	}

	// Token: 0x0400127E RID: 4734
	public WorldElement worldElementPrefab;

	// Token: 0x0400127F RID: 4735
	public WorldElement element;

	// Token: 0x04001280 RID: 4736
	public Transform transformContent;

	// Token: 0x04001281 RID: 4737
	public GameObject loadingSpinner;

	// Token: 0x04001282 RID: 4738
	public GameObject errorImage;

	// Token: 0x04001283 RID: 4739
	public GameObject textStatusBG;

	// Token: 0x04001284 RID: 4740
	public Text textStatusMessage;

	// Token: 0x04001285 RID: 4741
	public GameObject playButton;

	// Token: 0x04001286 RID: 4742
	public GameObject favButton;

	// Token: 0x04001287 RID: 4743
	public Text playButtonText;

	// Token: 0x04001288 RID: 4744
	public GameObject deleteButton;

	// Token: 0x04001289 RID: 4745
	public GameObject reportButton;

	// Token: 0x0400128A RID: 4746
	public GameObject bottomButtons;

	// Token: 0x0400128B RID: 4747
	public Image iconFavorite;

	// Token: 0x0400128C RID: 4748
	private bool startSpinning;

	// Token: 0x0400128D RID: 4749
	private float angle;
}
