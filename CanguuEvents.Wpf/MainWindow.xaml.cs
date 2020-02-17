using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CangguEvents.SQLite;
using Shared;

namespace CanguuEvents.Wpf
{
    public class MyObject //better to choose an appropriate name
    {
        public string ID { get; set; }

        public DateTime Date { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEventsRepository _repository;
        public ObservableCollection<EventInfo> Events { get; set; }

        public MainWindow(IEventsRepository repository)
        {
            _repository = repository;
            InitializeComponent();

            Events = new ObservableCollection<EventInfo>();
            DataGrid1.ItemsSource = Events;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Events.Clear();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var allEvents = await _repository.GetAllEvents();
                foreach (var eventInfo in allEvents)
                {
                    Events.Add(eventInfo);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Exception {exception.Message}");
            }
        }
    }
}