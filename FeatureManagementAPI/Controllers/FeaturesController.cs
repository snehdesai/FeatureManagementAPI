using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeatureManagementAPI.Models;
using FeatureManagementAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeaturesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Features
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            // Return all features from the database
            return await _context.Features.ToListAsync();
        }

        // GET: api/Features/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feature>> GetFeature(int id)
        {
            // Find the feature by ID
            var feature = await _context.Features.FindAsync(id);

            // Return 404 if the feature is not found
            if (feature == null)
            {
                return NotFound();
            }

            // Return the feature
            return feature;
        }

        // POST: api/Features
        [HttpPost]
        public async Task<ActionResult<Feature>> PostFeature(Feature feature)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the feature to the database
            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            // Return 201 Created with the location of the new resource
            return CreatedAtAction("GetFeature", new { id = feature.Id }, feature);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature(int id, Feature feature)
        {
            if (id != feature.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            _context.Entry(feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureExists(id))
                {
                    return NotFound(new { message = "Feature not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(feature); // Return the updated feature as JSON
        }

        // DELETE: api/Features/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            // Find the feature by ID
            var feature = await _context.Features.FindAsync(id);

            // Return 404 if the feature is not found
            if (feature == null)
            {
                return NotFound();
            }

            // Remove the feature from the database
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            // Return 204 No Content
            return NoContent();
        }

        // Method to check if a feature exists
        private bool FeatureExists(int id)
        {
            return _context.Features.Any(e => e.Id == id);
        }
    }
}