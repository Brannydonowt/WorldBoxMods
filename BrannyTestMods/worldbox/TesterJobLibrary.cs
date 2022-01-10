using System;
using System.Collections.Generic;

// Token: 0x020001FC RID: 508
public class TesterJobLibrary : AssetLibrary<JobTesterAsset>
{
	// Token: 0x06000B64 RID: 2916 RVA: 0x0006E5BA File Offset: 0x0006C7BA
	public override void init()
	{
		base.init();
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
	public string getNextJob()
	{
		if (this.list.Count == 0)
		{
			return string.Empty;
		}
		if (this._last_job > this.list.Count - 1)
		{
			this._last_job = 0;
			this.list.Shuffle<JobTesterAsset>();
		}
		List<JobTesterAsset> list = this.list;
		int last_job = this._last_job;
		this._last_job = last_job + 1;
		return list[last_job].id;
	}

	// Token: 0x04000D9C RID: 3484
	private int _last_job;
}
