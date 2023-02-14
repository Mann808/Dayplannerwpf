using Dayplanner.Class1;
using Dayplanner.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Dayplanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\datalist.json";
        private Kompdecom _kompdecom;
        private BindingList<Model> datalist ;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dgdayplanner_Loaded(object sender, RoutedEventArgs e)
        {
            _kompdecom = new Kompdecom(PATH);
            try
            {
                datalist = _kompdecom.Loaddata();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            dgdayplanner.ItemsSource= datalist;
            datalist.ListChanged += Datalist_ListChanged;
        }

        private void Datalist_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _kompdecom.savedata(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
