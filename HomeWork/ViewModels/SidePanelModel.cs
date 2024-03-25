
using System.Diagnostics;

namespace HomeWork.ViewModels
{
    public class SidePanelModel : ViewModelBase
    {
        public string Text { get; set; } = "SidePanel";
        public void Submit()
        {
            Debug.WriteLine(Text);
        }
    }
}
