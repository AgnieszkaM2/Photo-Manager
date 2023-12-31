﻿using Photo_Manager.AdditionalResources;
using Photo_Manager.Commands;
using Photo_Manager.ViewModels;
using Photo_Manager.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Photo_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnAppLogo_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAddDirectory_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new System.Windows.Thickness(10,10,10,40);
            }
            else
            {
                this.BorderThickness = new System.Windows.Thickness(0);
            }
        }

        private void btnAppTheme_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentResources.CurrentTheme == "Light")
            {
                AppTheme.ChangeTheme(new Uri("Assets/DarkTheme.xaml", UriKind.Relative));
                CurrentResources.CurrentTheme = "Dark";
            }
            else if(CurrentResources.CurrentTheme == "Dark")
            {
                AppTheme.ChangeTheme(new Uri("Assets/LightTheme.xaml", UriKind.Relative));
                CurrentResources.CurrentTheme = "Light";
            }
        }
    }
}
