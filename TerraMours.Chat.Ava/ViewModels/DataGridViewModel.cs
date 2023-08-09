using Avalonia.Collections;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using AvaloniaEdit.Utils;
using TerraMours.Chat.Ava.Models;
using TerraMours.Chat.Ava.Models.Class;

namespace TerraMours.Chat.Ava.ViewModels {
    public class DataGridViewModel : ViewModelBase {
        #region 
        public long SelectedItemId => _selectedItem?.Id ?? default;

        private int _selectedItemIndex;
        public int SelectedItemIndex {
            get => _selectedItemIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedItemIndex, value);
        }

        private bool _dataGridIsFocused;
        public bool DataGridIsFocused {
            get => _dataGridIsFocused;
            set => this.RaiseAndSetIfChanged(ref _dataGridIsFocused, value);
        }

        private DataGridCollectionView _dataGridCollection;
        public DataGridCollectionView DataGridCollection {
            get => _dataGridCollection;
            set => this.RaiseAndSetIfChanged(ref _dataGridCollection, value);
        }
        private ObservableCollection<ChatList> _chatList;
        public ObservableCollection<ChatList> ChatList {
            get => _chatList;
            set {
                if (_chatList != value) {
                    _chatList = value;
                    DataGridCollection = new DataGridCollectionView(ChatList);
                    DataGridCollection.GroupDescriptions.Add(new DataGridPathGroupDescription("Category"));
                    this.RaisePropertyChanged(nameof(ChatList));
                }
            }
        }


        private ChatList _selectedItem;
        public ChatList SelectedItem {
            get => _selectedItem;
            set {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);

                if (SelectedItemId != -1 && _selectedItem != null && !VMLocator.ChatViewModel.ChatIsRunning && DataGridIsFocused) {
                    VMLocator.ChatViewModel.LastId = _selectedItem.Id;
                    ShowChatLogAsync(_selectedItem.Id);
                    ScrollToEndAction?.Invoke();
                }
                else if (!string.IsNullOrWhiteSpace(VMLocator.MainViewModel.SearchLogKeyword)) {
                    SelectedItemIndex = -1;
                }
            }
        }

        public Action ScrollToEndAction { get; set; }
        public Action ScrollUpdateAction { get; set; }
        #endregion
        public async void ShowChatLogAsync(long _selectedItem) {
            var _chatViewModel = VMLocator.ChatViewModel;

            if (!_chatViewModel.ChatIsRunning) {
                if (VMLocator.MainViewModel.SelectedLeftPane != "AI Chat") {
                    VMLocator.MainViewModel.SelectedLeftPane = "AI Chat";
                }
            }

            GptChatListByConversationId(_selectedItem,true);
        }

        public void ChatListNext()
        {
            if (SelectedItem != null)
            {
                GptChatListByConversationId(SelectedItem.Id);
            }
        }

        private async Task GptChatListByConversationId(long conversationId, bool isNew = false) {
            var pageSize = 10;
            var pageIndex = isNew ? 1 : (VMLocator.ChatViewModel.ChatHistory?.Count / pageSize + 1) ?? 1;

            var res = await new TMHttpClient().PostAsync<PagedRes<Models.ChatMessage>>("/api/v1/Chat/ChatRecordList", new { ConversationId = conversationId, PageIndex = pageIndex, PageSize = pageSize });

            if (res.StatusCode != 200) {
                await VMLocator.MainViewModel.ContentDialogShowAsync(new ContentDialog() {
                    Title = $"接口报错：code：{res.StatusCode}.Msg:{JsonSerializer.Serialize(res.Message + res.Errors, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) })}",
                    PrimaryButtonText = "Ok"
                });

                return;
            }

            var observableCollection = new ObservableCollection<Models.ChatMessage>(res.Data.Items);
            VMLocator.ChatViewModel.ChatHistory = isNew ? observableCollection : new ObservableCollection<Models.ChatMessage>(VMLocator.ChatViewModel.ChatHistory.Concat(observableCollection));

            if (observableCollection.Any()) {
                ScrollUpdateAction?.Invoke();
            }
        }

    }
}
