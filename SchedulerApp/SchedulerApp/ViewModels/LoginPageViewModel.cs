using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Configuration;
using SchedulerApp.Models;
using SchedulerApp.Providers;
using SchedulerApp.Repositories;
using SchedulerApp.Services.DataService;
using SchedulerApp.Services.IdentityService;
using SchedulerApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IGroupRepository _groupRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPageRepository _pageRepository;

        //private readonly ISqlDataService _sqlDataService;

        public DelegateCommand LoginCommand { get; private set; }

        private bool _isLoginActive;
        public bool IsLoginActive
        {
            get => _isLoginActive;
            set => SetProperty(ref _isLoginActive, value);
        }

        public LoginPageViewModel(INavigationService navigationService, 
            IIdentityService identityService, 
            IPageDialogService pageDialogService,
            IGroupRepository groupRepository,
            ITeamRepository teamRepository,
            IPageRepository pageRepository) : base(navigationService)
        {
            _identityService = identityService;
            _pageDialogService = pageDialogService;
            _groupRepository = groupRepository;
            _teamRepository = teamRepository;
            _pageRepository = pageRepository;
            //_sqlDataService = sqlDataService;

            LoginCommand = new DelegateCommand(() => Retry(), () => CanRetry()).ObservesCanExecute(() => IsLoginActive);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            var is_logout = parameters["logout"] ?? false;

            if ((bool)is_logout)
                IsLoginActive = true;
            else
                await ValidateUser();
        }

        private bool CanRetry()
        {
            return IsLoginActive;
        }

        private void Retry()
        {
            ValidateUser();
        }

        private async Task ValidateUser()
        {
            try
            {
                IsLoginActive = false;
                IsTaskRunning = true;

                var user = await TryLoginAsync();
                var group = await GetGroupInfoAsync(user.Group.Id);

                user.Group = group;

                Session.SetUser(user);

                await NavigationService.NavigateAsync("/NavigationPage/MainPage");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await _pageDialogService.DisplayAlertAsync("Error on login", "Cannot validate user or retrieve info", "OK");

                IsLoginActive = true;
            }
            finally
            {
                IsTaskRunning = false;
            }
        }

        private async Task<User> TryLoginAsync()
        {
            return await _identityService.Login();
        }

        /// <summary>
        /// Get all teams and pages associated to the group with @groupId
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>All data related to the group. Teams and pages.</returns>
        private async Task<Group> GetGroupInfoAsync(string groupId)
        {
            try
            {
                var group = await _groupRepository.GetGroupInfoAsync(groupId);
                var teams = await _teamRepository.GetTeamsFromGroupAsync(groupId);

                foreach (var t in teams)
                {
                    t.Page = await _pageRepository.GetPageByTeamAsync(t.Id);
                }

                group.Teams = teams;

                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task NavigateToMainPage(User user)
        {
            var parameter = new NavigationParameters() { { "user", user } };
            await NavigationService.NavigateAsync("/NavigationPage/MainPage", parameter);
        }
    }
}
