using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class BuildingAsset : Asset
{
	// Token: 0x060002CC RID: 716 RVA: 0x0002E448 File Offset: 0x0002C648
	public BuildingAsset()
	{
		this.baseStats.health = 100;
		this.baseStats.size = 2f;
	}

	// Token: 0x040003BE RID: 958
	public string kingdom = "";

	// Token: 0x040003BF RID: 959
	public BuildingFundament fundament;

	// Token: 0x040003C0 RID: 960
	public bool mod_use_streaming_assets;

	// Token: 0x040003C1 RID: 961
	public bool mod_streaming_assets_path;

	// Token: 0x040003C2 RID: 962
	public Vector2 mod_streaming_assets_pivot = new Vector2(0.5f, 0f);

	// Token: 0x040003C3 RID: 963
	public string sprite_path = string.Empty;

	// Token: 0x040003C4 RID: 964
	public bool grow_creep;

	// Token: 0x040003C5 RID: 965
	public CreepWorkerMovementType grow_creep_movement_type;

	// Token: 0x040003C6 RID: 966
	public string grow_creep_type = string.Empty;

	// Token: 0x040003C7 RID: 967
	public string grow_creep_type_low = string.Empty;

	// Token: 0x040003C8 RID: 968
	public string grow_creep_type_high = string.Empty;

	// Token: 0x040003C9 RID: 969
	public int grow_creep_steps_max;

	// Token: 0x040003CA RID: 970
	public float grow_creep_step_inteval;

	// Token: 0x040003CB RID: 971
	public int grow_creep_workers = 1;

	// Token: 0x040003CC RID: 972
	public bool grow_creep_direction_random_position;

	// Token: 0x040003CD RID: 973
	public bool grow_creep_random_new_direction;

	// Token: 0x040003CE RID: 974
	public bool grow_creep_flash;

	// Token: 0x040003CF RID: 975
	public bool grow_creep_redraw_tile;

	// Token: 0x040003D0 RID: 976
	public int grow_creep_steps_before_new_direction = 7;

	// Token: 0x040003D1 RID: 977
	public string ruins = "same_id";

	// Token: 0x040003D2 RID: 978
	public string destroyedSound = "";

	// Token: 0x040003D3 RID: 979
	public bool isRuin;

	// Token: 0x040003D4 RID: 980
	public int orderInLayer;

	// Token: 0x040003D5 RID: 981
	public BuildingType buildingType;

	// Token: 0x040003D6 RID: 982
	public string resource_id = "";

	// Token: 0x040003D7 RID: 983
	public int resources_given;

	// Token: 0x040003D8 RID: 984
	public bool canBeHarvested;

	// Token: 0x040003D9 RID: 985
	public int maxTreesInZone;

	// Token: 0x040003DA RID: 986
	public float vegetationRandomChance = 0.5f;

	// Token: 0x040003DB RID: 987
	public bool hasKingdomColor;

	// Token: 0x040003DC RID: 988
	public bool cityBuilding;

	// Token: 0x040003DD RID: 989
	public bool canBeAbandoned;

	// Token: 0x040003DE RID: 990
	public bool destroyOnLiquid;

	// Token: 0x040003DF RID: 991
	public bool canBeUpgraded;

	// Token: 0x040003E0 RID: 992
	public string upgradeTo = "";

	// Token: 0x040003E1 RID: 993
	public int upgradeLevel;

	// Token: 0x040003E2 RID: 994
	public string type = "";

	// Token: 0x040003E3 RID: 995
	public int priority;

	// Token: 0x040003E4 RID: 996
	public bool shadow = true;

	// Token: 0x040003E5 RID: 997
	public bool auto_remove_ruin;

	// Token: 0x040003E6 RID: 998
	public bool iceTower;

	// Token: 0x040003E7 RID: 999
	public bool spawnUnits;

	// Token: 0x040003E8 RID: 1000
	public bool beehive;

	// Token: 0x040003E9 RID: 1001
	public string spawnUnits_asset = "-";

	// Token: 0x040003EA RID: 1002
	public bool tower;

	// Token: 0x040003EB RID: 1003
	public string tower_projectile = string.Empty;

	// Token: 0x040003EC RID: 1004
	public float tower_projectile_offset;

	// Token: 0x040003ED RID: 1005
	public float tower_projectile_reload = 3f;

	// Token: 0x040003EE RID: 1006
	public int tower_projectile_amount = 1;

	// Token: 0x040003EF RID: 1007
	public bool ignoreOtherBuildingsForUpgrade;

	// Token: 0x040003F0 RID: 1008
	public bool randomFlip;

	// Token: 0x040003F1 RID: 1009
	public ConstructionCost cost;

	// Token: 0x040003F2 RID: 1010
	public BaseStats baseStats = new BaseStats();

	// Token: 0x040003F3 RID: 1011
	public bool ignoredByCities;

	// Token: 0x040003F4 RID: 1012
	public bool buildRoadTo;

	// Token: 0x040003F5 RID: 1013
	public bool canBeDamagedByTornado;

	// Token: 0x040003F6 RID: 1014
	public bool canBePlacedOnLiquid;

	// Token: 0x040003F7 RID: 1015
	public bool canBePlacedOnBlocks;

	// Token: 0x040003F8 RID: 1016
	public bool damagedByRain;

	// Token: 0x040003F9 RID: 1017
	public bool onlyBuildTiles;

	// Token: 0x040003FA RID: 1018
	public CityBuildingPlacement buildingPlacement;

	// Token: 0x040003FB RID: 1019
	public bool checkForCloseBuilding;

	// Token: 0x040003FC RID: 1020
	public bool ignoreBuildings;

	// Token: 0x040003FD RID: 1021
	public bool ignoreDemolish;

	// Token: 0x040003FE RID: 1022
	public bool burnable;

	// Token: 0x040003FF RID: 1023
	public bool affectedByLava;

	// Token: 0x04000400 RID: 1024
	public bool affectedByAcid;

	// Token: 0x04000401 RID: 1025
	public int housing;

	// Token: 0x04000402 RID: 1026
	public bool storage;

	// Token: 0x04000403 RID: 1027
	public bool canBeLivingHouse = true;

	// Token: 0x04000404 RID: 1028
	public bool canBeLivingPlant = true;

	// Token: 0x04000405 RID: 1029
	public int limit_per_zone;

	// Token: 0x04000406 RID: 1030
	public bool docks;

	// Token: 0x04000407 RID: 1031
	public List<string> canBePlacedOnlyOn;

	// Token: 0x04000408 RID: 1032
	public string race = string.Empty;

	// Token: 0x04000409 RID: 1033
	public bool smoke;

	// Token: 0x0400040A RID: 1034
	public float smokeInterval = 0.5f;

	// Token: 0x0400040B RID: 1035
	public Vector2Int smokeOffset;

	// Token: 0x0400040C RID: 1036
	public bool spawnPixel;

	// Token: 0x0400040D RID: 1037
	public string spawnDropID = "";

	// Token: 0x0400040E RID: 1038
	public float spawnPixelInterval = 0.3f;

	// Token: 0x0400040F RID: 1039
	public float spawnPixelStartZ;

	// Token: 0x04000410 RID: 1040
	public string transformTilesToTileType = string.Empty;

	// Token: 0x04000411 RID: 1041
	public string transformTilesToTopTiles = string.Empty;

	// Token: 0x04000412 RID: 1042
	public string sfx = "none";

	// Token: 0x04000413 RID: 1043
	public bool spawnRats;

	// Token: 0x04000414 RID: 1044
	public bool fauna;

	// Token: 0x04000415 RID: 1045
	public string tech = string.Empty;

	// Token: 0x04000416 RID: 1046
	[NonSerialized]
	public BuildingSprites sprites;
}
