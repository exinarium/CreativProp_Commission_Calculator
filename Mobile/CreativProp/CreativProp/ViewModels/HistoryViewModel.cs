using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreativProp.Models;
using CreativProp.Repositories;
using CreativProp.Utilities.Enums;
using CreativProp.Utilities.SQLHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace CreativProp.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        public IRepository<History> HistoryDataStore => DependencyService.Get<IRepository<History>>();
        private const int pageSize = 20;
        private Order order = new Order()
        {
            ColumnName = "CreatedDate",
            Direction = SQLOrderingEnum.DESC,
            TableName = "History"
        };

        public int Page { get; set; } = 1;

        private InfiniteScrollCollection<History> historyList;
        public InfiniteScrollCollection<History> HistoryList
        {
            get { return historyList; }
            set {
                SetProperty(ref historyList, value);
                ShowEmpty = historyList == null || historyList.Count == 0;
                ShowHasItems = historyList != null && historyList.Count > 0;
            }
        }

        private bool showEmpty;
        public bool ShowEmpty
        {
            get { return showEmpty; }
            set { SetProperty(ref showEmpty, value); }
        }

        private bool showHasItems;
        public bool ShowHasItems
        {
            get { return showHasItems; }
            set { SetProperty(ref showHasItems, value); }
        }

        public bool IsWorking { get; set; }

        public HistoryViewModel()
        {
            
        }

        public async Task FetchHistory(int startPage = 0, int pageSize = 10)
        {
            if (startPage == 0)
            {
                IsBusy = true;
                HistoryList = new InfiniteScrollCollection<History>
                {
                    OnLoadMore = async () =>
                    {
                        var appendedItems = await HistoryDataStore.ListAsync(order: order, pageNumber: Page, itemsPerPage: pageSize);
                        Page++;
                        return appendedItems;
                    }
                };
            }

            var items = await HistoryDataStore.ListAsync(order: order, pageNumber: startPage, itemsPerPage: pageSize);
            HistoryList.AddRange(items);
            base.OnPropertyChanged("historyList");
            
            if (startPage == 0)
            {
                IsBusy = false;
            }
        }

        public async void DeleteItem(int id, int pageSize = 10)
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                await HistoryDataStore.DeleteAsync(id);
                IsBusy = false;

                FetchHistory(0, pageSize);
            });
        }
    }
}

