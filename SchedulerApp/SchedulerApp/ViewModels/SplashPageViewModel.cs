using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using SchedulerApp.Services.IdentityService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SchedulerApp.ViewModels
{
    public class SplashPageViewModel : ViewModelBase
    {
        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialogService;
        private readonly ISqlDataService _sqlDataService;

        public SplashPageViewModel(INavigationService navigationService, IIdentityService identityService, IPageDialogService pageDialogService, ISqlDataService sqlDataService) : base(navigationService)
        {
            _identityService = identityService;
            _pageDialogService = pageDialogService;
            _sqlDataService = sqlDataService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                IsTaskRunning = true;

                var user = await TryLoginAsync();
                var group = await GetGroupInfoAsync(user.Group.Id);

                user.Group = group;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                var group = await _sqlDataService.GetGroupInfoAsync(groupId);
                var teams = await _sqlDataService.GetGroupTeamsAsync(groupId);

                foreach (var t in teams)
                {
                    t.Page = await _sqlDataService.GetTeamPageAsync(t.Id);
                }

                group.Teams = teams;

                return group;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
