using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class MouseCursor : MonoBehaviour
{
	// Token: 0x06000FDC RID: 4060 RVA: 0x0008C7D8 File Offset: 0x0008A9D8
	private void Awake()
	{
		if (!Input.mousePresent)
		{
			Object.Destroy(this);
			return;
		}
		MouseCursor.cursors = new Texture2D[]
		{
			this.mouseCursorDefault,
			this.mouseCursorDown,
			this.mouseCursorUp1,
			this.mouseCursorUp2,
			this.mouseCursorUp3,
			this.mouseCursorUp4,
			this.mouseCursorHold,
			this.mouseCursorDrag,
			this.mouseCursorPinkie1,
			this.mouseCursorPinkie2,
			this.mouseCursorPinkie3,
			this.mouseCursorPinkie4,
			this.mouseCursorPinkie5
		};
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0008C878 File Offset: 0x0008AA78
	private void OnEnable()
	{
		this.setCursor(0);
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0008C884 File Offset: 0x0008AA84
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			if (!MouseCursor.pressed)
			{
				MouseCursor.pressing = 0;
				MouseCursor.counter = 0;
			}
			MouseCursor.pressed = true;
			MouseCursor.animDone = false;
			MouseCursor._right = Input.GetMouseButtonDown(1);
		}
		else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
		{
			MouseCursor.pressed = false;
			MouseCursor.pressing = 0;
			MouseCursor._right = Input.GetMouseButtonUp(1);
		}
		else if (MouseCursor.pressed)
		{
			MouseCursor.pressing++;
		}
		if (!MouseCursor.pressed && MouseCursor.animDone)
		{
			if (MouseCursor._right)
			{
				this.setCursor(8);
			}
			else
			{
				this.setCursor(0);
			}
			MouseCursor.counter = 0;
		}
		else if (MouseCursor.pressed && MouseCursor.pressing > 3)
		{
			if (!MapBox.instance.isOverUI() || MapBox.instance.isPointerOverUIScroll())
			{
				if (MouseCursor.pressing < 10)
				{
					this.setCursor(6);
				}
				else
				{
					this.setCursor(7);
				}
			}
			else if (MouseCursor._right)
			{
				this.setCursor(8);
			}
			else
			{
				this.setCursor(1);
			}
		}
		else if (MouseCursor._right)
		{
			int num = Mathf.CeilToInt((float)(MouseCursor.counter / 4));
			if (num > 0 && num < 5)
			{
				this.setCursor(num + 8);
			}
			else
			{
				this.setCursor(8);
			}
			MouseCursor.counter++;
			if (MouseCursor.counter > 40)
			{
				MouseCursor.counter = 0;
				if (!MouseCursor.pressed)
				{
					MouseCursor.animDone = true;
				}
			}
		}
		else
		{
			int num2 = Mathf.CeilToInt((float)(MouseCursor.counter / 4));
			if (num2 > 0 && num2 < 6)
			{
				this.setCursor(num2);
			}
			else
			{
				this.setCursor(0);
			}
			MouseCursor.counter++;
			if (MouseCursor.counter > 40)
			{
				MouseCursor.counter = 0;
				if (!MouseCursor.pressed)
				{
					MouseCursor.animDone = true;
				}
			}
		}
		this.renderCursor();
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0008CA60 File Offset: 0x0008AC60
	private void renderCursorTest()
	{
		this._powerID = string.Empty;
		string text = string.Empty;
		if (this._selectedCursorTexture != null)
		{
			text = this._selectedCursorTexture.GetHashCode().ToString();
		}
		if (!ScrollWindow.isWindowActive() && !MapBox.instance.isOverUI())
		{
			this._powerID = MapBox.instance.getSelectedPower();
			text += this._powerID;
		}
		if (this._lastTextureID == text)
		{
			return;
		}
		Texture2D texture2D = null;
		this._cached_textures.TryGetValue(text, ref texture2D);
		if (texture2D == null)
		{
			Texture2D texture2D2 = this.generateNewTexture();
			this._cached_textures.Add(text, texture2D2);
			this._cached_textures.TryGetValue(text, ref texture2D);
		}
		if (texture2D == null)
		{
			return;
		}
		Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto);
		this._lastTextureID = text;
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0008CB3C File Offset: 0x0008AD3C
	private void renderCursor()
	{
		string text = string.Empty;
		if (this._selectedCursorTexture != null)
		{
			text = this._selectedCursorTexture.GetHashCode().ToString();
		}
		if (this._lastTextureID == text)
		{
			return;
		}
		Cursor.SetCursor(this._selectedCursorTexture, Vector2.zero, CursorMode.Auto);
		this._lastTextureID = text;
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0008CB98 File Offset: 0x0008AD98
	private Texture2D generateNewTexture()
	{
		int num = this._selectedCursorTexture.width;
		int width = this._selectedCursorTexture.width;
		if (!string.IsNullOrEmpty(this._powerID))
		{
			num *= 2;
		}
		Texture2D texture2D = new Texture2D(num, width, TextureFormat.RGBA32, false);
		Color32[] pixels = texture2D.GetPixels32();
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = Toolbox.color_log_good;
		}
		texture2D.SetPixels32(pixels);
		int num2 = 0;
		int pOffsetY = 0;
		this.draw(texture2D, this._selectedCursorTexture, num2, pOffsetY);
		num2 += this._selectedCursorTexture.width;
		if (!string.IsNullOrEmpty(this._powerID))
		{
			GodPower godPower = AssetManager.powers.get(this._powerID);
			if (!string.IsNullOrEmpty(godPower.icon))
			{
				Sprite iconSprite = godPower.getIconSprite();
				this.draw(texture2D, iconSprite.texture, num2, pOffsetY);
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0008CC80 File Offset: 0x0008AE80
	private void draw(Texture2D pCanvasTexture, Texture2D pBrush, int pOffsetX, int pOffsetY)
	{
		for (int i = 0; i < pBrush.width; i++)
		{
			for (int j = 0; j < pBrush.height; j++)
			{
				Color32 c = pBrush.GetPixel(i, j);
				int x = i + pOffsetX;
				int y = j + pOffsetY;
				pCanvasTexture.SetPixel(x, y, c);
			}
		}
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0008CCD6 File Offset: 0x0008AED6
	private void setCursor(int cursor = -1)
	{
		MouseCursor.curCursor = cursor;
		this._selectedCursorTexture = MouseCursor.cursors[cursor];
		if (this._selectedCursorTexture == null)
		{
			this._selectedCursorTexture = this.mouseCursorDefault;
		}
	}

	// Token: 0x040012F2 RID: 4850
	public Texture2D mouseCursorDefault;

	// Token: 0x040012F3 RID: 4851
	public Texture2D mouseCursorDown;

	// Token: 0x040012F4 RID: 4852
	public Texture2D mouseCursorUp1;

	// Token: 0x040012F5 RID: 4853
	public Texture2D mouseCursorUp2;

	// Token: 0x040012F6 RID: 4854
	public Texture2D mouseCursorUp3;

	// Token: 0x040012F7 RID: 4855
	public Texture2D mouseCursorUp4;

	// Token: 0x040012F8 RID: 4856
	public Texture2D mouseCursorHold;

	// Token: 0x040012F9 RID: 4857
	public Texture2D mouseCursorDrag;

	// Token: 0x040012FA RID: 4858
	public Texture2D mouseCursorPinkie1;

	// Token: 0x040012FB RID: 4859
	public Texture2D mouseCursorPinkie2;

	// Token: 0x040012FC RID: 4860
	public Texture2D mouseCursorPinkie3;

	// Token: 0x040012FD RID: 4861
	public Texture2D mouseCursorPinkie4;

	// Token: 0x040012FE RID: 4862
	public Texture2D mouseCursorPinkie5;

	// Token: 0x040012FF RID: 4863
	private static int counter = 0;

	// Token: 0x04001300 RID: 4864
	private static bool pressed = false;

	// Token: 0x04001301 RID: 4865
	private static int pressing = 0;

	// Token: 0x04001302 RID: 4866
	private static bool _right = false;

	// Token: 0x04001303 RID: 4867
	private static bool animDone = true;

	// Token: 0x04001304 RID: 4868
	private static int curCursor = -1;

	// Token: 0x04001305 RID: 4869
	private static Texture2D[] cursors;

	// Token: 0x04001306 RID: 4870
	private Dictionary<string, Texture2D> _cached_textures = new Dictionary<string, Texture2D>();

	// Token: 0x04001307 RID: 4871
	private Texture2D _selectedCursorTexture;

	// Token: 0x04001308 RID: 4872
	public SpriteRenderer power;

	// Token: 0x04001309 RID: 4873
	public SpriteRenderer brushSize;

	// Token: 0x0400130A RID: 4874
	private string _lastTextureID = string.Empty;

	// Token: 0x0400130B RID: 4875
	private string _powerID = string.Empty;
}
