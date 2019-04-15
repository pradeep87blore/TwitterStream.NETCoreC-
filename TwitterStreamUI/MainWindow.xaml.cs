using System.Windows;
using System.Windows.Input;
//using TwitterAccess;

/*
 * 
 * Use the TwitterStreamAPIProvider to access the required twitter info */

namespace TwitterStreamUI
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

        private void TextBox_user_screen_name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Enter)
            {
                return;
            }

            PopulateUI();
        }

        private void PopulateUI()
        {
            //UserObject userObj;
        }
    }
}
