using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace Диплом
{
    public partial class CalendarWindow : Window
    {
        public CalendarWindow()
        {
            InitializeComponent();
        }

        // Пример данных событий
        private Dictionary<DateTime, List<Event>> events = new Dictionary<DateTime, List<Event>>()
        {
            { DateTime.Today, new List<Event>
                {
                    new Event { Time = "10:00", Description = "Встреча с клиентом" },
                    new Event { Time = "14:00", Description = "Обед" },
                    new Event { Time = "13:00", Description = "Завтрак" }
                }
            },
            { DateTime.Today.AddDays(1), new List<Event>
                {
                    new Event { Time = "09:00", Description = "Совещание" }
                }
            }
        };

        private void EventCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = EventCalendar.SelectedDate.Value;

                if (events.ContainsKey(selectedDate))
                {
                    EventListView.ItemsSource = events[selectedDate];
                }
                else
                {
                    EventListView.ItemsSource = null;
                }
            }
        }

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow MWindow = new MainWindow();
        //    MWindow.Show();
        //    // Закрытие текущего окна выбора пользователя
        //    this.Close();
        //}



        public class Event
        {
            public string Time { get; set; }
            public string Description { get; set; }
        }
    }
}
