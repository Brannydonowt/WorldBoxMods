using System;

namespace ai.behaviours
{
	// Token: 0x020003A1 RID: 929
	public class CityBehCheckSettleTarget : BehaviourActionCity
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x000A860C File Offset: 0x000A680C
		public override BehResult execute(City pCity)
		{
			if (pCity.status.population < 30)
			{
				pCity.settleTarget = null;
				return BehResult.Continue;
			}
			if (pCity.settlerTargetTimer > 0f)
			{
				return BehResult.Continue;
			}
			pCity.settleTarget = null;
			WorldTile tile = pCity.getTile();
			if (tile == null)
			{
				return BehResult.Continue;
			}
			TileZone tileZone = null;
			float num = 0f;
			if (tileZone == null)
			{
				pCity.kingdom.cities.Shuffle<City>();
				foreach (City city in pCity.kingdom.cities)
				{
					if (!(city == pCity) && city.status.population < 30)
					{
						WorldTile tile2 = city.getTile();
						tileZone = ((tile2 != null) ? tile2.zone : null);
						break;
					}
				}
			}
			if (tileZone == null && BehaviourActionBase<City>.world.cityPlaceFinder.zones.Count > 0)
			{
				foreach (TileZone tileZone2 in BehaviourActionBase<City>.world.cityPlaceFinder.zones)
				{
					if (tileZone2.centerTile.isSameIsland(tile))
					{
						float num2 = Toolbox.DistTile(tileZone2.centerTile, tile);
						if (tileZone == null || num2 < num)
						{
							num = num2;
							tileZone = tileZone2;
						}
					}
				}
				if (tileZone == null)
				{
					foreach (TileZone tileZone3 in BehaviourActionBase<City>.world.cityPlaceFinder.zones)
					{
						float num2 = Toolbox.DistTile(tileZone3.centerTile, tile);
						if (tileZone == null || num2 < num)
						{
							num = num2;
							tileZone = tileZone3;
						}
					}
				}
			}
			if (tileZone == null)
			{
				return BehResult.Continue;
			}
			pCity.settlerTargetTimer = Toolbox.randomFloat(100f, 400f);
			pCity.settleTarget = tileZone;
			return BehResult.Continue;
		}
	}
}
