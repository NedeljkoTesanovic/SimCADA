using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddSignalWindow.xaml
    /// </summary>
    public partial class AddSignalWindow : Window
    {
        public AddSignalWindow()
        {
            InitializeComponent();
            Reset();
        }

        public void Reset()
        {
            rbtn_analogue.IsChecked = true;
            rbtn_input.IsChecked = true;
            txb_address.Text = "Address";
            txb_description.Text = "Description";
            txb_initialValue.Text = "Initial Value";
            txb_scanTime.Text = "Scan Time";
            txb_tag.Text = "Tag (id)";
            txb_units.Text = "Units";
            rbtn_digital.IsChecked = false;
            rbtn_input.IsChecked = true;
            rbtn_analogue.IsChecked = true;
            rbtn_output.IsChecked = false;
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_analogue.IsChecked == true)
            {
                if((rbtn_input.IsChecked) == true){
                    //AI
                }
                else
                {
                    //AO
                }
            }
            else
            {
                if ((rbtn_input.IsChecked) == true)
                {
                    //DI
                }
                else
                {
                    //DO
                }
            }
        }

        private void rbtn_analogue_Checked(object sender, RoutedEventArgs e)
        {
            txb_units.IsEnabled = true;
        }

        private void rbtn_digital_Checked(object sender, RoutedEventArgs e)
        {
            txb_units.IsEnabled = false;
        }

        private void rbtn_input_Checked(object sender, RoutedEventArgs e)
        {
            txb_initialValue.IsEnabled = false;
            txb_scanTime.IsEnabled = true;
        }

        private void rbtn_output_Checked(object sender, RoutedEventArgs e)
        {
            txb_initialValue.IsEnabled = true;
            txb_scanTime.IsEnabled = false;
        }
    }
}
