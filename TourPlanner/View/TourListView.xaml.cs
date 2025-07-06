
using System.Windows.Controls;

using System.Windows.Input;
using tour_planner.ViewModel;

namespace tour_planner.View
{
    /// <summary>
    /// Interaction logic for Routes.xaml
    /// </summary>
    public partial class Routes : UserControl
    {
        public Routes()
        {
            InitializeComponent();

        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is TourListViewModel viewModel && viewModel.OpenDetailsPage.CanExecute(null))
            {
                viewModel.OpenDetailsPage.Execute(null);
            }
        }

    }
}
