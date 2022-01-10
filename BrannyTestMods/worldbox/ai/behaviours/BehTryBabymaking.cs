using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000392 RID: 914
	public class BehTryBabymaking : BehaviourActionActor
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x000A3A94 File Offset: 0x000A1C94
		public override BehResult execute(Actor pActor)
		{
			WorldTile currentTile = pActor.currentTile;
			bool flag;
			if (currentTile == null)
			{
				flag = (null != null);
			}
			else
			{
				MapRegion region = currentTile.region;
				flag = (((region != null) ? region.island : null) != null);
			}
			if (!flag)
			{
				return BehResult.Stop;
			}
			if (!this.isAvailableForReproduction(pActor))
			{
				return BehResult.Stop;
			}
			if (this.countUnitsInChunks(pActor.currentTile) >= pActor.stats.animal_baby_making_around_limit)
			{
				return BehResult.Stop;
			}
			if (!pActor.timerReproduction.isActive)
			{
				if (this.tryStartBabymaking(pActor))
				{
					return BehResult.RepeatStep;
				}
				pActor.timerReproduction.startTimer(20f);
			}
			return BehResult.Stop;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000A3B14 File Offset: 0x000A1D14
		private int countUnitsInChunks(WorldTile pTile)
		{
			this._units.Clear();
			Toolbox.fillListWithUnitsFromChunk(pTile.chunk, this._units);
			foreach (MapChunk pChunk in pTile.chunk.neighboursAll)
			{
				Toolbox.fillListWithUnitsFromChunk(pChunk, this._units);
			}
			return this._units.Count;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000A3B98 File Offset: 0x000A1D98
		internal bool tryStartBabymaking(Actor pActor)
		{
			if (pActor.race.units.Count > 30)
			{
				return false;
			}
			Toolbox.findSameRaceInChunkAround(pActor.currentTile.chunk, pActor.stats.race);
			if (Toolbox.temp_list_units.Count >= 4)
			{
				return false;
			}
			if (pActor.stats.layEggs && !pActor.isAffectedByLiquid())
			{
				this.makeEgg(pActor);
				return true;
			}
			Actor actor = this.findPartner(pActor);
			if (actor == null)
			{
				return false;
			}
			pActor.ai.setTask("babymaking", true, true);
			pActor.beh_actor_target = actor;
			pActor.beh_actor_target.a.makeWait(10f);
			return true;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000A3C48 File Offset: 0x000A1E48
		internal Actor findPartner(Actor pActor)
		{
			float num = 0f;
			Actor actor = null;
			foreach (Actor actor2 in pActor.race.units)
			{
				if (!(actor2 == pActor) && this.isAvailableForReproduction(actor2) && actor2.currentTile.isSameIsland(pActor.currentTile))
				{
					float num2 = Toolbox.DistTile(pActor.currentTile, actor2.currentTile);
					if (num2 <= 50f && (actor == null || num2 < num))
					{
						num = num2;
						actor = actor2;
					}
				}
			}
			return actor;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000A3CF8 File Offset: 0x000A1EF8
		public bool isAvailableForReproduction(Actor pActor)
		{
			return pActor.stats.procreate && pActor.data.hunger >= 60 && pActor.data.age > pActor.stats.procreate_age && pActor.data.alive && !pActor.timerReproduction.isActive;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000A3D60 File Offset: 0x000A1F60
		private void makeEgg(Actor pActor)
		{
			pActor.startBabymakingTimeout();
			pActor.data.children++;
			WorldTile worldTile = null;
			foreach (WorldTile worldTile2 in pActor.currentTile.neighbours)
			{
				if (worldTile2 != pActor.currentTile && worldTile2 != pActor.currentTile && !worldTile2.Type.liquid)
				{
					worldTile = worldTile2;
					break;
				}
			}
			if (worldTile == null)
			{
				worldTile = pActor.currentTile;
			}
			Actor actor = BehaviourActionBase<Actor>.world.createNewUnit(pActor.stats.eggStatsID, worldTile, null, 6f, null);
			if (pActor.stats.useSkinColors)
			{
				actor.data.skin_set = pActor.data.skin_set;
			}
		}

		// Token: 0x04001549 RID: 5449
		private List<Actor> _units = new List<Actor>();
	}
}
