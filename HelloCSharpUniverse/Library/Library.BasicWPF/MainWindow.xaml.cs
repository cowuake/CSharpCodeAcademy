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

namespace Library.BasicWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button btn = new Button();
            btn.Background = new SolidColorBrush(Colors.White);
            btn.Content = new SolidColorBrush(Colors.BlanchedAlmond);
            btn.FontSize = 20;
            
            mainPanel.Children.Add(btn);

            this.MouseMove += (s, e) =>
            {
                var pos = e.GetPosition(this);
                this.Title = $"Hello, WPF! (X: {pos.X}, Y: {pos.Y})";
            };

            resetButton.MouseMove += bindBtn_MouseEnter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn == bindButton)
            {
                string input = textInput.Text;

                if (!string.IsNullOrEmpty(input))
                    textOutput.Text = input;
            }
            else if (btn == resetButton)
            {
                textOutput.Text = string.Empty;
            }
        }

        private void CopyTextBlock(object sender)
        {
            if (sender is Button)
            {

            }

        }

        private void bindBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            CopyTextBlock(sender);

            var pos = e.GetPosition(this);
            pointerCoords.Text = $"{pos.X} {pos.Y}";
        }
    }
}