using AutoMapper;
using CaseStudyApp.API.Dtos;
using CaseStudyApp.API.Entities;
using CaseStudyApp.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public NoteController(INoteService noteService,IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAllNotes()
        {
            var notes = _noteService.GetAll();
            return Ok(notes);
        }
        
        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            var note = _noteService.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public ActionResult<NoteDto> AddNote([FromBody] NoteDto noteDto)
        {
            _noteService.AddNote(noteDto);
            return CreatedAtAction(nameof(GetNoteById), new { id = 0 }, noteDto);
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveNoteById(int id)
        {
            _noteService.RemoveNote(id);
            return NoContent();
        }
    }
}
