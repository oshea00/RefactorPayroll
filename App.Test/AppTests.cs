using System.Collections.Generic;
using App;
using NUnit.Framework;

namespace Tests
{
    public class AppTests
    {
        List<DeveloperReport> devReports = new List<DeveloperReport>
            {
                new DeveloperReport {Id = 1, Name = "Dev1", Level = "Senior developer", HourlyRate  = 30.5, WorkingHours = 160 },
                new DeveloperReport {Id = 2, Name = "Dev2", Level = "Junior developer", HourlyRate  = 20, WorkingHours = 150 },
                new DeveloperReport {Id = 3, Name = "Dev3", Level = "Senior developer", HourlyRate  = 30.5, WorkingHours = 180 }
            };


        [Test]
        public void CanCalculateSeniorDevPay()
        {
            var payroll = new SalaryCalculator(devReports);
            Assert.AreEqual(15444d, payroll.CalculateTotalSalaries());
        }
    }
}