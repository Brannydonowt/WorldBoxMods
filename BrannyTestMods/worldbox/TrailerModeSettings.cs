using System;
using System.IO;
using UnityEngine;

// Token: 0x020001D5 RID: 469
[Serializable]
public class TrailerModeSettings
{
	// Token: 0x06000AA3 RID: 2723 RVA: 0x0006AD6C File Offset: 0x00068F6C
	public static void startEvent()
	{
		string text = Application.persistentDataPath + "/trailer_settings";
		TrailerModeSettings trailerModeSettings;
		if (!File.Exists(text))
		{
			trailerModeSettings = new TrailerModeSettings();
			string text2 = JsonUtility.ToJson(trailerModeSettings);
			text2 = text2.Replace(",", ",\n");
			text2 = text2.Replace("{", "{\n");
			text2 = text2.Replace("}", "\n}");
			File.WriteAllText(text, text2);
		}
		else
		{
			trailerModeSettings = JsonUtility.FromJson<TrailerModeSettings>(File.ReadAllText(text));
		}
		trailerModeSettings.applyTrailerSettings();
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0006ADEC File Offset: 0x00068FEC
	public void applyTrailerSettings()
	{
		if (this.superOrcs)
		{
			AssetManager.unitStats.get("unit_orc").baseStats.damage = 10000;
		}
		else
		{
			AssetManager.unitStats.get("unit_orc").baseStats.damage = 18;
		}
		DebugConfig.setOption(DebugOption.FastSpawn, this.fastSpawn);
		DebugConfig.setOption(DebugOption.SonicSpeed, this.sonicSpeed);
		MapBox.instance.camera.GetComponent<MoveCamera>().cameraMoveSpeed = this.cameraMoveSpeed;
		MapBox.instance.camera.GetComponent<MoveCamera>().cameraMoveMax = this.cameraMoveMax;
		MapBox.instance.camera.GetComponent<MoveCamera>().cameraZoomSpeed = this.cameraZoomSpeed;
		Globals.TRAILER_MODE_USE_RESOURCES = this.cityUseResources;
	}

	// Token: 0x04000D2D RID: 3373
	public bool cityUseResources = true;

	// Token: 0x04000D2E RID: 3374
	public bool sonicSpeed = true;

	// Token: 0x04000D2F RID: 3375
	public bool fastSpawn = true;

	// Token: 0x04000D30 RID: 3376
	public float cameraMoveSpeed = 0.001f;

	// Token: 0x04000D31 RID: 3377
	public float cameraMoveMax = 0.02f;

	// Token: 0x04000D32 RID: 3378
	public float cameraZoomSpeed = 3.8f;

	// Token: 0x04000D33 RID: 3379
	public bool superOrcs = true;
}
