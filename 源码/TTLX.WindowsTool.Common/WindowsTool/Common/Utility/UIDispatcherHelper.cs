using System;
using System.Windows.Threading;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200001B RID: 27
	public class UIDispatcherHelper
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004794 File Offset: 0x00002994
		public static void DoEvents()
		{
			DispatcherFrame nestedFrame = new DispatcherFrame();
			DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, UIDispatcherHelper.ExitFrameCallback, nestedFrame);
			Dispatcher.PushFrame(nestedFrame);
			if (exitOperation.Status != DispatcherOperationStatus.Completed)
			{
				exitOperation.Abort();
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000047CF File Offset: 0x000029CF
		private static object ExitFrame(object state)
		{
			(state as DispatcherFrame).Continue = false;
			return null;
		}

		// Token: 0x04000024 RID: 36
		private static readonly DispatcherOperationCallback ExitFrameCallback = new DispatcherOperationCallback(UIDispatcherHelper.ExitFrame);
	}
}
