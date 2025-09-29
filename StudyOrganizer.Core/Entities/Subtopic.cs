using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOrganizer.Core.Entities
{
    public enum StudyStatus { NotStarted = 0, InProgress = 1, Done = 2 }


    public class Subtopic
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;


        public int DisciplineId { get; set; }
        public Discipline? Discipline { get; set; }


        public StudyStatus Status { get; set; } = StudyStatus.NotStarted;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }
        public string? MaterialUrl { get; set; }
        public int? MasteryLevel { get; set; } // 1..5

        public string? Content { get; set; }


        public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
    }
}
