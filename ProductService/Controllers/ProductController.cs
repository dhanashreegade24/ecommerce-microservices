using Microsoft.AspNetCore.Mvc;
using ProductService.Infrastructure.DTO;
using ProductService.Infrastructure.ServiceApp;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductServiceApp _service;

    public ProductController(ProductServiceApp service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        //test code to check cicd pipeline execution in the pipeline
        try
        {
            var conn = HttpContext.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetConnectionString("ProductDatabase");

            Console.WriteLine("REQUEST DB => " + conn);

            var products = await _service.GetProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR => " + ex.ToString());
            return StatusCode(500, ex.ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductDto dto)
    {
        await _service.AddProduct(dto);
        return Ok();
    }
}