using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.InsertBookWPF.Commands
{
    public static class InsertCommands
    {
        public static readonly RoutedUICommand InsertBookCommand = new RoutedUICommand(
            "Insert book",
            "InsertBook",
            typeof(CreateCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Control),
                new KeyGesture(Key.A, ModifierKeys.Alt),
            }
            );

        public static readonly RoutedUICommand ExitCommand = new RoutedUICommand(
            "Exit app",
            "Exit",
            typeof(CreateCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Q, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand ResetCommand = new RoutedUICommand(
            "Reset text fields",
            "Reset",
            typeof(CreateCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Q, ModifierKeys.Control),
            }
            );
    }
}
