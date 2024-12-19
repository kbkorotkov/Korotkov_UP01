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
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Page
    {
        private Request requests = new Request();
        public AddRequest(Request selectRequest)
        {
            InitializeComponent();
            if (selectRequest != null)
            {
                Add.Content = "Сохранить";

                requests = selectRequest;

            }
            equipment.ItemsSource = Entities.GetContext().Equipment.Select(u => u.description).ToList();
            faultType.ItemsSource = Entities.GetContext().FaultType.Select(u => u.description).ToList();
            client.ItemsSource = Entities.GetContext().Client.Select(u => u.client_surname).ToList();
            technician.ItemsSource = Entities.GetContext().Technician.Select(u => u.technician_surname).ToList();
            status.ItemsSource = Entities.GetContext().Status.Select(u => u.status_name).ToList();
            priority.ItemsSource = Entities.GetContext().Priority.Select(u => u.priority_name).ToList();

            DataContext = requests;
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Requests());
        }

        private int ReturnPriority(string addPriority)
        {
            return Entities.GetContext().Priority.
                Where(x => x.priority_name == addPriority).
                Select(u => u.priority_id).FirstOrDefault();
        }

        private int ReturnStatus(string addStatus)
        {
            return Entities.GetContext().Status.
                Where(x => x.status_name == addStatus).
                Select(u => u.status_id).FirstOrDefault();
        }

        private int ReturnEquipment(string addEquipment)
        {
            return Entities.GetContext().Equipment.
                Where(x => x.description == addEquipment).
                Select(u => u.equipment_id).FirstOrDefault();
        }

        private int ReturnFaultType(string addFaultType)
        {
            return Entities.GetContext().FaultType.
                Where(x => x.description == addFaultType).
                Select(u => u.fault_type_id).FirstOrDefault();
        }

        private int ReturnClient(string addClient)
        {
            return Entities.GetContext().Client.
                Where(x => x.client_surname == addClient).
                Select(u => u.client_id).FirstOrDefault();
        }

        private int ReturnTehnician(string addTehnician)
        {
            return Entities.GetContext().Technician.
                Where(x => x.technician_surname == addTehnician).
                Select(u => u.technician_id).FirstOrDefault();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string dateTime = DateCreate.Text;
            string endTime = DateEnd.Text;
            string addNumberRequest = NumberRequest.Text;
            string addEquipment = equipment.Text;
            string addFaultType = faultType.Text;
            string addStatus = status.Text;
            string addPriority = priority.Text;
            string addProblemDescription = descriptionProblem.Text;
            string addClient = client.Text;
            string addTechnician = technician.Text;

            if (string.IsNullOrEmpty(dateTime) || string.IsNullOrEmpty(addClient) || string.IsNullOrEmpty(addStatus) || string.IsNullOrEmpty(addPriority) ||
                string.IsNullOrEmpty(addProblemDescription) || string.IsNullOrEmpty(addTechnician) || string.IsNullOrEmpty(addNumberRequest) ||
                string.IsNullOrEmpty(addEquipment) || string.IsNullOrEmpty(addFaultType) || string.IsNullOrEmpty(endTime))
            { MessageBox.Show("Вы не заполнили все поля! Попробуйте ещё раз"); }
            else
            {
                if (DateTime.TryParse(endTime, out DateTime dateEnd) && (DateTime.TryParse(dateTime, out DateTime dateCreate)) && int.TryParse(addNumberRequest, out int numberRequest))
                {
                    requests.date_created = dateCreate;
                    requests.date_ended = dateEnd;
                    requests.priority_id = ReturnPriority(addPriority);
                    requests.status_id = ReturnStatus(addStatus);
                    requests.equipment_id = ReturnEquipment(addEquipment);
                    requests.problem_description = addProblemDescription;
                    requests.request_id = int.Parse(addNumberRequest);
                    requests.fault_type_id = ReturnFaultType(addFaultType);
                    requests.client_id = ReturnClient(addClient);
                    requests.technician_id = ReturnTehnician(addTechnician);

                    var request = Entities.GetContext().Request.Where(x => x.request_id == requests.request_id).FirstOrDefault();
                    if (request == null)
                    {
                        Entities.GetContext().Request.Add(requests);
                        try
                        {
                            Entities.GetContext().SaveChanges();
                            MessageBox.Show("Заявка добавлена");
                            NavigationService.Navigate(new Requests());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        try
                        {
                            var db = new Entities();
                            var currentRequest = db.Request.Where(x => x.request_id == requests.request_id).FirstOrDefault();

                            if (currentRequest != null)
                            {
                                currentRequest.technician_id = requests.technician_id;
                                currentRequest.client_id = requests.client_id;
                                currentRequest.problem_description = requests.problem_description;
                                currentRequest.equipment_id = requests.equipment_id;
                                currentRequest.fault_type_id = requests.fault_type_id;
                                currentRequest.status_id = requests.status_id;

                                if (requests.status_id == ReturnStatus("Завершено"))
                                {
                                    currentRequest.date_ended = DateTime.Now;
                                }
                                else
                                {
                                    currentRequest.date_ended = requests.date_ended;
                                }

                                db.SaveChanges();
                                MessageBox.Show("Вы успешно изменили данные");
                                NavigationService.Navigate(new Requests());
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка: {ex.Message}");
                        }
                    }
                }
            }

        }

    }
}
