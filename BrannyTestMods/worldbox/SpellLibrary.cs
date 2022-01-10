using System;
using Beebyte.Obfuscator;

// Token: 0x02000049 RID: 73
[ObfuscateLiterals]
public class SpellLibrary : AssetLibrary<Spell>
{
	// Token: 0x060001BB RID: 443 RVA: 0x00023064 File Offset: 0x00021264
	public override void init()
	{
		base.init();
		this.add(new Spell
		{
			id = "teleportRandom",
			chance = 0.7f,
			castTarget = CastTarget.Himself,
			healthPercent = 0.6f
		});
		this.t.action.Add(new WorldAction(ActionLibrary.teleportRandom));
		this.add(new Spell
		{
			id = "lightning",
			chance = 0.3f,
			min_distance = 6f
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castLightning));
		this.add(new Spell
		{
			id = "tornado",
			chance = 0.02f,
			min_distance = 9f
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castTornado));
		this.add(new Spell
		{
			id = "curse",
			chance = 0.2f,
			min_distance = 4f,
			castEntity = CastEntity.UnitsOnly
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castCurses));
		this.add(new Spell
		{
			id = "fire",
			chance = 0.2f,
			min_distance = 3f,
			castEntity = CastEntity.Both
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castFire));
		this.add(new Spell
		{
			id = "bloodRain",
			chance = 0.5f,
			min_distance = 0f,
			healthPercent = 0.9f,
			castTarget = CastTarget.Himself,
			castEntity = CastEntity.UnitsOnly
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castBloodRain));
		this.add(new Spell
		{
			id = "spawnGrassSeeds",
			chance = 1f,
			min_distance = 0f,
			castTarget = CastTarget.Region,
			castEntity = CastEntity.Tile
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castSpawnGrassSeeds));
		this.add(new Spell
		{
			id = "spawnSkeleton",
			chance = 0.6f,
			min_distance = 0f,
			castTarget = CastTarget.Himself
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castSpawnSkeleton));
		this.add(new Spell
		{
			id = "shield",
			chance = 0.2f,
			castTarget = CastTarget.Himself
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castShieldOnHimself));
		this.add(new Spell
		{
			id = "cure",
			chance = 1f,
			min_distance = 0f,
			castTarget = CastTarget.Friendly,
			castEntity = CastEntity.UnitsOnly
		});
		this.t.action.Add(new WorldAction(ActionLibrary.castCure));
	}
}
