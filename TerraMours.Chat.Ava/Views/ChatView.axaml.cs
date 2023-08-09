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

        // ����ճ����
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        var dataObject = new DataObject();
        dataObject.Set(DataFormats.Text, text);
        await clipboard.SetDataObjectAsync(dataObject);
        var dialog = new ContentDialog() { Title = "���Ƴɹ�", PrimaryButtonText = "OK" };
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

        // �����������λ�úͿɼ�����Ĵ�С
        double verticalOffset = scrollViewer.Offset.Length;
        double viewportHeight = scrollViewer.Viewport.Height;
        double extentHeight = scrollViewer.Extent.Height;

        // ���������ϻ����������һ��и�������ʱ
        if (verticalOffset == 0 && extentHeight > viewportHeight) {
            // ������һҳ����
            VMLocator.DataGridViewModel.ChatListNext();
        }
    }

}