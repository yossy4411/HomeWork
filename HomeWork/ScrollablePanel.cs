using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    public class ScrollablePanel : FlowLayoutPanel
    {
        public bool CanScroll { get; set; } = true;
        const int WM_MOUSEWHEEL = 0x020A;
        protected override void WndProc(ref Message m)
        {
            

            if (m.HWnd == Handle && m.Msg == WM_MOUSEWHEEL && !CanScroll) return;

            base.WndProc(ref m);
        }
    }
}
