using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class InitLibraries : MonoBehaviour
{
	// Token: 0x0600014C RID: 332 RVA: 0x000172E1 File Offset: 0x000154E1
	private void Awake()
	{
		this.initLibs();
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000172EC File Offset: 0x000154EC
	private void initLibs()
	{
		if (InitLibraries.initiated)
		{
			return;
		}
		InitLibraries.initiated = true;
		Config.gv = Application.version;
		Config.iname = Application.installerName;
		Config.gs = "";
		LogText.log("InitLibraries " + Config.gv, "initLibs", "st");
		Toolbox.init();
		DebugConfig.init();
		CityBuildOrder.init();
		BannerGenerator.init();
		NameGenerator.init();
		PlayerConfig.init();
		LocalizedTextManager.init();
		AssetManager.init();
		KingdomColors.init(null);
		ActorAnimationLoader.init();
		CityTaskList.init();
		GeneratorTool.Init();
		Brush.init();
		LogText.log("InitLibraries", "initLibs", "en");
	}

	// Token: 0x04000138 RID: 312
	public static bool initiated;
}
