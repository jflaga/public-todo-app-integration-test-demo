namespace DataCollectorService;

public class BooksDataService
{
    public static IDictionary<string, int> BooksData { get; private set; } = new Dictionary<string, int>();

    public void Add(string bookTitle)
    {
        if (!BooksData.ContainsKey(bookTitle))
            BooksData.Add(bookTitle, 0);

        BooksData[bookTitle]++;
    }
}
