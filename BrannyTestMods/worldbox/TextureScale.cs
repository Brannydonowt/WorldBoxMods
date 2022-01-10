using System;
using System.Threading;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class TextureScale
{
	// Token: 0x06001153 RID: 4435 RVA: 0x0009814F File Offset: 0x0009634F
	public static void Point(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, false);
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0009815A File Offset: 0x0009635A
	public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, true);
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00098168 File Offset: 0x00096368
	private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
	{
		TextureScale.texColors = tex.GetPixels();
		TextureScale.newColors = new Color[newWidth * newHeight];
		if (useBilinear)
		{
			TextureScale.ratioX = 1f / ((float)newWidth / (float)(tex.width - 1));
			TextureScale.ratioY = 1f / ((float)newHeight / (float)(tex.height - 1));
		}
		else
		{
			TextureScale.ratioX = (float)tex.width / (float)newWidth;
			TextureScale.ratioY = (float)tex.height / (float)newHeight;
		}
		TextureScale.w = tex.width;
		TextureScale.w2 = newWidth;
		int num = Mathf.Min(SystemInfo.processorCount, newHeight);
		int num2 = newHeight / num;
		TextureScale.finishCount = 0;
		if (TextureScale.mutex == null)
		{
			TextureScale.mutex = new Mutex(false);
		}
		if (num > 1)
		{
			int i;
			TextureScale.ThreadData threadData;
			for (i = 0; i < num - 1; i++)
			{
				threadData = new TextureScale.ThreadData(num2 * i, num2 * (i + 1));
				new Thread(useBilinear ? new ParameterizedThreadStart(TextureScale.BilinearScale) : new ParameterizedThreadStart(TextureScale.PointScale)).Start(threadData);
			}
			threadData = new TextureScale.ThreadData(num2 * i, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(threadData);
			}
			else
			{
				TextureScale.PointScale(threadData);
			}
			while (TextureScale.finishCount < num)
			{
				Thread.Sleep(1);
			}
		}
		else
		{
			TextureScale.ThreadData obj = new TextureScale.ThreadData(0, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(obj);
			}
			else
			{
				TextureScale.PointScale(obj);
			}
		}
		tex.Resize(newWidth, newHeight);
		tex.SetPixels(TextureScale.newColors);
		tex.Apply();
		TextureScale.texColors = null;
		TextureScale.newColors = null;
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x000982D0 File Offset: 0x000964D0
	public static void BilinearScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)Mathf.Floor((float)i * TextureScale.ratioY);
			int num2 = num * TextureScale.w;
			int num3 = (num + 1) * TextureScale.w;
			int num4 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				int num5 = (int)Mathf.Floor((float)j * TextureScale.ratioX);
				float value = (float)j * TextureScale.ratioX - (float)num5;
				TextureScale.newColors[num4 + j] = TextureScale.ColorLerpUnclamped(TextureScale.ColorLerpUnclamped(TextureScale.texColors[num2 + num5], TextureScale.texColors[num2 + num5 + 1], value), TextureScale.ColorLerpUnclamped(TextureScale.texColors[num3 + num5], TextureScale.texColors[num3 + num5 + 1], value), (float)i * TextureScale.ratioY - (float)num);
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000983F4 File Offset: 0x000965F4
	public static void PointScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)(TextureScale.ratioY * (float)i) * TextureScale.w;
			int num2 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				TextureScale.newColors[num2 + j] = TextureScale.texColors[(int)((float)num + TextureScale.ratioX * (float)j)];
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x00098490 File Offset: 0x00096690
	private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
	{
		return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value, c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
	}

	// Token: 0x04001476 RID: 5238
	private static Color[] texColors;

	// Token: 0x04001477 RID: 5239
	private static Color[] newColors;

	// Token: 0x04001478 RID: 5240
	private static int w;

	// Token: 0x04001479 RID: 5241
	private static float ratioX;

	// Token: 0x0400147A RID: 5242
	private static float ratioY;

	// Token: 0x0400147B RID: 5243
	private static int w2;

	// Token: 0x0400147C RID: 5244
	private static int finishCount;

	// Token: 0x0400147D RID: 5245
	private static Mutex mutex;

	// Token: 0x02000412 RID: 1042
	public class ThreadData
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x000AF798 File Offset: 0x000AD998
		public ThreadData(int s, int e)
		{
			this.start = s;
			this.end = e;
		}

		// Token: 0x0400166D RID: 5741
		public int start;

		// Token: 0x0400166E RID: 5742
		public int end;
	}
}
