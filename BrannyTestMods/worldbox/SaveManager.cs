using System;
using System.Collections.Generic;
using System.IO;
using Assets.SimpleZip;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class SaveManager : MonoBehaviour
{
	// Token: 0x06000C12 RID: 3090 RVA: 0x000771F3 File Offset: 0x000753F3
	private void Start()
	{
		SaveManager.persistentDataPath = Application.persistentDataPath;
		this.world = MapBox.instance;
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0007720A File Offset: 0x0007540A
	public static void clearCurrentSelectedWorld()
	{
		SaveManager.currentWorkshopMapData = null;
		SaveManager.currentSavePath = string.Empty;
		SaveManager.currentSlot = 0;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00077224 File Offset: 0x00075424
	public void clickSaveSlot()
	{
		try
		{
			this.saveToCurrentPath();
			ScrollWindow.hideAllEvent(true);
		}
		catch (Exception message)
		{
			Debug.Log("Error during saving");
			Debug.LogError(message);
			ScrollWindow.showWindow("error_happened");
		}
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0007726C File Offset: 0x0007546C
	public SavedMap saveToCurrentPath()
	{
		return SaveManager.saveWorldToDirectory(SaveManager.currentSavePath, true, true);
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0007727A File Offset: 0x0007547A
	public static SavedMap saveWorldToDirectory(string pFolder, bool pCompress = true, bool pCheckFolder = true)
	{
		pFolder = SaveManager.folderPath(pFolder);
		if (pCheckFolder)
		{
			if (!Directory.Exists(pFolder))
			{
				Directory.CreateDirectory(pFolder);
			}
		}
		else
		{
			Directory.CreateDirectory(pFolder);
		}
		SaveManager.saveImagePreview(pFolder);
		return SaveManager.saveMapData(pFolder, pCompress);
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x000772AC File Offset: 0x000754AC
	public static SavedMap currentWorldToSavedMap()
	{
		SavedMap savedMap = new SavedMap();
		savedMap.create();
		return savedMap;
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x000772B9 File Offset: 0x000754B9
	public static void deleteSavePath(string pPath)
	{
		if (Directory.Exists(pPath))
		{
			Directory.Delete(pPath, true);
		}
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x000772CA File Offset: 0x000754CA
	public static void deleteCurrentSave()
	{
		SaveManager.deleteSavePath(SaveManager.currentSavePath);
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x000772DC File Offset: 0x000754DC
	public static SavedMap saveMapData(string pFolder, bool pCompress = true)
	{
		SavedMap savedMap = SaveManager.currentWorldToSavedMap();
		pFolder = SaveManager.folderPath(pFolder);
		SaveManager.saveMetaData(savedMap.getMeta(), pFolder);
		if (pCompress)
		{
			string text = pFolder + "map.wbox";
			byte[] array = savedMap.toZip();
			File.WriteAllBytes(text, array);
		}
		else
		{
			string text2 = pFolder + "map.wbax";
			string text3 = savedMap.toJson();
			File.WriteAllText(text2, text3);
		}
		return savedMap;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00077339 File Offset: 0x00075539
	public static void saveMetaData(MapMetaData pMetaData, string pPath)
	{
		pMetaData.prepareForSave();
		SaveManager.saveMetaIn(pPath, pMetaData);
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00077348 File Offset: 0x00075548
	public static MapMetaData getCurrentMeta()
	{
		return SaveManager.getMetaFor(SaveManager.currentSavePath);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00077354 File Offset: 0x00075554
	public static MapMetaData getMetaFor(string pFolder)
	{
		string text = SaveManager.generateMetaPath(pFolder);
		if (File.Exists(text))
		{
			return JsonUtility.FromJson<MapMetaData>(File.ReadAllText(text));
		}
		return null;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00077380 File Offset: 0x00075580
	public static string getMetaDataCreationTime(string pFolder)
	{
		string text = SaveManager.generateMetaPath(pFolder);
		if (!File.Exists(text))
		{
			return "??";
		}
		DateTime t = DateTime.UtcNow.AddDays(7.0);
		DateTime creationTimeUtc = File.GetCreationTimeUtc(text);
		if (creationTimeUtc.Year < 2017)
		{
			return "GREG";
		}
		if (creationTimeUtc > t)
		{
			return "DREDD";
		}
		return creationTimeUtc.ToShortDateString();
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000773EA File Offset: 0x000755EA
	public static void saveMetaIn(string pFolder, MapMetaData pMetaData)
	{
		File.WriteAllText(SaveManager.generateMetaPath(pFolder), pMetaData.toJson());
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000773FD File Offset: 0x000755FD
	public static bool isFolderExists(string pFolder)
	{
		return Directory.Exists(pFolder);
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00077405 File Offset: 0x00075605
	public static bool slotExists(int pSlot)
	{
		return File.Exists(SaveManager.getSlotPathWbox(pSlot)) || File.Exists(SaveManager.getOldSlotPath(pSlot));
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00077426 File Offset: 0x00075626
	public static bool slotMetaExists(int pSlot = -1)
	{
		return File.Exists(SaveManager.getMetaSlotPath(pSlot));
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00077438 File Offset: 0x00075638
	public void copyDataToClipboard()
	{
		SaveManager.currentWorldToSavedMap();
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00077440 File Offset: 0x00075640
	private static void saveImagePreview(string pFolder)
	{
		Texture2D texture2D = PreviewHelper.convertMapToTexture();
		Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height);
		Graphics.CopyTexture(texture2D, texture2D2);
		TextureScale.Point(texture2D2, 32, 32);
		byte[] array = ImageConversion.EncodeToPNG(texture2D);
		File.WriteAllBytes(SaveManager.generatePngPreviewPath(pFolder), array);
		byte[] array2 = ImageConversion.EncodeToPNG(texture2D2);
		File.WriteAllBytes(SaveManager.generatePngSmallPreviewPath(pFolder), array2);
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0007749C File Offset: 0x0007569C
	public static int getCurrentSlot()
	{
		return SaveManager.currentSlot;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000774A3 File Offset: 0x000756A3
	public static string getSlotSavePath(int pSlot)
	{
		return SaveManager.generateMainPath("saves") + SaveManager.folderPath("save" + pSlot.ToString());
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000774CA File Offset: 0x000756CA
	public static string generateMainPath(string pFolder)
	{
		return SaveManager.folderPath(SaveManager.persistentDataPath) + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x000774E1 File Offset: 0x000756E1
	public static string generateAutosavesPath(string pFolder = "")
	{
		return SaveManager.generateMainPath("autosaves") + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x000774F8 File Offset: 0x000756F8
	public static string generateWorkshopPath(string pFolder = "")
	{
		return SaveManager.generateMainPath("workshop_upload") + SaveManager.folderPath(pFolder);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0007750F File Offset: 0x0007570F
	public static string generatePngPreviewPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "preview.png";
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00077521 File Offset: 0x00075721
	public static string generatePngSmallPreviewPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "preview_small.png";
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00077533 File Offset: 0x00075733
	public static string generateMetaPath(string pFolder)
	{
		return SaveManager.folderPath(pFolder) + "map.meta";
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00077545 File Offset: 0x00075745
	public static string getSlotPathWbox(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.wbox";
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00077557 File Offset: 0x00075757
	public static string getMetaSlotPath(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.meta";
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00077569 File Offset: 0x00075769
	public static string getOldSlotPath(int pSlot)
	{
		return SaveManager.getSlotSavePath(pSlot) + "map.json";
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0007757B File Offset: 0x0007577B
	public static string getPngSlotPath(int pSlot = -1)
	{
		return SaveManager.getSlotSavePath(pSlot) + "preview.png";
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0007758D File Offset: 0x0007578D
	public static void setCurrentSlot(int pSlotID)
	{
		SaveManager.currentSlot = pSlotID;
		SaveManager.currentSavePath = SaveManager.generateMainPath("saves") + SaveManager.folderPath("save" + pSlotID.ToString());
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x000775BF File Offset: 0x000757BF
	public static void setCurrentPath(string pPath)
	{
		SaveManager.currentSavePath = SaveManager.folderPath(pPath);
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x000775CC File Offset: 0x000757CC
	public static string getCurrentPreviewPath()
	{
		return SaveManager.currentSavePath + "preview.png";
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x000775DD File Offset: 0x000757DD
	public static bool currentSlotExists()
	{
		return Directory.Exists(SaveManager.currentSavePath);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x000775EE File Offset: 0x000757EE
	public static bool currentPreviewExists()
	{
		return File.Exists(SaveManager.currentSavePath + "preview.png");
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00077604 File Offset: 0x00075804
	public static bool currentMetaExists()
	{
		return File.Exists(SaveManager.currentSavePath + "map.meta");
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0007761A File Offset: 0x0007581A
	internal void loadWorld()
	{
		if (SaveManager.currentWorkshopMapData != null)
		{
			this.loadWorld(SaveManager.currentWorkshopMapData.main_path, true);
			SaveManager.currentWorkshopMapData = null;
			return;
		}
		this.loadWorld(SaveManager.currentSavePath, false);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00077648 File Offset: 0x00075848
	internal void loadWorld(string pPath, bool pLoadWorkshop = false)
	{
		try
		{
			SavedMap mapFromPath = SaveManager.getMapFromPath(pPath, pLoadWorkshop);
			mapFromPath.worldLaws.check();
			this.loadData(mapFromPath);
		}
		catch (Exception message)
		{
			Debug.Log("Error during loading of slot " + pPath);
			try
			{
				MapMetaData metaFor = SaveManager.getMetaFor(pPath);
				if (metaFor != null)
				{
					Debug.Log(JsonUtility.ToJson(metaFor));
				}
				else
				{
					Debug.Log("No meta data");
				}
			}
			catch (Exception)
			{
				Debug.Log("Failed to load meta data");
			}
			Debug.LogError(message);
			ScrollWindow.showWindow("error_happened");
		}
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x000776E0 File Offset: 0x000758E0
	public static void loadMapFromResources(string pPath)
	{
		SavedMap savedMap = JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress((Resources.Load(pPath) as TextAsset).bytes));
		savedMap.worldLaws.check();
		MapBox.instance.saveManager.loadData(savedMap);
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00077724 File Offset: 0x00075924
	public static void loadMapFromBytes(byte[] mapData)
	{
		SavedMap savedMap = JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(mapData));
		savedMap.worldLaws.check();
		MapBox.instance.saveManager.loadData(savedMap);
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00077758 File Offset: 0x00075958
	public static SavedMap getMapFromPath(string pMainPath, bool pLoadWorkshop = false)
	{
		if (pLoadWorkshop)
		{
			return JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(File.ReadAllBytes(SaveManager.folderPath(SaveManager.currentWorkshopMapData.main_path) + "map.wbox")));
		}
		pMainPath = SaveManager.folderPath(pMainPath);
		string text = pMainPath + "map.wbox";
		if (File.Exists(text))
		{
			return JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(File.ReadAllBytes(text)));
		}
		text = pMainPath + "map.wbax";
		if (File.Exists(text))
		{
			return JsonConvert.DeserializeObject<SavedMap>(File.ReadAllText(text));
		}
		text = pMainPath + "map.json";
		if (File.Exists(text))
		{
			return JsonConvert.DeserializeObject<SavedMap>(Zip.Decompress(File.ReadAllText(text)));
		}
		return null;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00077804 File Offset: 0x00075A04
	public void startLoadSlot()
	{
		this.world.transitionScreen.startTransition(new LoadingScreen.TransitionAction(this.loadWorld));
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00077824 File Offset: 0x00075A24
	private void loadTiles(string pString)
	{
		string[] array = pString.Split(new char[]
		{
			','
		});
		int zone_AMOUNT_X = Config.ZONE_AMOUNT_X;
		int zone_AMOUNT_Y = Config.ZONE_AMOUNT_Y;
		if (this.data.saveVersion < 7)
		{
			string[] array2 = new string[this.world.tilesList.Count];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (num2 >= 50 * zone_AMOUNT_X)
				{
					num2 = 0;
					num3++;
					num += 14 * zone_AMOUNT_X;
				}
				int num4 = i + num;
				if (num4 <= array2.Length)
				{
					array2[num4] = array[i];
					num2++;
				}
			}
			array = array2;
		}
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.data.tile_id >= array.Length || array[worldTile.data.tile_id] == null)
			{
				worldTile.setTileType("deep_ocean");
			}
			else
			{
				string[] array3 = array[worldTile.data.tile_id].Split(new char[]
				{
					':'
				});
				string text;
				if (array3.Length != 2)
				{
					text = null;
				}
				else
				{
					text = array3[1];
				}
				this._tile_id_main = array3[0];
				this._tile_id_top = string.Empty;
				this.convertOldTilesToNewOnes();
				worldTile.setTileType(this._tile_id_main);
				if (!string.IsNullOrEmpty(this._tile_id_top))
				{
					worldTile.setTopTileType(AssetManager.topTiles.get(this._tile_id_top));
				}
				worldTile.Height = worldTile.Type.heightMin;
				if (worldTile.Type.lava)
				{
					this.world.lavaLayer.loadLavaTile(worldTile);
				}
				if (this._tile_id_main == "grey_goo")
				{
					this.world.greyGooLayer.add(worldTile);
				}
				if (text != null)
				{
					if (text.Contains("fire"))
					{
						worldTile.data.fire = true;
					}
					if (text.Contains("conv0"))
					{
						worldTile.data.conwayType = ConwayType.Eater;
						this.world.conwayLayer.add(worldTile, "conway");
					}
					if (text.Contains("conv1"))
					{
						worldTile.data.conwayType = ConwayType.Creator;
						this.world.conwayLayer.add(worldTile, "conwayInverse");
					}
				}
			}
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00077AA4 File Offset: 0x00075CA4
	private void convertOldTilesToNewOnes()
	{
		if (this._tile_id_main.Contains("road"))
		{
			this._tile_id_main = "soil_low";
			this._tile_id_top = "road";
		}
		string tile_id_main = this._tile_id_main;
		if (tile_id_main != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(tile_id_main);
			if (num <= 3134936370U)
			{
				if (num <= 1332360248U)
				{
					if (num <= 624473654U)
					{
						if (num != 164218702U)
						{
							if (num != 174734082U)
							{
								if (num != 624473654U)
								{
									goto IL_52B;
								}
								if (!(tile_id_main == "hills_frozen"))
								{
									goto IL_52B;
								}
								this._tile_id_main = "mountains";
								this._tile_id_top = "snow_hills";
								return;
							}
							else
							{
								if (!(tile_id_main == "soil"))
								{
									goto IL_52B;
								}
								this._tile_id_main = "soil_low";
								return;
							}
						}
						else
						{
							if (!(tile_id_main == "deep_ocean"))
							{
								goto IL_52B;
							}
							return;
						}
					}
					else if (num != 1309421833U)
					{
						if (num != 1328097888U)
						{
							if (num != 1332360248U)
							{
								goto IL_52B;
							}
							if (!(tile_id_main == "soil_creep"))
							{
								goto IL_52B;
							}
							this._tile_id_main = "soil_low";
							this._tile_id_top = "tumor_low";
							return;
						}
						else
						{
							if (!(tile_id_main == "forest"))
							{
								goto IL_52B;
							}
							goto IL_44F;
						}
					}
					else
					{
						if (!(tile_id_main == "soil_frozen"))
						{
							goto IL_52B;
						}
						this._tile_id_main = "soil_low";
						this._tile_id_top = "snow_low";
						return;
					}
				}
				else if (num <= 2043630411U)
				{
					if (num != 1584873562U)
					{
						if (num != 1736598119U)
						{
							if (num != 2043630411U)
							{
								goto IL_52B;
							}
							if (!(tile_id_main == "shallow_waters_frozen"))
							{
								goto IL_52B;
							}
							this._tile_id_main = "shallow_waters";
							this._tile_id_top = "ice";
							return;
						}
						else
						{
							if (!(tile_id_main == "field"))
							{
								goto IL_52B;
							}
							this._tile_id_main = "soil_low";
							this._tile_id_top = "field";
							return;
						}
					}
					else
					{
						if (!(tile_id_main == "forest_soil"))
						{
							goto IL_52B;
						}
						this._tile_id_main = "soil_high";
						return;
					}
				}
				else if (num <= 2795768366U)
				{
					if (num != 2740153745U)
					{
						if (num != 2795768366U)
						{
							goto IL_52B;
						}
						if (!(tile_id_main == "mountains_frozen"))
						{
							goto IL_52B;
						}
						this._tile_id_main = "mountains";
						this._tile_id_top = "snow_block";
						return;
					}
					else
					{
						if (!(tile_id_main == "forest_soil_frozen"))
						{
							goto IL_52B;
						}
						this._tile_id_main = "soil_high";
						this._tile_id_top = "snow_high";
						return;
					}
				}
				else if (num != 2993663101U)
				{
					if (num != 3134936370U)
					{
						goto IL_52B;
					}
					if (!(tile_id_main == "close_ocean"))
					{
						goto IL_52B;
					}
					return;
				}
				else if (!(tile_id_main == "grass"))
				{
					goto IL_52B;
				}
			}
			else if (num <= 3365615729U)
			{
				if (num <= 3315282872U)
				{
					if (num != 3185372622U)
					{
						if (num != 3189014883U)
						{
							if (num != 3315282872U)
							{
								goto IL_52B;
							}
							if (!(tile_id_main == "lava3"))
							{
								goto IL_52B;
							}
							return;
						}
						else
						{
							if (!(tile_id_main == "sand"))
							{
								goto IL_52B;
							}
							this._tile_id_main = "sand";
							return;
						}
					}
					else
					{
						if (!(tile_id_main == "sand_frozen"))
						{
							goto IL_52B;
						}
						this._tile_id_main = "sand";
						this._tile_id_top = "snow_sand";
						return;
					}
				}
				else if (num != 3332060491U)
				{
					if (num != 3348838110U)
					{
						if (num != 3365615729U)
						{
							goto IL_52B;
						}
						if (!(tile_id_main == "lava0"))
						{
							goto IL_52B;
						}
						return;
					}
					else
					{
						if (!(tile_id_main == "lava1"))
						{
							goto IL_52B;
						}
						return;
					}
				}
				else
				{
					if (!(tile_id_main == "lava2"))
					{
						goto IL_52B;
					}
					return;
				}
			}
			else if (num <= 3635428898U)
			{
				if (num != 3517725693U)
				{
					if (num != 3632974288U)
					{
						if (num != 3635428898U)
						{
							goto IL_52B;
						}
						if (!(tile_id_main == "grass_flowers"))
						{
							goto IL_52B;
						}
					}
					else
					{
						if (!(tile_id_main == "shallow_waters"))
						{
							goto IL_52B;
						}
						return;
					}
				}
				else
				{
					if (!(tile_id_main == "sand_creep"))
					{
						goto IL_52B;
					}
					this._tile_id_main = "sand";
					this._tile_id_top = "tumor_low";
					return;
				}
			}
			else if (num <= 4171135248U)
			{
				if (num != 4022750299U)
				{
					if (num != 4171135248U)
					{
						goto IL_52B;
					}
					if (!(tile_id_main == "fuse"))
					{
						goto IL_52B;
					}
					this._tile_id_main = "soil_low";
					this._tile_id_top = "fuse";
					return;
				}
				else
				{
					if (!(tile_id_main == "hills"))
					{
						goto IL_52B;
					}
					return;
				}
			}
			else if (num != 4227381031U)
			{
				if (num != 4243777795U)
				{
					goto IL_52B;
				}
				if (!(tile_id_main == "mountains"))
				{
					goto IL_52B;
				}
				return;
			}
			else
			{
				if (!(tile_id_main == "forest_flowers"))
				{
					goto IL_52B;
				}
				goto IL_44F;
			}
			this._tile_id_main = "soil_low";
			this._tile_id_top = "grass_low";
			return;
			IL_44F:
			this._tile_id_main = "soil_high";
			this._tile_id_top = "grass_high";
			return;
		}
		IL_52B:
		this._tile_id_main = "soil_low";
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00077FE8 File Offset: 0x000761E8
	private void loadTileArray(SavedMap pData)
	{
		if (pData.tileAmounts.Length == 0)
		{
			return;
		}
		int num = pData.width * pData.height * 64 * 64;
		int num2 = 0;
		for (int i = 0; i < pData.tileArray.Length; i++)
		{
			for (int j = 0; j < pData.tileArray[i].Length; j++)
			{
				this._tile_id_top = string.Empty;
				this._tile_id_main = (pData.tileMap[pData.tileArray[i][j]] ?? "deep_ocean");
				if (this._tile_id_main.Contains(":"))
				{
					string[] array = this._tile_id_main.Split(new char[]
					{
						':'
					});
					this._tile_id_main = array[0];
					this._tile_id_top = array[1];
				}
				if (pData.saveVersion < 9)
				{
					this.convertOldTilesToNewOnes();
				}
				for (int k = 0; k < pData.tileAmounts[i][j]; k++)
				{
					WorldTile worldTile = this.world.tilesList[num2++];
					worldTile.setTileType(this._tile_id_main);
					if (!string.IsNullOrEmpty(this._tile_id_top))
					{
						worldTile.setTopTileType(AssetManager.topTiles.get(this._tile_id_top));
					}
					worldTile.Height = worldTile.Type.heightMin;
					if (worldTile.Type.lava)
					{
						this.world.lavaLayer.loadLavaTile(worldTile);
					}
					if (worldTile.Type.greyGoo)
					{
						this.world.greyGooLayer.add(worldTile);
					}
				}
			}
		}
		while (num2 + 1 < num)
		{
			this.world.tilesList[num2++].setTileType("deep_ocean");
		}
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x000781A8 File Offset: 0x000763A8
	private void loadConway(List<int> conv0, List<int> conv1)
	{
		if (conv0.Count == 0 && conv1.Count == 0)
		{
			return;
		}
		for (int i = 0; i < conv0.Count; i++)
		{
			this.world.tilesList[conv0[i]].data.conwayType = ConwayType.Eater;
			this.world.conwayLayer.add(this.world.tilesList[conv0[i]], "conway");
		}
		for (int j = 0; j < conv1.Count; j++)
		{
			this.world.tilesList[conv1[j]].data.conwayType = ConwayType.Creator;
			this.world.conwayLayer.add(this.world.tilesList[conv1[j]], "conwayInverse");
		}
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00078284 File Offset: 0x00076484
	private void loadFire(List<int> fireTiles)
	{
		if (fireTiles.Count == 0)
		{
			return;
		}
		for (int i = 0; i < fireTiles.Count; i++)
		{
			this.world.tilesList[fireTiles[i]].data.fire = true;
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000782D0 File Offset: 0x000764D0
	public void loadData(SavedMap pData)
	{
		this.data = pData;
		Config.worldLoading = true;
		this.world.clearWorld();
		this.world.setMapSize(pData.width, pData.height);
		this.world.mapStats = pData.mapStats;
		this.world.worldLaws = pData.worldLaws;
		if (pData.saveVersion > 0 && pData.saveVersion < 8)
		{
			this.loadTiles(pData.tileString);
		}
		else if (pData.saveVersion > 7)
		{
			this.loadTileArray(pData);
			this.loadFire(pData.fire);
			this.loadConway(pData.conwayEater, pData.conwayCreator);
		}
		else
		{
			foreach (WorldTileData worldTileData in pData.tiles)
			{
				WorldTile tileSimple = this.world.GetTileSimple(worldTileData.x, worldTileData.y);
				tileSimple.setTileType(worldTileData.type);
				tileSimple.data = worldTileData;
				if (worldTileData.conwayType != ConwayType.None)
				{
					this.world.conwayLayer.add(tileSimple, "conway");
				}
				if (worldTileData.type == "grey_goo")
				{
					this.world.greyGooLayer.add(tileSimple);
				}
			}
		}
		foreach (Culture pCulture in pData.cultures)
		{
			this.world.cultures.loadCulture(pCulture);
		}
		foreach (Kingdom kingdom in pData.kingdoms)
		{
			this.world.kingdoms.addKingdom(kingdom, true);
			if (string.IsNullOrEmpty(kingdom.raceID))
			{
				this.tryToFixMissingRace(pData);
			}
			kingdom.load1();
		}
		foreach (CityData cityData in pData.cities)
		{
			if (cityData.zones.Count != 0)
			{
				if (cityData.race == "ork")
				{
					cityData.race = "orc";
				}
				TileZone tileZone = this.world.zoneCalculator.getZone(cityData.zones[0].x, cityData.zones[0].y);
				if (pData.saveVersion < 7)
				{
					tileZone = this.findZoneViaBuilding(cityData.cityID, pData.buildings);
				}
				if (tileZone != null)
				{
					City city = this.world.buildNewCity(tileZone, cityData, null, true, null);
					if (!(city == null) && pData.saveVersion >= 7)
					{
						for (int i = 1; i < cityData.zones.Count; i++)
						{
							ZoneData zoneData = cityData.zones[i];
							TileZone zone = this.world.zoneCalculator.getZone(zoneData.x, zoneData.y);
							if (zone != null)
							{
								city.addZone(zone);
							}
						}
					}
				}
			}
		}
		foreach (ActorData actorData in pData.actors)
		{
			WorldTile tile = this.world.GetTile(actorData.x, actorData.y);
			if (actorData.status.alive)
			{
				if (actorData.inventory.resource == "food")
				{
					actorData.inventory.resource = "wheat";
				}
				if (actorData.status.gender == ActorGender.Unknown)
				{
					if (Toolbox.randomBool())
					{
						actorData.status.gender = ActorGender.Male;
					}
					else
					{
						actorData.status.gender = ActorGender.Female;
					}
				}
				if ((!(actorData.status.statsID == "livingPlants") && !(actorData.status.statsID == "livingHouse")) || !string.IsNullOrEmpty(actorData.status.special_graphics))
				{
					Actor actor = this.world.spawnAndLoadUnit(actorData.status.statsID, actorData, tile);
					if (!(actor == null) && pData.saveVersion < 6)
					{
						foreach (string pTrait in actor.stats.traits)
						{
							actor.addTrait(pTrait);
						}
					}
				}
			}
		}
		this.world.units.checkAddRemove();
		if (pData.saveVersion < 7)
		{
			foreach (BuildingData buildingData in pData.buildings)
			{
				City cityByID = this.world.getCityByID(buildingData.cityID);
				if (!(cityByID == null))
				{
					WorldTile tile2 = this.world.GetTile(buildingData.mainX, buildingData.mainY);
					cityByID.addZone(tile2.zone);
				}
			}
		}
		foreach (BuildingData buildingData2 in pData.buildings)
		{
			if (buildingData2.templateID.Contains("ork"))
			{
				buildingData2.templateID = buildingData2.templateID.Replace("ork", "orc");
			}
			if (AssetManager.buildings.get(buildingData2.templateID) != null)
			{
				this.world.loadBuilding(buildingData2) == null;
			}
		}
		this.world.buildings.checkAddRemove();
		foreach (Actor actor2 in this.world.units)
		{
			if (!string.IsNullOrEmpty(actor2.data.homeBuildingID))
			{
				actor2.homeBuilding = this.world.getBuildingByID(actor2.data.homeBuildingID);
				if (!(actor2.homeBuilding == null) && !actor2.homeBuilding.isNonUsable())
				{
					if (actor2.stats.isBoat)
					{
						Building buildingByID = this.world.getBuildingByID(actor2.data.homeBuildingID);
						if (buildingByID != null)
						{
							buildingByID.GetComponent<Docks>().addBoatToDock(actor2);
						}
					}
					if (actor2.homeBuilding.GetComponent<UnitSpawner>() != null)
					{
						actor2.homeBuilding.GetComponent<UnitSpawner>().setUnitFromHere(actor2);
					}
				}
			}
		}
		this.world.buildings.checkAddRemove();
		foreach (Kingdom kingdom2 in this.world.kingdoms.list_civs)
		{
			kingdom2.load2();
		}
		foreach (City city2 in this.world.citiesList)
		{
			city2.loadLeader();
		}
		this.world.kingdoms.checkKingdoms();
		this.world.kingdoms.diplomacyManager.load(pData.relations);
		foreach (Actor actor3 in this.world.units)
		{
			if (!string.IsNullOrEmpty(actor3.data.transportID))
			{
				Actor actorByID = this.world.getActorByID(actor3.data.transportID);
				if (actorByID != null)
				{
					actor3.embarkInto(actorByID.GetComponent<Boat>());
					actor3.ai.setTask("sit_inside_boat", true, false);
					actor3.ai.update();
				}
			}
		}
		this.world.finishMakingWorld();
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00078C04 File Offset: 0x00076E04
	private bool tryToFixMissingRace(SavedMap pData)
	{
		bool result = false;
		foreach (Kingdom kingdom in pData.kingdoms)
		{
			if (string.IsNullOrEmpty(kingdom.raceID))
			{
				foreach (CityData cityData in pData.cities)
				{
					if (cityData.kingdomID == kingdom.id && !string.IsNullOrEmpty(cityData.race))
					{
						kingdom.raceID = cityData.race;
						result = true;
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00078CD4 File Offset: 0x00076ED4
	private TileZone findZoneViaBuilding(string pID, List<BuildingData> pList)
	{
		foreach (BuildingData buildingData in pList)
		{
			if (buildingData.cityID == pID)
			{
				return this.world.GetTile(buildingData.mainX, buildingData.mainY).zone;
			}
		}
		return null;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00078D4C File Offset: 0x00076F4C
	private void saveFix(ActorData pData)
	{
		MonoBehaviour.print(pData);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00078D54 File Offset: 0x00076F54
	private static string folderPath(string pFolder)
	{
		if (string.IsNullOrEmpty(pFolder))
		{
			return string.Empty;
		}
		string text = Path.DirectorySeparatorChar.ToString();
		string value = Path.AltDirectorySeparatorChar.ToString();
		if (!pFolder.EndsWith(text) && !pFolder.EndsWith(value))
		{
			pFolder += text;
		}
		return pFolder;
	}

	// Token: 0x04000E8D RID: 3725
	public static int currentSlot = 0;

	// Token: 0x04000E8E RID: 3726
	public static string currentSavePath = string.Empty;

	// Token: 0x04000E8F RID: 3727
	public static WorkshopMapData currentWorkshopMapData;

	// Token: 0x04000E90 RID: 3728
	private const string saveslot_base = "saves";

	// Token: 0x04000E91 RID: 3729
	private const string workshop_base = "workshop_upload";

	// Token: 0x04000E92 RID: 3730
	private const string autosaves_base = "autosaves";

	// Token: 0x04000E93 RID: 3731
	internal const string name_main_data_old = "map.json";

	// Token: 0x04000E94 RID: 3732
	internal const string name_main_data = "map.wbox";

	// Token: 0x04000E95 RID: 3733
	internal const string name_main_data_non_zip = "map.wbax";

	// Token: 0x04000E96 RID: 3734
	internal const string name_meta_data = "map.meta";

	// Token: 0x04000E97 RID: 3735
	public const string name_png_preview_main = "preview.png";

	// Token: 0x04000E98 RID: 3736
	public const string name_png_preview_small = "preview_small.png";

	// Token: 0x04000E99 RID: 3737
	private static string persistentDataPath;

	// Token: 0x04000E9A RID: 3738
	private MapBox world;

	// Token: 0x04000E9B RID: 3739
	private string _tile_id_main;

	// Token: 0x04000E9C RID: 3740
	private string _tile_id_top;

	// Token: 0x04000E9D RID: 3741
	private SavedMap data;
}
