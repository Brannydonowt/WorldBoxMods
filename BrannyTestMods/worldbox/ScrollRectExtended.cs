using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000269 RID: 617
[SelectionBase]
[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public class ScrollRectExtended : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0007E89A File Offset: 0x0007CA9A
	// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0007E8A2 File Offset: 0x0007CAA2
	public RectTransform content
	{
		get
		{
			return this.m_Content;
		}
		set
		{
			this.m_Content = value;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0007E8AB File Offset: 0x0007CAAB
	// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0007E8B3 File Offset: 0x0007CAB3
	public bool horizontal
	{
		get
		{
			return this.m_Horizontal;
		}
		set
		{
			this.m_Horizontal = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0007E8BC File Offset: 0x0007CABC
	// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0007E8C4 File Offset: 0x0007CAC4
	public bool vertical
	{
		get
		{
			return this.m_Vertical;
		}
		set
		{
			this.m_Vertical = value;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0007E8CD File Offset: 0x0007CACD
	// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0007E8D5 File Offset: 0x0007CAD5
	public ScrollRectExtended.MovementType movementType
	{
		get
		{
			return this.m_MovementType;
		}
		set
		{
			this.m_MovementType = value;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0007E8DE File Offset: 0x0007CADE
	// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0007E8E6 File Offset: 0x0007CAE6
	public float elasticity
	{
		get
		{
			return this.m_Elasticity;
		}
		set
		{
			this.m_Elasticity = value;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0007E8EF File Offset: 0x0007CAEF
	// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0007E8F7 File Offset: 0x0007CAF7
	public bool inertia
	{
		get
		{
			return this.m_Inertia;
		}
		set
		{
			this.m_Inertia = value;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0007E900 File Offset: 0x0007CB00
	// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0007E908 File Offset: 0x0007CB08
	public float decelerationRate
	{
		get
		{
			return this.m_DecelerationRate;
		}
		set
		{
			this.m_DecelerationRate = value;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0007E911 File Offset: 0x0007CB11
	// (set) Token: 0x06000D5D RID: 3421 RVA: 0x0007E919 File Offset: 0x0007CB19
	public float scrollSensitivity
	{
		get
		{
			return this.m_ScrollSensitivity;
		}
		set
		{
			this.m_ScrollSensitivity = value;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0007E922 File Offset: 0x0007CB22
	// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0007E92A File Offset: 0x0007CB2A
	public RectTransform viewport
	{
		get
		{
			return this.m_Viewport;
		}
		set
		{
			this.m_Viewport = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0007E939 File Offset: 0x0007CB39
	// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0007E941 File Offset: 0x0007CB41
	public float scrollFactor
	{
		get
		{
			return this.m_ScrollFactor;
		}
		set
		{
			this.m_ScrollFactor = value;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0007E94A File Offset: 0x0007CB4A
	// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0007E954 File Offset: 0x0007CB54
	public Scrollbar horizontalScrollbar
	{
		get
		{
			return this.m_HorizontalScrollbar;
		}
		set
		{
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			this.m_HorizontalScrollbar = value;
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			this.SetDirtyCaching();
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0007E9C0 File Offset: 0x0007CBC0
	// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0007E9C8 File Offset: 0x0007CBC8
	public Scrollbar verticalScrollbar
	{
		get
		{
			return this.m_VerticalScrollbar;
		}
		set
		{
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_VerticalScrollbar = value;
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.SetDirtyCaching();
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0007EA34 File Offset: 0x0007CC34
	// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0007EA3C File Offset: 0x0007CC3C
	public ScrollRectExtended.ScrollbarVisibility horizontalScrollbarVisibility
	{
		get
		{
			return this.m_HorizontalScrollbarVisibility;
		}
		set
		{
			this.m_HorizontalScrollbarVisibility = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0007EA4B File Offset: 0x0007CC4B
	// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0007EA53 File Offset: 0x0007CC53
	public ScrollRectExtended.ScrollbarVisibility verticalScrollbarVisibility
	{
		get
		{
			return this.m_VerticalScrollbarVisibility;
		}
		set
		{
			this.m_VerticalScrollbarVisibility = value;
			this.SetDirtyCaching();
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0007EA62 File Offset: 0x0007CC62
	// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0007EA6A File Offset: 0x0007CC6A
	public float horizontalScrollbarSpacing
	{
		get
		{
			return this.m_HorizontalScrollbarSpacing;
		}
		set
		{
			this.m_HorizontalScrollbarSpacing = value;
			this.SetDirty();
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0007EA79 File Offset: 0x0007CC79
	// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0007EA81 File Offset: 0x0007CC81
	public float verticalScrollbarSpacing
	{
		get
		{
			return this.m_VerticalScrollbarSpacing;
		}
		set
		{
			this.m_VerticalScrollbarSpacing = value;
			this.SetDirty();
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0007EA90 File Offset: 0x0007CC90
	// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0007EA98 File Offset: 0x0007CC98
	public ScrollRectExtended.ScrollRectEvent onValueChanged
	{
		get
		{
			return this.m_OnValueChanged;
		}
		set
		{
			this.m_OnValueChanged = value;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0007EAA4 File Offset: 0x0007CCA4
	protected RectTransform viewRect
	{
		get
		{
			if (this.m_ViewRect == null)
			{
				this.m_ViewRect = this.m_Viewport;
			}
			if (this.m_ViewRect == null)
			{
				this.m_ViewRect = (RectTransform)base.transform;
			}
			return this.m_ViewRect;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0007EAF0 File Offset: 0x0007CCF0
	// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0007EAF8 File Offset: 0x0007CCF8
	public Vector2 velocity
	{
		get
		{
			return this.m_Velocity;
		}
		set
		{
			this.m_Velocity = value;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0007EB01 File Offset: 0x0007CD01
	private RectTransform rectTransform
	{
		get
		{
			if (this.m_Rect == null)
			{
				this.m_Rect = base.GetComponent<RectTransform>();
			}
			return this.m_Rect;
		}
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0007EB24 File Offset: 0x0007CD24
	protected ScrollRectExtended()
	{
		this.flexibleWidth = -1f;
		ScrollRectExtended.instance = this;
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0007EBE9 File Offset: 0x0007CDE9
	public virtual void Rebuild(CanvasUpdate executing)
	{
		if (executing == null)
		{
			this.UpdateCachedData();
		}
		if (executing == 2)
		{
			this.UpdateBounds();
			this.UpdateScrollbars(Vector2.zero);
			this.UpdatePrevData();
			this.m_HasRebuiltLayout = true;
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0007EC16 File Offset: 0x0007CE16
	public virtual void LayoutComplete()
	{
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0007EC18 File Offset: 0x0007CE18
	public virtual void GraphicUpdateComplete()
	{
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0007EC1C File Offset: 0x0007CE1C
	private void UpdateCachedData()
	{
		Transform transform = base.transform;
		this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
		this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
		bool flag = this.viewRect.parent == transform;
		bool flag2 = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
		bool flag3 = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
		bool flag4 = flag && flag2 && flag3;
		this.m_HSliderExpand = (flag4 && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRectExtended.ScrollbarVisibility.AutoHideAndExpandViewport);
		this.m_VSliderExpand = (flag4 && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRectExtended.ScrollbarVisibility.AutoHideAndExpandViewport);
		this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
		this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0007ED7C File Offset: 0x0007CF7C
	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.m_HorizontalScrollbar)
		{
			this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
		}
		if (this.m_VerticalScrollbar)
		{
			this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
		}
		CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0007EDE8 File Offset: 0x0007CFE8
	protected override void OnDisable()
	{
		CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
		if (this.m_HorizontalScrollbar)
		{
			this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
		}
		if (this.m_VerticalScrollbar)
		{
			this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
		}
		this.m_HasRebuiltLayout = false;
		this.m_Tracker.Clear();
		this.m_Velocity = Vector2.zero;
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		base.OnDisable();
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0007EE7B File Offset: 0x0007D07B
	public override bool IsActive()
	{
		return base.IsActive() && this.m_Content != null;
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0007EE93 File Offset: 0x0007D093
	private void EnsureLayoutHasRebuilt()
	{
		if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
		{
			Canvas.ForceUpdateCanvases();
		}
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0007EEA9 File Offset: 0x0007D0A9
	public virtual void StopMovement()
	{
		this.m_Velocity = Vector2.zero;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0007EEB8 File Offset: 0x0007D0B8
	public virtual void OnScroll(PointerEventData data)
	{
		if (!this.IsActive())
		{
			return;
		}
		CanvasMain.addTooltipShowTimeout(0.06f);
		this.EnsureLayoutHasRebuilt();
		this.UpdateBounds();
		Vector2 scrollDelta = data.scrollDelta;
		scrollDelta.y *= -1f;
		if (this.vertical && !this.horizontal)
		{
			if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
			{
				scrollDelta.y = scrollDelta.x;
			}
			scrollDelta.x = 0f;
		}
		if (this.horizontal && !this.vertical)
		{
			if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
			{
				scrollDelta.x = scrollDelta.y;
			}
			scrollDelta.y = 0f;
		}
		Vector2 vector = this.m_Content.anchoredPosition;
		vector += scrollDelta * this.m_ScrollSensitivity;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Clamped)
		{
			vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
		}
		this.SetContentAnchoredPosition(vector);
		this.UpdateBounds();
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0007EFD0 File Offset: 0x0007D1D0
	public virtual void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		this.m_Velocity = Vector2.zero;
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0007EFE8 File Offset: 0x0007D1E8
	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		this.UpdateBounds();
		this.m_PointerStartLocalCursor = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, ref this.m_PointerStartLocalCursor);
		this.m_ContentStartPosition = this.m_Content.anchoredPosition;
		this.m_Dragging = true;
		this._check_timer = this._check_timer_interval;
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0007F05A File Offset: 0x0007D25A
	public virtual void OnEndDrag(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		this.m_Dragging = false;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0007F06C File Offset: 0x0007D26C
	public virtual void OnDrag(PointerEventData eventData)
	{
		if (eventData.button != null)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		Vector2 a;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, ref a))
		{
			return;
		}
		this.UpdateBounds();
		Vector2 vector = a - this.m_PointerStartLocalCursor;
		vector *= this.m_ScrollFactor;
		Vector2 vector2 = this.m_ContentStartPosition + vector;
		Vector2 vector3 = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
		vector2 += vector3;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Elastic)
		{
			if (vector3.x != 0f)
			{
				vector2.x -= ScrollRectExtended.RubberDelta(vector3.x, this.m_ViewBounds.size.x);
			}
			if (vector3.y != 0f)
			{
				vector2.y -= ScrollRectExtended.RubberDelta(vector3.y, this.m_ViewBounds.size.y);
			}
		}
		this.SetContentAnchoredPosition(vector2);
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0007F170 File Offset: 0x0007D370
	protected virtual void SetContentAnchoredPosition(Vector2 position)
	{
		if (!this.m_Horizontal)
		{
			position.x = this.m_Content.anchoredPosition.x;
		}
		if (!this.m_Vertical)
		{
			position.y = this.m_Content.anchoredPosition.y;
		}
		if (position != this.m_Content.anchoredPosition)
		{
			this.m_Content.anchoredPosition = position;
			this.UpdateBounds();
		}
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0007F1E0 File Offset: 0x0007D3E0
	private void fixDragHack()
	{
		if (this.m_Dragging && !Input.GetMouseButton(0))
		{
			if (this._check_timer > 0f)
			{
				this._check_timer -= Time.fixedDeltaTime;
				return;
			}
			this.m_Dragging = false;
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0007F21C File Offset: 0x0007D41C
	protected virtual void LateUpdate()
	{
		if (!this.m_Content)
		{
			return;
		}
		this.fixDragHack();
		this.EnsureLayoutHasRebuilt();
		this.UpdateScrollbarVisibility();
		this.UpdateBounds();
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		Vector2 vector = this.CalculateOffset(Vector2.zero);
		if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
		{
			Vector2 vector2 = this.m_Content.anchoredPosition;
			for (int i = 0; i < 2; i++)
			{
				if (this.m_MovementType == ScrollRectExtended.MovementType.Elastic && vector[i] != 0f)
				{
					float value = this.m_Velocity[i];
					vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref value, this.m_Elasticity, float.PositiveInfinity, unscaledDeltaTime);
					this.m_Velocity[i] = value;
				}
				else if (this.m_Inertia)
				{
					ref Vector2 ptr = ref this.m_Velocity;
					int index = i;
					ptr[index] *= Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
					if (Mathf.Abs(this.m_Velocity[i]) < 1f)
					{
						this.m_Velocity[i] = 0f;
					}
					ptr = ref vector2;
					index = i;
					ptr[index] += this.m_Velocity[i] * unscaledDeltaTime;
				}
				else
				{
					this.m_Velocity[i] = 0f;
				}
			}
			if (this.m_Velocity != Vector2.zero)
			{
				if (this.m_MovementType == ScrollRectExtended.MovementType.Clamped)
				{
					vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
					vector2 += vector;
				}
				this.SetContentAnchoredPosition(vector2);
			}
		}
		if (this.m_Dragging && this.m_Inertia)
		{
			Vector3 vector3 = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
			vector3 *= this.velocityMod;
			this.m_Velocity = Vector3.Lerp(this.m_Velocity, vector3, unscaledDeltaTime * 10f);
		}
		if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
		{
			this.UpdateScrollbars(vector);
			this.m_OnValueChanged.Invoke(this.normalizedPosition);
			this.UpdatePrevData();
		}
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
	private void UpdatePrevData()
	{
		if (this.m_Content == null)
		{
			this.m_PrevPosition = Vector2.zero;
		}
		else
		{
			this.m_PrevPosition = this.m_Content.anchoredPosition;
		}
		this.m_PrevViewBounds = this.m_ViewBounds;
		this.m_PrevContentBounds = this.m_ContentBounds;
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0007F524 File Offset: 0x0007D724
	private void UpdateScrollbars(Vector2 offset)
	{
		if (this.m_HorizontalScrollbar)
		{
			if (this.m_ContentBounds.size.x > 0f)
			{
				this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
			}
			else
			{
				this.m_HorizontalScrollbar.size = 1f;
			}
			this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
		}
		if (this.m_VerticalScrollbar)
		{
			if (this.m_ContentBounds.size.y > 0f)
			{
				this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
			}
			else
			{
				this.m_VerticalScrollbar.size = 1f;
			}
			this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0007F639 File Offset: 0x0007D839
	// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0007F64C File Offset: 0x0007D84C
	public Vector2 normalizedPosition
	{
		get
		{
			return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
		}
		set
		{
			this.SetNormalizedPosition(value.x, 0);
			this.SetNormalizedPosition(value.y, 1);
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0007F668 File Offset: 0x0007D868
	// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0007F708 File Offset: 0x0007D908
	public float horizontalNormalizedPosition
	{
		get
		{
			this.UpdateBounds();
			if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x)
			{
				return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
			}
			return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
		}
		set
		{
			this.SetNormalizedPosition(value, 0);
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0007F714 File Offset: 0x0007D914
	// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0007F7B4 File Offset: 0x0007D9B4
	public float verticalNormalizedPosition
	{
		get
		{
			this.UpdateBounds();
			if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y)
			{
				return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
			}
			return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
		}
		set
		{
			this.SetNormalizedPosition(value, 1);
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0007F7BE File Offset: 0x0007D9BE
	private void SetHorizontalNormalizedPosition(float value)
	{
		this.SetNormalizedPosition(value, 0);
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0007F7C8 File Offset: 0x0007D9C8
	private void SetVerticalNormalizedPosition(float value)
	{
		this.SetNormalizedPosition(value, 1);
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0007F7D4 File Offset: 0x0007D9D4
	private void SetNormalizedPosition(float value, int axis)
	{
		this.EnsureLayoutHasRebuilt();
		this.UpdateBounds();
		float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
		float num2 = this.m_ViewBounds.min[axis] - value * num;
		float num3 = this.m_Content.localPosition[axis] + num2 - this.m_ContentBounds.min[axis];
		Vector3 localPosition = this.m_Content.localPosition;
		if (Mathf.Abs(localPosition[axis] - num3) > 0.01f)
		{
			localPosition[axis] = num3;
			this.m_Content.localPosition = localPosition;
			this.m_Velocity[axis] = 0f;
			this.UpdateBounds();
		}
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0007F8AF File Offset: 0x0007DAAF
	private static float RubberDelta(float overStretching, float viewSize)
	{
		return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0007F8DA File Offset: 0x0007DADA
	protected override void OnRectTransformDimensionsChange()
	{
		this.SetDirty();
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0007F8E2 File Offset: 0x0007DAE2
	private bool hScrollingNeeded
	{
		get
		{
			return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0007F915 File Offset: 0x0007DB15
	private bool vScrollingNeeded
	{
		get
		{
			return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0007F948 File Offset: 0x0007DB48
	public virtual void CalculateLayoutInputHorizontal()
	{
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0007F94A File Offset: 0x0007DB4A
	public virtual void CalculateLayoutInputVertical()
	{
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0007F94C File Offset: 0x0007DB4C
	public virtual float minWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0007F953 File Offset: 0x0007DB53
	public virtual float preferredWidth
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0007F95A File Offset: 0x0007DB5A
	// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0007F962 File Offset: 0x0007DB62
	public virtual float flexibleWidth { get; private set; }

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0007F96B File Offset: 0x0007DB6B
	public virtual float minHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0007F972 File Offset: 0x0007DB72
	public virtual float preferredHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0007F979 File Offset: 0x0007DB79
	public virtual float flexibleHeight
	{
		get
		{
			return -1f;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0007F980 File Offset: 0x0007DB80
	public virtual int layoutPriority
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0007F984 File Offset: 0x0007DB84
	public virtual void SetLayoutHorizontal()
	{
		this.m_Tracker.Clear();
		if (this.m_HSliderExpand || this.m_VSliderExpand)
		{
			this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			this.viewRect.anchorMin = Vector2.zero;
			this.viewRect.anchorMax = Vector2.one;
			this.viewRect.sizeDelta = Vector2.zero;
			this.viewRect.anchoredPosition = Vector2.zero;
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_VSliderExpand && this.vScrollingNeeded)
		{
			this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_HSliderExpand && this.hScrollingNeeded)
		{
			this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}
		if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
		{
			this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
		}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0007FBE0 File Offset: 0x0007DDE0
	public virtual void SetLayoutVertical()
	{
		this.UpdateScrollbarLayout();
		this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
		this.m_ContentBounds = this.GetBounds();
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0007FC3C File Offset: 0x0007DE3C
	private void UpdateScrollbarVisibility()
	{
		if (this.m_VerticalScrollbar && this.m_VerticalScrollbarVisibility != ScrollRectExtended.ScrollbarVisibility.Permanent && this.m_VerticalScrollbar.gameObject.activeSelf != this.vScrollingNeeded)
		{
			this.m_VerticalScrollbar.gameObject.SetActive(this.vScrollingNeeded);
		}
		if (this.m_HorizontalScrollbar && this.m_HorizontalScrollbarVisibility != ScrollRectExtended.ScrollbarVisibility.Permanent && this.m_HorizontalScrollbar.gameObject.activeSelf != this.hScrollingNeeded)
		{
			this.m_HorizontalScrollbar.gameObject.SetActive(this.hScrollingNeeded);
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0007FCD0 File Offset: 0x0007DED0
	private void UpdateScrollbarLayout()
	{
		if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
		{
			this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
			this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
			this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
			this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
			if (this.vScrollingNeeded)
			{
				this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
			}
			else
			{
				this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
			}
		}
		if (this.m_HSliderExpand && this.m_VerticalScrollbar)
		{
			this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
			this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
			this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
			this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
			if (this.hScrollingNeeded)
			{
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				return;
			}
			this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
		}
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0007FED8 File Offset: 0x0007E0D8
	private void UpdateBounds()
	{
		this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
		this.m_ContentBounds = this.GetBounds();
		if (this.m_Content == null)
		{
			return;
		}
		Vector3 size = this.m_ContentBounds.size;
		Vector3 center = this.m_ContentBounds.center;
		Vector3 vector = this.m_ViewBounds.size - size;
		if (vector.x > 0f)
		{
			center.x -= vector.x * (this.m_Content.pivot.x - 0.5f);
			size.x = this.m_ViewBounds.size.x;
		}
		if (vector.y > 0f)
		{
			center.y -= vector.y * (this.m_Content.pivot.y - 0.5f);
			size.y = this.m_ViewBounds.size.y;
		}
		this.m_ContentBounds.size = size;
		this.m_ContentBounds.center = center;
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00080018 File Offset: 0x0007E218
	private Bounds GetBounds()
	{
		if (this.m_Content == null)
		{
			return default(Bounds);
		}
		Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
		this.m_Content.GetWorldCorners(this.m_Corners);
		for (int i = 0; i < 4; i++)
		{
			Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(this.m_Corners[i]);
			vector = Vector3.Min(lhs, vector);
			vector2 = Vector3.Max(lhs, vector2);
		}
		Bounds result = new Bounds(vector, Vector3.zero);
		result.Encapsulate(vector2);
		return result;
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000800D0 File Offset: 0x0007E2D0
	private Vector2 CalculateOffset(Vector2 delta)
	{
		Vector2 zero = Vector2.zero;
		if (this.m_MovementType == ScrollRectExtended.MovementType.Unrestricted)
		{
			return zero;
		}
		Vector2 vector = this.m_ContentBounds.min;
		Vector2 vector2 = this.m_ContentBounds.max;
		if (this.m_Horizontal)
		{
			vector.x += delta.x;
			vector2.x += delta.x;
			if (vector.x > this.m_ViewBounds.min.x)
			{
				zero.x = this.m_ViewBounds.min.x - vector.x;
			}
			else if (vector2.x < this.m_ViewBounds.max.x)
			{
				zero.x = this.m_ViewBounds.max.x - vector2.x;
			}
		}
		if (this.m_Vertical)
		{
			vector.y += delta.y;
			vector2.y += delta.y;
			if (vector2.y < this.m_ViewBounds.max.y)
			{
				zero.y = this.m_ViewBounds.max.y - vector2.y;
			}
			else if (vector.y > this.m_ViewBounds.min.y)
			{
				zero.y = this.m_ViewBounds.min.y - vector.y;
			}
		}
		return zero;
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00080246 File Offset: 0x0007E446
	protected void SetDirty()
	{
		if (!this.IsActive())
		{
			return;
		}
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0008025C File Offset: 0x0007E45C
	protected void SetDirtyCaching()
	{
		if (!this.IsActive())
		{
			return;
		}
		CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00080278 File Offset: 0x0007E478
	public bool isDragged()
	{
		return this.m_Dragging;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00080280 File Offset: 0x0007E480
	Transform ICanvasElement.get_transform()
	{
		return base.transform;
	}

	// Token: 0x0400103B RID: 4155
	[SerializeField]
	private RectTransform m_Content;

	// Token: 0x0400103C RID: 4156
	[SerializeField]
	private bool m_Horizontal = true;

	// Token: 0x0400103D RID: 4157
	[SerializeField]
	private bool m_Vertical = true;

	// Token: 0x0400103E RID: 4158
	[SerializeField]
	private ScrollRectExtended.MovementType m_MovementType = ScrollRectExtended.MovementType.Elastic;

	// Token: 0x0400103F RID: 4159
	[SerializeField]
	private float m_Elasticity = 0.1f;

	// Token: 0x04001040 RID: 4160
	[SerializeField]
	private bool m_Inertia = true;

	// Token: 0x04001041 RID: 4161
	[SerializeField]
	private float m_DecelerationRate = 0.135f;

	// Token: 0x04001042 RID: 4162
	[SerializeField]
	private float m_ScrollSensitivity = 1f;

	// Token: 0x04001043 RID: 4163
	[SerializeField]
	private RectTransform m_Viewport;

	// Token: 0x04001044 RID: 4164
	[SerializeField]
	private float m_ScrollFactor = 1f;

	// Token: 0x04001045 RID: 4165
	public float velocityMod = 1f;

	// Token: 0x04001046 RID: 4166
	private Scrollbar m_HorizontalScrollbar;

	// Token: 0x04001047 RID: 4167
	[SerializeField]
	private Scrollbar m_VerticalScrollbar;

	// Token: 0x04001048 RID: 4168
	[SerializeField]
	private ScrollRectExtended.ScrollbarVisibility m_HorizontalScrollbarVisibility;

	// Token: 0x04001049 RID: 4169
	[SerializeField]
	private ScrollRectExtended.ScrollbarVisibility m_VerticalScrollbarVisibility;

	// Token: 0x0400104A RID: 4170
	[SerializeField]
	private float m_HorizontalScrollbarSpacing;

	// Token: 0x0400104B RID: 4171
	[SerializeField]
	private float m_VerticalScrollbarSpacing;

	// Token: 0x0400104C RID: 4172
	[SerializeField]
	private ScrollRectExtended.ScrollRectEvent m_OnValueChanged = new ScrollRectExtended.ScrollRectEvent();

	// Token: 0x0400104D RID: 4173
	private Vector2 m_PointerStartLocalCursor = Vector2.zero;

	// Token: 0x0400104E RID: 4174
	private Vector2 m_ContentStartPosition = Vector2.zero;

	// Token: 0x0400104F RID: 4175
	private RectTransform m_ViewRect;

	// Token: 0x04001050 RID: 4176
	private Bounds m_ContentBounds;

	// Token: 0x04001051 RID: 4177
	private Bounds m_ViewBounds;

	// Token: 0x04001052 RID: 4178
	private Vector2 m_Velocity;

	// Token: 0x04001053 RID: 4179
	private bool m_Dragging;

	// Token: 0x04001054 RID: 4180
	private Vector2 m_PrevPosition = Vector2.zero;

	// Token: 0x04001055 RID: 4181
	private Bounds m_PrevContentBounds;

	// Token: 0x04001056 RID: 4182
	private Bounds m_PrevViewBounds;

	// Token: 0x04001057 RID: 4183
	[NonSerialized]
	private bool m_HasRebuiltLayout;

	// Token: 0x04001058 RID: 4184
	private bool m_HSliderExpand;

	// Token: 0x04001059 RID: 4185
	private bool m_VSliderExpand;

	// Token: 0x0400105A RID: 4186
	private float m_HSliderHeight;

	// Token: 0x0400105B RID: 4187
	private float m_VSliderWidth;

	// Token: 0x0400105C RID: 4188
	[NonSerialized]
	private RectTransform m_Rect;

	// Token: 0x0400105D RID: 4189
	private RectTransform m_HorizontalScrollbarRect;

	// Token: 0x0400105E RID: 4190
	private RectTransform m_VerticalScrollbarRect;

	// Token: 0x0400105F RID: 4191
	private DrivenRectTransformTracker m_Tracker;

	// Token: 0x04001060 RID: 4192
	public static ScrollRectExtended instance;

	// Token: 0x04001061 RID: 4193
	private float _check_timer = 0.05f;

	// Token: 0x04001062 RID: 4194
	private float _check_timer_interval = 0.05f;

	// Token: 0x04001064 RID: 4196
	private readonly Vector3[] m_Corners = new Vector3[4];

	// Token: 0x02000401 RID: 1025
	public enum MovementType
	{
		// Token: 0x04001633 RID: 5683
		Unrestricted,
		// Token: 0x04001634 RID: 5684
		Elastic,
		// Token: 0x04001635 RID: 5685
		Clamped
	}

	// Token: 0x02000402 RID: 1026
	public enum ScrollbarVisibility
	{
		// Token: 0x04001637 RID: 5687
		Permanent,
		// Token: 0x04001638 RID: 5688
		AutoHide,
		// Token: 0x04001639 RID: 5689
		AutoHideAndExpandViewport
	}

	// Token: 0x02000403 RID: 1027
	[Serializable]
	public class ScrollRectEvent : UnityEvent<Vector2>
	{
	}
}
