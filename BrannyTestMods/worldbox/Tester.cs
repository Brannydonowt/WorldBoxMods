using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class Tester : MonoBehaviour
{
	// Token: 0x06000A97 RID: 2711 RVA: 0x0006A7F0 File Offset: 0x000689F0
	private void init()
	{
		this.events = new List<TestingEvent>();
		this.eventsCivs = new List<TestingEvent>();
		foreach (GodPower godPower in AssetManager.powers.list)
		{
			if (godPower.id[0] != '_')
			{
				TestingEvent pEvent = this.add(new TestingEvent
				{
					type = TestingEventType.RandomClick,
					powerID = godPower.id
				});
				if (godPower.type == PowerActionType.Tile)
				{
					this.add(pEvent);
					this.add(pEvent);
					this.add(pEvent);
				}
			}
		}
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "humans",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "orcs",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "elves",
			type = TestingEventType.RandomClick
		});
		this.eventsCivs.Add(new TestingEvent
		{
			powerID = "dwarfs",
			type = TestingEventType.RandomClick
		});
		this.world = MapBox.instance;
		this.setTestStage(TestStage.SPAWN_CIVS);
		this.smoke.enabled = false;
		this.fire.enabled = false;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0006A958 File Offset: 0x00068B58
	private void setTestStage(TestStage pStage)
	{
		this.testStage = pStage;
		switch (this.testStage)
		{
		case TestStage.SPAWN_CIVS:
			this.testStageTimer = 10f;
			return;
		case TestStage.WAIT_CIVS:
			this.testStageTimer = 60f;
			return;
		case TestStage.SPAWN_CHAOS:
			this.testStageTimer = 30f;
			return;
		case TestStage.REGENERATE:
			this.testStageTimer = 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0006A9B9 File Offset: 0x00068BB9
	private TestingEvent add(TestingEvent pEvent)
	{
		this.events.Add(pEvent);
		return pEvent;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0006A9C8 File Offset: 0x00068BC8
	private void Update()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (this.events == null)
		{
			this.init();
			return;
		}
		if (!this.enableRandomSpawn)
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			return;
		}
		if (this.testStageTimer > 0f)
		{
			this.testStageTimer -= Time.deltaTime;
			TestStage testStage = this.testStage;
			TestingEvent random;
			if (testStage != TestStage.SPAWN_CIVS)
			{
				if (testStage != TestStage.SPAWN_CHAOS)
				{
					return;
				}
				random = this.events.GetRandom<TestingEvent>();
			}
			else
			{
				random = this.eventsCivs.GetRandom<TestingEvent>();
			}
			ScrollWindow.hideAllEvent(false);
			if (random == null)
			{
				return;
			}
			TestingEventType type = random.type;
			if (type != TestingEventType.RandomClick)
			{
				return;
			}
			int x = Toolbox.randomInt(0, MapBox.width);
			int y = Toolbox.randomInt(0, MapBox.height);
			LogText.log(random.powerID, "Test Power", "st");
			if (!AssetManager.powers.dict.ContainsKey(random.powerID))
			{
				MonoBehaviour.print("TESTER ERROR... " + random.powerID);
			}
			GodPower godPower = AssetManager.powers.dict[random.powerID];
			if (!godPower.tester_enabled)
			{
				return;
			}
			Config.currentBrush = Brush.getRandom();
			this.world.Clicked(new Vector2Int(x, y), godPower, null);
			LogText.log(random.powerID, "Test Power", "en");
			return;
		}
		else
		{
			switch (this.testStage)
			{
			case TestStage.SPAWN_CIVS:
				this.setTestStage(TestStage.WAIT_CIVS);
				return;
			case TestStage.WAIT_CIVS:
				this.setTestStage(TestStage.SPAWN_CHAOS);
				return;
			case TestStage.SPAWN_CHAOS:
				this.setTestStage(TestStage.REGENERATE);
				return;
			case TestStage.REGENERATE:
				Config.customZoneX = 7;
				Config.customZoneY = 7;
				this.world.generateNewMap("custom");
				this.testStageTimer = 20f;
				this.setTestStage(TestStage.SPAWN_CIVS);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x04000D1F RID: 3359
	public GlowParticles smoke;

	// Token: 0x04000D20 RID: 3360
	public GlowParticles fire;

	// Token: 0x04000D21 RID: 3361
	public TestStage testStage;

	// Token: 0x04000D22 RID: 3362
	private List<TestingEvent> events;

	// Token: 0x04000D23 RID: 3363
	private List<TestingEvent> eventsCivs;

	// Token: 0x04000D24 RID: 3364
	private float timer = 1f;

	// Token: 0x04000D25 RID: 3365
	public float testStageTimer = 20f;

	// Token: 0x04000D26 RID: 3366
	public bool enableFastBuilding;

	// Token: 0x04000D27 RID: 3367
	public bool enableRandomSpawn = true;

	// Token: 0x04000D28 RID: 3368
	private MapBox world;
}
