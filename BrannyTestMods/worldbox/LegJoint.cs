using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class LegJoint : MonoBehaviour
{
	// Token: 0x0600080D RID: 2061 RVA: 0x00058924 File Offset: 0x00056B24
	internal void create()
	{
		this.length0 = Vector2.Distance(this.Joint0.position, this.Joint1.position);
		this.length1 = Vector2.Distance(this.Joint1.position, this.Hand.position);
		bool flag = this.mirrored;
		this.targetDistance = Vector2.Distance(this.Joint0.position, this.Target.position);
		Vector2 vector = this.Target.position - this.Joint0.position;
		this.atan = -this.main.transform.rotation.eulerAngles.z + Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		float f = (this.targetDistance * this.targetDistance + this.length0 * this.length0 - this.length1 * this.length1) / (2f * this.targetDistance * this.length0);
		this.defaultAngle = Mathf.Acos(f) * 57.29578f;
		this.angleMin = this.defaultAngle + 20f;
		this.angleMax = this.defaultAngle + 20f;
		this.bodyPoint = new GameObject("leg_point_" + base.transform.name)
		{
			transform = 
			{
				position = base.transform.position,
				parent = this.main.mainBody.transform
			}
		}.transform;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00058AE4 File Offset: 0x00056CE4
	public bool isAngleOk(Vector2 pMinMax)
	{
		this.angleMin = this.defaultAngle + pMinMax.x;
		this.angleMax = this.defaultAngle + pMinMax.y;
		bool flag = Toolbox.inBounds(this.angle0, this.angleMin, this.angleMax);
		bool flag2 = Toolbox.inBounds(this.angle1, 65f, 110f);
		Vector2 vector = this.Joint1.transform.position - this.Hand.transform.position;
		this.groundAngle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		flag2 = Toolbox.inBounds(this.groundAngle, this.groundAngleMin, this.groundAngleMax);
		return flag && flag2;
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00058BA8 File Offset: 0x00056DA8
	internal void LateUpdate()
	{
		Vector3 position = this.bodyPoint.position;
		position.z = 0f;
		base.transform.position = position;
		this.targetDistance = Vector2.Distance(this.Joint0.position, this.Target.position);
		this.targetDistanceHand = Vector2.Distance(this.Hand.position, this.Target.position);
		if (this.targetDistanceHand < 1f)
		{
			this.moving = false;
		}
		else
		{
			this.moving = true;
		}
		Vector2 vector = this.Target.position - this.Joint0.position;
		this.atan = -this.main.transform.rotation.eulerAngles.z + Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.tooFar = false;
		if (this.length0 + this.length1 < this.targetDistance)
		{
			this.jointAngle0 = this.atan;
			this.jointAngle1 = 0f;
			this.tooFar = true;
		}
		else
		{
			float f = (this.targetDistance * this.targetDistance + this.length0 * this.length0 - this.length1 * this.length1) / (2f * this.targetDistance * this.length0);
			this.angle0 = Mathf.Acos(f) * 57.29578f;
			float f2 = (this.length1 * this.length1 + this.length0 * this.length0 - this.targetDistance * this.targetDistance) / (2f * this.length1 * this.length0);
			this.angle1 = Mathf.Acos(f2) * 57.29578f;
			if (this.mirrored)
			{
				this.jointAngle0 = this.atan + this.angle0;
				this.jointAngle1 = 180f + this.angle1;
			}
			else
			{
				this.jointAngle0 = this.atan - this.angle0;
				this.jointAngle1 = 180f - this.angle1;
			}
		}
		if (float.IsNaN(this.jointAngle0))
		{
			return;
		}
		Vector3 localEulerAngles = this.Joint0.transform.localEulerAngles;
		localEulerAngles.z = this.jointAngle0;
		this.Joint0.transform.localEulerAngles = localEulerAngles;
		Vector3 localEulerAngles2 = this.Joint1.transform.localEulerAngles;
		localEulerAngles2.z = this.jointAngle1;
		this.Joint1.transform.localEulerAngles = localEulerAngles2;
	}

	// Token: 0x04000A7B RID: 2683
	[Header("Joints")]
	public Transform Joint0;

	// Token: 0x04000A7C RID: 2684
	public Transform Joint1;

	// Token: 0x04000A7D RID: 2685
	public Transform Hand;

	// Token: 0x04000A7E RID: 2686
	[Header("Target")]
	public Transform Target;

	// Token: 0x04000A7F RID: 2687
	private float length0;

	// Token: 0x04000A80 RID: 2688
	private float length1;

	// Token: 0x04000A81 RID: 2689
	public bool tooFar;

	// Token: 0x04000A82 RID: 2690
	public bool moving;

	// Token: 0x04000A83 RID: 2691
	public float targetDistance;

	// Token: 0x04000A84 RID: 2692
	public float targetDistanceHand;

	// Token: 0x04000A85 RID: 2693
	public bool mirrored;

	// Token: 0x04000A86 RID: 2694
	public Giantzilla main;

	// Token: 0x04000A87 RID: 2695
	public float angleMax;

	// Token: 0x04000A88 RID: 2696
	public float angleMin;

	// Token: 0x04000A89 RID: 2697
	public float defaultAngle;

	// Token: 0x04000A8A RID: 2698
	private float atan;

	// Token: 0x04000A8B RID: 2699
	private float jointAngle0;

	// Token: 0x04000A8C RID: 2700
	private float jointAngle1;

	// Token: 0x04000A8D RID: 2701
	public float angle0;

	// Token: 0x04000A8E RID: 2702
	public float angle1;

	// Token: 0x04000A8F RID: 2703
	public float groundAngle;

	// Token: 0x04000A90 RID: 2704
	public float groundAngleMin = 50f;

	// Token: 0x04000A91 RID: 2705
	public float groundAngleMax = 140f;

	// Token: 0x04000A92 RID: 2706
	internal Transform bodyPoint;
}
