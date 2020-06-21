using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using SchedulerApp.ViewModels.Base;

namespace SchedulerApp.ViewModels
{
    public class TeamSchedulePageViewModel : SchedulePageBase<TeamSchedule>
    {
        public TeamSchedulePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IScheduleRepository<TeamSchedule> teamScheduleRepository) 
            : base(navigationService, pageDialogService, teamScheduleRepository)
        {

        }
    }
}
