namespace OOPLab1.Models
{
	public enum TaskPriority
	{
		Low,
		Medium,
		High
	}

	public enum TaskStatus
	{
		NotStarted,
		InProgress,
		Completed
	}


	public class TodoModel
	{
		private string _title = "Новая задача";
		private string _description = "";
		private DateTime _dueDate;
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title
		{
			get => _title;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Введите название задачи!");
				_title = value;
			}
		}

		public string Description
		{
			get => _description;
			set => _description = value ?? string.Empty;
		}

		public DateTime CreatedDate { get; init; } = DateTime.Now;
		public DateTime DueDate
		{
			get => _dueDate;
			set
			{
				if (value < CreatedDate)
					throw new ArgumentException("Дата дедлайна не может быть меньше даты создания задачи!");
				_dueDate = value;
			}
		}

		public TaskPriority Priority { get; set; } = TaskPriority.Medium;
		public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

		public TodoModel()
		{
			DueDate = CreatedDate.AddDays(1);
		}

		public TodoModel(string title, string description, DateTime dueDate, TaskPriority priority)
		{
			Title = title;
			Description = description;
			DueDate = dueDate;
			Priority = priority;
		}

		public void UpdateFrom(TodoModel source)
		{
            ArgumentNullException.ThrowIfNull(source);

            Title = source.Title;
			Description = source.Description;
			DueDate = source.DueDate;
			Priority = source.Priority;
			Status = source.Status;
		}
	}
}
