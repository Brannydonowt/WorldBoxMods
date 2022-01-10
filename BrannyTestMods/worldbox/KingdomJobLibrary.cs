using System;

// Token: 0x02000162 RID: 354
public class KingdomJobLibrary : AssetLibrary<KingdomJob>
{
	// Token: 0x060007F6 RID: 2038 RVA: 0x00057994 File Offset: 0x00055B94
	public override void init()
	{
		base.init();
		this.add(new KingdomJob
		{
			id = "kingdom"
		});
		this.t.addTask("do_checks");
		this.t.addTask("wait1");
		this.t.addTask("check_dead_kingdom");
		this.t.addTask("wait1");
		this.t.addTask("check_diplomacy");
		this.t.addTask("wait1");
		this.t.addTask("check_attack_target");
		this.t.addTask("check_revolts");
		this.t.addTask("wait_random");
		this.t.addTask("check_culture");
	}
}
