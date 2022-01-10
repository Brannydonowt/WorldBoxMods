using System;

namespace ai.behaviours
{
	// Token: 0x0200038C RID: 908
	public class BehSpecialRat : BehaviourActionActor
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x000A33A8 File Offset: 0x000A15A8
		public override BehResult execute(Actor pActor)
		{
			if (pActor.haveTrait("rat") && Toolbox.randomChance(0.7f))
			{
				WorldTile worldTile = this.findNearbyRatKingTile(pActor);
				if (worldTile != null)
				{
					pActor.beh_tile_target = worldTile;
					return BehResult.Continue;
				}
			}
			MapRegion mapRegion = pActor.currentTile.region;
			if (mapRegion.neighbours.Count > 0 && Toolbox.randomBool())
			{
				mapRegion = mapRegion.neighbours.GetRandom<MapRegion>();
			}
			if (mapRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = mapRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000A3434 File Offset: 0x000A1634
		internal WorldTile findNearbyRatKingTile(Actor pActor)
		{
			if (BehaviourActionBase<Actor>.world.kingdoms.getKingdomByID("ratKings").units.Count == 0)
			{
				return null;
			}
			Actor actor = this.findRatKing(pActor);
			if (actor == null)
			{
				return null;
			}
			WorldTile random = Toolbox.getRandom<WorldTile>(actor.currentTile.neighboursAll);
			if (random == null)
			{
				return null;
			}
			return random;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000A3490 File Offset: 0x000A1690
		internal Actor findRatKing(Actor pThisActor)
		{
			BehaviourActionBase<Actor>.world.getObjectsInChunks(pThisActor.currentTile, 10, MapObjectType.Actor);
			Actor result = null;
			foreach (BaseSimObject baseSimObject in BehaviourActionBase<Actor>.world.temp_map_objects)
			{
				Actor actor = (Actor)baseSimObject;
				if (!(actor == pThisActor) && actor.haveTrait("ratKing"))
				{
					result = actor;
					break;
				}
			}
			return result;
		}
	}
}
