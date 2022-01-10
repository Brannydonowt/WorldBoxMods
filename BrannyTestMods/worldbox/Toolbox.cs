using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x020002FC RID: 764
public static class Toolbox
{
	// Token: 0x0600115A RID: 4442 RVA: 0x00098502 File Offset: 0x00096702
	public static string coloredText(string pText, string pColor, bool pLocalize = false)
	{
		if (pLocalize)
		{
			pText = LocalizedTextManager.getText(pText, null);
		}
		return string.Concat(new string[]
		{
			"<color=",
			pColor,
			">",
			pText,
			"</color>"
		});
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0009853B File Offset: 0x0009673B
	public static bool areColorsEqual(Color32 pC1, Color32 pC2)
	{
		return pC1.r == pC2.r && pC1.g == pC2.g && pC1.b == pC2.b;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x00098569 File Offset: 0x00096769
	public static bool inBounds(float pVal, float pMin, float pMax)
	{
		return pVal > pMin && pVal < pMax;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x00098575 File Offset: 0x00096775
	public static void init()
	{
		Toolbox.color_plague = Toolbox.makeColor("#CE4A9B");
		Toolbox.color_infected = Toolbox.makeColor("#35CC6E");
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00098598 File Offset: 0x00096798
	public static string firstLetterToUpper(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length > 1)
		{
			return char.ToUpper(str[0]).ToString() + str.Substring(1);
		}
		return str.ToUpper();
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000985DA File Offset: 0x000967DA
	public static Vector3 RotatePointAroundPivot(ref Vector3 point, ref Vector3 pivot, ref Vector3 angles)
	{
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x00098608 File Offset: 0x00096808
	public static Vector3 RotatePointAroundPivot2(ref Vector3 point, ref Vector3 pivot, ref Vector3 angles)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.Euler(angles) * vector;
		point = vector + pivot;
		return point;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00098654 File Offset: 0x00096854
	public static Vector3 cubeBezier3(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, float t)
	{
		float num = 1f - t;
		float num2 = num * num * num;
		float num3 = num * num * t * 3f;
		float num4 = num * t * t * 3f;
		float num5 = t * t * t;
		return new Vector3(num2 * p0.x + num3 * p1.x + num4 * p2.x + num5 * p3.x, num2 * p0.y + num3 * p1.y + num4 * p2.y + num5 * p3.y, num2 * p0.z + num3 * p1.z + num4 * p2.z + num5 * p3.z);
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x00098708 File Offset: 0x00096908
	public static string encode(string pString)
	{
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		string str = "WorldboxIsAwesome";
		pString = Encryption.EncryptString(pString, str + "555" + deviceUniqueIdentifier);
		return pString;
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x00098738 File Offset: 0x00096938
	public static float easeInOutQuart(float x)
	{
		if (x < 0.5f)
		{
			return 8f * x * x * x * x;
		}
		return 1f - (float)Math.Pow((double)(-2f * x + 2f), 4.0) / 2f;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00098784 File Offset: 0x00096984
	public static string decode(string pString)
	{
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		string str = "WorldboxIsAwesome";
		pString = Encryption.DecryptString(pString, str + "555" + deviceUniqueIdentifier);
		return pString;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000987B2 File Offset: 0x000969B2
	public static string generateID_old()
	{
		return Toolbox.shortGUID(Guid.NewGuid());
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000987BE File Offset: 0x000969BE
	public static string shortGUID(Guid guid)
	{
		return Convert.ToBase64String(guid.ToByteArray()).Replace('+', '-').Replace('/', '_').Substring(0, 8);
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000987E8 File Offset: 0x000969E8
	public static Vector3 getNewPoint(float pX1, float pY1, float pX2, float pY2, float pDist, bool pConvertNegative = true)
	{
		Vector3 result = default(Vector3);
		float num = Toolbox.Dist(pX1, pY1, pX2, pY2);
		if (num - pDist == 0f)
		{
			result.Set(pX2, pY2, 0f);
			return result;
		}
		float num2 = pDist / (num - pDist);
		if (pConvertNegative && num2 < 0f)
		{
			num2 = -num2;
		}
		float x = (pX1 + num2 * pX2) / (1f + num2);
		float y = (pY1 + num2 * pY2) / (1f + num2);
		result.x = x;
		result.y = y;
		return result;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0009886E File Offset: 0x00096A6E
	public static float DistVec3(Vector3 pT1, Vector3 pT2)
	{
		return Mathf.Sqrt((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y));
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000988AC File Offset: 0x00096AAC
	public static float DistVec2(Vector2Int pT1, Vector2Int pT2)
	{
		return Mathf.Sqrt((float)((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y)));
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000988FE File Offset: 0x00096AFE
	public static float DistVec2Float(Vector2 pT1, Vector2 pT2)
	{
		return Mathf.Sqrt((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y));
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0009893C File Offset: 0x00096B3C
	public static float DistTile(WorldTile pT1, WorldTile pT2)
	{
		return Mathf.Sqrt((float)((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y)));
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0009897B File Offset: 0x00096B7B
	public static float Dist(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00098991 File Offset: 0x00096B91
	public static bool randomChance(float pVal)
	{
		return pVal >= Random.value;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0009899E File Offset: 0x00096B9E
	public static bool randomBool()
	{
		return (double)Random.value > 0.5;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000989B1 File Offset: 0x00096BB1
	public static float randomFloat(float pMin, float pMax)
	{
		return Random.Range(pMin, pMax);
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x000989BC File Offset: 0x00096BBC
	public static Vector2 randomPointOnCircle(int pRadiusMin, int pRadiusMax)
	{
		return Random.insideUnitCircle.normalized * (float)Random.Range(pRadiusMin, pRadiusMax);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000989E3 File Offset: 0x00096BE3
	public static int randomInt(int pMinInclusive, int pMaxExclusive)
	{
		return Random.Range(pMinInclusive, pMaxExclusive);
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000989EC File Offset: 0x00096BEC
	public static IEnumerable<TKey> getRandomFromDict<TKey, TValue>(IDictionary<TKey, TValue> dict)
	{
		List<TKey> values = Enumerable.ToList<TKey>(dict.Keys);
		int size = dict.Count;
		for (;;)
		{
			yield return values[Toolbox.rand.Next(size)];
		}
		yield break;
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000989FC File Offset: 0x00096BFC
	public static WorldTile getRandomDictTile(Dictionary<WorldTile, bool> dict)
	{
		return dict.ElementAt(Toolbox.rand.Next(0, dict.Count)).Key;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x00098A28 File Offset: 0x00096C28
	public static T getRandom<T>(T[] pArray)
	{
		return pArray[Random.Range(0, pArray.Length)];
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x00098A3C File Offset: 0x00096C3C
	public static T getRandom<T>(List<T> pList)
	{
		if (pList.Count == 0)
		{
			return default(T);
		}
		return pList[Random.Range(0, pList.Count)];
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x00098A70 File Offset: 0x00096C70
	public static T RandomEnumValue<T>()
	{
		Array values = Enum.GetValues(typeof(T));
		return (T)((object)values.GetValue(Toolbox.randomInt(0, values.Length)));
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x00098AA4 File Offset: 0x00096CA4
	public static Color getRandomColor()
	{
		return new Color(Random.value, Random.value, Random.value, 1f);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00098AC0 File Offset: 0x00096CC0
	public static Color makeColor(string pH)
	{
		Color result;
		ColorUtility.TryParseHtmlString(pH, out result);
		return result;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x00098AD7 File Offset: 0x00096CD7
	public static string colorToHex(Color32 pColor, bool pAlpha = true)
	{
		if (pAlpha)
		{
			return "#" + ColorUtility.ToHtmlStringRGBA(pColor);
		}
		return "#" + ColorUtility.ToHtmlStringRGB(pColor);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x00098B08 File Offset: 0x00096D08
	public static string coloredString(string pText, Color32? pColor)
	{
		if (pColor == null)
		{
			return pText;
		}
		string pColor2 = Toolbox.colorToHex(pColor.Value, true);
		return Toolbox.coloredString(pText, pColor2);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x00098B35 File Offset: 0x00096D35
	public static string coloredString(string pText, string pColor)
	{
		if (pColor == null)
		{
			return pText;
		}
		return string.Concat(new string[]
		{
			"<color=",
			pColor,
			">",
			pText,
			"</color>"
		});
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00098B68 File Offset: 0x00096D68
	public static float getAngle(float pX1, float pY1, float pX2, float pY2)
	{
		float num = pX2 - pX1;
		return (float)Math.Atan2((double)(pY2 - pY1), (double)num);
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x00098B85 File Offset: 0x00096D85
	public static Color makeDarkerColor(Color pColor, float pMod = 0.4f)
	{
		return new Color(pColor.r * pMod, pColor.g * pMod, pColor.b * pMod, pColor.a);
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00098BAC File Offset: 0x00096DAC
	public static Color blendColor(Color pFrom, Color pTo, float amount)
	{
		float r = pFrom.r * amount + pTo.r * (1f - amount);
		float g = pFrom.g * amount + pTo.g * (1f - amount);
		float b = pFrom.b * amount + pTo.b * (1f - amount);
		return new Color(r, g, b);
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00098C08 File Offset: 0x00096E08
	public static WorldTile getClosestTile(List<WorldTile> pArray, WorldTile pTarget)
	{
		WorldTile worldTile = null;
		float num = 10000f;
		for (int i = 0; i < pArray.Count; i++)
		{
			WorldTile worldTile2 = pArray[i];
			float num2 = Toolbox.Dist((float)pTarget.pos.x, (float)pTarget.pos.y, (float)worldTile2.pos.x, (float)worldTile2.pos.y);
			if (worldTile == null || num2 < num)
			{
				num = num2;
				worldTile = worldTile2;
			}
		}
		return worldTile;
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x00098C94 File Offset: 0x00096E94
	public static WorldTile getRandomTileWithinDistance(List<WorldTile> pArray, WorldTile pTarget, float minDist, float maxDist)
	{
		List<WorldTile> list = new List<WorldTile>();
		for (int i = 0; i < pArray.Count; i++)
		{
			WorldTile worldTile = pArray[i];
			float num = Toolbox.Dist((float)pTarget.pos.x, (float)pTarget.pos.y, (float)worldTile.pos.x, (float)worldTile.pos.y);
			if (num > minDist && num < maxDist)
			{
				list.Add(worldTile);
			}
		}
		return Toolbox.getRandom<WorldTile>(list);
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00098D1C File Offset: 0x00096F1C
	public static Actor getClosestActor(List<Actor> pArray, WorldTile pTarget)
	{
		Actor actor = null;
		float num = 10000f;
		for (int i = 0; i < pArray.Count; i++)
		{
			Actor actor2 = pArray[i];
			WorldTile currentTile = actor2.currentTile;
			float num2 = Toolbox.Dist((float)pTarget.pos.x, (float)pTarget.pos.y, (float)currentTile.pos.x, (float)currentTile.pos.y);
			if (actor == null || num2 < num)
			{
				num = num2;
				actor = actor2;
			}
		}
		return actor;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00098DB8 File Offset: 0x00096FB8
	public static Task<byte[]> ReadAllBytes(string filePath)
	{
		Toolbox.<ReadAllBytes>d__69 <ReadAllBytes>d__;
		<ReadAllBytes>d__.filePath = filePath;
		<ReadAllBytes>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
		<ReadAllBytes>d__.<>1__state = -1;
		AsyncTaskMethodBuilder<byte[]> <>t__builder = <ReadAllBytes>d__.<>t__builder;
		<>t__builder.Start<Toolbox.<ReadAllBytes>d__69>(ref <ReadAllBytes>d__);
		return <ReadAllBytes>d__.<>t__builder.Task;
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00098E00 File Offset: 0x00097000
	public static Sprite LoadSprite(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		if (File.Exists(path))
		{
			byte[] array = File.ReadAllBytes(path);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.anisoLevel = 0;
			ImageConversion.LoadImage(texture2D, array);
			return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
		}
		return null;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00098E74 File Offset: 0x00097074
	public static Sprite LoadResizedSprite(string path, int width, int height)
	{
		Sprite sprite = Toolbox.LoadSprite(path);
		if (sprite == null)
		{
			return null;
		}
		Sprite result = Sprite.Create(Toolbox.ScaleTexture(sprite.texture, width, height), new Rect(0f, 0f, (float)width, (float)height), new Vector2(0f, 0f));
		Object.DestroyImmediate(sprite.texture);
		Object.DestroyImmediate(sprite);
		return result;
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00098ED8 File Offset: 0x000970D8
	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] pixels = texture2D.GetPixels(0);
		float num = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
		float num2 = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor((float)(i / targetWidth)));
		}
		texture2D.SetPixels(pixels, 0);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00098F74 File Offset: 0x00097174
	public static string formatTimer(float pTime)
	{
		int num = (int)(pTime / 60f);
		int num2 = (int)(pTime - (float)(num * 60));
		string text;
		if (num < 10)
		{
			text = "0" + num.ToString() + ":";
		}
		else
		{
			text = num.ToString() + ":";
		}
		if (num2 < 10)
		{
			text = text + "0" + num2.ToString();
		}
		else
		{
			text += num2.ToString();
		}
		return text;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00098FF4 File Offset: 0x000971F4
	public static string formatTime(float pTime)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)pTime);
		string text = ((int)timeSpan.TotalHours).ToString() ?? "";
		if (timeSpan.Minutes < 10)
		{
			text = text + ":0" + timeSpan.Minutes.ToString();
		}
		else
		{
			text = text + ":" + timeSpan.Minutes.ToString();
		}
		if (timeSpan.Seconds < 10)
		{
			text = text + ":0" + timeSpan.Seconds.ToString();
		}
		else
		{
			text = text + ":" + timeSpan.Seconds.ToString();
		}
		return text;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000990B0 File Offset: 0x000972B0
	internal static MapChunk getRandomChunkFromTile(WorldTile pTile)
	{
		Toolbox.temp_list_chunks.Clear();
		Toolbox.temp_list_chunks.Add(pTile.chunk);
		Toolbox.temp_list_chunks.AddRange(pTile.chunk.neighboursAll);
		return Toolbox.temp_list_chunks.GetRandom<MapChunk>();
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000990EB File Offset: 0x000972EB
	internal static WorldTile getRandomTileAround(WorldTile pTile)
	{
		return Toolbox.getRandomChunkFromTile(pTile).tiles.GetRandom<WorldTile>();
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x000990FD File Offset: 0x000972FD
	internal static List<MapChunk> getAllChunksFromTile(WorldTile pTile)
	{
		Toolbox.temp_list_chunks.Clear();
		Toolbox.temp_list_chunks.Add(pTile.chunk);
		Toolbox.temp_list_chunks.AddRange(pTile.chunk.neighboursAll);
		return Toolbox.temp_list_chunks;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x00099134 File Offset: 0x00097334
	internal static void findNotSameRaceInChunkAround(MapChunk pChunk, string pRace)
	{
		Toolbox.temp_list_objects.Clear();
		Toolbox.temp_list_units.Clear();
		Toolbox.fillChunkWithUnits(pChunk);
		for (int i = 0; i < pChunk.neighboursAll.Count; i++)
		{
			Toolbox.fillChunkWithUnits(pChunk.neighboursAll[i]);
		}
		for (int j = 0; j < Toolbox.temp_list_objects.Count; j++)
		{
			List<BaseSimObject> list = Toolbox.temp_list_objects[j];
			for (int k = 0; k < list.Count; k++)
			{
				BaseSimObject baseSimObject = list[k];
				if (!(baseSimObject == null) && baseSimObject.isActor() && !baseSimObject.a.isRace(pRace))
				{
					Toolbox.temp_list_units.Add(baseSimObject.a);
				}
			}
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000991F0 File Offset: 0x000973F0
	internal static void findSameUnitInChunkAround(MapChunk pChunk, string pUnitID)
	{
		Toolbox.temp_list_objects.Clear();
		Toolbox.temp_list_units.Clear();
		Toolbox.fillChunkWithUnits(pChunk);
		for (int i = 0; i < pChunk.neighboursAll.Count; i++)
		{
			Toolbox.fillChunkWithUnits(pChunk.neighboursAll[i]);
		}
		for (int j = 0; j < Toolbox.temp_list_objects.Count; j++)
		{
			List<BaseSimObject> list = Toolbox.temp_list_objects[j];
			for (int k = 0; k < list.Count; k++)
			{
				BaseSimObject baseSimObject = list[k];
				if (!(baseSimObject == null) && baseSimObject.isActor() && !(baseSimObject.a.stats.id != pUnitID))
				{
					Toolbox.temp_list_units.Add(baseSimObject.a);
				}
			}
		}
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000992B8 File Offset: 0x000974B8
	internal static void findSameRaceInChunkAround(MapChunk pChunk, string pRace)
	{
		Toolbox.temp_list_objects.Clear();
		Toolbox.temp_list_units.Clear();
		Toolbox.fillChunkWithUnits(pChunk);
		for (int i = 0; i < pChunk.neighboursAll.Count; i++)
		{
			Toolbox.fillChunkWithUnits(pChunk.neighboursAll[i]);
		}
		for (int j = 0; j < Toolbox.temp_list_objects.Count; j++)
		{
			List<BaseSimObject> list = Toolbox.temp_list_objects[j];
			for (int k = 0; k < list.Count; k++)
			{
				BaseSimObject baseSimObject = list[k];
				if (!(baseSimObject == null) && baseSimObject.isActor() && baseSimObject.a.isRace(pRace))
				{
					Toolbox.temp_list_units.Add(baseSimObject.a);
				}
			}
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x00099374 File Offset: 0x00097574
	internal static void fillChunkWithUnits(MapChunk pChunk)
	{
		if (pChunk.k_list_objects.Count == 0)
		{
			return;
		}
		int count = pChunk.k_list_objects.Count;
		for (int i = 0; i < count; i++)
		{
			Kingdom kingdom = pChunk.k_list_objects[i];
			if (kingdom != null)
			{
				Toolbox.temp_list_objects.Add(pChunk.k_dict_objects[kingdom]);
			}
		}
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000993D0 File Offset: 0x000975D0
	internal static void fillListWithUnitsFromChunk(MapChunk pChunk, List<Actor> pList)
	{
		if (pChunk.k_list_objects.Count == 0)
		{
			return;
		}
		int count = pChunk.k_list_objects.Count;
		for (int i = 0; i < count; i++)
		{
			Kingdom kingdom = pChunk.k_list_objects[i];
			if (kingdom != null)
			{
				List<BaseSimObject> list = pChunk.k_dict_objects[kingdom];
				for (int j = 0; j < list.Count; j++)
				{
					BaseSimObject baseSimObject = list[j];
					if (!(baseSimObject == null) && baseSimObject.isActor())
					{
						pList.Add(baseSimObject.a);
					}
				}
			}
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00099460 File Offset: 0x00097660
	internal static void findEnemiesOfKingdomInChunk(MapChunk pChunk, Kingdom pMainKingdom)
	{
		if (pChunk.k_list_objects.Count == 0)
		{
			return;
		}
		if (pMainKingdom == null)
		{
			return;
		}
		int count = pChunk.k_list_objects.Count;
		for (int i = 0; i < count; i++)
		{
			Kingdom kingdom = pChunk.k_list_objects[i];
			if (kingdom != null && ((!kingdom.asset.mobs && !pMainKingdom.asset.mobs) || !MapBox.instance.worldLaws.world_law_peaceful_monsters.boolVal) && pMainKingdom.isEnemy(kingdom))
			{
				Toolbox.temp_list_objects_enemies.Add(pChunk.k_dict_objects[kingdom]);
			}
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000994F6 File Offset: 0x000976F6
	internal static bool inMapBorder(Vector3 point)
	{
		return point.x <= (float)MapBox.width && point.y <= (float)MapBox.height && point.x >= 0f && point.y >= 0f;
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x00099534 File Offset: 0x00097734
	internal static void addToTempChunksBuildingsType(MapChunk pChunk, string pType, bool pOnlyNonTargeted = false)
	{
		if (pChunk.k_list_objects.Count == 0)
		{
			return;
		}
		for (int i = 0; i < pChunk.k_list_objects.Count; i++)
		{
			Kingdom kingdom = pChunk.k_list_objects[i];
			List<BaseSimObject> list = pChunk.k_dict_objects[kingdom];
			for (int j = 0; j < list.Count; j++)
			{
				BaseSimObject baseSimObject = list[j];
				if (!(baseSimObject == null) && baseSimObject.isBuilding() && baseSimObject.b.data.alive && baseSimObject.b.haveResources && !(baseSimObject.b.currentTile.targetedBy != null) && baseSimObject.b.stats.type == pType)
				{
					Toolbox.temp_list_buildings_2.Add(baseSimObject.b);
				}
			}
		}
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0009961C File Offset: 0x0009781C
	public static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00099644 File Offset: 0x00097844
	public static string LowerCaseFirst(string pString)
	{
		if (pString.Length == 0)
		{
			return "";
		}
		return char.ToLower(pString[0]).ToString() + ((pString.Length > 1) ? pString.Substring(1) : string.Empty);
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x0009968F File Offset: 0x0009788F
	public static T[] resizeArray<T>(T[] pArray, int aPos)
	{
		Array.Resize<T>(ref pArray, aPos);
		return pArray;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0009969C File Offset: 0x0009789C
	public static string getRoundedTimestamp()
	{
		DateTime utcNow = DateTime.UtcNow;
		new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		string str = (utcNow.Month < 10) ? ("0" + utcNow.Month.ToString()) : utcNow.Month.ToString();
		string str2 = (utcNow.Day < 10) ? ("0" + utcNow.Day.ToString()) : utcNow.Day.ToString();
		return utcNow.Year.ToString() + str + str2;
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x00099744 File Offset: 0x00097944
	public static List<string> getDirectories(string pPath)
	{
		List<string> list = new List<string>();
		foreach (string text in Directory.GetDirectories(pPath))
		{
			if (!text.Contains(".meta"))
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x00099788 File Offset: 0x00097988
	public static List<string> getFiles(string pPath)
	{
		List<string> list = new List<string>();
		foreach (string text in Directory.GetFiles(pPath))
		{
			if (!text.Contains(".meta"))
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000997CC File Offset: 0x000979CC
	public static string cacheBuster()
	{
		return DateTime.UtcNow.RoundMinutes().ToFileTime().ToString() + "_" + Config.versionCodeText;
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x00099802 File Offset: 0x00097A02
	public static DateTime RoundMinutes(this DateTime value)
	{
		return value.RoundMinutes(30);
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0009980C File Offset: 0x00097A0C
	public static DateTime RoundMinutes(this DateTime value, int roundMinutes)
	{
		DateTime result = new DateTime(value.Ticks);
		int minute = value.Minute;
		int hour = value.Hour;
		int num = minute % roundMinutes;
		if (num <= roundMinutes / 2)
		{
			result = result.AddMinutes((double)(-(double)num));
		}
		else
		{
			result = result.AddMinutes((double)(roundMinutes - num));
		}
		return result;
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00099859 File Offset: 0x00097A59
	public static string textureID(string pStringData, string pID)
	{
		return Encryption.EncryptString(pStringData, pID);
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00099862 File Offset: 0x00097A62
	public static float bench(string pID)
	{
		return ToolBenchmark.bench(pID);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0009986A File Offset: 0x00097A6A
	public static void benchCounterSet(string pID, float pVal)
	{
		ToolBenchmark.benchSet(pID, pVal);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x00099873 File Offset: 0x00097A73
	public static float benchEnd(string pID)
	{
		return ToolBenchmark.benchEnd(pID);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0009987B File Offset: 0x00097A7B
	public static int getBenchCounter(string pID)
	{
		return ToolBenchmark.getBenchCounter(pID);
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x00099883 File Offset: 0x00097A83
	public static string getBenchResult(string pID, bool pAverage = true)
	{
		return ToolBenchmark.getBenchResult(pID, pAverage);
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x0009988C File Offset: 0x00097A8C
	public static void printBenchResult(string pID, bool pAverage = true)
	{
		ToolBenchmark.printBenchResult(pID, pAverage);
	}

	// Token: 0x0400147E RID: 5246
	public static readonly ActorDirection[] directions = new ActorDirection[]
	{
		ActorDirection.Up,
		ActorDirection.Right,
		ActorDirection.Down,
		ActorDirection.Left
	};

	// Token: 0x0400147F RID: 5247
	public static readonly ActorDirection[] directions_all = new ActorDirection[]
	{
		ActorDirection.Up,
		ActorDirection.UpRight,
		ActorDirection.UpLeft,
		ActorDirection.Right,
		ActorDirection.DownRight,
		ActorDirection.DownLeft,
		ActorDirection.Down,
		ActorDirection.Left
	};

	// Token: 0x04001480 RID: 5248
	public static Color32 EVERYTHING_MAGIC_COLOR32 = Toolbox.makeColor("#DF7FFF");

	// Token: 0x04001481 RID: 5249
	public static Color32 color_green_0 = Toolbox.makeColor("#B8FF96");

	// Token: 0x04001482 RID: 5250
	public static Color32 color_green_1 = Toolbox.makeColor("#00FF00");

	// Token: 0x04001483 RID: 5251
	public static Color32 color_green_2 = Toolbox.makeColor("#00AF00");

	// Token: 0x04001484 RID: 5252
	public static Color32 color_green_3 = Toolbox.makeColor("#4A831F");

	// Token: 0x04001485 RID: 5253
	public static Color32 color_magenta_0 = Toolbox.makeColor("#FF00FF");

	// Token: 0x04001486 RID: 5254
	public static Color32 color_magenta_1 = Toolbox.makeColor("#DE00DE");

	// Token: 0x04001487 RID: 5255
	public static Color32 color_magenta_2 = Toolbox.makeColor("#A700A7");

	// Token: 0x04001488 RID: 5256
	public static Color32 color_magenta_3 = Toolbox.makeColor("#7F007F");

	// Token: 0x04001489 RID: 5257
	public static Color32 color_magenta_4 = Toolbox.makeColor("#580058");

	// Token: 0x0400148A RID: 5258
	public static Color32 color_clear = Color.clear;

	// Token: 0x0400148B RID: 5259
	public static Color color_white = Color.white;

	// Token: 0x0400148C RID: 5260
	public static Color32 color_white_32 = Color.white;

	// Token: 0x0400148D RID: 5261
	public static Color color_red = Color.red;

	// Token: 0x0400148E RID: 5262
	public static Color color_yellow = Color.yellow;

	// Token: 0x0400148F RID: 5263
	public static Color color_abandoned_building = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x04001490 RID: 5264
	public static string color_positive = "#43FF43";

	// Token: 0x04001491 RID: 5265
	public static string color_negative = "#FB2C21";

	// Token: 0x04001492 RID: 5266
	public static Color color_log_good = Toolbox.makeColor("#95DD5D");

	// Token: 0x04001493 RID: 5267
	public static Color color_log_warning = Toolbox.makeColor("#FF8686");

	// Token: 0x04001494 RID: 5268
	public static Color color_log_neutral = Toolbox.makeColor("#F3961F");

	// Token: 0x04001495 RID: 5269
	public static Color color_heal = Toolbox.makeColor("#23F3FF");

	// Token: 0x04001496 RID: 5270
	public static Color color_plague = Toolbox.makeColor("#CE4A9B");

	// Token: 0x04001497 RID: 5271
	public static Color color_mushSpores = Toolbox.makeColor("#8CFF99");

	// Token: 0x04001498 RID: 5272
	public static Color color_infected = Toolbox.makeColor("#35CC6E");

	// Token: 0x04001499 RID: 5273
	public static Color32 clear;

	// Token: 0x0400149A RID: 5274
	private static Random rand = new Random();

	// Token: 0x0400149B RID: 5275
	internal static List<Actor> temp_list_units = new List<Actor>();

	// Token: 0x0400149C RID: 5276
	internal static List<WorldTile> temp_list_tiles = new List<WorldTile>();

	// Token: 0x0400149D RID: 5277
	internal static List<MapChunk> temp_list_chunks = new List<MapChunk>();

	// Token: 0x0400149E RID: 5278
	internal static List<List<BaseSimObject>> temp_list_objects = new List<List<BaseSimObject>>();

	// Token: 0x0400149F RID: 5279
	internal static List<List<Building>> temp_list_buildings = new List<List<Building>>();

	// Token: 0x040014A0 RID: 5280
	internal static List<Building> temp_list_buildings_2 = new List<Building>();

	// Token: 0x040014A1 RID: 5281
	internal static List<List<BaseSimObject>> temp_list_objects_enemies = new List<List<BaseSimObject>>();

	// Token: 0x040014A2 RID: 5282
	internal static MapChunk temp_list_objects_enemies_chunk = null;

	// Token: 0x040014A3 RID: 5283
	internal static Kingdom temp_list_objects_enemies_kingdom = null;
}
