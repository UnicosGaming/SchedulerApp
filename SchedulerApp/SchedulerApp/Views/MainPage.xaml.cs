using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.Views
{
    public partial class MainPage : ContentPage
    {
        // Change the color of the selected cell
        // https://stackoverflow.com/a/53079812/844372

        ViewCell _lastCell;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (_lastCell != null)
                _lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#4F4F4F");
                _lastCell = viewCell;
            }
        }
    }
}