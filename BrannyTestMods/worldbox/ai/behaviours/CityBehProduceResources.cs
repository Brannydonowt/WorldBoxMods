using System;

namespace ai.behaviours
{
	// Token: 0x020003A5 RID: 933
	public class CityBehProduceResources : BehaviourActionCity
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x000A8A4C File Offset: 0x000A6C4C
		public override BehResult execute(City pCity)
		{
			pCity.race.production.Shuffle<string>();
			foreach (string pNewResource in pCity.race.production)
			{
				this.workers = pCity.status.population / 10 + 1;
				this.tryToProduce(pNewResource, pCity);
			}
			return BehResult.Continue;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x000A8AA8 File Offset: 0x000A6CA8
		private bool tryToProduce(string pNewResource, City pCity)
		{
			ResourceAsset resourceAsset = AssetManager.resources.get(pNewResource);
			for (int i = 0; i < this.workers; i++)
			{
				if (pCity.data.storage.get(pNewResource) == resourceAsset.maximum)
				{
					return false;
				}
				foreach (string pRes in resourceAsset.ingredients)
				{
					if (pCity.data.storage.get(pRes) < resourceAsset.ingredientsAmount)
					{
						return false;
					}
				}
				foreach (string pRes2 in resourceAsset.ingredients)
				{
					pCity.data.storage.change(pRes2, -resourceAsset.ingredientsAmount);
				}
				pCity.data.storage.change(pNewResource, 1);
			}
			return true;
		}

		// Token: 0x04001569 RID: 5481
		private int workers;
	}
}
