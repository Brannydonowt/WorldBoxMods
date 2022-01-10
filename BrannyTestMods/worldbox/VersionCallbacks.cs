using System;
using Beebyte.Obfuscator;

// Token: 0x020001D8 RID: 472
[ObfuscateLiterals]
internal static class VersionCallbacks
{
	// Token: 0x06000AA9 RID: 2729 RVA: 0x0006AF14 File Offset: 0x00069114
	public static void init()
	{
		VersionCallbacks.versionCheck = VersionCheck._vsCheck;
		if (string.IsNullOrEmpty(VersionCallbacks.versionCheck))
		{
			return;
		}
		if (VersionCallbacks.versionCheck.Split(new char[]
		{
			'.'
		}).Length == 3)
		{
			return;
		}
		if (VersionCallbacks.versionCallbacks != null && VersionCallbacks.versionCallbacks.GetInvocationList().Length != 0)
		{
			return;
		}
		TestingCB.init();
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0006AF70 File Offset: 0x00069170
	internal static void updateVC(float pElapsed)
	{
		VersionCallbacks.timer -= pElapsed;
		if (VersionCallbacks.timer > 0f)
		{
			return;
		}
		VersionCallbacks.timer = 0f;
		try
		{
			VersionCallbacks.init();
			if (!string.IsNullOrEmpty(VersionCallbacks.versionCheck))
			{
				Action<string> action = VersionCallbacks.versionCallbacks;
				if (action != null)
				{
					action(VersionCallbacks.versionCheck);
				}
			}
			if (VersionCallbacks.versionCheck.Split(new char[]
			{
				'.'
			}).Length != 3)
			{
				VersionCallbacks.timer = Toolbox.randomFloat(300f, 600f);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000D36 RID: 3382
	internal static Action<string> versionCallbacks;

	// Token: 0x04000D37 RID: 3383
	internal static float timer = 0f;

	// Token: 0x04000D38 RID: 3384
	internal static string versionCheck = string.Empty;
}
