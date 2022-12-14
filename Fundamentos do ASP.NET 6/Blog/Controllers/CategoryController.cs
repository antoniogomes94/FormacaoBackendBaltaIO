using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> 
        GetAsync([FromServices]BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna no servidor"));
        }
    }
    
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> 
        GetByIdAsync([FromRoute] int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada!"));
        
            return Ok(new ResultViewModel<Category>(category) );
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
        }
    }
    
    [HttpPost("v1/categories/")]
    public async Task<IActionResult> 
        PostAsync([FromBody] EditorCategoryViewModel model, [FromServices]BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        
        try
        {
            var category = new Category()
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
    }
    
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> 
        PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices]BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
        
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
    }
    
    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> 
        DeleteAsync([FromRoute] int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada!"));
        
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
        }
    }
}