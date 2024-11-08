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
using System.Windows.Shapes;

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for BajnoksagWindow.xaml
    /// </summary>
    public partial class BajnoksagWindow : Window
    {
        public BajnoksagWindow()
        {
            InitializeComponent();
        }

        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            BajnoksagSzerkesztesWindow window = new BajnoksagSzerkesztesWindow();
            window.Show();
        }

        private void btnUjBajnoksag_Click(object sender, RoutedEventArgs e)
        {
            UjBajknoksagWindow window = new UjBajknoksagWindow();
            window.Show();
        }
    }
}
