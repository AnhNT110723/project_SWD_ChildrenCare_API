using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Children_Care_API.Models;
using System;
using Children_Care_API.Data;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalPrescriptionController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;

        public MedicalPrescriptionController(ChildrenCareDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalPrescription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalPrescription>>> GetPrescriptions()
        {
            return await _context.MedicalPrescriptions.ToListAsync();
        }

        // GET: api/MedicalPrescription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalPrescription>> GetPrescription(int id)
        {
            var prescription = await _context.MedicalPrescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return prescription;
        }

        // POST: api/MedicalPrescription
        [HttpPost]
        public async Task<ActionResult<MedicalPrescription>> PostPrescription(MedicalPrescription prescription)
        {
            _context.MedicalPrescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.Id }, prescription);
        }

        // PUT: api/MedicalPrescription/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription(int id, MedicalPrescription prescription)
        {
            if (id != prescription.Id)
            {
                return BadRequest();
            }
            _context.Entry(prescription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/MedicalPrescription/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.MedicalPrescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            _context.MedicalPrescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
