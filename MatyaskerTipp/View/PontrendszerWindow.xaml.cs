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
    /// Interaction logic for PontrendszerWindow.xaml
    /// </summary>
    public partial class PontrendszerWindow : Window
    {
        public PontrendszerWindow()
        {
            InitializeComponent();
        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            tbxHDVpont.IsEnabled = true;
        }
    }
}
