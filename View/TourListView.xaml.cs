﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
