using System;
using System.Collections.Generic;

// Token: 0x020002BD RID: 701
public class WindowHistory
{
	// Token: 0x06000F5C RID: 3932 RVA: 0x0008A782 File Offset: 0x00088982
	public static void clear()
	{
		if (WindowHistory.historyClearCallback != null)
		{
			WindowHistory.historyClearCallback();
			WindowHistory.historyClearCallback = null;
		}
		WindowHistory.list.Clear();
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0008A7A8 File Offset: 0x000889A8
	public static void addIntoHistory(ScrollWindow pWindow)
	{
		WindowHistoryElement windowHistoryElement = new WindowHistoryElement
		{
			window = pWindow,
			unit = Config.selectedUnit,
			city = Config.selectedCity,
			kingdom = Config.selectedKingdom,
			culture = Config.selectedCulture
		};
		WindowHistory.list.Add(windowHistoryElement);
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0008A804 File Offset: 0x00088A04
	public static void updateHistory(ScrollWindow pWindow)
	{
		WindowHistoryElement windowHistoryElement = WindowHistory.list[WindowHistory.list.Count - 1];
		windowHistoryElement.window = pWindow;
		windowHistoryElement.unit = Config.selectedUnit;
		windowHistoryElement.city = Config.selectedCity;
		windowHistoryElement.kingdom = Config.selectedKingdom;
		windowHistoryElement.culture = Config.selectedCulture;
		WindowHistory.list[WindowHistory.list.Count - 1] = windowHistoryElement;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0008A877 File Offset: 0x00088A77
	public static void clickBack()
	{
		WindowHistory.returnWindowBack();
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0008A880 File Offset: 0x00088A80
	public static void returnWindowBack()
	{
		if (!WindowHistory.canReturnWindowBack())
		{
			return;
		}
		WindowHistoryElement windowHistoryElement = WindowHistory.list[WindowHistory.list.Count - 1];
		WindowHistory.list.RemoveAt(WindowHistory.list.Count - 1);
		WindowHistoryElement windowHistoryElement2 = WindowHistory.list[WindowHistory.list.Count - 1];
		WindowHistory.list.RemoveAt(WindowHistory.list.Count - 1);
		Config.selectedCity = windowHistoryElement2.city;
		Config.selectedUnit = windowHistoryElement2.unit;
		Config.selectedKingdom = windowHistoryElement2.kingdom;
		Config.selectedCulture = windowHistoryElement2.culture;
		windowHistoryElement2.window.clickShowLeft();
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0008A924 File Offset: 0x00088B24
	private static bool canReturnWindowBack()
	{
		return !WorkshopUploadingWorldWindow.uploading && WindowHistory.list.Count >= 2;
	}

	// Token: 0x04001245 RID: 4677
	public static List<WindowHistoryElement> list = new List<WindowHistoryElement>();

	// Token: 0x04001246 RID: 4678
	public static Action historyClearCallback;
}
