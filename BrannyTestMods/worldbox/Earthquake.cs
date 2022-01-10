using System;

// Token: 0x02000173 RID: 371
public class Earthquake : BaseMapObject
{
	// Token: 0x0600085D RID: 2141 RVA: 0x0005AEB4 File Offset: 0x000590B4
	public void startQuake(WorldTile pTile, EarthquakeType pType = EarthquakeType.RandomPower)
	{
		if (this.quakeActive)
		{
			return;
		}
		Sfx.play("earthquake", true, -1f, -1f);
		this.type = pType;
		this.quakeActive = true;
		this.currentPrintIndex++;
		if (this.currentPrintIndex >= this.world.printLibrary.listQuakes.Count)
		{
			this.world.printLibrary.listQuakes.Shuffle<PrintTemplate>();
			this.currentPrintIndex = 0;
		}
		this.currentPrint = this.world.printLibrary.listQuakes[this.currentPrintIndex];
		this.currentPrint.steps.Shuffle<PrintStep>();
		this.printTileOrigin = pTile;
		this.printTick = 0;
		if (pType == EarthquakeType.RandomPower)
		{
			if (Toolbox.randomChance(0.5f))
			{
				this.type = EarthquakeType.BigDecrease;
				return;
			}
			this.type = EarthquakeType.BigIncrease;
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0005AF94 File Offset: 0x00059194
	private void Update()
	{
		if (!this.quakeActive)
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= this.world.elapsed;
			return;
		}
		this.timer = this.interval;
		for (int i = 0; i < 300; i++)
		{
			if (this.printTick >= this.currentPrint.steps.Count)
			{
				this.endQuake();
				break;
			}
			PrintStep printStep = this.currentPrint.steps[this.printTick];
			this.printTick++;
			WorldTile tile = this.world.GetTile(this.printTileOrigin.pos.x + printStep.x, this.printTileOrigin.pos.y + printStep.y);
			if (tile != null)
			{
				this.tileAction(tile);
				if (this.printTick >= this.currentPrint.steps.Count)
				{
					this.endQuake();
					break;
				}
			}
		}
		this.world.startShake(0.3f, 0.01f, 2f, true, true);
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0005B0C0 File Offset: 0x000592C0
	private void tileAction(WorldTile pTile)
	{
		switch (this.type)
		{
		case EarthquakeType.BigIncrease:
			MapAction.increaseTile(pTile, "earthquake");
			return;
		case EarthquakeType.BigDecrease:
			MapAction.decreaseTile(pTile, "earthquake");
			return;
		case EarthquakeType.SmallDisaster:
			MapAction.terraformMain(pTile, pTile.main_type, AssetManager.terraform.get("earthquakeDisaster"));
			return;
		default:
			return;
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0005B11C File Offset: 0x0005931C
	private void endQuake()
	{
		this.quakeActive = false;
	}

	// Token: 0x04000AD8 RID: 2776
	private PrintTemplate currentPrint;

	// Token: 0x04000AD9 RID: 2777
	private int printTick;

	// Token: 0x04000ADA RID: 2778
	private WorldTile printTileOrigin;

	// Token: 0x04000ADB RID: 2779
	private float timer;

	// Token: 0x04000ADC RID: 2780
	private float interval = 0.05f;

	// Token: 0x04000ADD RID: 2781
	private int quakePrints;

	// Token: 0x04000ADE RID: 2782
	public bool quakeActive;

	// Token: 0x04000ADF RID: 2783
	public EarthquakeType type;

	// Token: 0x04000AE0 RID: 2784
	private int currentPrintIndex;
}
