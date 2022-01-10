using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class ModLoader : MonoBehaviour
{
	// Token: 0x06000162 RID: 354 RVA: 0x00017837 File Offset: 0x00015A37
	public void Update()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (!Config.experimentalMode)
		{
			return;
		}
		if (!this.initialized)
		{
			this.initialized = true;
			this.Initialize();
			base.enabled = false;
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00017868 File Offset: 0x00015A68
	public void Initialize()
	{
		string text = Path.Combine(Application.streamingAssetsPath, "Mods");
		if (!Directory.Exists(text))
		{
			Debug.LogError("Can not find mod dlls - there is no 'Mods' folder");
			return;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		Random rng = new Random();
		IEnumerable<FileInfo> files = directoryInfo.GetFiles();
		Func<FileInfo, int> <>9__0;
		Func<FileInfo, int> keySelector;
		if ((keySelector = <>9__0) == null)
		{
			keySelector = (<>9__0 = ((FileInfo f) => rng.Next()));
		}
		foreach (FileInfo fileInfo in files.OrderBy(keySelector))
		{
			if (fileInfo.Name.ToLower().EndsWith(".dll"))
			{
				string text2 = Path.Combine(Application.streamingAssetsPath, "Mods", fileInfo.Name);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
				try
				{
					Debug.Log("[" + fileNameWithoutExtension + "] Loading " + fileInfo.Name);
					Assembly assembly = Assembly.LoadFile(text2);
					Debug.Log("[" + fileNameWithoutExtension + "] Assembly: " + assembly.ToString());
					Debug.Log("[" + fileNameWithoutExtension + "] classes inside the mod:");
					foreach (Type type in assembly.GetTypes())
					{
						string str = "[";
						string str2 = fileNameWithoutExtension;
						string str3 = "] ";
						Type type2 = type;
						Debug.Log(str + str2 + str3 + ((type2 != null) ? type2.ToString() : null));
					}
					Debug.Log(string.Concat(new string[]
					{
						"[",
						fileNameWithoutExtension,
						"] Attempting to load ",
						fileNameWithoutExtension,
						".WorldBoxMod"
					}));
					Type type3 = assembly.GetType(fileNameWithoutExtension + ".WorldBoxMod");
					if (type3 != null)
					{
						new GameObject(fileNameWithoutExtension)
						{
							transform = 
							{
								parent = base.transform
							}
						}.AddComponent(type3);
						ModLoader.modsLoaded.Add(fileNameWithoutExtension);
						Config.MODDED = true;
						Debug.Log("[" + fileNameWithoutExtension + "] Was added");
					}
					else
					{
						Debug.LogError(string.Concat(new string[]
						{
							"[",
							fileNameWithoutExtension,
							"] Missing className: ",
							fileNameWithoutExtension,
							".WorldBoxMod"
						}));
					}
				}
				catch (Exception ex)
				{
					Debug.Log("[" + fileNameWithoutExtension + "] Failed to load mod from path : ");
					Debug.Log("[" + fileNameWithoutExtension + "] " + text2);
					Debug.LogError(ex.Message);
				}
			}
		}
	}

	// Token: 0x0400014F RID: 335
	private bool initialized;

	// Token: 0x04000150 RID: 336
	private static List<string> modsLoaded = new List<string>();
}
