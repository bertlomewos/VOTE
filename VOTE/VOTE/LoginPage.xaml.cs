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
using VOTE.Model;

namespace VOTE
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string UID = id.Text;
            string Pass = pass.Password;

            GetFromDb gd = new GetFromDb();
            PartyPage.UID = UID;

            var (exists, role) = gd.GetfromUsers(UID, Pass);
            gd.GetParties(UID);



            if (exists)
            {
                if (role == "Party")
                {
                    PartyPage pp = new PartyPage();
                    pp.Show();
                    this.Close();
                }
                else if(role == "voter") { 
                    MainPage mainPage = new MainPage();
                    mainPage.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid user ID or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterPageBtn(object sender, MouseButtonEventArgs e)
        {
            RegisterPage rp = new RegisterPage();
            rp.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
                    }
    }
}
