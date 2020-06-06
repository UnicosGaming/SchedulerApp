using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SchedulerApp.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleItemTemplate : ContentView
    {
        public ScheduleItemTemplate()
        {
            InitializeComponent();
        }
    }
}