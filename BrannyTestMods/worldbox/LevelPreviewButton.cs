using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x020002E1 RID: 737
public class LevelPreviewButton : MonoBehaviour
{
	// Token: 0x06000FD3 RID: 4051 RVA: 0x0008C400 File Offset: 0x0008A600
	public void click()
	{
		if (ScrollWindow.animationActive)
		{
			return;
		}
		if (this.buttonAnimation == null)
		{
			this.buttonAnimation = base.transform.parent.parent.parent.GetComponent<ButtonAnimation>();
		}
		this.buttonAnimation.clickAnimation();
		SaveManager.setCurrentSlot(this.slotData.slotID);
		if (this.worldNetUpload)
		{
			if (!SaveManager.currentSlotExists())
			{
				return;
			}
			if (!SaveManager.currentPreviewExists())
			{
				return;
			}
			if (!SaveManager.currentMetaExists())
			{
				return;
			}
			ScrollWindow.showWindow("worldnet_upload_world_name");
			return;
		}
		else
		{
			if (SaveManager.currentSlotExists())
			{
				ScrollWindow.showWindow("save_slot");
				return;
			}
			ScrollWindow.showWindow("save_slot_new");
			return;
		}
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x0008C4A8 File Offset: 0x0008A6A8
	public void checkTextureDestroy()
	{
		if (this.button.image.sprite.texture != this.defaultSprite.texture)
		{
			Object.Destroy(this.button.image.sprite.texture);
		}
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x0008C4F6 File Offset: 0x0008A6F6
	private void OnEnable()
	{
		if (this.autoload)
		{
			this.reloadImage();
		}
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x0008C508 File Offset: 0x0008A708
	private void OnDisable()
	{
		Button button = this.button;
		Object x;
		if (button == null)
		{
			x = null;
		}
		else
		{
			Image image = button.image;
			if (image == null)
			{
				x = null;
			}
			else
			{
				Sprite sprite = image.sprite;
				x = ((sprite != null) ? sprite.texture : null);
			}
		}
		if (x == this.defaultSprite.texture)
		{
			return;
		}
		Button button2 = this.button;
		Object obj;
		if (button2 == null)
		{
			obj = null;
		}
		else
		{
			Image image2 = button2.image;
			if (image2 == null)
			{
				obj = null;
			}
			else
			{
				Sprite sprite2 = image2.sprite;
				obj = ((sprite2 != null) ? sprite2.texture : null);
			}
		}
		Object.Destroy(obj);
		Button button3 = this.button;
		Object obj2;
		if (button3 == null)
		{
			obj2 = null;
		}
		else
		{
			Image image3 = button3.image;
			obj2 = ((image3 != null) ? image3.sprite : null);
		}
		Object.Destroy(obj2);
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x0008C5A4 File Offset: 0x0008A7A4
	public void reloadImage()
	{
		if (this == null)
		{
			return;
		}
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (this.loaded)
		{
			Button button = this.button;
			Object x;
			if (button == null)
			{
				x = null;
			}
			else
			{
				Image image = button.image;
				x = ((image != null) ? image.sprite : null);
			}
			if (x != null)
			{
				return;
			}
		}
		if (this.loading)
		{
			return;
		}
		this.loading = true;
		if (SaveManager.currentWorkshopMapData != null)
		{
			this.loadWorkshopMapPreview();
			return;
		}
		bool flag = SaveManager.currentSlotExists();
		if (this.slotData.slotID == -1 && !flag)
		{
			this.loadImage(PreviewHelper.getCurrentWorldPreview());
			return;
		}
		base.StartCoroutine(this.loadSaveSlotImage(this.slotData.slotID));
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0008C64C File Offset: 0x0008A84C
	private void loadWorkshopMapPreview()
	{
		this.loadImage(PreviewHelper.loadWorkshopMapPreview());
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0008C659 File Offset: 0x0008A859
	private IEnumerator loadSaveSlotImage(int slotID)
	{
		string path = SaveManager.getPngSlotPath(slotID);
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			this.loadImage(null);
			yield break;
		}
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + path))
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
					path
				}));
				this.loadImage(null);
			}
			else
			{
				Texture2D content = DownloadHandlerTexture.GetContent(webRequest);
				Sprite tSprite = Sprite.Create(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0.5f, 0.5f));
				this.loadImage(tSprite);
			}
		}
		UnityWebRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0008C670 File Offset: 0x0008A870
	public void loadImage(Sprite tSprite)
	{
		if (this == null || !base.isActiveAndEnabled)
		{
			this.loaded = false;
			this.loading = false;
			return;
		}
		if (!this.premiumOnly || Config.havePremium)
		{
			this.premiumIcon.gameObject.SetActive(false);
		}
		bool flag = false;
		if (tSprite != null)
		{
			flag = true;
			tSprite.texture.anisoLevel = 0;
			tSprite.texture.filterMode = FilterMode.Point;
		}
		else
		{
			tSprite = this.defaultSprite;
		}
		this.button.image.sprite = tSprite;
		base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(tSprite.textureRect.width, tSprite.textureRect.height);
		RectTransform component = this.button.transform.parent.parent.GetComponent<RectTransform>();
		float num = component.sizeDelta.x / tSprite.rect.width;
		float num2 = component.sizeDelta.y / tSprite.rect.height;
		float num3 = (num > num2) ? num : num2;
		Transform parent = base.transform.parent;
		if (!flag)
		{
			num3 = 1f;
		}
		parent.localScale = new Vector3(num3, num3, 1f);
		this.loaded = true;
		this.loading = false;
	}

	// Token: 0x040012E7 RID: 4839
	public bool premiumOnly = true;

	// Token: 0x040012E8 RID: 4840
	public bool worldNetUpload;

	// Token: 0x040012E9 RID: 4841
	public Image premiumIcon;

	// Token: 0x040012EA RID: 4842
	public Image rewardAdIcon;

	// Token: 0x040012EB RID: 4843
	public Button button;

	// Token: 0x040012EC RID: 4844
	public SlotButtonCallback slotData;

	// Token: 0x040012ED RID: 4845
	public Sprite defaultSprite;

	// Token: 0x040012EE RID: 4846
	private ButtonAnimation buttonAnimation;

	// Token: 0x040012EF RID: 4847
	public bool loaded;

	// Token: 0x040012F0 RID: 4848
	public bool loading;

	// Token: 0x040012F1 RID: 4849
	public bool autoload;
}
