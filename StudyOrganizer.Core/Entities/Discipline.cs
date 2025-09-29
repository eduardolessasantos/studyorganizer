using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOrganizer.Core.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public int ModuleId { get; set; }
        public Module? Module { get; set; }


        public ICollection<Subtopic> Subtopics { get; set; } = new List<Subtopic>();
    }
}
