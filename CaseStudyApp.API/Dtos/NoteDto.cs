namespace CaseStudyApp.API.Dtos
{
    public class NoteDto
    {
        public NoteDto()
        {

        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
