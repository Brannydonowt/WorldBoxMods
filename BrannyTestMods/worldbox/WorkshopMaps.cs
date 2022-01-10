using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RSG;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x02000237 RID: 567
public static class WorkshopMaps
{
	// Token: 0x06000C85 RID: 3205 RVA: 0x0007A994 File Offset: 0x00078B94
	public static bool workshopAvailable()
	{
		return SteamSDK.steamInitialized != null && SteamSDK.steamInitialized.CurState == PromiseState.Resolved;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0007A9B0 File Offset: 0x00078BB0
	internal static Promise uploadMap()
	{
		Promise promise = new Promise();
		WorkshopMaps.uploadProgress = 0f;
		WorkshopMapData workshopMapData = WorkshopMapData.currentMapToWorkshop();
		SaveManager.currentWorkshopMapData = workshopMapData;
		MapMetaData meta_data_map = workshopMapData.meta_data_map;
		if (SaveManager.currentWorkshopMapData == null)
		{
			promise.Reject(new Exception("Missing world data"));
			return promise;
		}
		if (!MapSizePresset.isSizeValid(meta_data_map.width))
		{
			promise.Reject(new Exception("Not a valid world size!"));
			return promise;
		}
		if (meta_data_map.width != meta_data_map.height)
		{
			promise.Reject(new Exception("Not a square world!"));
			return promise;
		}
		MapMetaData meta_data_map2 = workshopMapData.meta_data_map;
		string name = meta_data_map2.mapStats.name;
		string description = meta_data_map2.mapStats.description;
		if (string.IsNullOrEmpty(name))
		{
			promise.Reject(new Exception("Give your world a name!"));
			return promise;
		}
		string main_path = workshopMapData.main_path;
		string preview_image_path = workshopMapData.preview_image_path;
		Editor editor = Editor.NewCommunityFile.WithTag("World");
		if (!string.IsNullOrWhiteSpace(name))
		{
			editor = editor.WithTitle(name);
		}
		if (!string.IsNullOrWhiteSpace(description))
		{
			editor = editor.WithDescription(description);
		}
		if (!string.IsNullOrWhiteSpace(preview_image_path))
		{
			editor = editor.WithPreviewFile(preview_image_path);
		}
		if (!string.IsNullOrWhiteSpace(main_path))
		{
			editor = editor.WithContent(main_path);
		}
		editor = editor.WithPublicVisibility();
		WorkshopMaps.uploadProgressTracker = new WorkshopUploadProgress();
		editor.SubmitAsync(WorkshopMaps.uploadProgressTracker).ContinueWith(delegate(Task<PublishResult> taskResult)
		{
			if (taskResult.Status != TaskStatus.RanToCompletion)
			{
				promise.Reject(taskResult.Exception.GetBaseException());
				return;
			}
			PublishResult result = taskResult.Result;
			if (!result.Success)
			{
				Debug.LogError("Error when uploading Workshop world");
			}
			if (result.NeedsWorkshopAgreement)
			{
				Debug.Log("w: Needs Workshop Agreement");
				WorkshopUploadingWorldWindow.needsWorkshopAgreement = true;
				WorkshopOpenSteamWorkshop.fileID = result.FileId.ToString();
			}
			if (result.Result != 1)
			{
				Debug.LogError(result.Result);
				promise.Reject(new Exception("Something went wrong: " + result.Result.ToString()));
				return;
			}
			WorkshopMaps.uploaded_file_id = result.FileId;
			promise.Resolve();
		}, TaskScheduler.FromCurrentSynchronizationContext());
		return promise;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0007AB54 File Offset: 0x00078D54
	internal static Task<List<Item>> listWorkshopMaps(bool pOrder = false, bool pByFriends = false)
	{
		WorkshopMaps.<listWorkshopMaps>d__6 <listWorkshopMaps>d__;
		<listWorkshopMaps>d__.pOrder = pOrder;
		<listWorkshopMaps>d__.pByFriends = pByFriends;
		<listWorkshopMaps>d__.<>t__builder = AsyncTaskMethodBuilder<List<Item>>.Create();
		<listWorkshopMaps>d__.<>1__state = -1;
		AsyncTaskMethodBuilder<List<Item>> <>t__builder = <listWorkshopMaps>d__.<>t__builder;
		<>t__builder.Start<WorkshopMaps.<listWorkshopMaps>d__6>(ref <listWorkshopMaps>d__);
		return <listWorkshopMaps>d__.<>t__builder.Task;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0007ABA4 File Offset: 0x00078DA4
	internal static bool filesPresent(Item pEntry)
	{
		if (!Directory.Exists(pEntry.Directory))
		{
			return false;
		}
		string[] files = Directory.GetFiles(pEntry.Directory);
		Debug.Log(string.Concat(new string[]
		{
			"w: ",
			pEntry.Directory,
			" with ",
			files.Length.ToString(),
			" Files"
		}));
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		foreach (string text in files)
		{
			if (text.Contains("map.wbox"))
			{
				flag = true;
			}
			else if (text.Contains("map.meta"))
			{
				flag4 = true;
			}
			else if (text.Contains("preview.png"))
			{
				flag2 = true;
			}
			else if (text.Contains("preview_small.png"))
			{
				flag3 = true;
			}
		}
		if (!flag)
		{
			Debug.Log("w: Missing Map");
		}
		if (!flag2)
		{
			Debug.Log("w: Missing Preview");
		}
		if (!flag3)
		{
			Debug.Log("w: Missing PreviewSmall");
		}
		if (!flag4)
		{
			Debug.Log("w: Missing Meta");
		}
		return flag4 && flag && flag2 && flag3;
	}

	// Token: 0x04000F54 RID: 3924
	internal static WorkshopUploadProgress uploadProgressTracker = new WorkshopUploadProgress();

	// Token: 0x04000F55 RID: 3925
	internal static float uploadProgress = 0f;

	// Token: 0x04000F56 RID: 3926
	public static PublishedFileId uploaded_file_id;

	// Token: 0x04000F57 RID: 3927
	internal static List<Item> foundMaps = new List<Item>();
}
