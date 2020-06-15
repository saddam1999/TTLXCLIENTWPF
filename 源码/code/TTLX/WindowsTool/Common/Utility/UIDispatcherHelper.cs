namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Windows.Threading;

    public class UIDispatcherHelper
    {
        private static readonly DispatcherOperationCallback ExitFrameCallback = new DispatcherOperationCallback(null, ExitFrame);

        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            DispatcherOperation operation = Dispatcher.get_CurrentDispatcher().BeginInvoke(4, ExitFrameCallback, frame);
            Dispatcher.PushFrame(frame);
            if (operation.get_Status() != 2)
            {
                operation.Abort();
            }
        }

        private static object ExitFrame(object state)
        {
            (state as DispatcherFrame).set_Continue(false);
            return null;
        }
    }
}

