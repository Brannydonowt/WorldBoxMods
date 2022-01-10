using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CD RID: 717
public class WorldListWindow : MonoBehaviour
{
	// Token: 0x040012B3 RID: 4787
	private static WorldListWindow instance;

	// Token: 0x040012B4 RID: 4788
	public WorldElement worldElementPrefab;

	// Token: 0x040012B5 RID: 4789
	public GameObject notFound;

	// Token: 0x040012B6 RID: 4790
	public ScrollWindow windowWorldList;

	// Token: 0x040012B7 RID: 4791
	private List<WorldElement> elements = new List<WorldElement>();

	// Token: 0x040012B8 RID: 4792
	public Transform transformContent;

	// Token: 0x040012B9 RID: 4793
	public Transform listContent;

	// Token: 0x040012BA RID: 4794
	public Transform tagContent;

	// Token: 0x040012BB RID: 4795
	public GameObject loadingSpinner;

	// Token: 0x040012BC RID: 4796
	public GameObject textStatusBG;

	// Token: 0x040012BD RID: 4797
	public Text textStatusMessage;

	// Token: 0x040012BE RID: 4798
	public LocalizedText windowTitle;

	// Token: 0x040012BF RID: 4799
	private bool loaded;

	// Token: 0x040012C0 RID: 4800
	public static List<MapTagType> tagsActive = new List<MapTagType>();

	// Token: 0x040012C1 RID: 4801
	public static string authorId;

	// Token: 0x040012C2 RID: 4802
	public GameObject sectionTextBG;

	// Token: 0x040012C3 RID: 4803
	public GameObject profileImage;

	// Token: 0x040012C4 RID: 4804
	public GameObject filterButton;

	// Token: 0x040012C5 RID: 4805
	public Text sectionText;

	// Token: 0x040012C6 RID: 4806
	public Image filterTag1;

	// Token: 0x040012C7 RID: 4807
	public Image filterTag2;
}
