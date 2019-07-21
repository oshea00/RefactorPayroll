using System.Collections.Generic;
using App;
using NUnit.Framework;

namespace Tests
{
    public class AppTests
    {
        List<DeveloperReport> devReports = new List<DeveloperReport>
            {
                new DeveloperReport {
                    Developer = new Developer { Id = 1, Name = "Dev1", Level = Developer.DeveloperLevel.Senior },
                    HourlyRate  = 30.5, WorkingHours = 160 },
                new DeveloperReport {
                    Developer = new Developer { Id = 2, Name = "Dev2", Level = Developer.DeveloperLevel.Junior },
                    HourlyRate  = 20, WorkingHours = 150 },
                new DeveloperReport {
                    Developer = new Developer { Id = 3, Name = "Dev3", Level = Developer.DeveloperLevel.Senior },
                    HourlyRate  = 30.5, WorkingHours = 180 }
            };

        [Test]
        public void CanCalculateSeniorDevPay()
        {
            var payroll = new SalaryCalculator(devReports);
            Assert.AreEqual(15444d, payroll.CalculateTotalSalaries());
        }

    }
}