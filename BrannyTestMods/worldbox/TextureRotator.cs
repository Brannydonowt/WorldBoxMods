using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class TextureRotator
{
	// Token: 0x06001150 RID: 4432 RVA: 0x00097FB0 File Offset: 0x000961B0
	public static Texture2D Rotate(Texture2D originTexture, int angle)
	{
		Texture2D texture2D = new Texture2D(originTexture.width, originTexture.height);
		Color32[] pixels = texture2D.GetPixels32();
		Color32[] pixels2 = originTexture.GetPixels32();
		int width = originTexture.width;
		int height = originTexture.height;
		int num = 0;
		int num2 = 0;
		Color32[] array = TextureRotator.rotateSquare(pixels2, 0.017453292519943295 * (double)angle, originTexture);
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				pixels[texture2D.width / 2 - width / 2 + num + j + texture2D.width * (texture2D.height / 2 - height / 2 + i + num2)] = array[j + i * width];
			}
		}
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x00098074 File Offset: 0x00096274
	private static Color32[] rotateSquare(Color32[] arr, double phi, Texture2D originTexture)
	{
		double num = Math.Sin(phi);
		double num2 = Math.Cos(phi);
		Color32[] pixels = originTexture.GetPixels32();
		int width = originTexture.width;
		int height = originTexture.height;
		int num3 = width / 2;
		int num4 = height / 2;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				pixels[i * width + j] = new Color32(0, 0, 0, 0);
				int num5 = (int)(num2 * (double)(j - num3) + num * (double)(i - num4) + (double)num3);
				int num6 = (int)(-num * (double)(j - num3) + num2 * (double)(i - num4) + (double)num4);
				if (num5 > -1 && num5 < width && num6 > -1 && num6 < height)
				{
					pixels[i * width + j] = arr[num6 * width + num5];
				}
			}
		}
		return pixels;
	}
}
