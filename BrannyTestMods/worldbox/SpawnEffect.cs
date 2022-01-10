using System;

// Token: 0x02000119 RID: 281
public class SpawnEffect : BaseEffect
{
	// Token: 0x06000639 RID: 1593 RVA: 0x00049801 File Offset: 0x00047A01
	internal override void create()
	{
		base.create();
		this._animation = base.GetComponent<SpriteAnimation>();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00049818 File Offset: 0x00047A18
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._eventUsed)
		{
			return;
		}
		if (this._animation.currentFrameIndex == 14)
		{
			this._eventUsed = true;
			if (this._event == "crabzilla")
			{
				GodPower godPower = AssetManager.powers.get("crabzilla");
				this.world.createNewUnit(godPower.actorStatsId, this._tile, "", godPower.actorSpawnHeight, null);
			}
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00049891 File Offset: 0x00047A91
	public void setEvent(string pEvent, WorldTile pTile)
	{
		this._tile = pTile;
		this._eventUsed = false;
		this._event = pEvent;
	}

	// Token: 0x04000815 RID: 2069
	private string _event;

	// Token: 0x04000816 RID: 2070
	private SpriteAnimation _animation;

	// Token: 0x04000817 RID: 2071
	private bool _eventUsed;

	// Token: 0x04000818 RID: 2072
	private WorldTile _tile;
}
