using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tour_planner.ViewModel;

namespace tour_planner.View
{
    /// <summary>
    /// Interaction logic for AddEditTour.xaml
    /// </summary>
    public partial class AddTourView : Window
    {
        public AddTourView()
        {
            InitializeComponent();
            
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
