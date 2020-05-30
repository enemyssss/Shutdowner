﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shutdown {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        static long time;

        public delegate void Timer();

        private void AcceptOperation_Click(object sender, RoutedEventArgs e) {
            string Command = null;
            var eingabe = InputBox.Text;

            if(!long.TryParse(eingabe, out time) && ToggleSwitch1.Toggled1 == false) {
                if(eingabe.Contains(':')) {
                    var currentTime = DateTime.Now;
                    if(DateTime.TryParse(eingabe,out DateTime targetTime)) {
                        if(targetTime.Hour <= currentTime.Hour && targetTime.Minute < currentTime.Minute) {
                            var lastDay = DateTime.DaysInMonth(currentTime.Year, currentTime.Month);
                            targetTime = new DateTime(
                                currentTime.Month == 12 && currentTime.Day == 31 ? currentTime.Year + 1 : currentTime.Year, 
                                currentTime.Day == lastDay ? currentTime.Month + 1 : currentTime.Month, 
                                currentTime.Day + 1, 
                                targetTime.Hour, 
                                targetTime.Minute,
                                0);
                        }
                        time = Convert.ToInt64(Math.Ceiling(targetTime.Subtract(currentTime).TotalSeconds));
                        ForceOperation(ref Command, time);
                    }
                }
                else {
                    InputBox.FontSize = 15;
                    InputBox.Text = "Zeitwert eingeben";
                }
            }
            else {
                time *= 60;
                ForceOperation(ref Command, time);
            }
        }

        private void ToggleSwitch1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(ToggleSwitch1.Toggled1) {
                InputBox.Text = "";
                InputBox.Visibility = Visibility.Hidden;
                TextShutdown.Visibility = Visibility.Hidden;
                InputBox.IsEnabled = false;
                AcceptOperation.Focus();
            }
            else {
                InputBox.Visibility = Visibility.Visible;
                TextShutdown.Visibility = Visibility.Visible;
                InputBox.IsEnabled = true;
            }
        }

        private void ToggleSwitch2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            AcceptOperation.Focus();
        }

        private void CancelOperation_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start("shutdown", " /a");
            RunningOp.Text = "";
            CancelOperation.Visibility = Visibility.Hidden;
            RunningOp.Visibility = Visibility.Hidden;
            ToggleSwitch1.Visibility = Visibility.Visible;
            ToggleSwitch2.Visibility = Visibility.Visible;
            AcceptOperation.Visibility = Visibility.Visible;
            TextShutdown.Visibility = Visibility.Visible;
            InputBox.Visibility = Visibility.Visible;
            TextSofort.Visibility = Visibility.Visible;
            TextVoll.Visibility = Visibility.Visible;
            AcceptOperation.Visibility = Visibility.Visible;
            InputBox.Text = "";
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e) {
            Close();
        }

        private void InputBox_MouseEnter(object sender, MouseEventArgs e) {
            InputBox.FontSize = 20;
            InputBox.IsEnabled = true;
            InputBox.Text = "";
        }

        private void ForceOperation(ref string Command, long time) {
           Command = ToggleSwitch1.Toggled1 == true ? ToggleSwitch2.Toggled1 == true ? $" /s /t 0 /f" : $" /hybrid /s /t 0 /f" : ToggleSwitch2.Toggled1 == true ? $" /s /t {time} /f" : $" /hybrid /s /t {time} /f";

            if(System.Diagnostics.Process.Start("shutdown", Command) != null) {
                ToggleSwitch1.Visibility = Visibility.Hidden;
                ToggleSwitch2.Visibility = Visibility.Hidden;
                AcceptOperation.Visibility = Visibility.Hidden;
                TextShutdown.Visibility = Visibility.Hidden;
                InputBox.Visibility = Visibility.Hidden;
                TextSofort.Visibility = Visibility.Hidden;
                TextVoll.Visibility = Visibility.Hidden;
                AcceptOperation.Visibility = Visibility.Hidden;
                RunningOp.Visibility = Visibility.Visible;
                CancelOperation.Visibility = Visibility.Visible;
                RunningOp.Text = "Shutdown um ";
                RunningOp.Text += DateTime.Now.AddSeconds(time).ToString("HH:mm");
            }
        }
    }
}