using System;
using Beebyte.Obfuscator;

// Token: 0x020000A5 RID: 165
[ObfuscateLiterals]
public class TerraformLibrary : AssetLibrary<TerraformOptions>
{
	// Token: 0x0600035A RID: 858 RVA: 0x000365A0 File Offset: 0x000347A0
	public override void init()
	{
		base.init();
		TerraformLibrary.tumor_attack = this.add(new TerraformOptions
		{
			id = "tumor_attack",
			flash = true,
			destroyBuildings = true,
			ignoreKingdoms = new string[]
			{
				"tumor"
			}
		});
		TerraformLibrary.destroy = this.add(new TerraformOptions
		{
			id = "destroy",
			flash = true,
			destroyBuildings = true,
			removeBorders = true
		});
		TerraformLibrary.destroy_life = this.add(new TerraformOptions
		{
			id = "destroy_life",
			destroyOnly = List.Of<string>(new string[]
			{
				"tumor",
				"super_pumpkins",
				"biomass"
			}),
			destroyBuildings = true,
			removeBuilding = true,
			flash = true,
			removeBorders = true,
			removeTornado = true
		});
		TerraformLibrary.earthquake = this.add(new TerraformOptions
		{
			id = "earthquake",
			removeLava = true,
			destroyBuildings = true,
			removeBuilding = true
		});
		TerraformLibrary.earthquake = this.add(new TerraformOptions
		{
			id = "earthquakeDisaster",
			removeLava = true,
			destroyBuildings = true,
			removeTopTile = true,
			removeRoads = true,
			removeFrozen = true,
			removeWater = true
		});
		TerraformLibrary.road = this.add(new TerraformOptions
		{
			id = "road",
			flash = true,
			removeBuilding = true,
			destroyBuildings = true,
			destroyOnly = List.Of<string>(new string[]
			{
				"nature"
			})
		});
		TerraformLibrary.flash = this.add(new TerraformOptions
		{
			id = "flash",
			flash = true
		});
		TerraformLibrary.grey_goo = this.add(new TerraformOptions
		{
			id = "grey_goo",
			destroyBuildings = true,
			removeBuilding = true,
			flash = true,
			removeBorders = true
		});
		TerraformLibrary.remove = this.add(new TerraformOptions
		{
			id = "remove",
			flash = true,
			removeBuilding = true,
			destroyBuildings = true
		});
		TerraformLibrary.draw = this.add(new TerraformOptions
		{
			id = "draw",
			flash = true
		});
		TerraformLibrary.water_fill = this.add(new TerraformOptions
		{
			id = "water_fill",
			flash = true,
			removeBuilding = true,
			destroyBuildings = true,
			ignoreBuildings = List.Of<string>(new string[]
			{
				"geyser",
				"volcano",
				"geyserAcid"
			})
		});
		TerraformLibrary.destroy_no_flash = this.add(new TerraformOptions
		{
			id = "destroy_no_flash",
			removeBurned = true,
			destroyBuildings = true
		});
		TerraformLibrary.nothing = this.add(new TerraformOptions
		{
			id = "nothing"
		});
		TerraformLibrary.removeLava = this.add(new TerraformOptions
		{
			id = "removeLava",
			removeLava = true
		});
		TerraformLibrary.lavaDamage = this.add(new TerraformOptions
		{
			id = "lavaDamage",
			flash = true,
			damageBuildings = true,
			damage = 10
		});
		this.add(new TerraformOptions
		{
			id = "grenade",
			flash = true,
			damageBuildings = true,
			damage = 50,
			applyForce = true,
			spawnPixels = true,
			explode_and_set_random_fire = true,
			explode_tile = true,
			explosion_pixel_effect = true,
			explode_strength = 1,
			shake = true,
			removeTornado = true
		});
		this.add(new TerraformOptions
		{
			id = "lightning",
			flash = true,
			lightningEffect = true,
			addBurned = true,
			addHeat = 9,
			damage = 77,
			setFire = true,
			applyForce = true,
			shake = true
		});
		this.t.shake_intensity = 0.1f;
		this.add(new TerraformOptions
		{
			id = "madness_ball",
			applyForce = true,
			force_power = 0.2f,
			addTrait = "madness",
			flash = true
		});
		this.add(new TerraformOptions
		{
			id = "ufo_attack",
			flash = true,
			addBurned = true,
			addHeat = 3,
			damage = 50,
			setFire = true,
			applyForce = true,
			shake = true
		});
		this.t.ignoreKingdoms = new string[]
		{
			"aliens"
		};
		this.t.shake_intensity = 0.1f;
		this.clone("ufo_explosion", "grenade");
		this.t.ignoreKingdoms = new string[]
		{
			"aliens"
		};
		this.clone("bomb", "grenade");
		this.t.damage = 100;
		this.t.explode_strength = 3;
		this.t.spawnPixels = true;
		TerraformLibrary.atomicBomb = this.clone("atomicBomb", "bomb");
		this.t.damage = 10000;
		this.t.explode_strength = 1;
		this.t.transformToWasteland = true;
		this.t.applies_to_high_flyers = true;
		TerraformLibrary.czarBomba = this.clone("czarBomba", "bomb");
		this.t.damage = 10000;
		this.t.transformToWasteland = true;
		this.t.explode_strength = 4;
		this.t.applies_to_high_flyers = true;
		this.add(new TerraformOptions
		{
			id = "crab",
			flash = true,
			damageBuildings = true,
			damage = 10,
			applyForce = true,
			explode_tile = true,
			explosion_pixel_effect = false,
			explode_strength = 1,
			shake = true,
			shake_intensity = 0.1f
		});
		this.clone("crab_laser", "bomb");
		this.t.damage = 200;
		this.t.shake_intensity = 1f;
		this.clone("santa_bomb", "bomb");
		this.t.ignoreKingdoms = new string[]
		{
			"santa"
		};
		this.clone("demon_fireball", "grenade");
		this.t.ignoreKingdoms = new string[]
		{
			"demons"
		};
		this.t.shake = false;
		this.t.damageBuildings = true;
		this.t.damage = 30;
		this.t.setFire = true;
		this.t.spawnPixels = false;
		this.clone("plasma_ball", "grenade");
		this.t.shake = false;
		this.t.damageBuildings = true;
		this.t.damage = 30;
		this.t.spawnPixels = false;
		this.clone("torch", "grenade");
		this.t.ignoreKingdoms = new string[]
		{
			"demons"
		};
		this.t.shake = false;
		this.t.damageBuildings = true;
		this.t.damage = 25;
		this.t.setFire = true;
		this.t.spawnPixels = false;
		this.clone("assimilator_bullet", "flash");
		this.t.ignoreKingdoms = new string[]
		{
			"assimilators"
		};
	}

