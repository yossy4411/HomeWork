using HomeWork.Windows.Views;
using Wpf.Ui.Controls;

namespace HomeWork.Windows.ViewModels
{
    public class MainViewModel
    {
        public List<NavigationViewItem> NavigationViewItems { get; set; } = [
                new("ホーム", SymbolRegular.Home24, typeof(HomePage)),
                new("カレンダー", SymbolRegular.Calendar24, typeof(CalendarPage))
            ];
    }
}
