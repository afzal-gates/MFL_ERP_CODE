using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution
{
    public class SchedulerRegistry : Registry
    {
        
        public SchedulerRegistry()
            {
                //https://github.com/fluentscheduler/FluentScheduler for documentation
                //Schedule<TnATaskSummaryMailJob>().ToRunEvery(1).Days().At(0, 1);
                Schedule<TnATaskSummaryMailJob>().ToRunOnceIn(20).Seconds();

                //Schedule<TnATaskSummaryMailJob>().ToRunNow().AndEvery(1).Minutes();
            }
    }
}