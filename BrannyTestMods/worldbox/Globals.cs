using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class Globals
{
	// Token: 0x040004C9 RID: 1225
	public const int CITY_MIN_ISLAND_TILES = 500;

	// Token: 0x040004CA RID: 1226
	public const int CITY_MIN_CAN_BE_FARMS_TILES = 5;

	// Token: 0x040004CB RID: 1227
	public const float CITY_TIMER_SUPPLY_DEFAULT = 30f;

	// Token: 0x040004CC RID: 1228
	public const float REGION_UPDATE_TIMEOUT = 0.5f;

	// Token: 0x040004CD RID: 1229
	public const int CITY_POP_FOR_SETTLE_TARGET = 30;

	// Token: 0x040004CE RID: 1230
	public const int CITY_MAXIMUM_ZONES_PER_BUILDING = 3;

	// Token: 0x040004CF RID: 1231
	public const bool AI_TEST_ACTIVE = true;

	// Token: 0x040004D0 RID: 1232
	public const bool XMAS = false;

	// Token: 0x040004D1 RID: 1233
	public const float DEFAULT_SCALE_UNIT = 0.1f;

	// Token: 0x040004D2 RID: 1234
	public const float DEFAULT_SCALE_BUILDING = 0.25f;

	// Token: 0x040004D3 RID: 1235
	public const bool DIAGNOSTIC = false;

	// Token: 0x040004D4 RID: 1236
	public const bool DEBUG_CRAB = false;

	// Token: 0x040004D5 RID: 1237
	public const bool TRAILER_MODE = false;

	// Token: 0x040004D6 RID: 1238
	public static bool TRAILER_MODE_USE_RESOURCES = true;

	// Token: 0x040004D7 RID: 1239
	public static bool TRAILER_MODE_UPGRADE_BUILDINGS = true;

	// Token: 0x040004D8 RID: 1240
	public const int TIPS_AMOUNT = 16;

	// Token: 0x040004D9 RID: 1241
	public const float TORNADO_TIMER = 35f;

	// Token: 0x040004DA RID: 1242
	public const int CAST_HEIGHT = 15;

	// Token: 0x040004DB RID: 1243
	public const int UNITS_IN_SPAWNER_PER_REGION = 4;

	// Token: 0x040004DC RID: 1244
	public const int PATHFINDER_REGION_LIMIT = 4;

	// Token: 0x040004DD RID: 1245
	public const int ATTRIBUTE_BAD = 4;

	// Token: 0x040004DE RID: 1246
	public const int ATTRIBUTE_NORMAL = 9;

	// Token: 0x040004DF RID: 1247
	public const int ATTRIBUTE_GOOD = 20;

	// Token: 0x040004E0 RID: 1248
	public const int BUILDING_TOWER_CAPTURE_POINTS = 10;

	// Token: 0x040004E1 RID: 1249
	public const int GENERATOR_MAX_TREES_PER_ZONE = 3;

	// Token: 0x040004E2 RID: 1250
	public const float MIN_ADS_INTERVAL_MINUTES = 2f;

	// Token: 0x040004E3 RID: 1251
	public const float ADS_INTERVAL_MINUTES = 5f;

	// Token: 0x040004E4 RID: 1252
	public const bool specialAbstudio = false;

	// Token: 0x040004E5 RID: 1253
	public const float SAME_GRASS_CHANCE = 0.05f;

	// Token: 0x040004E6 RID: 1254
	public const int GREY_GOO_DAMAGE = 50;

	// Token: 0x040004E7 RID: 1255
	public const int DAMAGE_ACID = 20;

	// Token: 0x040004E8 RID: 1256
	public const int DAMAGE_HEAT = 50;

	// Token: 0x040004E9 RID: 1257
	public const int DAMAGE_BOULDER = 1000;

	// Token: 0x040004EA RID: 1258
	public const int CITIZEN_FOOD_COST = 1;

	// Token: 0x040004EB RID: 1259
	public const int ZONES_BETWEEN_CITIES = 7;

	// Token: 0x040004EC RID: 1260
	public const int CIV_ARMY_MAX = 65;

	// Token: 0x040004ED RID: 1261
	public const int CIV_TIMEOUT_YEARS_FOR_DIPLOMACY = 22;

	// Token: 0x040004EE RID: 1262
	public const int BOMB_RANGE_ATOMIC_NUKE = 30;

	// Token: 0x040004EF RID: 1263
	public const int BOMB_RANGE_CZAR_BOMB = 70;

	// Token: 0x040004F0 RID: 1264
	public const int ANIMAL_BABY_MAKING_UNITS_AROUND_LIMIT = 6;

	// Token: 0x040004F1 RID: 1265
	public const int FOOD_FROM_FISH = 1;

	// Token: 0x040004F2 RID: 1266
	public const int FOOD_FROM_TILE = 1;

	// Token: 0x040004F3 RID: 1267
	public const int FOOD_FROM_FARM = 2;

	// Token: 0x040004F4 RID: 1268
	public const int ANIMAL_GOOD_FOR_HUNTING_AGE = 3;

	// Token: 0x040004F5 RID: 1269
	public const int DEFAULT_MAX_UNIT_LEVEL = 10;

	// Token: 0x040004F6 RID: 1270
	public const int BOAT_UNITS_LIMIT = 100;

	// Token: 0x040004F7 RID: 1271
	public const int TRANSPORT_WAIT_TRY_LIMIT = 4;

	// Token: 0x040004F8 RID: 1272
	public const float TIMER_HUNGER = 8f;

	// Token: 0x040004F9 RID: 1273
	public const float HUNGRY = 50f;

	// Token: 0x040004FA RID: 1274
	public const int HUNGRY_UNIT = 10;

	// Token: 0x040004FB RID: 1275
	public const float COLORS_ZONE_ALPHA = 0.78f;

	// Token: 0x040004FC RID: 1276
	public const int ISLAND_TILES_FOR_DOCKS = 2500;

	// Token: 0x040004FD RID: 1277
	public const int FOOD_BUSH_REGROW_TIME = 90;

	// Token: 0x040004FE RID: 1278
	internal const int REWARD_MINUTES = 30;

	// Token: 0x040004FF RID: 1279
	internal const int REWARD_DURATION = 1800;

	// Token: 0x04000500 RID: 1280
	internal const int REWARD_SAVESLOT_HOURS = 3;

	// Token: 0x04000501 RID: 1281
	internal const int REWARD_SAVESLOT_DURATION = 10800;

	// Token: 0x04000502 RID: 1282
	internal const int REWARDS_PER_GIFT = 3;

	// Token: 0x04000503 RID: 1283
	internal const int REWARD_AD_SAVESLOTS_OLD = 6;

	// Token: 0x04000504 RID: 1284
	internal const int REWARD_AD_SAVESLOTS_10HRS = 2;

	// Token: 0x04000505 RID: 1285
	internal const int REWARD_AD_SAVESLOTS_20HRS = 3;

	// Token: 0x04000506 RID: 1286
	internal const string REDDIT_DISCORD_MEMBERS = "300.000";

	// Token: 0x04000507 RID: 1287
	internal const string WB_EXAMPLE_MAP = "WB-5555-1166-5555";

	// Token: 0x04000508 RID: 1288
	public const int TIMEOUT_CONQUER = 300;

	// Token: 0x04000509 RID: 1289
	public static readonly Vector3 POINT_IN_VOID = new Vector3(-1000000f, -1000000f);

	// Token: 0x0400050A RID: 1290
	public const string NORMAL = "0";

	// Token: 0x0400050B RID: 1291
	public const int RATE_US_ID = 11;

	// Token: 0x0400050C RID: 1292
	public const string voteLink = "https://play.google.com/store/apps/details?id=com.mkarpenko.worldbox";

	// Token: 0x0400050D RID: 1293
	internal static Vector3 emptyVector = new Vector3(-100000f, -10000f);
}
