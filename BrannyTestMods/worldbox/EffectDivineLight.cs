using System;

// Token: 0x02000105 RID: 261
public class EffectDivineLight : BaseAnimatedObject
{
	// Token: 0x060005CC RID: 1484 RVA: 0x000463D9 File Offset: 0x000445D9
	private new void Awake()
	{
		this.setState(DivineLightState.SpawnFirstStage);
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x000463E4 File Offset: 0x000445E4
	private void setState(DivineLightState pState)
	{
		this.state = pState;
		switch (this.state)
		{
		case DivineLightState.SpawnFirstStage:
			this.raySpawn.gameObject.SetActive(true);
			this.rayIdle.gameObject.SetActive(false);
			this.baseSpawn.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(false);
			return;
		case DivineLightState.SpawnSecondStage:
			this.raySpawn.gameObject.SetActive(false);
			this.rayIdle.gameObject.SetActive(true);
			this.baseSpawn.gameObject.SetActive(true);
			this.baseIdle.gameObject.SetActive(false);
			return;
		case DivineLightState.Idle:
			this.raySpawn.gameObject.SetActive(false);
			this.rayIdle.gameObject.SetActive(true);
			this.baseSpawn.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(true);
			return;
		case DivineLightState.Hide:
			this.raySpawn.gameObject.SetActive(true);
			this.rayIdle.gameObject.SetActive(false);
			this.baseSpawn.gameObject.SetActive(true);
			this.baseIdle.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00046529 File Offset: 0x00044729
	private void stopEffet()
	{
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0004652B File Offset: 0x0004472B
	private void useEffect()
	{
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00046530 File Offset: 0x00044730
	private void Update()
	{
		if (this.isOn)
		{
			this.raySpawn.playType = AnimPlayType.Forward;
			this.baseSpawn.playType = AnimPlayType.Forward;
			if (this.raySpawn.isLastFrame())
			{
				this.raySpawn.gameObject.SetActive(false);
				this.rayIdle.gameObject.SetActive(true);
			}
			else
			{
				this.raySpawn.gameObject.SetActive(true);
				this.rayIdle.gameObject.SetActive(false);
			}
			if (this.baseSpawn.isLastFrame())
			{
				this.baseSpawn.gameObject.SetActive(false);
				this.baseIdle.gameObject.SetActive(true);
			}
			else
			{
				this.baseSpawn.gameObject.SetActive(true);
				this.baseIdle.gameObject.SetActive(false);
			}
		}
		else
		{
			this.raySpawn.playType = AnimPlayType.Backward;
			this.baseSpawn.playType = AnimPlayType.Backward;
			this.rayIdle.gameObject.SetActive(false);
			this.baseIdle.gameObject.SetActive(false);
			if (this.raySpawn.isFirstFrame())
			{
				this.raySpawn.gameObject.SetActive(false);
			}
			else
			{
				this.raySpawn.gameObject.SetActive(true);
			}
			if (this.baseSpawn.isFirstFrame())
			{
				this.baseSpawn.gameObject.SetActive(false);
			}
			else
			{
				this.baseSpawn.gameObject.SetActive(true);
			}
		}
		if (this.baseSpawn.gameObject.activeSelf)
		{
			this.baseSpawn.update(this.world.deltaTime);
		}
		if (this.baseIdle.gameObject.activeSelf)
		{
			this.baseIdle.update(this.world.deltaTime);
		}
		if (this.raySpawn.gameObject.activeSelf)
		{
			this.raySpawn.update(this.world.deltaTime);
		}
		if (this.rayIdle.gameObject.activeSelf)
		{
			this.rayIdle.update(this.world.deltaTime);
		}
		this.isOn = false;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00046751 File Offset: 0x00044951
	public void playOn(WorldTile pTile)
	{
		base.gameObject.transform.localPosition = pTile.posV3;
		this.isOn = true;
	}

	// Token: 0x040007BA RID: 1978
	public SpriteAnimation raySpawn;

	// Token: 0x040007BB RID: 1979
	public SpriteAnimation rayIdle;

	// Token: 0x040007BC RID: 1980
	public SpriteAnimation baseSpawn;

	// Token: 0x040007BD RID: 1981
	public SpriteAnimation baseIdle;

	// Token: 0x040007BE RID: 1982
	public bool isOn;

	// Token: 0x040007BF RID: 1983
	private DivineLightState state;
}
