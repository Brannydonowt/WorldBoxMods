using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class TouchCamera : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x00003D41 File Offset: 0x00001F41
	private void Awake()
	{
		this._camera = Camera.main;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00003D50 File Offset: 0x00001F50
	private void Update()
	{
		if (Input.touchCount == 0)
		{
			this.oldTouchPositions[0] = null;
			this.oldTouchPositions[1] = null;
			return;
		}
		if (Input.touchCount == 1)
		{
			if (this.oldTouchPositions[0] == null || this.oldTouchPositions[1] != null)
			{
				this.oldTouchPositions[0] = new Vector2?(Input.GetTouch(0).position);
				this.oldTouchPositions[1] = null;
				return;
			}
			Vector2 position = Input.GetTouch(0).position;
			Transform transform = base.transform;
			Vector3 position2 = transform.position;
			Transform transform2 = base.transform;
			Vector2? vector = (this.oldTouchPositions[0] - position) * this._camera.orthographicSize;
			float d = (float)this._camera.pixelHeight;
			transform.position = position2 + transform2.TransformDirection(((vector != null) ? new Vector2?(vector.GetValueOrDefault() / d * 2f) : null).Value);
			this.oldTouchPositions[0] = new Vector2?(position);
			return;
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
			Vector2 vector2 = new Vector2((float)this._camera.pixelWidth, (float)this._camera.pixelHeight);
			Vector2[] array = new Vector2[]
			{
				Input.GetTouch(0).position,
				Input.GetTouch(1).position
			};
			Vector2 vector3 = array[0] - array[1];
			float magnitude = vector3.magnitude;
			base.transform.position += base.transform.TransformDirection(((this.oldTouchPositions[0] + this.oldTouchPositions[1] - vector2) * this._camera.orthographicSize / vector2.y).Value);
			this._camera.orthographicSize = Mathf.Clamp(this._camera.orthographicSize * (this.oldTouchDistance / magnitude), 10f, this.orthographicSizeMax);
			base.transform.position -= base.transform.TransformDirection((array[0] + array[1] - vector2) * this._camera.orthographicSize / vector2.y);
			this.oldTouchPositions[0] = new Vector2?(array[0]);
			this.oldTouchPositions[1] = new Vector2?(array[1]);
			this.oldTouchVector = vector3;
			this.oldTouchDistance = magnitude;
			return;
		}
	}

	// Token: 0x04000027 RID: 39
	private Vector2?[] oldTouchPositions = new Vector2?[2];

	// Token: 0x04000028 RID: 40
	private Vector2 oldTouchVector;

	// Token: 0x04000029 RID: 41
	private float oldTouchDistance;

	// Token: 0x0400002A RID: 42
	private Camera _camera;

	// Token: 0x0400002B RID: 43
	private const int orthographicSizeMin = 10;

	// Token: 0x0400002C RID: 44
	internal float orthographicSizeMax = 130f;
}
