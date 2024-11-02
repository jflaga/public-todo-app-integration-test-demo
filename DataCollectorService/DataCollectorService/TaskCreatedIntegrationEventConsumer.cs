using MassTransit;
using PublicTodoApp.Etos;

namespace DataCollectorService;

public class TaskCreatedIntegrationEventConsumer : IConsumer<TaskCreatedIntegrationEvent>
{
    private readonly BooksDataService booksDataService;

    public TaskCreatedIntegrationEventConsumer(BooksDataService booksDataService)
    {
        this.booksDataService = booksDataService;
    }

    public async Task Consume(ConsumeContext<TaskCreatedIntegrationEvent> context)
    {
        // if format is "Read '<title of book>'"
        if (context.Message.Task.StartsWith("Read"))
        {
            var bookTitle = context.Message.Task.Replace("'", "").Replace("Read ", "");
            booksDataService.Add(bookTitle);
        }
    }
}
