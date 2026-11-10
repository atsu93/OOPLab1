using OOPLab1.Models;
using OOPLab1.Services;
using System.Windows;

namespace OOPLab1
{
    public partial class MainWindow : Window
    {
        private readonly TodoService _todoService;

        public MainWindow()
        {
            InitializeComponent();

            ITaskRepository repository = new JsonTaskRepository();
            _todoService = new TodoService(repository);

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            DgTasks.ItemsSource = _todoService.GetAllTasks();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new TaskWindow();
            taskWindow.Owner = this;

            if (taskWindow.ShowDialog() == true)
            {
                try
                {
                    _todoService.AddTask(taskWindow.TaskResult);
                    UpdateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось сохранить задачу: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedItem is TodoModel selectedTask)
            {
                var taskWindow = new TaskWindow(selectedTask);
                taskWindow.Owner = this;

                if (taskWindow.ShowDialog() == true)
                {
                    try
                    {
                        _todoService.UpdateTask(taskWindow.TaskResult);
                        UpdateGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось изменить задачу: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите задачу в таблице!", "Внимание");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedItem is TodoModel selectedTask)
            {
                var result = MessageBox.Show($"Удалить задачу \"{selectedTask.Title}\"?", "Удаление", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _todoService.DeleteTask(selectedTask.Id);
                        UpdateGrid();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Не удалось удалить задачу: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Сначала выберите задачу для удаления!", "Внимание");
            }
        }
    }
}
