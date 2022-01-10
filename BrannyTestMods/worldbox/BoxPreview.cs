using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x02000296 RID: 662
public class BoxPreview : MonoBehaviour
{
	// Token: 0x06000E95 RID: 3733 RVA: 0x00087974 File Offset: 0x00085B74
	public void setSlot(int pID)
	{
		this._slot_id = pID;
		this._world_path = SaveManager.getSlotSavePath(pID);
		if (Input.mousePresent)
		{
			this.button.OnHover(new UnityAction(this.showHoverTooltip));
			this.button.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
		}
		bool flag = false;
		bool flag2 = false;
		if (pID > 1)
		{
			flag = true;
		}
		bool flag3 = SaveManager.isFolderExists(this._world_path);
		if (flag3)
		{
			this._metaData = SaveManager.getMetaFor(this._world_path);
		}
		this.preview_image.sprite = this.preview_default;
		this.icon_gift.gameObject.SetActive(false);
		this.icon_premium.gameObject.SetActive(false);
		if (flag2 && !Config.havePremium)
		{
			this.state = BoxState.OpenWithReward;
			this.icon_gift.gameObject.SetActive(true);
		}
		else if (flag && !Config.havePremium)
		{
			this.state = BoxState.PremiumOnly;
			this.icon_premium.gameObject.SetActive(true);
		}
		else if (flag3)
		{
			this.state = BoxState.WithMap;
		}
		else
		{
			this.state = BoxState.Empty;
		}
		this.wantLoadPreview = true;
		this.timer_preview = 0.02f * (float)pID;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00087A94 File Offset: 0x00085C94
	private void showHoverTooltip()
	{
		if (this._metaData == null)
		{
			return;
		}
		if (!Config.tooltipsActive)
		{
			return;
		}
		this._metaData.temp_date_string = SaveManager.getMetaDataCreationTime(this._world_path);
		Tooltip.info_map_meta = this._metaData;
		Tooltip.instance.show(this.button.gameObject, "map_meta", null, null);
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00087AEF File Offset: 0x00085CEF
	private void Update()
	{
		if (this.wantLoadPreview)
		{
			if (this.timer_preview > 0f)
			{
				this.timer_preview -= Time.deltaTime;
				return;
			}
			this.wantLoadPreview = false;
			base.StartCoroutine(this.loadSaveSlotImage());
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00087B2D File Offset: 0x00085D2D
	public void loadImage(Sprite tSprite)
	{
		if (tSprite == null)
		{
			this.preview_image.sprite = this.preview_default;
			return;
		}
		this.preview_image.sprite = tSprite;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x00087B56 File Offset: 0x00085D56
	private IEnumerator loadSaveSlotImage()
	{
		string tPath = SaveManager.generatePngPreviewPath(this._world_path);
		if (string.IsNullOrEmpty(tPath) || !File.Exists(tPath))
		{
			this.loadImage(null);
			yield break;
		}
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + tPath))
		{
			yield return webRequest.SendWebRequest();
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{
				Debug.LogError(string.Concat(new string[]
				{
					base.gameObject.name,
					" ",
					webRequest.error,
					" ",
					tPath
				}));
				this.loadImage(null);
			}
			else
			{
				Texture2D content = DownloadHandlerTexture.GetContent(webRequest);
				this.setSpriteFromTexture(content);
			}
		}
		UnityWebRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00087B68 File Offset: 0x00085D68
	private void setSpriteFromTexture(Texture2D pTexture)
	{
		pTexture = Toolbox.ScaleTexture(pTexture, 100, 100);
		Sprite tSprite = Sprite.Create(pTexture, new Rect(0f, 0f, (float)pTexture.width, (float)pTexture.height), new Vector2(0.5f, 0.5f));
		this.loadImage(tSprite);
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00087BBB File Offset: 0x00085DBB
	public void click()
	{
		if (ScrollWindow.animationActive)
		{
			return;
		}
		SaveManager.setCurrentPath(this._world_path);
		if (SaveManager.currentSlotExists())
		{
			ScrollWindow.showWindow("save_slot");
			return;
		}
		ScrollWindow.showWindow("save_slot_new");
	}

	// Token: 0x04001176 RID: 4470
	public BoxState state;

	// Token: 0x04001177 RID: 4471
	private string _world_path;

	// Token: 0x04001178 RID: 4472
	public Sprite preview_default;

	// Token: 0x04001179 RID: 4473
	public Image icon_gift;

	// Token: 0x0400117A RID: 4474
	public Image icon_premium;

	// Token: 0x0400117B RID: 4475
	public Image preview_image;

	// Token: 0x0400117C RID: 4476
	public Button button;

	// Token: 0x0400117D RID: 4477
	private bool wantLoadPreview;

	// Token: 0x0400117E RID: 4478
	private float timer_preview;

	// Token: 0x0400117F RID: 4479
	private int _slot_id;

	// Token: 0x04001180 RID: 4480
	private MapMetaData _metaData;
}
