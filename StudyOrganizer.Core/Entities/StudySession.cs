using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOrganizer.Core.Entities
{
    public class StudySession
    {
        public int Id { get; set; }
        public int SubtopicId { get; set; }
        public Subtopic? Subtopic { get; set; }


        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
    }
}
