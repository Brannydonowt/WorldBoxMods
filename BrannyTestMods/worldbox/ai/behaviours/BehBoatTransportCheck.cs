using System;

namespace ai.behaviours
{
	// Token: 0x02000337 RID: 823
	public class BehBoatTransportCheck : BehBoat
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x0009FAE8 File Offset: 0x0009DCE8
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			Boat boat = base.getBoat(pActor);
			if (boat.unitsInside.Count <= 0)
			{
				return base.forceTask(pActor, "boat_transport_check_taxi", true);
			}
			WorldTile worldTile = null;
			if (boat.unitsInside.Count > 5)
			{
				bool flag;
				if (pActor == null)
				{
					flag = (null != null);
				}
				else
				{
					City city = pActor.city;
					flag = (((city != null) ? city.attackZone : null) != null);
				}
				if (flag)
				{
					worldTile = pActor.city.attackZone.centerTile;
				}
			}
			if (worldTile == null)
			{
				WorldTile worldTile2;
				if (pActor == null)
				{
					worldTile2 = null;
				}
				else
				{
					City city2 = pActor.city;
					worldTile2 = ((city2 != null) ? city2.getTile() : null);
				}
				worldTile = worldTile2;
			}
			if (worldTile != null)
			{
				boat.taxiTarget = worldTile;
				pActor.beh_tile_target = worldTile;
				return base.forceTask(pActor, "boat_transport_go_unload", false);
			}
			return BehResult.Stop;
		}
	}
}
