using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Films.Helpers
{
    internal static class AsyncHelper
    {
        public static void InvokeIfRequired(Action action, Control control)
        {
            if (action == null || control == null)
            {
                throw new ArgumentNullException();
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void InvokeIfRequired(Action action, ToolStrip toolStrip)
        {
            if (action == null || toolStrip == null)
            {
                throw new ArgumentNullException();
            }

            if (toolStrip.InvokeRequired)
            {
                toolStrip.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
