using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class Giantzilla : BaseActorComponent
{
	// Token: 0x06000806 RID: 2054 RVA: 0x00058008 File Offset: 0x00056208
	internal override void create()
	{
		base.create();
		this.bodyPos = new Vector3(0f, 27.8f, 0f);
		this.bodyPosTarget = new Vector3(0f, 27.8f, 0f);
		for (int i = 0; i < base.gameObject.transform.childCount; i++)
		{
			GameObject gameObject = base.gameObject.transform.GetChild(i).gameObject;
			if (gameObject.GetComponent<GiantLeg>() != null)
			{
				gameObject.GetComponent<GiantLeg>().mainBody = this.mainBody;
				gameObject.GetComponent<GiantLeg>().giantzilla = this;
				this.list_legs.Add(gameObject.GetComponent<GiantLeg>());
			}
			if (gameObject.GetComponent<LegJoint>() != null)
			{
				this.list_joints.Add(gameObject.GetComponent<LegJoint>());
			}
		}
		this.actor = base.GetComponent<Actor>();
		this.actor.create();
		Config.setControllableCreature(base.GetComponent<Actor>());
		if (Input.touchSupported)
		{
			WorldTip.showNow("crabzilla_controls_mobile", true, "top", 8f);
		}
		else
		{
			WorldTip.showNow("crabzilla_controls_pc", true, "top", 8f);
		}
		foreach (GiantLeg giantLeg in this.list_legs)
		{
			giantLeg.create();
			giantLeg.Update();
		}
		foreach (LegJoint legJoint in this.list_joints)
		{
			legJoint.create();
			legJoint.LateUpdate();
		}
		this.Update();
		this.arm1.Update();
		this.arm2.Update();
		foreach (GiantLeg giantLeg2 in this.list_legs)
		{
			giantLeg2.moveLeg();
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00058220 File Offset: 0x00056420
	internal void legMoved()
	{
		if (this.bodyPosTimeout > 0f)
		{
			return;
		}
		this.bodyPosTarget.y = 27.8f + Toolbox.randomFloat(-3f, 3f);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00058250 File Offset: 0x00056450
	public void movesLegs(string pTag)
	{
		foreach (GiantLeg giantLeg in this.list_legs)
		{
			if (giantLeg.legTag.CompareTo(pTag) == 0)
			{
				giantLeg.moveLeg();
			}
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x000582B0 File Offset: 0x000564B0
	private void Update()
	{
		if (this.bodyPosTimeout > 0f)
		{
			this.bodyPosTimeout -= Time.deltaTime;
		}
		if (!Config.joyControls)
		{
			this.beamEnabled = Input.GetMouseButton(0);
		}
		else if (this.beamEnabled && !UltimateJoystick.GetJoystickState("JoyRight"))
		{
			this.beamEnabled = false;
		}
		else if (UltimateJoystick.GetTapCount("JoyRight"))
		{
			this.beamEnabled = !this.beamEnabled;
		}
		this.movementVector = Vector2.zero;
		if (!Config.joyControls)
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				this.movementVector.y = 1f;
			}
			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				this.movementVector.y = -1f;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				this.movementVector.x = 1f;
			}
			else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				this.movementVector.x = -1f;
			}
		}
		else
		{
			float verticalAxis = UltimateJoystick.GetVerticalAxis("JoyLeft");
			float horizontalAxis = UltimateJoystick.GetHorizontalAxis("JoyLeft");
			this.movementVector.x = horizontalAxis;
			this.movementVector.y = verticalAxis;
		}
		this.mouthSprite.SetActive(this.beamEnabled);
		if (this.mouthSprite.gameObject.activeSelf)
		{
			this.mouthSprite.GetComponent<SpriteAnimation>().update(Time.deltaTime);
		}
		if (this.movementVector.x > 0f)
		{
			this.bodyRotationTarget.z = this.moveRotationLimit;
		}
		else if (this.movementVector.x < 0f)
		{
			this.bodyRotationTarget.z = -this.moveRotationLimit;
		}
		else
		{
			this.bodyRotationTarget.z = 0f;
		}
		float num = this.world.elapsed * 60f;
		this.bodyRotation = Vector3.MoveTowards(this.bodyRotation, this.bodyRotationTarget, 0.7f * num);
		if (this.movementVector.y > 0f && this.bodyRotation.z > this.moveRotationLimit)
		{
			this.bodyRotation.z = this.moveRotationLimit;
		}
		else if (this.movementVector.y < 0f && this.bodyRotation.z < -this.moveRotationLimit)
		{
			this.bodyRotation.z = -this.moveRotationLimit;
		}
		float z = this.mainBody.transform.localPosition.z;
		this.bodyPos.z = 0f;
		this.bodyPosTarget.z = 0f;
		this.mainBody.transform.localRotation = Quaternion.Euler(this.bodyRotation);
		this.bodyPos = Vector2.MoveTowards(this.bodyPos, this.bodyPosTarget, 0.7f * num);
		this.bodyPos.z = z;
		this.mainBody.transform.localPosition = this.bodyPos;
		if (!object.Equals(this.movementVector, Vector2.zero))
		{
			Vector2 vector = base.transform.position;
			vector = Vector2.MoveTowards(vector, vector + this.movementVector * 0.2f * num, 1f * num);
			Vector3 vector2 = new Vector3(vector.x, vector.y, vector.y - 3f);
			if (vector2.x < 0f)
			{
				vector2.x = 0f;
			}
			if (vector2.y < 0f)
			{
				vector2.y = 0f;
			}
			if (vector2.x > (float)MapBox.width)
			{
				vector2.x = (float)MapBox.width;
			}
			if (vector2.y > (float)MapBox.height)
			{
				vector2.y = (float)MapBox.height;
			}
			base.transform.position = vector2;
		}
		this.actor.currentPosition = base.transform.position;
		this.actor._currentTileDirty = true;
		this.updateArms();
		this.followCamera();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00058708 File Offset: 0x00056908
	private void followCamera()
	{
		Vector3 position = this.world.camera.transform.position;
		position.x = base.transform.position.x;
		position.y = base.transform.position.y;
		float t = 1f / this.world.camera.orthographicSize;
		this.world.camera.transform.position = Vector3.Lerp(this.world.camera.transform.position, position, t);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x000587A4 File Offset: 0x000569A4
	private void updateArms()
	{
		Vector2 vector = Vector3.zero;
		Vector2 vector2 = this.armTarget.transform.position;
		if (!Config.joyControls)
		{
			Vector3 vector3 = this.world.camera.ScreenToWorldPoint(Input.mousePosition);
			vector.x = vector3.x;
			vector.y = vector3.y;
			this.armTarget.transform.position = vector;
			return;
		}
		float verticalAxis = UltimateJoystick.GetVerticalAxis("JoyRight");
		float horizontalAxis = UltimateJoystick.GetHorizontalAxis("JoyRight");
		vector.x = horizontalAxis;
		vector.y = verticalAxis;
		if (!object.Equals(vector, Vector2.zero))
		{
			vector2 = Vector2.MoveTowards(vector2, vector2 + vector * 2f, 1f);
			if (Toolbox.DistVec3(vector2, base.transform.position) > 35f)
			{
				vector2 = Vector2.MoveTowards(base.transform.position, vector2, 35f);
			}
		}
		this.armTarget.transform.position = vector2;
	}

	// Token: 0x04000A66 RID: 2662
	internal List<GiantLeg> list_legs = new List<GiantLeg>();

	// Token: 0x04000A67 RID: 2663
	internal List<LegJoint> list_joints = new List<LegJoint>();

	// Token: 0x04000A68 RID: 2664
	public GiantBody mainBody;

	// Token: 0x04000A69 RID: 2665
	public float legTimeout;

	// Token: 0x04000A6A RID: 2666
	public Vector2 minMax_angle0;

	// Token: 0x04000A6B RID: 2667
	public Vector2 minMax_angle1;

	// Token: 0x04000A6C RID: 2668
	public float distanceLimit = 7f;

	// Token: 0x04000A6D RID: 2669
	public Vector3 lastPosition = Vector3.zero;

	// Token: 0x04000A6E RID: 2670
	public Vector2 movementVector = Vector2.zero;

	// Token: 0x04000A6F RID: 2671
	public GameObject armTarget;

	// Token: 0x04000A70 RID: 2672
	public GameObject mouthSprite;

	// Token: 0x04000A71 RID: 2673
	public bool beamEnabled;

	// Token: 0x04000A72 RID: 2674
	private Vector3 bodyRotationTarget;

	// Token: 0x04000A73 RID: 2675
	private Vector3 bodyRotation;

	// Token: 0x04000A74 RID: 2676
	private float moveRotationLimit = 5f;

	// Token: 0x04000A75 RID: 2677
	private Vector3 bodyPosTarget;

	// Token: 0x04000A76 RID: 2678
	private Vector3 bodyPos;

	// Token: 0x04000A77 RID: 2679
	private float bodyPosTimeout;

	// Token: 0x04000A78 RID: 2680
	public CrabArm arm1;

	// Token: 0x04000A79 RID: 2681
	public CrabArm arm2;

	// Token: 0x04000A7A RID: 2682
	private bool beamActive;
}
