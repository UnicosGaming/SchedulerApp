using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using SchedulerApp.ViewModels.Base;

namespace SchedulerApp.ViewModels
{
    public class TeamSchedulePageViewModel : SchedulePageBase<TeamSchedule>
    {
        public TeamSchedulePageViewModel(INavigationService navigationService, 
            IPageDialogService pageDialogService, 
            IWriteRepository<TeamSchedule> writeRepository,
            IReadRepository<TeamSchedule> readRepository) 
            : base(navigationService, pageDialogService, writeRepository, readRepository)
        {

        }

    }
}
