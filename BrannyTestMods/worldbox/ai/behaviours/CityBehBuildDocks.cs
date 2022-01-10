using System;

namespace ai.behaviours
{
	// Token: 0x0200039B RID: 923
	public class CityBehBuildDocks : BehaviourActionCity
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x000A77E8 File Offset: 0x000A59E8
		public override BehResult execute(City pCity)
		{
			WorldTile dockTile = CityBehBuild.getDockTile(pCity);
			if (dockTile == null)
			{
				return BehResult.Continue;
			}
			BehaviourActionBase<City>.world.flashEffects.flashPixel(dockTile, 10, ColorType.White);
			CityBehBuild.tryToBuild(pCity, "docks_" + pCity.race.id);
			return BehResult.Continue;
		}
	}
}
