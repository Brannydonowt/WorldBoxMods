using System;

// Token: 0x020000AB RID: 171
public class WorldLayer : MapLayer
{
	// Token: 0x06000373 RID: 883 RVA: 0x0003726F File Offset: 0x0003546F
	public override void update(float pElapsed)
	{
		this.UpdateDirty(pElapsed);
	}
}
