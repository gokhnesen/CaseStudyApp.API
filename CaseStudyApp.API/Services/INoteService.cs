using CaseStudyApp.API.Dtos;
using CaseStudyApp.API.Entities;

namespace CaseStudyApp.API.Services
{
    public interface INoteService
    {
        void AddNote(NoteDto note);
        List<NoteDto> GetAll();
        Note GetById(int id);
        void RemoveNote(int noteId);
        void UpdateNote(NoteDto note);
    }
}
