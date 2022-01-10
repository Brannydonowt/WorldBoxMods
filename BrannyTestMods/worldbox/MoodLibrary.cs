using System;

// Token: 0x02000038 RID: 56
public class MoodLibrary : AssetLibrary<MoodAsset>
{
	// Token: 0x06000168 RID: 360 RVA: 0x00017B88 File Offset: 0x00015D88
	public override void init()
	{
		base.init();
		this.add(new MoodAsset
		{
			id = "sad",
			icon = "iconMoodSad"
		});
		this.t.baseStats.mod_speed = -20f;
		this.t.baseStats.mod_diplomacy = -10f;
		this.t.baseStats.mod_attackSpeed = -10f;
		this.t.baseStats.loyalty_mood = -5;
		this.t.baseStats.opinion = -5;
		this.add(new MoodAsset
		{
			id = "normal",
			icon = "iconMoodNormal"
		});
		this.add(new MoodAsset
		{
			id = "happy",
			icon = "iconMoodHappy"
		});
		this.t.baseStats.mod_speed = 10f;
		this.t.baseStats.mod_diplomacy = 10f;
		this.t.baseStats.mod_attackSpeed = 10f;
		this.t.baseStats.loyalty_mood = 10;
		this.t.baseStats.opinion = 10;
		this.add(new MoodAsset
		{
			id = "angry",
			icon = "iconMoodAngry"
		});
		this.t.baseStats.mod_speed = 10f;
		this.t.baseStats.mod_diplomacy = -30f;
		this.t.baseStats.mod_attackSpeed = 10f;
		this.t.baseStats.loyalty_mood = -15;
		this.t.baseStats.opinion = -15;
		this.add(new MoodAsset
		{
			id = "dark"
		});
		this.t.baseStats.loyalty_mood = -20;
		this.t.baseStats.opinion = -20;
	}
}
