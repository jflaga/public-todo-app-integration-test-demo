using Microsoft.AspNetCore.Mvc;

namespace DataCollectorService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataCollectorController : ControllerBase
{
    private readonly BooksDataService booksDataService;

    public DataCollectorController(BooksDataService booksDataService)
    {
        this.booksDataService = booksDataService;
    }

    [HttpGet]
    public IActionResult GetData()
    {
        return Ok(BooksDataService.BooksData);
    }
}
