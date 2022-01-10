using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class CrabArm : MonoBehaviour
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x00057A66 File Offset: 0x00055C66
	private void Start()
	{
		this.laser.enabled = false;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00057A74 File Offset: 0x00055C74
	internal void Update()
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(this.giantzilla.armTarget.transform.position);
		vector.z = 5.23f;
		this.object_pos = Camera.main.WorldToScreenPoint(this.joint.transform.position);
		vector.x -= this.object_pos.x;
		vector.y -= this.object_pos.y;
		this.angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
		if (this.mirrored)
		{
			this.angle += 180f;
		}
		this.joint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, this.angle));
		this.updateLaser(Time.deltaTime);
		if (this.giantzilla.beamEnabled && this.laserFrameIndex > 6 && this.laserFrameIndex < 10)
		{
			this.damageWorld();
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00057B98 File Offset: 0x00055D98
	private void damageWorld()
	{
		WorldTile tile = MapBox.instance.GetTile((int)this.laserPoint.transform.position.x, (int)this.laserPoint.transform.position.y);
		if (tile != null)
		{
			MapAction.damageWorld(tile, 4, AssetManager.terraform.get("crab_laser"));
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00057BF8 File Offset: 0x00055DF8
	private void updateLaser(float pTime)
	{
		this.laserTimer -= pTime;
		if (this.giantzilla.beamEnabled)
		{
			if (this.laserTimer <= 0f)
			{
				this.laserFrameIndex++;
				if (this.laserFrameIndex >= 10)
				{
					this.laserFrameIndex = 6;
				}
			}
		}
		else if (this.laserFrameIndex != 0)
		{
			this.laserFrameIndex++;
			if (this.laserFrameIndex > 13)
			{
				this.laserFrameIndex = 0;
			}
		}
		if (this.laserTimer <= 0f)
		{
			this.laserTimer = 0.07f;
		}
		if (this.laser.sprite.name != this.laserSprites[this.laserFrameIndex].name)
		{
			this.laser.sprite = this.laserSprites[this.laserFrameIndex];
		}
		this.laser.enabled = (this.laserFrameIndex != 0 || this.giantzilla.beamEnabled);
	}

	// Token: 0x04000A50 RID: 2640
	public Giantzilla giantzilla;

	// Token: 0x04000A51 RID: 2641
	public SpriteRenderer laser;

	// Token: 0x04000A52 RID: 2642
	public Transform laserPoint;

	// Token: 0x04000A53 RID: 2643
	public GameObject joint;

	// Token: 0x04000A54 RID: 2644
	private float rotationSpeed = 50f;

	// Token: 0x04000A55 RID: 2645
	public List<Sprite> laserSprites;

	// Token: 0x04000A56 RID: 2646
	public bool mirrored;

	// Token: 0x04000A57 RID: 2647
	private Vector3 mouse_pos;

	// Token: 0x04000A58 RID: 2648
	private Vector3 object_pos;

	// Token: 0x04000A59 RID: 2649
	private float angle;

	// Token: 0x04000A5A RID: 2650
	private const float laserInterval = 0.07f;

	// Token: 0x04000A5B RID: 2651
	public float laserTimer = 0.07f;

	// Token: 0x04000A5C RID: 2652
	public int laserFrameIndex;
}
