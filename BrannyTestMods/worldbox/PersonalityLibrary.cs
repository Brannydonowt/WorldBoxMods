using System;

// Token: 0x0200003C RID: 60
public class PersonalityLibrary : AssetLibrary<PersonalityAsset>
{
	// Token: 0x06000177 RID: 375 RVA: 0x0001B634 File Offset: 0x00019834
	public override void init()
	{
		base.init();
		this.add(new PersonalityAsset
		{
			id = "administrator"
		});
		this.t.baseStats.personality_diplomatic = 0.1f;
		this.t.baseStats.personality_administration = 0.5f;
		this.t.baseStats.personality_aggression = 0.1f;
		this.add(new PersonalityAsset
		{
			id = "militarist"
		});
		this.t.baseStats.personality_diplomatic = 0.05f;
		this.t.baseStats.personality_administration = 0.1f;
		this.t.baseStats.personality_aggression = 0.5f;
		this.add(new PersonalityAsset
		{
			id = "diplomat"
		});
		this.t.baseStats.personality_diplomatic = 0.5f;
		this.t.baseStats.personality_aggression = 0.05f;
		this.t.baseStats.personality_administration = 0.2f;
		this.add(new PersonalityAsset
		{
			id = "balanced"
		});
		this.t.baseStats.personality_administration = 0.1f;
		this.t.baseStats.personality_diplomatic = 0.1f;
		this.t.baseStats.personality_aggression = 0.1f;
		this.add(new PersonalityAsset
		{
			id = "wildcard"
		});
		this.t.baseStats.personality_rationality = -1f;
	}
}
