using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x02000297 RID: 663
public class GlobusPreview : MonoBehaviour
{
	// Token: 0x06000E9D RID: 3741 RVA: 0x00087BF4 File Offset: 0x00085DF4
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (this.use_current_world_info)
		{
			this.setCurrentWorldSprite();
		}
		else if (SaveManager.currentWorkshopMapData != null)
		{
			this.setWorkshopSlotSprite();
		}
		else
		{
			this.startLoadCurrentSaveSlotSprite();
		}
		this.startTweenGlobus();
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x00087C29 File Offset: 0x00085E29
	private void startLoadCurrentSaveSlotSprite()
	{
		base.StartCoroutine(this.loadSaveSlotImage());
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00087C38 File Offset: 0x00085E38
	private void setCurrentWorldSprite()
	{
		Sprite currentWorldPreview = PreviewHelper.getCurrentWorldPreview();
		this.setSprites(currentWorldPreview);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00087C54 File Offset: 0x00085E54
	private void setWorkshopSlotSprite()
	{
		Sprite sprites = PreviewHelper.loadWorkshopMapPreview();
		this.setSprites(sprites);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00087C6E File Offset: 0x00085E6E
	private void setSprites(Sprite pSprite)
	{
		this.makeGradient(pSprite);
		this.main_image_1.sprite = pSprite;
		this.main_image_2.sprite = pSprite;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00087C8F File Offset: 0x00085E8F
	private IEnumerator loadSaveSlotImage()
	{
		string currentPreviewPath = SaveManager.getCurrentPreviewPath();
		if (string.IsNullOrEmpty(currentPreviewPath) || !File.Exists(currentPreviewPath))
		{
			yield break;
		}
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + currentPreviewPath))
		{
			yield return webRequest.SendWebRequest();
			if (!webRequest.isNetworkError && !webRequest.isHttpError)
			{
				Texture2D content = DownloadHandlerTexture.GetContent(webRequest);
				Sprite sprites = Sprite.Create(content, new Rect(0f, 0f, (float)content.width, (float)content.height), new Vector2(0.5f, 0.5f));
				this.setSprites(sprites);
			}
		}
		UnityWebRequest webRequest = null;
		yield break;
		yield break;
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x00087CA0 File Offset: 0x00085EA0
	private void makeGradient(Sprite pSprite)
	{
		float num = (float)pSprite.texture.width * 0.1f;
		Texture2D texture = pSprite.texture;
		int num2 = 0;
		while ((float)num2 < num)
		{
			for (int i = 0; i < texture.height; i++)
			{
				int num3 = num2;
				Color pixel = texture.GetPixel(num3, i);
				pixel.a = (float)num3 / num;
				texture.SetPixel(num3, i, pixel);
				num3 = pSprite.texture.width - num2;
				pixel = texture.GetPixel(num3, i);
				pixel.a = (float)num2 / num;
				texture.SetPixel(num3, i, pixel);
			}
			num2++;
		}
		texture.Apply();
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x00087D40 File Offset: 0x00085F40
	private void startTweenGlobus()
	{
		float num = this._box_size + this._gap_size;
		float num2 = num / this._tweenSpeed;
		ShortcutExtensions.DOKill(this.images_parent.transform, false);
		this.images_parent.transform.localPosition = new Vector3(this._gap_size, 0f, 0f);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.images_parent.transform, new Vector3(-num, 0f, 0f), num2, false), 1).onComplete = new TweenCallback(this.tweenLoop);
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00087DD8 File Offset: 0x00085FD8
	private void tweenLoop()
	{
		float num = this._box_size + this._gap_size;
		float num2 = num / this._tweenSpeed;
		this.images_parent.transform.localPosition = new Vector3(0f, 0f, 0f);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.images_parent.transform, new Vector3(-num, 0f, 0f), num2, false), 1).onComplete = new TweenCallback(this.tweenLoop);
	}

	// Token: 0x04001181 RID: 4481
	public bool use_current_world_info;

	// Token: 0x04001182 RID: 4482
	public Image main_image_1;

	// Token: 0x04001183 RID: 4483
	public Image main_image_2;

	// Token: 0x04001184 RID: 4484
	public GameObject images_parent;

	// Token: 0x04001185 RID: 4485
	public Image clouds;

	// Token: 0x04001186 RID: 4486
	private float _tweenSpeed = 18f;

	// Token: 0x04001187 RID: 4487
	private float _gap_size = 25f;

	// Token: 0x04001188 RID: 4488
	private float _box_size = 100f;
}
