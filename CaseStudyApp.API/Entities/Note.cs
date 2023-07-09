using CaseStudyApp.API.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyApp.API.Entities
{
    public class Note : IAudit
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual Note Parent { get; set; }
        public virtual List<Note> Children { get; set; }
        public bool IsDeleted { get; set; }
    }
}
