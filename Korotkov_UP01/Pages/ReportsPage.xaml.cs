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

namespace Korotkov_UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        string defaultQCPContent;
        int FaultTypeCount;
        int RequestCount;
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void AVGTimeCompletedRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPage();
            AVGTimeCompletedRequestsLabel.Visibility = Visibility.Visible;

            var requests = Entities.GetContext().Request.ToList();
            if (!requests.Any())
            {
                AVGTimeCompletedRequestsLabel.Content = "Нет данных для анализа.";
                return;
            }

            float counter = 0;
            float time = 0;

            foreach (var request in requests)
            {
                if (request.date_created == null) continue;

                DateTime startDate = (DateTime)request.date_created;
                DateTime endDate = request.status_id == 3 && request.date_ended != null
                    ? (DateTime)request.date_ended
                    : DateTime.Now;

                time += (endDate - startDate).Days;
                counter++;
            }

            if (counter == 0)
            {
                AVGTimeCompletedRequestsLabel.Content = "Нет завершенных заявок.";
                return;
            }

            AVGTimeCompletedRequestsLabel.Content = "Среднее время выполнения заявки: " + (time / counter).ToString("F2") + " дней";
        }

        private void QuantityCompletedRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPage();
            QuantityCompletedRequestsLabel.Visibility = Visibility.Visible;
            QuantityCompletedRequestsLabel.Content = defaultQCPContent + " " + (Entities.GetContext().Request.Where(x => x.status_id == 3).Count().ToString());
        }

        private void StatisticsOnTypesOfFaultsButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPage();

            FaultTypeCount = Entities.GetContext().FaultType.Count();

            for (int i = 1; i <= FaultTypeCount; i++)
            {
                FillingLabelsContent(i.ToString());
            }
        }

        private void FillingLabelsContent(string labelName)
        {
            int id = int.Parse(labelName);
            string FaultTypeName = Entities.GetContext().FaultType.Where(x => x.fault_type_id == id).Select(u => u.description).FirstOrDefault();
            string FaultTypeCount = Entities.GetContext().Request.Where(x => x.fault_type_id == id).Count().ToString();
            Label label = new Label()
            {
                Name = "FaultTypeQuantity" + labelName,
                Content = FaultTypeName + " " + FaultTypeCount,
                FontSize = 12,
                FontFamily = new System.Windows.Media.FontFamily("Monotype Corsiva"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            StatisticsLabelsContainer.Children.Add(label);
        }

        private void ClearPage()
        {
            StatisticsLabelsContainer.Children.Clear();
            QuantityCompletedRequestsLabel.Visibility = Visibility.Hidden;
            AVGTimeCompletedRequestsLabel.Visibility = Visibility.Hidden;
        }

    }
}
