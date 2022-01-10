using System;
using System.Collections.Generic;

// Token: 0x02000040 RID: 64
[Serializable]
public class ProfessionLibrary : AssetLibrary<ProfessionAsset>
{
	// Token: 0x060001AF RID: 431 RVA: 0x00022128 File Offset: 0x00020328
	public override void init()
	{
		base.init();
		this.add(new ProfessionAsset
		{
			id = "null",
			profession_id = UnitProfession.Null
		});
		this.add(new ProfessionAsset
		{
			id = "baby",
			profession_id = UnitProfession.Baby,
			special_skin_path = "unit_child",
			is_civilian = true
		});
		this.add(new ProfessionAsset
		{
			id = "unit",
			profession_id = UnitProfession.Unit,
			is_civilian = true,
			use_skin_culture = true
		});
		this.add(new ProfessionAsset
		{
			id = "warrior",
			profession_id = UnitProfession.Warrior,
			can_capture = true,
			special_skin_path = "unit_warrior_1",
			use_skin_culture = true
		});
		this.add(new ProfessionAsset
		{
			id = "king",
			profession_id = UnitProfession.King,
			can_capture = true,
			special_skin_path = "unit_king"
		});
		this.add(new ProfessionAsset
		{
			id = "leader",
			profession_id = UnitProfession.Leader,
			can_capture = true,
			special_skin_path = "unit_leader"
		});
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0002224C File Offset: 0x0002044C
	public override ProfessionAsset add(ProfessionAsset pAsset)
	{
		this.dict_profeesion_id.Add(pAsset.profession_id, pAsset);
		return base.add(pAsset);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00022267 File Offset: 0x00020467
	public virtual ProfessionAsset get(UnitProfession pID)
	{
		return this.dict_profeesion_id[pID];
	}

	// Token: 0x04000175 RID: 373
	private Dictionary<UnitProfession, ProfessionAsset> dict_profeesion_id = new Dictionary<UnitProfession, ProfessionAsset>();
}
