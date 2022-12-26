using System.Security.Claims;
using CommandsAndSnippets2.Data;
using CommandsAndSnippets2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippets2.Controllers
{
    /// <summary>
    /// Snippet Api controller. 
    /// </summary>
    [ApiController]
    public class SnippetController : ControllerBase
    {
        private readonly ILogger<SnippetController> _logger;
        private readonly SnippetsDbContext _snippetsDbContext;

        /// <inheritdoc />
        public SnippetController(ILogger<SnippetController> logger,  SnippetsDbContext snippetsDb)
        {
            _logger = logger;
            _snippetsDbContext = snippetsDb;
        }

        /// <summary>
        /// Gets Snippets categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/categories")]
        public ActionResult<List<Category>> GetCategories()
        {
            var (success, result) = _snippetsDbContext.GetCategories();

            if (success)
            {
                return Ok(result);
            }

            return new NotFoundResult();
        }
        
        /// <summary>
        /// Add a new category.
        /// </summary>
        /// <param name="categoryToCreate">Category data to add.</param>
        /// <returns></returns>
        [HttpPost("/api/categories")]
        public ActionResult AddCategory(CategoryCreate categoryToCreate)
        {
            var (success, result) = _snippetsDbContext.AddCategory(categoryToCreate);

            if (success)
            {
                return Ok(result);
            }

            return new BadRequestResult();
        }
         

        [HttpPost("snippets")]
        public ActionResult AddSnippet(SnippetCreate snippetToCreate)
        {
            bool success = false;
            var existingCategories = (from cat in snippetToCreate.Categories let existingCategory = _snippetsDbContext.Categories.Find(cat.Id) select existingCategory ?? cat).ToList();
            snippetToCreate.Categories = existingCategories;
            var newSnippet = new Snippet(snippetToCreate);
            newSnippet.CreatedDate = DateTime.Now;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Ok(new {Error = "Unable to add a new snippet."});
            newSnippet.UserId = userId;
            _snippetsDbContext.Snippets.Update(newSnippet);
            success = _snippetsDbContext.SaveChanges() >= 0;
            return success ? Ok(new SnippetUnique(newSnippet)) : Ok(new {Error = "Unable to add a new snippet."});
        }
        
        
        [HttpPost("snippets/{snippetId}/update")]
        public ActionResult UpdateSnippet(string snippetId, SnippetCreate snippetToCreate)
        {
            bool success = false;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var snippet = _snippetsDbContext.Snippets.FirstOrDefault(x => x.Id == snippetId);
                if (snippet != null)
                {
                    snippet.Update(snippetToCreate);
                    _snippetsDbContext.Snippets.Update(snippet);
                    success = _snippetsDbContext.SaveChanges() >= 0;
                    if (success)
                    {
                        return Ok(new SnippetUnique(snippet));
                    }
                }
                

            }

            return Ok(new {Error = "Unable to update snippet."});
        }
        
        

        [HttpGet("snippets")]
        public ActionResult GetSnippets()
        {
            var (success, result) = _snippetsDbContext.GetSnippets();

            if (success)
            {
                return Ok(result);
            }

            return Ok(new List<Snippet>());
        }

        [HttpGet("snippets/{id}")]
        public ActionResult GetSnippetById(string id)
        {
            var (success, result) = _snippetsDbContext.GetOneSnippetById(id);
            if (success)
            {
                return Ok(result);
            }

            return Ok(null);
        }
        
        [HttpPost("snippets/{id}/content")]
        public ActionResult AddContent(string id, Content content)
        {
            var snippet = _snippetsDbContext.Snippets.FirstOrDefault(x => x.Id == id);
            bool success;
            if (snippet != null)
            {
                content.CreatedDate = DateTime.Now;
                snippet.Contents.Add(content);
                success = _snippetsDbContext.SaveChanges() >= 0;
                if (success)
                {
                    return Ok(new {success = true});
                }
            }
           
            return Ok(new {error = "Unable to update content", success=false});
        }
        
        [HttpPost("snippets/{id}/contents")]
        public ActionResult AddContents(string id, List<Content> contents)
        {
            var snippet = _snippetsDbContext.Snippets.FirstOrDefault(x => x.Id == id);
            bool success;
            if (snippet == null) return Ok(new {error = "Unable to update contents", success = false});
            foreach (var content in contents)
            {
                content.CreatedDate = DateTime.Now;
                content.SnippetId = id;
            }
            _snippetsDbContext.Contents.AddRange(contents);
            success = _snippetsDbContext.SaveChanges() >= 0;
            return success ? Ok(new {success = true}) : Ok(new {error = "Unable to update contenta", success=false});
        }
        
        
        
        [HttpGet("snippets/{snippetId}/contents")]
        public ActionResult GetSnippetContents(string snippetId)
        {
            var snippet = _snippetsDbContext
                .Snippets
                .Include(x => x.Contents)
                .FirstOrDefault(x => x.Id == snippetId);
            bool success = snippet != null;
            if (success)
            {
                return Ok(snippet.Contents);

            }
            return Ok(new List<Content>());

        }
        
        
        
        [HttpPost("snippets/{id}/contents/update")]
        public ActionResult UpdateContents(string id, List<Content> contents)
        {
            var snippet = _snippetsDbContext.Snippets.FirstOrDefault(x => x.Id == id);
            bool success;
            if (snippet == null) return Ok(new {error = "Unable to update contents", success = false});
            _snippetsDbContext.Contents.UpdateRange(contents);
            success = _snippetsDbContext.SaveChanges() >= 0;
            return success ? Ok(new {success = true}) : Ok(new {error = "Unable to update contents", success=false});
        }
        
        

    } 
}

