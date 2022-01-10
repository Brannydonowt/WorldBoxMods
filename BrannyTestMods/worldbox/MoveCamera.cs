using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class MoveCamera : BaseMapObject
{
	// Token: 0x06001139 RID: 4409 RVA: 0x00096934 File Offset: 0x00094B34
	private void Awake()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x00096941 File Offset: 0x00094B41
	internal override void create()
	{
		base.create();
		this.resetZoom();
		this.ResetCamera = this.mainCamera.transform.position;
		this.targetZoom = this.mainCamera.orthographicSize;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x00096978 File Offset: 0x00094B78
	internal void focusOn(Vector3 pPos)
	{
		this.clearFocusUnit();
		this.targetZoom = 15f;
		this.focusZoom = this.targetZoom;
		pPos.z = base.transform.position.z;
		base.transform.position = pPos;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000969C8 File Offset: 0x00094BC8
	internal void focusOn(Vector3 pPos, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		this.clearFocusUnit();
		this.targetZoom = 15f;
		this.focusZoom = this.targetZoom;
		this.focusReachedCallback = pFocusReachedCallback;
		this.focusCancelCallback = pFocusCancelCallback;
		pPos.z = base.transform.position.z;
		base.transform.position = pPos;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x00096A24 File Offset: 0x00094C24
	internal void focusOnAndFollow(Actor pActor, Action pFocusReachedCallback, Action pFocusCancelCallback)
	{
		this.clearFocusUnit();
		this.targetZoom = 15f;
		this.focusZoom = this.targetZoom;
		this.focusUnitZoom = this.targetZoom;
		this.focusReachedCallback = pFocusReachedCallback;
		this.focusCancelCallback = pFocusCancelCallback;
		MoveCamera.focusUnit = pActor;
		this.focusTimer = 0f;
		WorldTip.addWordReplacement("$name$", MoveCamera.focusUnit.coloredName);
		WorldTip.showNowTop("tip_following_unit");
		PowerButtonSelector.instance.setPower(PowerButtonSelector.instance.followUnit);
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x00096AAC File Offset: 0x00094CAC
	internal void resetZoom()
	{
		int num;
		if (Screen.width < Screen.height)
		{
			num = Screen.width / 4;
		}
		else
		{
			num = Screen.height / 4;
		}
		if (MapBox.width > MapBox.height)
		{
			this.orthographicSizeMax = (float)((int)((float)MapBox.width * 1.1f));
		}
		else
		{
			this.orthographicSizeMax = (float)((int)((float)MapBox.height * 1.1f));
		}
		if ((float)num > this.orthographicSizeMax)
		{
			num = (int)this.orthographicSizeMax;
		}
		this.targetZoom = (float)num;
		this.mainCamera.orthographicSize = Mathf.Clamp(this.targetZoom, 10f, this.orthographicSizeMax);
		this.world.zoomUpdated(this.mainCamera.orthographicSize, this.orthographicSizeMax);
		this.mainCamera.farClipPlane = (float)MapBox.height * 1.1f;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00096B7C File Offset: 0x00094D7C
	private void updateZoom()
	{
		if (Input.touchSupported)
		{
			bool flag = false;
			if (UltimateJoystick.getJoyCount() == 2)
			{
				flag = (UltimateJoystick.GetJoystickState("JoyRight") || UltimateJoystick.GetJoystickState("JoyLeft"));
			}
			if (flag)
			{
				return;
			}
			if (Input.touchCount == 2 && !this.world.alreadyUsedPower)
			{
				this.world.alreadyUsedZoom = true;
				this.touchZero = Input.GetTouch(0);
				this.touchOne = Input.GetTouch(1);
				this.touchZeroPrevPos = this.touchZero.position - this.touchZero.deltaPosition;
				this.touchOnePrevPos = this.touchOne.position - this.touchOne.deltaPosition;
				float magnitude = (this.touchZeroPrevPos - this.touchOnePrevPos).magnitude;
				float magnitude2 = (this.touchZero.position - this.touchOne.position).magnitude;
				float num = magnitude - magnitude2;
				this.targetZoom += num * 0.2f * (this.mainCamera.orthographicSize * 0.015f);
			}
		}
		if (Input.anyKey)
		{
			if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
			{
				this.targetZoom -= this.mainCamera.orthographicSize * 0.05f;
			}
			else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
			{
				this.targetZoom += this.mainCamera.orthographicSize * 0.05f;
			}
		}
		if (Input.mousePresent)
		{
			float num2 = this.mainCamera.orthographicSize * 0.2f;
			if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				this.targetZoom += num2;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				this.targetZoom -= num2;
			}
		}
		if (this.targetZoom != this.mainCamera.orthographicSize)
		{
			this.zoomToBounds();
		}
		if (this.focusZoom != -1000000f)
		{
			this.checkFocusReached();
		}
		if (Config.spectatorMode)
		{
			this.followFocusUnit();
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00096DB8 File Offset: 0x00094FB8
	private void checkFocusReached()
	{
		if (this.mainCamera.orthographicSize == this.focusZoom)
		{
			if (this.focusReachedCallback != null)
			{
				this.focusReachedCallback();
			}
			this.clearFocus();
		}
		if (this.targetZoom != this.focusZoom)
		{
			if (this.focusCancelCallback != null)
			{
				this.focusCancelCallback();
			}
			this.clearFocus();
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x00096E18 File Offset: 0x00095018
	private void followFocusUnit()
	{
		if (MoveCamera.focusUnit == null)
		{
			return;
		}
		if (!MoveCamera.focusUnit.data.alive)
		{
			if (MoveCamera.focusUnit.attackedBy != null)
			{
				Actor actor = MoveCamera.focusUnit;
				Object x;
				if (actor == null)
				{
					x = null;
				}
				else
				{
					BaseSimObject attackedBy = actor.attackedBy;
					x = ((attackedBy != null) ? attackedBy.a : null);
				}
				if (x != null)
				{
					WorldTip.addWordReplacement("$name$", MoveCamera.focusUnit.coloredName);
					WorldTip.addWordReplacement("$killer$", MoveCamera.focusUnit.attackedBy.a.coloredName);
					WorldTip.showNowTop("tip_followed_unit_killed");
					Actor actor2 = MoveCamera.focusUnit;
					Actor actor3;
					if (actor2 == null)
					{
						actor3 = null;
					}
					else
					{
						BaseSimObject attackedBy2 = actor2.attackedBy;
						actor3 = ((attackedBy2 != null) ? attackedBy2.a : null);
					}
					MoveCamera.focusUnit.attackedBy = null;
					MoveCamera.focusUnit = actor3;
					this.focusTimer = 0f;
					return;
				}
			}
			WorldTip.addWordReplacement("$name$", MoveCamera.focusUnit.coloredName);
			WorldTip.showNowTop("tip_followed_unit_died");
			this.clearFocusUnit();
			return;
		}
		if (MoveCamera.cameraDragRun || Input.touchCount > 0)
		{
			this.focusTimer = 0f;
			return;
		}
		Vector3 vector = MoveCamera.focusUnit.currentPosition;
		vector.z = base.transform.position.z;
		if (this.focusTimer <= 1f)
		{
			this.focusTimer += Time.deltaTime;
			this.focusTimer = Mathf.Clamp(this.focusTimer, 0f, 1f);
			vector.x = iTween.easeOutCubic(base.transform.position.x, vector.x, this.focusTimer);
			vector.y = iTween.easeOutCubic(base.transform.position.y, vector.y, this.focusTimer);
		}
		base.transform.position = vector;
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x00096FF3 File Offset: 0x000951F3
	private void clearFocus()
	{
		this.focusReachedCallback = null;
		this.focusCancelCallback = null;
		this.focusZoom = -1000000f;
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x0009700E File Offset: 0x0009520E
	private void clearFocusUnit()
	{
		this.focusUnitZoom = -1000000f;
		MoveCamera.focusUnit = null;
		this.focusTimer = 0f;
		if (PowerButtonSelector.instance.isPowerSelected("follow_unit"))
		{
			PowerButtonSelector.instance.unselectAll();
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x00097048 File Offset: 0x00095248
	private void zoomToBounds()
	{
		this.targetZoom = Mathf.Clamp(this.targetZoom, 10f, this.orthographicSizeMax);
		if (this.mainCamera.orthographicSize == this.targetZoom)
		{
			return;
		}
		if (this.targetZoom > this.mainCamera.orthographicSize)
		{
			this.mainCamera.orthographicSize += Time.deltaTime * this.cameraZoomSpeed * (Mathf.Abs(this.mainCamera.orthographicSize - this.targetZoom) + 5f);
			if (this.mainCamera.orthographicSize > this.targetZoom)
			{
				this.mainCamera.orthographicSize = Mathf.Clamp(this.targetZoom, 10f, this.orthographicSizeMax);
			}
		}
		else if (this.targetZoom < this.mainCamera.orthographicSize)
		{
			this.mainCamera.orthographicSize -= Time.deltaTime * this.cameraZoomSpeed * (Mathf.Abs(this.mainCamera.orthographicSize - this.targetZoom) + 5f);
			if (this.mainCamera.orthographicSize < this.targetZoom)
			{
				this.mainCamera.orthographicSize = Mathf.Clamp(this.targetZoom, 10f, this.orthographicSizeMax);
			}
		}
		this.world.zoomUpdated(this.mainCamera.orthographicSize, this.orthographicSizeMax);
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x000971B0 File Offset: 0x000953B0
	private void updateMouseCameraDrag()
	{
		MoveCamera.cameraDragRun = false;
		bool flag = false;
		bool flag2 = false;
		if (Input.mousePresent)
		{
			if (this.world.selectedButtons.isPowerSelected())
			{
				flag = (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1));
				flag2 = (Input.GetMouseButton(2) || Input.GetMouseButton(1));
			}
			else
			{
				flag = (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1));
				flag2 = (Input.GetMouseButton(0) || Input.GetMouseButton(2) || Input.GetMouseButton(1));
			}
		}
		if (!flag2)
		{
			this.clearTouches();
			return;
		}
		if (flag && this.world.isOverUI())
		{
			this.clearTouches();
			return;
		}
		if (flag && this.Origin.x == -1f && this.Origin.z == -1f)
		{
			this.Origin = this.MousePos();
		}
		if (this.Origin.x == -1f && this.Origin.y == -1f && this.Origin.z == -1f)
		{
			return;
		}
		if (flag2)
		{
			MoveCamera.cameraDragRun = true;
			this.Difference = this.MousePos() - base.transform.position;
			if (Toolbox.DistVec3(this.Origin, this.MousePos()) > 0.1f)
			{
				MoveCamera.cameraDragActivated = true;
			}
			Vector3 vector = this.Origin - this.Difference;
			if (Input.touchSupported)
			{
				MoveCamera.distDebug = Toolbox.DistVec3(this.firstTouch, this.TouchPos(true));
				if (this.world.touchTimer > 0.06f)
				{
					if (MoveCamera.distDebug >= 20f || this.world.touchTimer > 0.3f)
					{
						this.world.alreadyUsedZoom = true;
						this.world.alreadyUsedPower = false;
					}
				}
				else if (Input.touchCount == 1)
				{
					return;
				}
			}
			MoveCamera.debugDragDiff = Toolbox.DistVec3(base.transform.position, vector) / this.mainCamera.orthographicSize;
			if (!Input.mousePresent)
			{
				return;
			}
			base.transform.position = vector;
			MoveCamera.cameraDragNew = vector.ToString();
			this.cameraToBounds();
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000973DC File Offset: 0x000955DC
	private void clearTouches()
	{
		this.firstTouch.Set(-1f, -1f, -1f);
		this.Origin.Set(-1f, -1f, -1f);
		MoveCamera.cameraDragActivated = false;
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x00097418 File Offset: 0x00095618
	private void cameraToBounds()
	{
		Vector3 position = default(Vector3);
		position.x = Mathf.Clamp(base.transform.position.x, 0f, (float)MapBox.width);
		position.y = Mathf.Clamp(base.transform.position.y, 0f, (float)MapBox.height);
		position.z = -0.5f;
		base.transform.position = position;
		MapNamesManager.instance.update();
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x000974A0 File Offset: 0x000956A0
	private Vector3 TouchPos(bool pScreenCoords = false)
	{
		Vector2 a = default(Vector2);
		int num = 0;
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase != 4 && touch.phase != 3)
			{
				a += touch.position;
				num++;
			}
		}
		Vector3 vector = a / (float)num;
		if (pScreenCoords)
		{
			return vector;
		}
		return this.mainCamera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x0009751E File Offset: 0x0009571E
	private Vector3 MousePos()
	{
		if (Input.mousePresent)
		{
			return this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}
		return Vector3.one;
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x00097540 File Offset: 0x00095740
	private void LateUpdate()
	{
		if (this.world.tutorial.gameObject.activeSelf)
		{
			return;
		}
		if (Input.touchCount > 0)
		{
			if (Input.touches[0].phase == null && this.world.isOverUI())
			{
				this.firstTouchOnUI = true;
			}
		}
		else
		{
			this.firstTouchOnUI = false;
		}
		if (!ScrollWindow.isWindowActive() && !this.world.isOverUI())
		{
			this.updateZoom();
		}
		if (this.world.isGameplayControlsLocked() || ScrollWindow.animationActive || this.firstTouchOnUI)
		{
			this.clearTouches();
			this.oldTouchPositions[0] = null;
			this.oldTouchPositions[1] = null;
			return;
		}
		MoveCamera.debugDragDiff = 0f;
		if (Input.touchSupported)
		{
			this.updateMobileCamera();
		}
		if (Input.mousePresent && (!Input.touchSupported || Input.touchCount <= 0))
		{
			this.updateMouseCameraDrag();
			if (!ScrollWindow.isWindowActive() && Config.controllableUnit == null)
			{
				float num = Time.deltaTime * 55f;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					num *= 2.5f;
				}
				bool flag = false;
				Vector3 position = base.transform.position;
				if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				{
					this.keyMoveVelocity.y = this.keyMoveVelocity.y + num * this.targetZoom * this.cameraMoveSpeed;
					if (this.keyMoveVelocity.y > this.targetZoom * this.cameraMoveMax)
					{
						this.keyMoveVelocity.y = this.targetZoom * this.cameraMoveMax;
					}
				}
				else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				{
					this.keyMoveVelocity.y = this.keyMoveVelocity.y - num * this.targetZoom * this.cameraMoveSpeed;
					if (this.keyMoveVelocity.y < -this.targetZoom * this.cameraMoveMax)
					{
						this.keyMoveVelocity.y = -this.targetZoom * this.cameraMoveMax;
					}
				}
				if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				{
					this.keyMoveVelocity.x = this.keyMoveVelocity.x + num * this.targetZoom * this.cameraMoveSpeed;
					if (this.keyMoveVelocity.x > this.targetZoom * this.cameraMoveMax)
					{
						this.keyMoveVelocity.x = this.targetZoom * this.cameraMoveMax;
					}
				}
				else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				{
					this.keyMoveVelocity.x = this.keyMoveVelocity.x - num * this.targetZoom * this.cameraMoveSpeed;
					if (this.keyMoveVelocity.x < -this.targetZoom * this.cameraMoveMax)
					{
						this.keyMoveVelocity.x = -this.targetZoom * this.cameraMoveMax;
					}
				}
				if (this.keyMoveVelocity.x != 0f || this.keyMoveVelocity.y != 0f)
				{
					base.transform.position += this.keyMoveVelocity;
					flag = true;
					this.keyMoveVelocity *= 0.8f;
					if (Mathf.Abs(this.keyMoveVelocity.x) < 0.01f)
					{
						this.keyMoveVelocity.x = 0f;
					}
					if (Mathf.Abs(this.keyMoveVelocity.y) < 0.01f)
					{
						this.keyMoveVelocity.y = 0f;
					}
				}
				if (flag)
				{
					this.cameraToBounds();
				}
			}
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000978D2 File Offset: 0x00095AD2
	private bool ignoreTouchControlls()
	{
		return this.world.isOverUI() || ScrollWindow.isWindowActive() || ScrollWindow.animationActive;
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000978F0 File Offset: 0x00095AF0
	private void updateMobileCamera()
	{
		if (Input.touchCount == 0)
		{
			this.oldTouchPositions[0] = null;
			this.oldTouchPositions[1] = null;
			return;
		}
		if (this.world.alreadyUsedPower)
		{
			return;
		}
		if (Input.touchCount == 1)
		{
			if (this.oldTouchPositions[0] == null || this.oldTouchPositions[1] != null)
			{
				this.oldTouchPositions[0] = new Vector2?(Input.GetTouch(0).position);
				this.oldTouchPositions[1] = null;
			}
			else
			{
				Vector2 position = Input.GetTouch(0).position;
				Transform transform = base.transform;
				Vector3 position2 = transform.position;
				Transform transform2 = base.transform;
				Vector2? vector = (this.oldTouchPositions[0] - position) * this.mainCamera.orthographicSize;
				float d = (float)this.mainCamera.pixelHeight;
				transform.position = position2 + transform2.TransformDirection(((vector != null) ? new Vector2?(vector.GetValueOrDefault() / d * 2f) : null).Value);
				this.oldTouchPositions[0] = new Vector2?(position);
				this.cameraToBounds();
			}
			if (this.world.touchTimer > 0.06f && (MoveCamera.distDebug >= 20f || this.world.touchTimer > 0.3f))
			{
				this.world.alreadyUsedZoom = true;
				return;
			}
		}
		else
		{
			if (this.oldTouchPositions[1] == null)
			{
				this.oldTouchPositions[0] = new Vector2?(Input.GetTouch(0).position);
				this.oldTouchPositions[1] = new Vector2?(Input.GetTouch(1).position);
				this.oldTouchVector = (this.oldTouchPositions[0] - this.oldTouchPositions[1]).Value;
				this.oldTouchDistance = this.oldTouchVector.magnitude;
				return;
			}
			Vector2 vector2 = new Vector2((float)this.mainCamera.pixelWidth, (float)this.mainCamera.pixelHeight);
			Vector2[] array = new Vector2[]
			{
				Input.GetTouch(0).position,
				Input.GetTouch(1).position
			};
			Vector2 vector3 = array[0] - array[1];
			float magnitude = vector3.magnitude;
			base.transform.position += base.transform.TransformDirection(((this.oldTouchPositions[0] + this.oldTouchPositions[1] - vector2) * this.mainCamera.orthographicSize / vector2.y).Value);
			if (this.oldTouchDistance != magnitude && magnitude != 0f)
			{
				this.mainCamera.orthographicSize = Mathf.Clamp(this.mainCamera.orthographicSize * (this.oldTouchDistance / magnitude), 10f, this.orthographicSizeMax);
			}
			this.world.zoomUpdated(this.mainCamera.orthographicSize, this.orthographicSizeMax);
			base.transform.position -= base.transform.TransformDirection((array[0] + array[1] - vector2) * this.mainCamera.orthographicSize / vector2.y);
			this.cameraToBounds();
			this.oldTouchPositions[0] = new Vector2?(array[0]);
			this.oldTouchPositions[1] = new Vector2?(array[1]);
			this.oldTouchVector = vector3;
			this.oldTouchDistance = magnitude;
			this.world.alreadyUsedZoom = true;
		}
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x00097E44 File Offset: 0x00096044
	public void debug(DebugTool pTool)
	{
		pTool.setText("Input.touchCount:", Input.touchCount);
		pTool.setText("world.isGameplayControlsLocked():", this.world.isGameplayControlsLocked());
		pTool.setText("ScrollWindow.animationActive:", ScrollWindow.animationActive);
		pTool.setText("firstTouchOnUI", this.firstTouchOnUI);
		pTool.setText("world.alreadyUsedZoom", this.world.alreadyUsedZoom);
		pTool.setText("world.alreadyUsedPower", this.world.alreadyUsedPower);
		if (UltimateJoystick.getJoyCount() == 2)
		{
			pTool.setText("JoyRight", UltimateJoystick.GetJoystickState("JoyRight"));
			pTool.setText("JoyLeft", UltimateJoystick.GetJoystickState("JoyLeft"));
		}
	}

	// Token: 0x04001453 RID: 5203
	private Vector3 ResetCamera;

	// Token: 0x04001454 RID: 5204
	private Vector3 Origin;

	// Token: 0x04001455 RID: 5205
	private Vector3 Difference;

	// Token: 0x04001456 RID: 5206
	public bool movementEnabled = true;

	// Token: 0x04001457 RID: 5207
	private bool isZooming;

	// Token: 0x04001458 RID: 5208
	public float zoom;

	// Token: 0x04001459 RID: 5209
	private const int orthographicSizeMin = 10;

	// Token: 0x0400145A RID: 5210
	internal float orthographicSizeMax = 130f;

	// Token: 0x0400145B RID: 5211
	private float targetZoom;

	// Token: 0x0400145C RID: 5212
	private Vector3 firstTouch;

	// Token: 0x0400145D RID: 5213
	internal Camera mainCamera;

	// Token: 0x0400145E RID: 5214
	private Action focusReachedCallback;

	// Token: 0x0400145F RID: 5215
	private Action focusCancelCallback;

	// Token: 0x04001460 RID: 5216
	private float focusZoom = -1000000f;

	// Token: 0x04001461 RID: 5217
	private float focusUnitZoom = -1000000f;

	// Token: 0x04001462 RID: 5218
	private float focusTimer;

	// Token: 0x04001463 RID: 5219
	public static Actor focusUnit = null;

	// Token: 0x04001464 RID: 5220
	private Touch touchZero;

	// Token: 0x04001465 RID: 5221
	private Touch touchOne;

	// Token: 0x04001466 RID: 5222
	private Vector2 touchZeroPrevPos;

	// Token: 0x04001467 RID: 5223
	private Vector2 touchOnePrevPos;

	// Token: 0x04001468 RID: 5224
	public static bool cameraDragActivated;

	// Token: 0x04001469 RID: 5225
	public static bool cameraDragRun;

	// Token: 0x0400146A RID: 5226
	public static string cameraDragNew;

	// Token: 0x0400146B RID: 5227
	public static float debugDragDiff = 0f;

	// Token: 0x0400146C RID: 5228
	public static float distDebug = 0f;

	// Token: 0x0400146D RID: 5229
	public static string debugString = "";

	// Token: 0x0400146E RID: 5230
	private bool firstTouchOnUI;

	// Token: 0x0400146F RID: 5231
	public float cameraZoomSpeed = 5f;

	// Token: 0x04001470 RID: 5232
	public float cameraMoveSpeed = 0.01f;

	// Token: 0x04001471 RID: 5233
	public float cameraMoveMax = 0.06f;

	// Token: 0x04001472 RID: 5234
	private Vector3 keyMoveVelocity;

	// Token: 0x04001473 RID: 5235
	private Vector2?[] oldTouchPositions = new Vector2?[2];

	// Token: 0x04001474 RID: 5236
	private Vector2 oldTouchVector;

	// Token: 0x04001475 RID: 5237
	private float oldTouchDistance;
}
