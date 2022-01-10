using System;

// Token: 0x0200004D RID: 77
public static class TileActionLibrary
{
	// Token: 0x060001C7 RID: 455 RVA: 0x00023A22 File Offset: 0x00021C22
	public static bool damage(WorldTile pTile, ActorBase pActor)
	{
		throw new NotImplementedException("damage logic per tile not implemented yet");
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00023A2E File Offset: 0x00021C2E
	public static bool landmine(WorldTile pTile, ActorBase pActor)
	{
		MapBox.instance.explosionLayer.explodeBomb(pTile, false);
		return true;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00023A42 File Offset: 0x00021C42
	public static bool setUnitOnFire(WorldTile pTile, ActorBase pActor)
	{
		if (pActor.haveTrait("burning_feet"))
		{
			return false;
		}
		pActor.addStatusEffect("burning", -1f);
		pTile.setFire(true);
		return true;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00023A6C File Offset: 0x00021C6C
	public static bool giveTumorTrait(WorldTile pTile, ActorBase pActor)
	{
		if (pActor.stats.immune_to_tumor)
		{
			return false;
		}
		if (!pActor.stats.canTurnIntoTumorMonster)
		{
			return false;
		}
		pActor.addTrait("tumorInfection");
		return true;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00023A99 File Offset: 0x00021C99
	public static bool giveSlownessStatus(WorldTile pTile, ActorBase pActor)
	{
		if (pActor.stats.immune_to_slowness)
		{
			return false;
		}
		pActor.addStatusEffect("slowness", -1f);
		return true;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00023ABB File Offset: 0x00021CBB
	public static bool giveMadnessTrait(WorldTile pTile, ActorBase pActor)
	{
		if (pActor.stats.immune_to_tumor)
		{
			return false;
		}
		if (!pActor.stats.canTurnIntoTumorMonster)
		{
			return false;
		}
		pActor.addTrait("madness");
		return true;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00023AE8 File Offset: 0x00021CE8
	public static BuildingAsset getGrowTypeRandomTrees(WorldTile pTile)
	{
		if (pTile.Type.grow_types_list_trees == null)
		{
			return null;
		}
		string random = pTile.Type.grow_types_list_trees.GetRandom<string>();
		return AssetManager.buildings.get(random);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00023B20 File Offset: 0x00021D20
	public static BuildingAsset getGrowTypeRandomPlants(WorldTile pTile)
	{
		if (pTile.Type.grow_types_list_plants == null)
		{
			return null;
		}
		string random = pTile.Type.grow_types_list_plants.GetRandom<string>();
		return AssetManager.buildings.get(random);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00023B58 File Offset: 0x00021D58
	public static BuildingAsset getGrowTypeSand(WorldTile pTile)
	{
		if (pTile.zone.tilesWithLiquid > 0)
		{
			if (Toolbox.randomChance(0.7f))
			{
				return null;
			}
			return AssetManager.buildings.get("palm");
		}
		else
		{
			bool flag = false;
			for (int i = 0; i < pTile.zone.neighboursAll.Count; i++)
			{
				if (pTile.zone.neighboursAll[i].tilesWithLiquid > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return null;
			}
			if (Toolbox.randomChance(0.7f))
			{
				return null;
			}
			return AssetManager.buildings.get("cacti");
		}
	}
}
