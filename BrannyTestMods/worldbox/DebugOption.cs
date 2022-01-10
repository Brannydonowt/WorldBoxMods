using System;

// Token: 0x0200020C RID: 524
public enum DebugOption
{
	// Token: 0x04000DDB RID: 3547
	Islands,
	// Token: 0x04000DDC RID: 3548
	Connections,
	// Token: 0x04000DDD RID: 3549
	LastPath,
	// Token: 0x04000DDE RID: 3550
	CursorChunk,
	// Token: 0x04000DDF RID: 3551
	Region,
	// Token: 0x04000DE0 RID: 3552
	RegionNeighbours,
	// Token: 0x04000DE1 RID: 3553
	CityZones,
	// Token: 0x04000DE2 RID: 3554
	Chunks,
	// Token: 0x04000DE3 RID: 3555
	ChunksDirty,
	// Token: 0x04000DE4 RID: 3556
	RegionsDirty,
	// Token: 0x04000DE5 RID: 3557
	CityPlaces,
	// Token: 0x04000DE6 RID: 3558
	ChunkEdges,
	// Token: 0x04000DE7 RID: 3559
	PathRegions,
	// Token: 0x04000DE8 RID: 3560
	ActivePaths,
	// Token: 0x04000DE9 RID: 3561
	Greg,
	// Token: 0x04000DEA RID: 3562
	Graphy,
	// Token: 0x04000DEB RID: 3563
	NewDebugWindow,
	// Token: 0x04000DEC RID: 3564
	Buildings,
	// Token: 0x04000DED RID: 3565
	FastSpawn,
	// Token: 0x04000DEE RID: 3566
	SonicSpeed,
	// Token: 0x04000DEF RID: 3567
	TabMain,
	// Token: 0x04000DF0 RID: 3568
	TabUnits,
	// Token: 0x04000DF1 RID: 3569
	JobFarmer,
	// Token: 0x04000DF2 RID: 3570
	JobGatherer,
	// Token: 0x04000DF3 RID: 3571
	JobMiner,
	// Token: 0x04000DF4 RID: 3572
	TargetedBy,
	// Token: 0x04000DF5 RID: 3573
	JobHunter,
	// Token: 0x04000DF6 RID: 3574
	JobBuilder,
	// Token: 0x04000DF7 RID: 3575
	JobFireman,
	// Token: 0x04000DF8 RID: 3576
	JobAttacker,
	// Token: 0x04000DF9 RID: 3577
	JobDefender,
	// Token: 0x04000DFA RID: 3578
	JobMetalWorker,
	// Token: 0x04000DFB RID: 3579
	JobBlacksmith,
	// Token: 0x04000DFC RID: 3580
	UnitsInside,
	// Token: 0x04000DFD RID: 3581
	UnitKingdoms,
	// Token: 0x04000DFE RID: 3582
	TabSystem,
	// Token: 0x04000DFF RID: 3583
	SystemUnitPathfinding,
	// Token: 0x04000E00 RID: 3584
	SystemZoneGrowth,
	// Token: 0x04000E01 RID: 3585
	SystemBuildTick,
	// Token: 0x04000E02 RID: 3586
	SystemCityPlaceFinder,
	// Token: 0x04000E03 RID: 3587
	SystemWorldBehaviours,
	// Token: 0x04000E04 RID: 3588
	SystemProduceNewCitizens,
	// Token: 0x04000E05 RID: 3589
	SystemCheckUnitAction,
	// Token: 0x04000E06 RID: 3590
	SystemUpdateUnits,
	// Token: 0x04000E07 RID: 3591
	SystemUpdateBuildings,
	// Token: 0x04000E08 RID: 3592
	SystemRedrawMap,
	// Token: 0x04000E09 RID: 3593
	SystemUpdateCities,
	// Token: 0x04000E0A RID: 3594
	SystemUseCitizensDict,
	// Token: 0x04000E0B RID: 3595
	SystemCityTasks,
	// Token: 0x04000E0C RID: 3596
	ProKing,
	// Token: 0x04000E0D RID: 3597
	ProLeader,
	// Token: 0x04000E0E RID: 3598
	ProUnit,
	// Token: 0x04000E0F RID: 3599
	ProBaby,
	// Token: 0x04000E10 RID: 3600
	ProWarrior,
	// Token: 0x04000E11 RID: 3601
	TabsLogs,
	// Token: 0x04000E12 RID: 3602
	SystemCalculateIslands,
	// Token: 0x04000E13 RID: 3603
	SystemUpdateDirtyChunks,
	// Token: 0x04000E14 RID: 3604
	DisplayUnitTiles,
	// Token: 0x04000E15 RID: 3605
	UseGlobalPathLock,
	// Token: 0x04000E16 RID: 3606
	NOT_USED,
	// Token: 0x04000E17 RID: 3607
	ConstructionTiles,
	// Token: 0x04000E18 RID: 3608
	JobSettler,
	// Token: 0x04000E19 RID: 3609
	NOT_USED2,
	// Token: 0x04000E1A RID: 3610
	SystemMusic,
	// Token: 0x04000E1B RID: 3611
	BurgerHeads,
	// Token: 0x04000E1C RID: 3612
	TrunkHeads,
	// Token: 0x04000E1D RID: 3613
	JobCleaner,
	// Token: 0x04000E1E RID: 3614
	RenderMapRegionEdges,
	// Token: 0x04000E1F RID: 3615
	SystemUpdateUnitAnimation,
	// Token: 0x04000E20 RID: 3616
	RenderConnectedIslands,
	// Token: 0x04000E21 RID: 3617
	RenderIslandsTileCorners,
	// Token: 0x04000E22 RID: 3618
	RenderIslandCenterRegions,
	// Token: 0x04000E23 RID: 3619
	KingdomDrawAttackTarget,
	// Token: 0x04000E24 RID: 3620
	CivDrawSettleTarget,
	// Token: 0x04000E25 RID: 3621
	KingdomHeads,
	// Token: 0x04000E26 RID: 3622
	Console,
	// Token: 0x04000E27 RID: 3623
	ActorGizmosCurrentPosition,
	// Token: 0x04000E28 RID: 3624
	ActorGizmosAttackTarget,
	// Token: 0x04000E29 RID: 3625
	ActorGizmosTileTarget,
	// Token: 0x04000E2A RID: 3626
	ActorGizmosNextStepTile,
	// Token: 0x04000E2B RID: 3627
	ActorGizmosBoatTaxiRequest,
	// Token: 0x04000E2C RID: 3628
	ActorGizmosBoatTaxiRequestTarget,
	// Token: 0x04000E2D RID: 3629
	ActorGizmosBoatTaxiTarget,
	// Token: 0x04000E2E RID: 3630
	SystemCheckGoodForBoat,
	// Token: 0x04000E2F RID: 3631
	MakeUnitsFollowCursor,
	// Token: 0x04000E30 RID: 3632
	SystemSplitAstar,
	// Token: 0x04000E31 RID: 3633
	UseCacheForRegionPath,
	// Token: 0x04000E32 RID: 3634
	FastCultures,
	// Token: 0x04000E33 RID: 3635
	CityInfiniteResources,
	// Token: 0x04000E34 RID: 3636
	CityFastConstruction
}
