using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SchedulerApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        ObservableCollection<Schedule> _items;
        private readonly IDataService _dataService;

        public ObservableCollection<Schedule> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        public DelegateCommand<Schedule> ItemTappedCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IDataService dataService) : base(navigationService)
        {
            _dataService = dataService;

            ItemTappedCommand = new DelegateCommand<Schedule>((x) => Debug.WriteLine(x.Competition), (x) => true);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsTaskRunning = true;

                var schedules = await _dataService.Get();
                Items = new ObservableCollection<Schedule>(schedules.OrderBy(x => x.Date));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"### [MainPageViewModel - Initialize] Error: {ex.Message}");
            }
            finally
            {
                IsTaskRunning = false;
            }


        }
    }
}
