using System;

// Token: 0x020001FD RID: 509
public class TesterBehTaskLibrary : AssetLibrary<BehaviourTaskTester>
{
	// Token: 0x06000B67 RID: 2919 RVA: 0x0006E634 File Offset: 0x0006C834
	public override void init()
	{
		base.init();
		BehaviourTaskTester behaviourTaskTester = new BehaviourTaskTester();
		behaviourTaskTester.id = "close_all_windows";
		BehaviourTaskTester pAsset = behaviourTaskTester;
		this.t = behaviourTaskTester;
		this.add(pAsset);
		this.t.addBeh(new TesterBehCloseWindows());
		this.t.addBeh(new TesterBehWait(1f));
		BehaviourTaskTester behaviourTaskTester2 = new BehaviourTaskTester();
		behaviourTaskTester2.id = "open_random_window";
		pAsset = behaviourTaskTester2;
		this.t = behaviourTaskTester2;
		this.add(pAsset);
		this.t.addBeh(new TesterBehOpenWindow("random"));
		this.t.addBeh(new TesterBehWait(1f));
		BehaviourTaskTester behaviourTaskTester3 = new BehaviourTaskTester();
		behaviourTaskTester3.id = "fill_world_lava";
		pAsset = behaviourTaskTester3;
		this.t = behaviourTaskTester3;
		this.add(pAsset);
		for (int i = 0; i < 50; i++)
		{
			this.t.addBeh(new TesterBehFillWorld("lava2"));
		}
		BehaviourTaskTester behaviourTaskTester4 = new BehaviourTaskTester();
		behaviourTaskTester4.id = "fill_world_pit";
		pAsset = behaviourTaskTester4;
		this.t = behaviourTaskTester4;
		this.add(pAsset);
		for (int j = 0; j < 50; j++)
		{
			this.t.addBeh(new TesterBehFillWorld("pit_close_ocean"));
		}
		BehaviourTaskTester behaviourTaskTester5 = new BehaviourTaskTester();
		behaviourTaskTester5.id = "fill_world_water";
		pAsset = behaviourTaskTester5;
		this.t = behaviourTaskTester5;
		this.add(pAsset);
		for (int k = 0; k < 50; k++)
		{
			this.t.addBeh(new TesterBehFillWorld("deep_ocean"));
		}
		BehaviourTaskTester behaviourTaskTester6 = new BehaviourTaskTester();
		behaviourTaskTester6.id = "fill_world_soil";
		pAsset = behaviourTaskTester6;
		this.t = behaviourTaskTester6;
		this.add(pAsset);
		for (int l = 0; l < 50; l++)
		{
			this.t.addBeh(new TesterBehFillWorld("soil_low"));
		}
		BehaviourTaskTester behaviourTaskTester7 = new BehaviourTaskTester();
		behaviourTaskTester7.id = "fill_world_randomly";
		pAsset = behaviourTaskTester7;
		this.t = behaviourTaskTester7;
		this.add(pAsset);
		for (int m = 0; m < 50; m++)
		{
			this.t.addBeh(new TesterBehFillWorld("random"));
		}
		BehaviourTaskTester behaviourTaskTester8 = new BehaviourTaskTester();
		behaviourTaskTester8.id = "random_power";
		pAsset = behaviourTaskTester8;
		this.t = behaviourTaskTester8;
		this.add(pAsset);
		for (int n = 0; n < 10; n++)
		{
			this.t.addBeh(new TesterBehRandomPower());
			this.t.addBeh(new TesterBehWait(0.1f));
		}
		BehaviourTaskTester behaviourTaskTester9 = new BehaviourTaskTester();
		behaviourTaskTester9.id = "destroy_sim_objects";
		pAsset = behaviourTaskTester9;
		this.t = behaviourTaskTester9;
		this.add(pAsset);
		this.t.addBeh(new TesterBehDestroySimObjects());
		BehaviourTaskTester behaviourTaskTester10 = new BehaviourTaskTester();
		behaviourTaskTester10.id = "end_test";
		pAsset = behaviourTaskTester10;
		this.t = behaviourTaskTester10;
		this.add(pAsset);
		this.t.addBeh(new TesterBehEndJobTest());
		BehaviourTaskTester behaviourTaskTester11 = new BehaviourTaskTester();
		behaviourTaskTester11.id = "wait_1";
		pAsset = behaviourTaskTester11;
		this.t = behaviourTaskTester11;
		this.add(pAsset);
		this.t.addBeh(new TesterBehWait(1f));
		BehaviourTaskTester behaviourTaskTester12 = new BehaviourTaskTester();
		behaviourTaskTester12.id = "wait_5";
		pAsset = behaviourTaskTester12;
		this.t = behaviourTaskTester12;
		this.add(pAsset);
		this.t.addBeh(new TesterBehWait(5f));
		BehaviourTaskTester behaviourTaskTester13 = new BehaviourTaskTester();
		behaviourTaskTester13.id = "wait_10";
		pAsset = behaviourTaskTester13;
		this.t = behaviourTaskTester13;
		this.add(pAsset);
		this.t.addBeh(new TesterBehWait(10f));
		BehaviourTaskTester behaviourTaskTester14 = new BehaviourTaskTester();
		behaviourTaskTester14.id = "wait_20";
		pAsset = behaviourTaskTester14;
		this.t = behaviourTaskTester14;
		this.add(pAsset);
		this.t.addBeh(new TesterBehWait(20f));
		BehaviourTaskTester behaviourTaskTester15 = new BehaviourTaskTester();
		behaviourTaskTester15.id = "spawn_units";
		pAsset = behaviourTaskTester15;
		this.t = behaviourTaskTester15;
		this.add(pAsset);
		for (int num = 0; num < 10; num++)
		{
			this.t.addBeh(new TesterBehCreateUnits());
			this.t.addBeh(new TesterBehWait(0.1f));
		}
		BehaviourTaskTester behaviourTaskTester16 = new BehaviourTaskTester();
		behaviourTaskTester16.id = "spawn_buildings";
		pAsset = behaviourTaskTester16;
		this.t = behaviourTaskTester16;
		this.add(pAsset);
		for (int num2 = 0; num2 < 10; num2++)
		{
			this.t.addBeh(new TesterBehCreateBuildings());
		}
		BehaviourTaskTester behaviourTaskTester17 = new BehaviourTaskTester();
		behaviourTaskTester17.id = "generate_map_random";
		pAsset = behaviourTaskTester17;
		this.t = behaviourTaskTester17;
		this.add(pAsset);
		this.t.addBeh(new TesterBehGenerateMap());
		BehaviourTaskTester behaviourTaskTester18 = new BehaviourTaskTester();
		behaviourTaskTester18.id = "super_damage_units";
		pAsset = behaviourTaskTester18;
		this.t = behaviourTaskTester18;
		this.add(pAsset);
		this.t.addBeh(new TesterBehSuperDamageToUnits());
	}
}
