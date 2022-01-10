using System;
using Steamworks.Data;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C8 RID: 712
public class WorkshopUploadingWorldWindow : MonoBehaviour
{
	// Token: 0x06000F84 RID: 3972 RVA: 0x0008B07C File Offset: 0x0008927C
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		WorkshopUploadingWorldWindow.needsWorkshopAgreement = false;
		this.windowState = -1;
		this.errorImage.gameObject.SetActive(false);
		this.doneButton.gameObject.SetActive(false);
		this.workshopAgreementButton.gameObject.SetActive(false);
		this.statusMessage.text = LocalizedTextManager.getText("uploading_your_world", null);
		this.loadingImage.gameObject.SetActive(true);
		this.doneImage.gameObject.SetActive(false);
		this.bar.gameObject.SetActive(true);
		this.percents.gameObject.SetActive(true);
		this.mask.gameObject.SetActive(true);
		this.barParent.SetActive(true);
		this.bar.transform.localScale = new Vector3(0f, 1f, 1f);
		WorkshopUploadingWorldWindow.uploading = true;
		SteamSDK.steamInitialized.Then(() => WorkshopMaps.uploadMap()).Then(delegate()
		{
			this.progressBarUpdate();
			WorkshopUploadingWorldWindow.uploading = false;
			this.doneButton.gameObject.SetActive(true);
			this.statusMessage.text = LocalizedTextManager.getText("world_uploaded", null);
			this.loadingImage.gameObject.SetActive(false);
			this.doneImage.gameObject.SetActive(true);
			if (WorkshopUploadingWorldWindow.needsWorkshopAgreement)
			{
				this.statusMessage.text = LocalizedTextManager.getText("workshop_agreement", null);
				this.workshopAgreementButton.SetActive(true);
			}
			else
			{
				string str = "steam://url/CommunityFilePage/";
				PublishedFileId uploaded_file_id = WorkshopMaps.uploaded_file_id;
				Application.OpenURL(str + uploaded_file_id.ToString());
			}
			this.barParent.SetActive(false);
			this.bar.gameObject.SetActive(false);
			this.percents.gameObject.SetActive(false);
			this.mask.gameObject.SetActive(false);
		}).Catch(delegate(Exception e)
		{
			this.statusMessage.text = LocalizedTextManager.getText("upload_error", null) + "\n( " + e.Message.ToString() + " )";
			WorkshopUploadingWorldWindow.uploading = false;
			Debug.LogError(e.Message.ToString());
			this.doneButton.gameObject.SetActive(true);
			this.doneImage.gameObject.SetActive(false);
			this.loadingImage.gameObject.SetActive(false);
			this.errorImage.gameObject.SetActive(true);
		});
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x0008B1BE File Offset: 0x000893BE
	private void Update()
	{
		if (WorkshopUploadingWorldWindow.uploading || this.percents.isActiveAndEnabled)
		{
			this.progressBarUpdate();
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x0008B1DC File Offset: 0x000893DC
	private void progressBarUpdate()
	{
		float uploadProgress = WorkshopMaps.uploadProgress;
		float num = this.bar.transform.localScale.x;
		if (this.bar.transform.localScale.x < uploadProgress)
		{
			num = this.bar.transform.localScale.x + Time.deltaTime;
			if (num > uploadProgress || uploadProgress > 0.75f)
			{
				num = uploadProgress;
			}
			this.bar.transform.localScale = new Vector3(num, 1f, 1f);
			this.percents.text = Mathf.CeilToInt(num * 100f).ToString() + " %";
			return;
		}
		this.percents.text = Mathf.CeilToInt(uploadProgress * 100f).ToString() + " %";
	}

	// Token: 0x0400126A RID: 4714
	public Button doneButton;

	// Token: 0x0400126B RID: 4715
	public Image loadingImage;

	// Token: 0x0400126C RID: 4716
	public Image doneImage;

	// Token: 0x0400126D RID: 4717
	public Image errorImage;

	// Token: 0x0400126E RID: 4718
	public GameObject barParent;

	// Token: 0x0400126F RID: 4719
	public Text statusMessage;

	// Token: 0x04001270 RID: 4720
	private int windowState = -1;

	// Token: 0x04001271 RID: 4721
	public Text percents;

	// Token: 0x04001272 RID: 4722
	public Image bar;

	// Token: 0x04001273 RID: 4723
	public Image mask;

	// Token: 0x04001274 RID: 4724
	public static bool uploading;

	// Token: 0x04001275 RID: 4725
	public static bool needsWorkshopAgreement;

	// Token: 0x04001276 RID: 4726
	public GameObject workshopAgreementButton;
}