	// Token: 0x040005AE RID: 1454
	public static TerraformOptions nothing;

	// Token: 0x040005AF RID: 1455
	public static TerraformOptions removeLava;

	// Token: 0x040005B0 RID: 1456
	public static TerraformOptions lavaDamage;

	// Token: 0x040005B1 RID: 1457
	public static TerraformOptions destroy_no_flash;

	// Token: 0x040005B2 RID: 1458
	public static TerraformOptions remove;

	// Token: 0x040005B3 RID: 1459
	public static TerraformOptions draw;

	// Token: 0x040005B4 RID: 1460
	public static TerraformOptions water_fill;

	// Token: 0x040005B5 RID: 1461
	public static TerraformOptions grey_goo;

	// Token: 0x040005B6 RID: 1462
	public static TerraformOptions flash;

	// Token: 0x040005B7 RID: 1463
	public static TerraformOptions road;

	// Token: 0x040005B8 RID: 1464
	public static TerraformOptions tumor_attack;

	// Token: 0x040005B9 RID: 1465
	public static TerraformOptions destroy;

	// Token: 0x040005BA RID: 1466
	public static TerraformOptions destroy_life;

	// Token: 0x040005BB RID: 1467
	public static TerraformOptions earthquake;

	// Token: 0x040005BC RID: 1468
	public static TerraformOptions atomicBomb;

	// Token: 0x040005BD RID: 1469
	public static TerraformOptions czarBomba;
}
