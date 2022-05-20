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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExamples.AnimationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DoubleAnimation animation = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(3)));
            RotateTransform rotateTransform = new RotateTransform();
            // ???????
            //img.RenderTransform = rotateTransform;
            //img.RenderTransformOrigin = new Point(0.5, 0.5);
            animation.RepeatBehavior = RepeatBehavior.Forever;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }
    }
}
