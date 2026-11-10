using OOPLab1.Models;
using System.Windows;

namespace OOPLab1
{
    public partial class TaskWindow : Window
    {
        public TodoModel TaskResult { get; private set; }

        public TaskWindow(TodoModel? task = null)
        {
            InitializeComponent();

            CbPriority.ItemsSource = Enum.GetValues(typeof(TaskPriority));
            CbStatus.ItemsSource = Enum.GetValues(typeof(Models.TaskStatus));

            if (task != null)
            {
                TaskResult = new TodoModel
                {
                    Id = task.Id,
                    CreatedDate = task.CreatedDate,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    Status = task.Status
                };

                TxtTitle.Text = task.Title;
                TxtDescription.Text = task.Description;
                DpDueDate.SelectedDate = task.DueDate;
                CbPriority.SelectedItem = task.Priority;
                CbStatus.SelectedItem = task.Status;
            }
            else
            {
                TaskResult = new TodoModel();
                DpDueDate.SelectedDate = DateTime.Now.AddDays(1);
                CbPriority.SelectedIndex = 1;
                CbStatus.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskResult.Title = TxtTitle.Text;
                TaskResult.Description = TxtDescription.Text;
                TaskResult.DueDate = DpDueDate.SelectedDate ?? DateTime.Now;
                TaskResult.Priority = (TaskPriority)CbPriority.SelectedItem;
                TaskResult.Status = (Models.TaskStatus)CbStatus.SelectedItem;

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
