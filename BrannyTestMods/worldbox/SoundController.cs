using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class SoundController : MonoBehaviour
{
	// Token: 0x06000989 RID: 2441 RVA: 0x00064780 File Offset: 0x00062980
	private void Awake()
	{
		this.s = base.GetComponent<AudioSource>();
		this._camera = Camera.main.GetComponent<MoveCamera>();
		this.originVolume = this.s.volume;
		if (this.ambientSound)
		{
			this.s.spatialBlend = 1f;
			this.s.dopplerLevel = 0f;
			this.s.rolloffMode = 0;
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x000647F0 File Offset: 0x000629F0
	internal void play(float pX = 0f, float pY = 0f)
	{
		float num = 1f;
		if (this.ambientSound)
		{
			this.sfxPos = new Vector3(pX, pY, 0f);
			this.sfxPosCamera = this._camera.mainCamera.WorldToViewportPoint(this.sfxPos);
			if (this.sfxPosCamera.x > 0f && this.sfxPosCamera.x < 1f && this.sfxPosCamera.y > 0f)
			{
				float y = this.sfxPosCamera.y;
			}
			if (pX != 0f && pY != 0f)
			{
				num = 1f - this._camera.mainCamera.orthographicSize / this._camera.orthographicSizeMax * 0.7f;
				num = Mathf.Clamp(num, 0f, 1f);
			}
		}
		if (this.clips != null && this.clips.Count > 0)
		{
			this.s.clip = Toolbox.getRandom<AudioClip>(this.clips);
		}
		base.gameObject.SetActive(true);
		this.s.volume = this.originVolume * num;
		this.s.pitch = this.originPitch + Toolbox.randomFloat(-this.randomizePitch, this.randomizePitch);
		this.s.transform.position = new Vector3(pX, pY);
		this.s.Play();
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00064960 File Offset: 0x00062B60
	internal void update(float pElapsed)
	{
		if (this.timeout > 0f)
		{
			this.timeout -= pElapsed;
		}
		if (this.ambientSound)
		{
			this.sfxPosCamera = this._camera.mainCamera.WorldToViewportPoint(this.sfxPos);
			if (this.sfxPosCamera.x <= 0f || this.sfxPosCamera.x >= 1f || this.sfxPosCamera.y <= 0f || this.sfxPosCamera.y >= 1f)
			{
			}
			float num = 1f - this._camera.mainCamera.orthographicSize / this._camera.orthographicSizeMax * 0.7f;
			num = Mathf.Clamp(num, 0f, 1f);
			this.s.volume = this.originVolume * num;
		}
	}

	// Token: 0x04000C32 RID: 3122
	public List<AudioClip> clips;

	// Token: 0x04000C33 RID: 3123
	public bool soundEnabled = true;

	// Token: 0x04000C34 RID: 3124
	public bool ambientSound;

	// Token: 0x04000C35 RID: 3125
	internal int curCopies;

	// Token: 0x04000C36 RID: 3126
	public int copies;

	// Token: 0x04000C37 RID: 3127
	public float randomizePitch;

	// Token: 0x04000C38 RID: 3128
	internal float timeout;

	// Token: 0x04000C39 RID: 3129
	public float timeoutInterval;

	// Token: 0x04000C3A RID: 3130
	public float originPitch = 1f;

	// Token: 0x04000C3B RID: 3131
	internal AudioSource s;

	// Token: 0x04000C3C RID: 3132
	private MoveCamera _camera;

	// Token: 0x04000C3D RID: 3133
	private float maxDistance = 50f;

	// Token: 0x04000C3E RID: 3134
	private float originVolume = 1f;

	// Token: 0x04000C3F RID: 3135
	private Vector3 sfxPos;

	// Token: 0x04000C40 RID: 3136
	private Vector3 sfxPosCamera;
}
