﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;

namespace TourPlanner.BL.ImportExport
{
    public interface ITourExportService
    {
        Task ExportTourAsync(TourModel tour);
    }
}
