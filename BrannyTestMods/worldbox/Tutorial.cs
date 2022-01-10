using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029F RID: 671
public class Tutorial : MonoBehaviour
{
	// Token: 0x06000EC0 RID: 3776 RVA: 0x00088638 File Offset: 0x00086838
	private void create()
	{
		this.pages = new List<TutorialPage>();
		this.color_red = Toolbox.makeColor("#FF3700");
		this.color_red.a = 0.4f;
		this.color_white = Toolbox.makeColor("#4AA5FF");
		this.color_white.a = 0.4f;
		this.color_yellow = Toolbox.makeColor("#FEFE00");
		this.color_yellow_transparent = Toolbox.makeColor("#FEFE00");
		this.color_yellow_transparent.a = 0f;
		this.add(new TutorialPage
		{
			text = "tut_page1",
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page2",
			wait = 0.3f
		});
		this.add(new TutorialPage
		{
			text = "tut_page3_mobile",
			mobileOnly = true,
			centerImage = this.icon_finger,
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page3_pc",
			pcOnly = true,
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page4"
		});
		this.add(new TutorialPage
		{
			text = "tut_page5",
			object1 = this.saveButton,
			centerImage = this.icon_saveBox,
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page6",
			object1 = this.customMapButton,
			centerImage = this.icon_customWorld,
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page7",
			object1 = this.worldRules,
			centerImage = this.icon_worldLaws
		});
		this.add(new TutorialPage
		{
			text = "tut_page8",
			object1 = this.tabDrawing
		});
		this.add(new TutorialPage
		{
			text = "tut_page9",
			icon = "brush"
		});
		this.add(new TutorialPage
		{
			text = "tut_page10",
			object1 = this.tabCivs,
			centerImage = this.icon_ivilizations
		});
		this.add(new TutorialPage
		{
			text = "tut_page11",
			object1 = this.tabCreatures,
			centerImage = this.icon_dragon
		});
		this.add(new TutorialPage
		{
			text = "tut_page12",
			object1 = this.tabNature,
			centerImage = this.icon_tornado
		});
		this.add(new TutorialPage
		{
			text = "tut_page13",
			object1 = this.tabBombs,
			centerImage = this.icon_nuke
		});
		this.add(new TutorialPage
		{
			text = "tut_page14",
			object1 = this.tabOther,
			centerImage = this.icon_greyGoo
		});
		this.add(new TutorialPage
		{
			text = "tut_page15",
			centerImage = this.icon_ufo
		});
		this.add(new TutorialPage
		{
			text = "tut_page16",
			mobileOnly = true,
			icon = "reward",
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page17",
			centerImage = this.icon_heart,
			wait = 0.5f
		});
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x000889C0 File Offset: 0x00086BC0
	public void startTutorial()
	{
		if (this.pages == null)
		{
			this.create();
		}
		base.gameObject.SetActive(true);
		this.curPage = -1;
		PowerButtonSelector.instance.unselectAll();
		PowerButtonSelector.instance.unselectTabs();
		this.attentionBox.gameObject.SetActive(false);
		this.nextPage();
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00088A1C File Offset: 0x00086C1C
	private void nextPage()
	{
		this.curPage++;
		if (this.curPage >= this.pages.Count)
		{
			this.endTutorial();
			return;
		}
		this.pressAnywhere.GetComponent<LocalizedText>().updateText(true);
		TutorialPage tutorialPage = this.pages[this.curPage];
		if (!Config.isMobile && tutorialPage.mobileOnly)
		{
			this.nextPage();
			return;
		}
		if (Config.isMobile && tutorialPage.pcOnly)
		{
			this.nextPage();
			return;
		}
		if (tutorialPage.object1 == null)
		{
			this.attentionBox.gameObject.SetActive(false);
		}
		else
		{
			this.attentionBox.gameObject.SetActive(true);
			Vector3 position = tutorialPage.object1.transform.position;
			Vector2 sizeDelta = tutorialPage.object1.GetComponent<RectTransform>().sizeDelta;
			sizeDelta.x += 10f;
			sizeDelta.y += 10f;
			this.attentionBox.transform.position = position;
			this.attentionBox.rectTransform.sizeDelta = sizeDelta;
			ShortcutExtensions.DOKill(this.attentionBox, false);
			this.attentionBox.transform.localScale = new Vector3(0.5f, 0.5f);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.attentionBox.transform, new Vector3(1f, 1f, 1f), 0.3f), 27);
			this.attentionBox.color = this.color_white;
		}
		if (tutorialPage.centerImage == null)
		{
			tutorialPage.centerImage = this.icon_default;
		}
		this.centerObject.GetComponent<Image>().sprite = tutorialPage.centerImage;
		this.centerObject.gameObject.SetActive(false);
		this.adButton.gameObject.SetActive(false);
		this.brushSize.gameObject.SetActive(false);
		this.text.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		ShortcutExtensions.DOKill(this.text, false);
		this.text.text = "";
		string text = LocalizedTextManager.getText(tutorialPage.text, null);
		float num = (float)(text.Length / 25);
		if (num <= 1f)
		{
			num = 1f;
		}
		this.text.text = text;
		this.text.GetComponent<LocalizedText>().checkTextFont();
		this.text.GetComponent<LocalizedText>().checkSpecialLanguages();
		text = this.text.text;
		this.text.text = "";
		this.textTypeTween = DOTweenModuleUI.DOText(this.text, text, num, false, 0, null);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.text.transform, new Vector3(1f, 1f, 1f), 0.3f), 26);
		this.waitTimer = tutorialPage.wait;
		if (this.canSkipTutorial())
		{
			this.waitTimer = 0f;
		}
		if (this.waitTimer > 0f)
		{
			this.pressAnywhere.gameObject.SetActive(false);
		}
		ShortcutExtensions.DOKill(this.bear.transform, false);
		this.bear.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		ShortcutExtensions.DOShakeRotation(this.bear.transform, num, 90f, 10, 90f, true);
		if (tutorialPage.icon == "default")
		{
			this.centerObject.SetActive(true);
			ShortcutExtensions.DOKill(this.centerObject.transform, false);
			this.centerObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.centerObject.transform, new Vector3(1f, 1f, 1f), 0.5f), 26);
			return;
		}
		if (tutorialPage.icon == "reward")
		{
			this.adButton.SetActive(true);
			ShortcutExtensions.DOKill(this.adButton.transform, false);
			this.adButton.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.adButton.transform, new Vector3(1f, 1f, 1f), 0.5f), 26);
			return;
		}
		if (tutorialPage.icon == "brush")
		{
			this.brushSize.SetActive(true);
			ShortcutExtensions.DOKill(this.brushSize.transform, false);
			this.brushSize.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.brushSize.transform, new Vector3(1f, 1f, 1f), 0.5f), 26);
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00088F2A File Offset: 0x0008712A
	internal void completeText()
	{
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00088F2C File Offset: 0x0008712C
	private bool canSkipTutorial()
	{
		return true;
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00088F2F File Offset: 0x0008712F
	public static void restartTutorial()
	{
		PlayerConfig.instance.data.tutorialFinished = false;
		PlayerConfig.saveData();
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00088F46 File Offset: 0x00087146
	private void endTutorial()
	{
		base.gameObject.SetActive(false);
		PlayerConfig.instance.data.tutorialFinished = true;
		PlayerConfig.saveData();
		ScrollWindow.clearQueue();
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00088F70 File Offset: 0x00087170
	private void LateUpdate()
	{
		if (this.attentionBox.gameObject.activeSelf)
		{
			if (this.attentionBox.color == this.color_red)
			{
				DOTweenModuleUI.DOColor(this.attentionBox, this.color_white, 1f);
			}
			else if (this.attentionBox.color == this.color_white)
			{
				DOTweenModuleUI.DOColor(this.attentionBox, this.color_red, 1f);
			}
		}
		if (this.canSkipTutorial())
		{
			if (this.textTypeTween != null && TweenExtensions.IsActive(this.textTypeTween) && Input.GetMouseButtonUp(0))
			{
				TweenExtensions.Kill(this.textTypeTween, true);
				return;
			}
		}
		else if (this.textTypeTween != null && TweenExtensions.IsActive(this.textTypeTween))
		{
			return;
		}
		if (this.waitTimer > 0f)
		{
			this.waitTimer -= Time.deltaTime;
			return;
		}
		if (!this.pressAnywhere.gameObject.activeSelf)
		{
			this.pressAnywhere.gameObject.SetActive(true);
			this.pressAnywhere.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.pressAnywhere.transform, new Vector3(1f, 1f, 1f), 1f), 27);
			this.pressAnywhere.color = this.color_yellow_transparent;
			DOTweenModuleUI.DOColor(this.pressAnywhere, this.color_yellow, 1f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.nextPage();
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00089108 File Offset: 0x00087308
	private void add(TutorialPage pPage)
	{
		this.pages.Add(pPage);
	}

	// Token: 0x040011A6 RID: 4518
	public Sprite icon_default;

	// Token: 0x040011A7 RID: 4519
	public Sprite icon_bear;

	// Token: 0x040011A8 RID: 4520
	public Sprite icon_ivilizations;

	// Token: 0x040011A9 RID: 4521
	public Sprite icon_nuke;

	// Token: 0x040011AA RID: 4522
	public Sprite icon_dragon;

	// Token: 0x040011AB RID: 4523
	public Sprite icon_tornado;

	// Token: 0x040011AC RID: 4524
	public Sprite icon_saveBox;

	// Token: 0x040011AD RID: 4525
	public Sprite icon_customWorld;

	// Token: 0x040011AE RID: 4526
	public Sprite icon_worldLaws;

	// Token: 0x040011AF RID: 4527
	public Sprite icon_greyGoo;

	// Token: 0x040011B0 RID: 4528
	public Sprite icon_ufo;

	// Token: 0x040011B1 RID: 4529
	public Sprite icon_heart;

	// Token: 0x040011B2 RID: 4530
	public Sprite icon_finger;

	// Token: 0x040011B3 RID: 4531
	public GameObject bear;

	// Token: 0x040011B4 RID: 4532
	public GameObject centerObject;

	// Token: 0x040011B5 RID: 4533
	public GameObject brushSize;

	// Token: 0x040011B6 RID: 4534
	public GameObject adButton;

	// Token: 0x040011B7 RID: 4535
	public GameObject saveButton;

	// Token: 0x040011B8 RID: 4536
	public GameObject customMapButton;

	// Token: 0x040011B9 RID: 4537
	public GameObject worldRules;

	// Token: 0x040011BA RID: 4538
	public GameObject tabDrawing;

	// Token: 0x040011BB RID: 4539
	public GameObject tabCivs;

	// Token: 0x040011BC RID: 4540
	public GameObject tabCreatures;

	// Token: 0x040011BD RID: 4541
	public GameObject tabNature;

	// Token: 0x040011BE RID: 4542
	public GameObject tabBombs;

	// Token: 0x040011BF RID: 4543
	public GameObject tabOther;

	// Token: 0x040011C0 RID: 4544
	public GameObject settingsButton;

	// Token: 0x040011C1 RID: 4545
	public Text text;

	// Token: 0x040011C2 RID: 4546
	public Image attentionBox;

	// Token: 0x040011C3 RID: 4547
	public Text pressAnywhere;

	// Token: 0x040011C4 RID: 4548
	private int curPage;

	// Token: 0x040011C5 RID: 4549
	private List<TutorialPage> pages;

	// Token: 0x040011C6 RID: 4550
	private float waitTimer;

	// Token: 0x040011C7 RID: 4551
	private Color color_red;

	// Token: 0x040011C8 RID: 4552
	private Color color_white;

	// Token: 0x040011C9 RID: 4553
	private Color color_yellow;

	// Token: 0x040011CA RID: 4554
	private Color color_yellow_transparent;

	// Token: 0x040011CB RID: 4555
	private Tweener textTypeTween;
}
