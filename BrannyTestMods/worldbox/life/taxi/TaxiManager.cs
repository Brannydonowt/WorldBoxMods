using System;
using System.Collections.Generic;

namespace life.taxi
{
	// Token: 0x020003BB RID: 955
	public class TaxiManager
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x000AAA20 File Offset: 0x000A8C20
		public static void newRequest(Actor pActor, WorldTile pTileTarget)
		{
			TaxiRequest taxiRequest = null;
			foreach (TaxiRequest taxiRequest2 in TaxiManager.list)
			{
				if (taxiRequest2.target.isSameIsland(pTileTarget) && taxiRequest2.requestTile.isSameIsland(pActor.currentTile))
				{
					taxiRequest = taxiRequest2;
					break;
				}
			}
			if (taxiRequest != null)
			{
				if (!taxiRequest.actors.Contains(pActor))
				{
					taxiRequest.actors.Add(pActor);
				}
				return;
			}
			taxiRequest = new TaxiRequest();
			taxiRequest.kingdom = pActor.kingdom;
			taxiRequest.setState(TaxiRequestState.Pending);
			taxiRequest.actors.Add(pActor);
			taxiRequest.requestTile = pActor.currentTile;
			taxiRequest.target = pTileTarget;
			TaxiManager.list.Add(taxiRequest);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000AAAF4 File Offset: 0x000A8CF4
		public static void cancelRequest(TaxiRequest pRequest)
		{
			pRequest.cancel();
			TaxiManager.list.Remove(pRequest);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000AAB08 File Offset: 0x000A8D08
		public static TaxiRequest getRequestForActor(Actor pActor)
		{
			foreach (TaxiRequest taxiRequest in TaxiManager.list)
			{
				if (taxiRequest.actors.Contains(pActor))
				{
					return taxiRequest;
				}
			}
			return null;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000AAB68 File Offset: 0x000A8D68
		public static TaxiRequest getNewRequestForBoat(Actor pTaxi)
		{
			TaxiRequest taxiRequest = null;
			foreach (TaxiRequest taxiRequest2 in TaxiManager.list)
			{
				if (taxiRequest2.isState(TaxiRequestState.Assigned) && taxiRequest2.isStillLegit() && taxiRequest2.taxi.actor == pTaxi)
				{
					return taxiRequest2;
				}
				if (taxiRequest2.isState(TaxiRequestState.Pending) && taxiRequest2.isStillLegit() && taxiRequest2.kingdom == pTaxi.kingdom && (taxiRequest == null || taxiRequest.actors.Count < taxiRequest2.actors.Count))
				{
					taxiRequest = taxiRequest2;
				}
			}
			return taxiRequest;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000AAC1C File Offset: 0x000A8E1C
		public static void clear()
		{
			TaxiManager.list.Clear();
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000AAC28 File Offset: 0x000A8E28
		public static void finish(TaxiRequest pRequest)
		{
			pRequest.finish();
			TaxiManager.list.Remove(pRequest);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000AAC3C File Offset: 0x000A8E3C
		public static void cancelTaxiRequestForActor(Actor pActor)
		{
			TaxiRequest requestForActor = TaxiManager.getRequestForActor(pActor);
			if (requestForActor == null)
			{
				return;
			}
			TaxiManager.cancelRequest(requestForActor);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000AAC5C File Offset: 0x000A8E5C
		public static void update(float pElapsed)
		{
			if (TaxiManager.timer_check > 0f)
			{
				TaxiManager.timer_check -= pElapsed;
				return;
			}
			TaxiManager.timer_check = 5f;
			int num = 0;
			while (TaxiManager.list.Count > num)
			{
				TaxiRequest taxiRequest = TaxiManager.list[num];
				if (taxiRequest.isStillLegit())
				{
					num++;
				}
				else
				{
					taxiRequest.finish();
					TaxiManager.list.RemoveAt(num);
				}
			}
		}

		// Token: 0x04001579 RID: 5497
		public static List<TaxiRequest> list = new List<TaxiRequest>();

		// Token: 0x0400157A RID: 5498
		private static float timer_check = 0f;
	}
}
