using System;
using System.Collections.Generic;
using System.IO;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class WorkshopMapListWindow : MonoBehaviour
{
	// Token: 0x06000F71 RID: 3953 RVA: 0x0008AC50 File Offset: 0x00088E50
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this._timer = 0.3f;
		this.world = MapBox.instance;
		foreach (WorkshopMapElement workshopMapElement in this.elements)
		{
			Object.Destroy(workshopMapElement.gameObject);
		}
		this.elements.Clear();
		SteamSDK.steamInitialized.Then(delegate()
		{
			this.prepareList();
		}).Catch(delegate(Exception err)
		{
			Debug.LogError(err);
			ErrorWindow.errorMessage = "Error happened while connecting to Steam Workshop:\n" + err.Message.ToString();
			ScrollWindow.get("error_with_reason").clickShow();
		});
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0008AD0C File Offset: 0x00088F0C
	private void OnDisable()
	{
		this._showQueue.Clear();
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x0008AD1C File Offset: 0x00088F1C
	private void Update()
	{
		if (this._timer > 0f)
		{
			this._timer -= Time.deltaTime;
		}
		else
		{
			this._timer = 0.015f;
			this.showNextItemFromQueue();
		}
		if (this._no_items)
		{
			this._no_items = false;
			ScrollWindow.showWindow("steam_workshop_empty");
		}
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0008AD74 File Offset: 0x00088F74
	private async void prepareList()
	{
		List<Item> list = await WorkshopMaps.listWorkshopMaps(false, false);
		if (list.Count > 0)
		{
			using (List<Item>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Item item = enumerator.Current;
					this._showQueue.Enqueue(item);
				}
				return;
			}
		}
		this._no_items = true;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0008ADB0 File Offset: 0x00088FB0
	private void showNextItemFromQueue()
	{
		if (this._showQueue.Count == 0)
		{
			return;
		}
		Item pSteamworksItem = this._showQueue.Dequeue();
		this.renderMapElement(pSteamworksItem);
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0008ADE0 File Offset: 0x00088FE0
	private WorkshopMapData loadMapDataFromStorage(Item pSteamworksItem)
	{
		string text = SaveManager.generatePngSmallPreviewPath(pSteamworksItem.Directory);
		WorkshopMapData workshopMapData = new WorkshopMapData();
		workshopMapData.main_path = pSteamworksItem.Directory;
		workshopMapData.workshop_item = pSteamworksItem;
		if (this.cached_sprites.ContainsKey(text))
		{
			workshopMapData.sprite_small_preview = this.cached_sprites[text];
		}
		else
		{
			try
			{
				byte[] array = File.ReadAllBytes(text);
				Texture2D texture2D = new Texture2D(32, 32);
				texture2D.anisoLevel = 0;
				texture2D.filterMode = FilterMode.Point;
				ImageConversion.LoadImage(texture2D, array);
				workshopMapData.sprite_small_preview = Sprite.Create(texture2D, new Rect(0f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));
				this.cached_sprites.Add(text, workshopMapData.sprite_small_preview);
			}
			catch (Exception)
			{
			}
		}
		MapMetaData metaFor = SaveManager.getMetaFor(pSteamworksItem.Directory);
		bool flag = false;
		if (!string.IsNullOrWhiteSpace(pSteamworksItem.Title) && metaFor.mapStats.name != pSteamworksItem.Title)
		{
			metaFor.mapStats.name = pSteamworksItem.Title;
			flag = true;
		}
		if (metaFor.mapStats.description != pSteamworksItem.Description)
		{
			metaFor.mapStats.description = pSteamworksItem.Description;
			flag = true;
		}
		if (flag)
		{
			SaveManager.saveMetaIn(pSteamworksItem.Directory, metaFor);
		}
		workshopMapData.meta_data_map = metaFor;
		return workshopMapData;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x0008AF50 File Offset: 0x00089150
	private void renderMapElement(Item pSteamworksItem)
	{
		WorkshopMapElement workshopMapElement = Object.Instantiate<WorkshopMapElement>(this.elementPrefab, this.transformContent);
		this.elements.Add(workshopMapElement);
		WorkshopMapData pData = this.loadMapDataFromStorage(pSteamworksItem);
		workshopMapElement.load(pData);
	}

	// Token: 0x0400125E RID: 4702
	public WorkshopMapElement elementPrefab;

	// Token: 0x0400125F RID: 4703
	private Dictionary<string, Sprite> cached_sprites = new Dictionary<string, Sprite>();

	// Token: 0x04001260 RID: 4704
	private List<WorkshopMapElement> elements = new List<WorkshopMapElement>();

	// Token: 0x04001261 RID: 4705
	private MapBox world;

	// Token: 0x04001262 RID: 4706
	public Transform transformContent;

	// Token: 0x04001263 RID: 4707
	private float _timer;

	// Token: 0x04001264 RID: 4708
	private bool _no_items;

	// Token: 0x04001265 RID: 4709
	private Queue<Item> _showQueue = new Queue<Item>();
}
