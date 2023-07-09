using AutoMapper;
using CaseStudyApp.API.DatabaseContext;
using CaseStudyApp.API.Dtos;
using CaseStudyApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyApp.API.Services
{
    public class NoteService : INoteService
    {
        private readonly NoteAppDbContext _noteAppDbContext;
        private readonly IMapper _mapper;

        public NoteService(NoteAppDbContext noteAppDbContext,IMapper mapper)
        {
            _noteAppDbContext = noteAppDbContext;
            _mapper = mapper;
        }


        public void AddNote(NoteDto note)
        {
            var noteEntity = _mapper.Map<Note>(note);
            _noteAppDbContext.Notes.Add(noteEntity);
            _noteAppDbContext.SaveChanges();
        }

        public List<NoteDto> GetAll()
        {
            var notes = _noteAppDbContext.Notes.Where(x => x.IsDeleted == false).ToList();
            var noteDtos = _mapper.Map<List<NoteDto>>(notes);
            return noteDtos;
        }

        public Note GetById(int id)
        {
            var note = _noteAppDbContext.Notes.FirstOrDefault(x => x.Id == id);
            return note;
        }

        public void RemoveNote(int noteId)
        {
            var note = _noteAppDbContext.Notes.FirstOrDefault(x => x.Id == noteId);

            if (note != null)
            {
                _noteAppDbContext.Remove(note);
                _noteAppDbContext.SaveChanges();
            }
        }
        public void UpdateNote(NoteDto note)
        {

        }
    }
}
