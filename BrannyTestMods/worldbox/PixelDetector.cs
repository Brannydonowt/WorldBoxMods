using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class PixelDetector : ScriptableObject
{
	// Token: 0x06001129 RID: 4393 RVA: 0x000964BC File Offset: 0x000946BC
	public static bool GetSpritePixelColorUnderMousePointer(MonoBehaviour mono, out Vector2Int pVector)
	{
		pVector = new Vector2Int(-1, -1);
		if (PixelDetector.mainCamera == null)
		{
			PixelDetector.mainCamera = Camera.main;
		}
		Vector2 v = Input.mousePosition;
		Vector2 vector = PixelDetector.mainCamera.ScreenToViewportPoint(v);
		if (vector.x <= 0f || vector.x >= 1f || vector.y <= 0f || vector.y >= 1f)
		{
			return false;
		}
		Ray ray;
		try
		{
			ray = PixelDetector.mainCamera.ViewportPointToRay(vector);
		}
		catch (Exception)
		{
			return false;
		}
		return PixelDetector.IntersectsSprite(mono, ray, out pVector);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x00096578 File Offset: 0x00094778
	private static bool IntersectsSprite(MonoBehaviour mono, Ray ray, out Vector2Int pVector)
	{
		SpriteRenderer component = mono.GetComponent<SpriteRenderer>();
		pVector = new Vector2Int(-1, -1);
		if (component == null)
		{
			return false;
		}
		Sprite sprite = component.sprite;
		if (sprite == null)
		{
			return false;
		}
		Texture2D texture = sprite.texture;
		if (texture == null)
		{
			return false;
		}
		if (sprite.packed && sprite.packingMode == SpritePackingMode.Tight)
		{
			Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
			return false;
		}
		Plane plane = new Plane(mono.transform.forward, mono.transform.position);
		float d;
		if (!plane.Raycast(ray, out d))
		{
			return false;
		}
		Vector3 vector = component.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + ray.direction * d);
		Rect textureRect = sprite.textureRect;
		float pixelsPerUnit = sprite.pixelsPerUnit;
		float num = (float)texture.width * 0f;
		float num2 = (float)texture.height * 0f;
		int num3 = (int)(vector.x * pixelsPerUnit + num);
		int num4 = (int)(vector.y * pixelsPerUnit + num2);
		if (num3 < 0 || (float)num3 < textureRect.x || num3 >= Mathf.FloorToInt(textureRect.xMax))
		{
			return false;
		}
		if (num4 < 0 || (float)num4 < textureRect.y || num4 >= Mathf.FloorToInt(textureRect.yMax))
		{
			return false;
		}
		pVector = new Vector2Int(num3, num4);
		return true;
	}

	// Token: 0x04001451 RID: 5201
	private static Camera mainCamera;
}
