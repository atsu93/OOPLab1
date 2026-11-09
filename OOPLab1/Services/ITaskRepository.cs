using OOPLab1.Models;

namespace OOPLab1.Services
{
    public interface ITaskRepository
    {
        void Save(IEnumerable<TodoModel> tasks);
        List<TodoModel> Load();
    }
}
