﻿using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
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

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for UjMeccsWindow.xaml
    /// </summary>
    public partial class UjMeccsWindow : Window
    {
        public UjMeccsWindow()
        {
            InitializeComponent();
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnHozzadas_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
            try
            {
                if (tbxHazai.Text != null && tbxVendeg.Text != null && dpHatarido.SelectedDate > DateTime.Now)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(SqlCommans.addMatch1, conn);
                    cmd.Parameters.AddWithValue("@homeName", tbxHazai.Text);
                    cmd.Parameters.AddWithValue("@guestName", tbxVendeg.Text);
                    cmd.Parameters.AddWithValue("@date", dpHatarido.SelectedDate);
                    cmd.Parameters.AddWithValue("@homeGoals", 0);
                    cmd.Parameters.AddWithValue("@guestGoals", 0);
                    cmd.Parameters.AddWithValue("@isAvailable", 1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sikeres hozzáadás!");
                    tbxHazai.Text = "";
                    tbxVendeg.Text = "";
                }
                else
                {
                    if(tbxHazai.Text != null && tbxVendeg.Text != null && dpHatarido.SelectedDate < DateTime.Now)
                    MessageBox.Show("Nem megfelelő határidő!");
                    else
                    {
                        MessageBox.Show("Minden mező kitöltése kötelező!");
                    }
                }conn.Close();
            }
            catch (Exception ex)

            { MessageBox.Show(ex.Message); }
        }
    }
}
