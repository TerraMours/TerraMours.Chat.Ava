using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DynamicData;
using FluentAvalonia.UI.Controls;
using System;
using System.Linq;
using TerraMours.Chat.Ava.ViewModels;

namespace TerraMours.Chat.Ava.Views;

public partial class ChatView : UserControl
{
    public ChatViewModel ChatViewModel { get; } = new ChatViewModel();
    public ChatView()
    {
        InitializeComponent();
        DataContext = ChatViewModel;
        VMLocator.ChatViewModel = ChatViewModel;
        VMLocator.DataGridViewModel.ScrollToEndAction = () =>
        {
            Scroll.ScrollToEnd();
        };
        VMLocator.DataGridViewModel.ScrollUpdateAction = () => {
            Scroll.ScrollToEnd();
        };
    }

    private async void CopyClick(object? sender, RoutedEventArgs e) {
        if (sender is not MenuItem item) return;
        if (item.Tag is not string text) return;

        // 设置粘贴板
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        var dataObject = new DataObject();
        dataObject.Set(DataFormats.Text, text);
        await clipboard.SetDataObjectAsync(dataObject);
        var dialog = new ContentDialog() { Title = "复制成功", PrimaryButtonText = "OK" };
        await VMLocator.MainViewModel.ContentDialogShowAsync(dialog);
    }

    private async void DeleteClick(object? sender, RoutedEventArgs e) {
        if (sender is not MenuItem item) return;
        if (item.Tag is not long id) return;
        var res = await ChatViewModel.DeleteChatRecord(id);
        if (!res)return;
        VMLocator.ChatViewModel.ChatHistory.Remove(VMLocator.ChatViewModel.ChatHistory.First(m => m.ChatRecordId == id));
        //VMLocator.ChatDbcontext.ChatMessages.Remove(VMLocator.ChatDbcontext.ChatMessages.First(m => m.ChatRecordId == id));
    }

    private void Scroll_OnScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        var scrollViewer = (ScrollViewer)sender;

        // 计算滚动条的位置和可见区域的大小
        double verticalOffset = scrollViewer.Offset.Length;
        double viewportHeight = scrollViewer.Viewport.Height;
        double extentHeight = scrollViewer.Extent.Height;

        // 当滚动条上划到顶，并且还有更多数据时
        if (verticalOffset == 0 && extentHeight > viewportHeight) {
            // 加载下一页数据
            VMLocator.DataGridViewModel.ChatListNext();
        }
    }

}