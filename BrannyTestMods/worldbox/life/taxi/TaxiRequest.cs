using System;
using System.Collections.Generic;

namespace life.taxi
{
	// Token: 0x020003BE RID: 958
	public class TaxiRequest
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x000AACE6 File Offset: 0x000A8EE6
		public void cancel()
		{
			this.setState(TaxiRequestState.Finished);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000AACEF File Offset: 0x000A8EEF
		public void finish()
		{
			this.setState(TaxiRequestState.Finished);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000AACF8 File Offset: 0x000A8EF8
		private void checkList()
		{
			int num = 0;
			while (this.actors.Count > num)
			{
				bool flag = false;
				Actor actor = this.actors[num];
				if (!actor.data.alive)
				{
					flag = true;
				}
				else if (!actor.currentTile.isSameIsland(this.requestTile))
				{
					flag = true;
				}
				else if (actor.currentTile.isSameIsland(this.target))
				{
					flag = true;
				}
				if (flag)
				{
					this.actors.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000AAD7C File Offset: 0x000A8F7C
		public bool isStillLegit()
		{
			if (!this.isState(TaxiRequestState.Pending) && (this.taxi == null || !this.taxi.actor.data.alive))
			{
				return false;
			}
			this.checkList();
			return this.actors.Count != 0 && !this.isState(TaxiRequestState.Finished);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000AADDA File Offset: 0x000A8FDA
		public void assign(Boat pTaxi)
		{
			this.taxi = pTaxi;
			this.setState(TaxiRequestState.Assigned);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000AADEA File Offset: 0x000A8FEA
		public void setState(TaxiRequestState pState)
		{
			this.state = pState;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000AADF4 File Offset: 0x000A8FF4
		public void cancelForLatePassengers()
		{
			int i = 0;
			while (i < this.actors.Count)
			{
				if (this.actors[i].insideBoat == this.taxi)
				{
					i++;
				}
				else
				{
					this.actors.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000AAE44 File Offset: 0x000A9044
		public bool everyoneEmbarked()
		{
			this.checkList();
			foreach (Actor actor in this.actors)
			{
				if (!this.taxi.unitsInside.Contains(actor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x000AAEB0 File Offset: 0x000A90B0
		public bool isState(TaxiRequestState pState)
		{
			return this.state == pState;
		}

		// Token: 0x04001585 RID: 5509
		public WorldTile target;

		// Token: 0x04001586 RID: 5510
		public WorldTile requestTile;

		// Token: 0x04001587 RID: 5511
		public List<Actor> actors = new List<Actor>();

		// Token: 0x04001588 RID: 5512
		public Boat taxi;

		// Token: 0x04001589 RID: 5513
		public TaxiRequestState state;

		// Token: 0x0400158A RID: 5514
		public Kingdom kingdom;
	}
}
