using System;

namespace ai.behaviours
{
	// Token: 0x0200034F RID: 847
	public class BehCityRemoveFire : BehCity
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x000A0871 File Offset: 0x0009EA71
		public override void create()
		{
			base.create();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000A0880 File Offset: 0x0009EA80
		public override BehResult execute(Actor pActor)
		{
			if (pActor.currentTile.zone.tilesOnFire > 0)
			{
				for (int i = 0; i < pActor.currentTile.zone.tiles.Count; i++)
				{
					WorldTile worldTile = pActor.currentTile.zone.tiles[i];
					if (worldTile.data.fire)
					{
						worldTile.stopFire(false);
					}
					if (worldTile.building != null)
					{
						worldTile.building.stopFire();
					}
				}
			}
			return BehResult.Continue;
		}
	}
}
