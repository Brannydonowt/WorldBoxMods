using System;
using System.Collections.Generic;

// Token: 0x020000F2 RID: 242
public class WorldBehaviourUnitTemperatures
{
	// Token: 0x060004FF RID: 1279 RVA: 0x000412C8 File Offset: 0x0003F4C8
	public static void updateUnitTemperatures()
	{
		if (WorldBehaviourUnitTemperatures.temperatures.Count == 0)
		{
			return;
		}
		WorldBehaviourUnitTemperatures._temperature_updaters.Clear();
		bool flag = true;
		foreach (Actor actor in WorldBehaviourUnitTemperatures.temperatures.Keys)
		{
			if (!(actor == null) && actor.data.alive)
			{
				flag = false;
				int num = WorldBehaviourUnitTemperatures.temperatures[actor];
				int num2 = num;
				if (num2 > 0)
				{
					num2--;
					if (num2 < 0)
					{
						num2 = 0;
					}
				}
				else
				{
					num2++;
					if (num2 > 0)
					{
						num2 = 0;
					}
				}
				if (num2 != num)
				{
					WorldBehaviourUnitTemperatures._temperature_updaters.Add(new TemperatureMod
					{
						actor = actor,
						new_temperature = num2
					});
				}
			}
		}
		if (flag)
		{
			WorldBehaviourUnitTemperatures.temperatures.Clear();
			return;
		}
		if (WorldBehaviourUnitTemperatures._temperature_updaters.Count > 0)
		{
			for (int i = 0; i < WorldBehaviourUnitTemperatures._temperature_updaters.Count; i++)
			{
				TemperatureMod temperatureMod = WorldBehaviourUnitTemperatures._temperature_updaters[i];
				if (temperatureMod.new_temperature == 0)
				{
					WorldBehaviourUnitTemperatures.temperatures.Remove(temperatureMod.actor);
				}
				else
				{
					WorldBehaviourUnitTemperatures.temperatures[temperatureMod.actor] = temperatureMod.new_temperature;
				}
			}
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00041420 File Offset: 0x0003F620
	private static void changeUnitTemperature(Actor pActor, int pTemperatureChangeSpeed)
	{
		if (!WorldBehaviourUnitTemperatures.temperatures.ContainsKey(pActor))
		{
			WorldBehaviourUnitTemperatures.temperatures.Add(pActor, 0);
		}
		Dictionary<Actor, int> dictionary = WorldBehaviourUnitTemperatures.temperatures;
		dictionary[pActor] += pTemperatureChangeSpeed;
		float num = (float)WorldBehaviourUnitTemperatures.temperatures[pActor];
		if (pTemperatureChangeSpeed < 0)
		{
			pActor.removeStatusEffect("burning", null, -1);
			if (num < -200f)
			{
				pActor.addStatusEffect("frozen", -1f);
				WorldBehaviourUnitTemperatures.temperatures[pActor] = 0;
				return;
			}
		}
		else
		{
			pActor.removeStatusEffect("frozen", null, -1);
			if (num > 300f)
			{
				pActor.addStatusEffect("burning", -1f);
				WorldBehaviourUnitTemperatures.temperatures[pActor] = 0;
			}
		}
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000414D4 File Offset: 0x0003F6D4
	public static void checkTile(WorldTile pTile, int pTemperatureChangeSpeed)
	{
		if (pTile.units.Count == 0)
		{
			return;
		}
		for (int i = 0; i < pTile.units.Count; i++)
		{
			WorldBehaviourUnitTemperatures.changeUnitTemperature(pTile.units[i], pTemperatureChangeSpeed);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00041517 File Offset: 0x0003F717
	public static void clear()
	{
		WorldBehaviourUnitTemperatures.temperatures.Clear();
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00041524 File Offset: 0x0003F724
	public static void debug(DebugTool pTool)
	{
		pTool.setText("units", WorldBehaviourUnitTemperatures.temperatures.Count);
		int num = 5;
		foreach (Actor actor in WorldBehaviourUnitTemperatures.temperatures.Keys)
		{
			pTool.setText(": " + actor.data.firstName, WorldBehaviourUnitTemperatures.temperatures[actor].ToString("0.####"));
			num--;
			if (num == 0)
			{
				break;
			}
		}
	}

	// Token: 0x04000709 RID: 1801
	public static Dictionary<Actor, int> temperatures = new Dictionary<Actor, int>();

	// Token: 0x0400070A RID: 1802
	private static List<TemperatureMod> _temperature_updaters = new List<TemperatureMod>();
}
