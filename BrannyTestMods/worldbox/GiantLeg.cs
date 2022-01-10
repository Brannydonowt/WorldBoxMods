using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class GiantLeg : MonoBehaviour
{
	// Token: 0x06000800 RID: 2048 RVA: 0x00057D24 File Offset: 0x00055F24
	internal void create()
	{
		this.targetPosition = this.limbPoint.transform.position;
		this.targetPosition.z = 0f;
		this.currentPosition = this.targetPosition;
		base.transform.position = this.targetPosition;
		base.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00057D80 File Offset: 0x00055F80
	internal void Update()
	{
		float num = Toolbox.DistVec3(this.currentPosition, this.targetPosition);
		this.currentPosition = Vector3.MoveTowards(this.currentPosition, this.targetPosition, 1.5f + num / 5f);
		base.transform.position = this.currentPosition;
		this.target_pos = this.limbPoint.transform.position + this.randomPos;
		Toolbox.DistVec3(this.target_pos, this.targetPosition);
		Toolbox.DistVec3(this.currentPosition, this.mainBody.transform.position);
		if (!this.legJoint.isAngleOk(this.giantzilla.minMax_angle0))
		{
			this.moveLeg();
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00057E44 File Offset: 0x00056044
	public void moveLeg()
	{
		Vector3 b = default(Vector2);
		float num = 2f;
		if (this.giantzilla.movementVector.x != 0f)
		{
			if (this.giantzilla.movementVector.x > 0f)
			{
				b.x += num;
			}
			else
			{
				b.x -= num;
			}
		}
		if (this.giantzilla.movementVector.y != 0f)
		{
			if (this.giantzilla.movementVector.y > 0f)
			{
				b.y += num;
			}
			else
			{
				b.y -= num;
			}
		}
		this.target_pos = this.limbPoint.transform.position + this.randomPos;
		this.target_pos.z = 0f;
		this.legJoint.tooFar = false;
		this.targetPosition = this.target_pos;
		this.randomPos.x = Toolbox.randomFloat(-1f, 1f);
		this.randomPos.y = Toolbox.randomFloat(-1f, 1f);
		this.randomPos += b;
		this.giantzilla.legTimeout = 0.1f;
		this.giantzilla.legMoved();
		WorldTile tile = MapBox.instance.GetTile((int)this.target_pos.x, (int)this.target_pos.y);
		if (tile == null)
		{
			return;
		}
		MapAction.damageWorld(tile, 3, AssetManager.terraform.get("crab"));
	}

	// Token: 0x04000A5D RID: 2653
	public string legTag;

	// Token: 0x04000A5E RID: 2654
	public GiantLimbPoint limbPoint;

	// Token: 0x04000A5F RID: 2655
	internal GiantBody mainBody;

	// Token: 0x04000A60 RID: 2656
	internal Giantzilla giantzilla;

	// Token: 0x04000A61 RID: 2657
	internal Vector3 currentPosition;

	// Token: 0x04000A62 RID: 2658
	internal Vector3 targetPosition;

	// Token: 0x04000A63 RID: 2659
	internal Vector3 randomPos = Vector3.zero;

	// Token: 0x04000A64 RID: 2660
	public LegJoint legJoint;

	// Token: 0x04000A65 RID: 2661
	private Vector3 target_pos;
}
