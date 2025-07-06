using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;

namespace TourPlanner.DAL.Dto
{
    public class TourLogsDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float Distance { get; set; }
        public string Comment { get; set; }
        public string Difficulty { get; set; }
        public int Rating { get; set; }

        //fk key
        [ForeignKey("TourId")]
        public Guid TourId { get; set; }

        // Navigation property
        public TourDto Tour { get; set; }

    }
}
