using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using SchedulerApp.ViewModels.Base;

namespace SchedulerApp.ViewModels
{
    public class MotorSchedulePageViewModel : SchedulePageBase<MotorSchedule>
    {
        public MotorSchedulePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IScheduleRepository<MotorSchedule> motorScheduleRepository)
            : base(navigationService, pageDialogService, motorScheduleRepository)
        {
        }
    }
}
