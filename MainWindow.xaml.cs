using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace MyCompani
{
    public class InsuranceCase
    {
        public DateTime Time { get; set; }
        public string Severity { get; set; }
        public string Location { get; set; }
    }
 
    public partial class MainWindow : Window
    {
        public ObservableCollection<InsuranceCase> Cases { get; set; }

        public const string DB = "DB.json";
        public MainWindow()
        {
            InitializeComponent();
            Cases = new ObservableCollection<InsuranceCase>();
            listView.ItemsSource = Cases;
                LoadData();
     


        }
        private void LoadData()
        {
            if (File.Exists(DB))
            {
                var json = File.ReadAllText(DB);
                var Convert = JsonConvert.DeserializeObject<List<InsuranceCase>>(json);

                foreach (var insuranceCase in Convert)
                {
                    Cases.Add(insuranceCase);
                }
            }
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(Cases.ToList(), Formatting.Indented);
            File.WriteAllText(DB, json);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyTabControl.SelectedItem = MyTabControl.Items[1];
        }
        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            MyTabControl.SelectedItem = MyTabControl.Items[0];
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedSeverity = (severityComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedLocation = (locationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selectedTime = datePicker.SelectedDate;

            if (selectedTime.HasValue && selectedSeverity != null && selectedLocation != null)
            {
                var newCase = new InsuranceCase
                {
                    Time = selectedTime.Value,
                    Severity = selectedSeverity,
                    Location = selectedLocation
                };
                Cases.Add(newCase);
                SaveData();



            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }
    }
}
