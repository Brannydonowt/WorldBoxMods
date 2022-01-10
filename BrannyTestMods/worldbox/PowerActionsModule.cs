using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class PowerActionsModule
{
	// Token: 0x06000339 RID: 825 RVA: 0x00034EE5 File Offset: 0x000330E5
	public PowerActionsModule(MapBox pWorld)
	{
		this.world = pWorld;
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00034F00 File Offset: 0x00033100
	internal void magnetAction(bool pFromUpdate, WorldTile pTile = null)
	{
		if (ScrollWindow.isWindowActive())
		{
			this.dropPickedUnits();
			return;
		}
		if (pFromUpdate && this.magnetState != 1 && this.magnetState != 3)
		{
			return;
		}
		if (pTile != null)
		{
			this.magnetLastPos = pTile;
		}
		this.updatePickedUnits();
		if (pTile != null)
		{
			this.world.flashEffects.flashPixel(pTile, 10, ColorType.White);
		}
		switch (this.magnetState)
		{
		case 0:
			if (Input.GetMouseButton(0))
			{
				this.magnetState = 1;
				return;
			}
			break;
		case 1:
			if (!pFromUpdate)
			{
				this.pickupUnits(pTile);
			}
			if (Input.GetMouseButtonUp(0))
			{
				this.magnetState = 2;
				this.dropPickedUnits();
				return;
			}
			break;
		case 2:
			if (!pFromUpdate && Input.GetMouseButton(0))
			{
				this.dropPickedUnits();
				this.magnetState = 0;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00034FBC File Offset: 0x000331BC
	public void dropPickedUnits()
	{
		for (int i = 0; i < this.units.Count; i++)
		{
			Actor actor = this.units[i];
			if (!(actor == null))
			{
				this.world.units.Add(actor);
				actor.currentPosition = actor.transform.position;
				Actor actor2 = actor;
				actor2.currentPosition.y = actor2.currentPosition.y - actor.zPosition.y;
				actor.lastShadowPos.x = -1f;
				actor._currentTileDirty = true;
				actor.findCurrentTile(true);
				actor.spawnOn(actor.currentTile, actor.zPosition.y);
			}
		}
		this.units.Clear();
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0003507C File Offset: 0x0003327C
	private void updatePickedUnits()
	{
		if (this.magnetLastPos == null)
		{
			return;
		}
		for (int i = 0; i < this.units.Count; i++)
		{
			Actor actor = this.units[i];
			if (!(actor == null) && actor.data.alive)
			{
				float num = Toolbox.randomFloat(7f, 14f);
				Vector2 vector = Random.insideUnitCircle * 3f;
				Vector3 posV = this.magnetLastPos.posV3;
				posV.x += vector.x;
				posV.y += vector.y + num;
				actor.zPosition.y = num;
				actor.currentPosition = new Vector3(posV.x, posV.y - actor.zPosition.y);
				actor.transform.localPosition = posV;
			}
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00035170 File Offset: 0x00033370
	private void pickupUnits(WorldTile pTile)
	{
		BrushData currentBrushData = Config.currentBrushData;
		for (int i = 0; i < currentBrushData.pos.Count; i++)
		{
			BrushPixelData brushPixelData = currentBrushData.pos[i];
			WorldTile tile = this.world.GetTile(brushPixelData.x + pTile.x, brushPixelData.y + pTile.y);
			if (tile != null)
			{
				for (int j = 0; j < tile.units.Count; j++)
				{
					Actor actor = tile.units[j];
					if (actor.stats.canBeMovedByPowers && actor.data.alive && !actor.isInsideSomething())
					{
						if (actor.stats.isBoat)
						{
							actor.GetComponent<Boat>().updateBoatAnim(null);
						}
						if (!this.units.Contains(actor))
						{
							actor.cancelAllBeh(null);
							this.world.units.Remove(actor);
							this.units.Add(actor);
							AnimationDataUnit actorAnimationData = actor.actorAnimationData;
							if (((actorAnimationData != null) ? actorAnimationData.walking : null) != null)
							{
								actor.spriteAnimation.setFrames(actor.actorAnimationData.walking.frames);
								actor.updateAnimation(0.3f, true);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04000530 RID: 1328
	private MapBox world;

	// Token: 0x04000531 RID: 1329
	private int magnetState;

	// Token: 0x04000532 RID: 1330
	private WorldTile magnetLastPos;

	// Token: 0x04000533 RID: 1331
	private List<Actor> units = new List<Actor>();
}
