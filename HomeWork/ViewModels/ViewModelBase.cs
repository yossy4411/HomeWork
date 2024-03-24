using Avalonia.Media;
using ReactiveUI;
using System;

namespace HomeWork.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public static readonly FontFamily FontFamily = new(new Uri("Assets/NotoSansJP-VariableFont_wght.ttf"), "Noto Sans JP");
}
