using MediatR;
using SolarMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarMonitor.Application.Queries
{
    public class GetDashboardSummaryQuery : IRequest <DashboardSummaryResponse>
    {
    }
}
