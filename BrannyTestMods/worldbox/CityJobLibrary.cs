using System;

// Token: 0x0200015F RID: 351
public class CityJobLibrary : AssetLibrary<JobCityAsset>
{
	// Token: 0x060007F2 RID: 2034 RVA: 0x000577C0 File Offset: 0x000559C0
	public override void init()
	{
		base.init();
		this.add(new JobCityAsset
		{
			id = "city"
		});
		this.t.addTask("build");
		this.t.addTask("wait1");
		this.t.addTask("check_army");
		this.t.addTask("wait1");
		this.t.addTask("do_checks");
		this.t.addTask("wait1");
		this.t.addTask("border_growth");
		this.t.addTask("wait1");
		this.t.addTask("border_steal");
		this.t.addTask("wait1");
		this.t.addTask("produce_unit");
		this.t.addTask("wait1");
		this.t.addTask("give_inventory_item");
		this.t.addTask("wait5");
		this.t.addTask("produce_boat");
		this.t.addTask("wait1");
		this.t.addTask("upgrade_random_building");
		this.t.addTask("wait1");
		this.t.addTask("supply_kingdom_cities");
		this.t.addTask("wait1");
		this.t.addTask("produce_resources");
		this.t.addTask("wait1");
		this.t.addTask("check_pop_points");
		this.t.addTask("wait1");
		this.t.addTask("check_culture");
	}
}
