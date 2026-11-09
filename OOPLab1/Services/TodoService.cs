using OOPLab1.Models;

namespace OOPLab1.Services
{
    public class TodoService
    {
        private readonly ITaskRepository _repository;
        private readonly List<TodoModel> _tasks;

        public TodoService(ITaskRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);
            _repository = repository;
            _tasks = _repository.Load();
        }

        public IReadOnlyList<TodoModel> GetAllTasks() => _tasks.AsReadOnly();

        public void AddTask(TodoModel task)
        {
            ArgumentNullException.ThrowIfNull(task);
            _tasks.Add(task);
            _repository.Save(_tasks);
        }

        public void UpdateTask(TodoModel updatedTask)
        {
            ArgumentNullException.ThrowIfNull(updatedTask);
            var task = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (task != null)
            {
                task.UpdateFrom(updatedTask);
                _repository.Save(_tasks);
            }
        }

        public void DeleteTask(Guid taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
                _repository.Save(_tasks);
            }
        }
    }
}
