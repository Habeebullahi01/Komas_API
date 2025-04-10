using Komas_API.Models;
using Komas_API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Komas_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all items", Description = "Fetches a list of available items.")]
    [SwaggerResponse(200, "Successfully retrieved items", typeof(List<Product>))]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Successfully retrieved items", typeof(Product))]
    public IActionResult GetById(int id)
    {

        Product? product = _productService.GetById(id);
        if (product != null)
        {
            return Ok(product);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    [SwaggerResponse(200, "Successfully retrieved items", typeof(Product))]
    public IActionResult CreateProduct([FromBody] ProductDto newProduct)
    {
        if (newProduct.ProductName == "" || newProduct.ProductName == null)
        {
            // throw new Exception("Product Name should not be empty");
            ModelState.AddModelError("name", "Name cannot be empty");
        }
        if (newProduct.Price == 0)
        {
            // throw new Exception("Product Price should be greater than zero");
            ModelState.AddModelError("price", "Price should be greater than zero");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var p = _productService.CreateProduct(newProduct);
        return CreatedAtAction(nameof(CreateProduct), nameof(ProductController), p);
    }

    [HttpPatch("{id}")]
    [SwaggerResponse(200, "Successfully modified item", typeof(Product))]
    public IActionResult UpdateById(int id, [FromBody] ProductDto product)
    {
        Product? product1 = _productService.UpdateById(id, product);
        if (product1 == null)
        {
            return NotFound();
        }
        return AcceptedAtAction(nameof(CreateProduct), new { id = product1.ProductId }, product1);

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        var deleteResult = _productService.DeleteById(id);
        if (deleteResult)
        {

            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }


}