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

namespace Shutdowner {
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl {

        Thickness LeftSide = new Thickness(-155, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -155, 0);
        SolidColorBrush Disabled = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        SolidColorBrush Enabled = new SolidColorBrush(Color.FromRgb(253,46,0));
        private bool clicked = false;

        public UserControl1() {
            InitializeComponent();
            Back.Fill = Disabled;
            Dot.Margin = LeftSide;
            clicked = false;
        }

        public bool Clicked { get => clicked; set => clicked = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(!clicked) {
                Back.Fill = Enabled;
                Dot.Margin = RightSide;
                clicked = true;
            }
            else {
                Back.Fill = Disabled;
                Dot.Margin = LeftSide;
                clicked = false;
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(!clicked) {
                Back.Fill = Enabled;
                Dot.Margin = RightSide;
                clicked = true;
            }
            else {
                Back.Fill = Disabled;
                Dot.Margin = LeftSide;
                clicked = false;
            }
        }
    }
}
