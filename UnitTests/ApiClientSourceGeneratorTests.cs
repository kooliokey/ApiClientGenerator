namespace UnitTests;

[UsesVerify]
public class ApiClientSourceGeneratorTests
{
    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // The source code to test
        var source = @"
[GenerateApiClient]
[ApiController]
[Route(""[controller]"")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        ""Freezing"", ""Bracing"", ""Chilly"", ""Cool"", ""Mild"", ""Warm"", ""Balmy"", ""Hot"", ""Sweltering"", ""Scorching""
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
