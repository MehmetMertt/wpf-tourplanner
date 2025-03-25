using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    }
}
