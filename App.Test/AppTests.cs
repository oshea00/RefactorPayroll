using System.Collections.Generic;
using System.Linq;
using App;
using NUnit.Framework;

namespace Tests
{
    public class AppTests
    {
        List<ITimecard> devReports = new List<ITimecard>
            {
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 1, Name = "Dev1", Level = Developer.DeveloperLevel.Senior,
                        HourlyRate  = 30.5 },
                        WorkingHours = 160
                    },
                    SalaryStrategy = new SeniorSalaryStrategy()
                },
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 2, Name = "Dev2", Level = Developer.DeveloperLevel.Junior,
                        HourlyRate  = 20 },
                        WorkingHours = 150
                    },
                    SalaryStrategy = new JuniorSalaryStrategy()
                },
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 3, Name = "Dev3", Level = Developer.DeveloperLevel.Senior,
                        HourlyRate  = 30.5 },
                        WorkingHours = 180
                    },
                    SalaryStrategy = new SeniorSalaryStrategy()
                }
            };

        [Test]
        public void CanCalculatePayroll()
        {
            var payroll = new SalaryCalculator(devReports);
            Assert.AreEqual(15444d, payroll.CalculateTotalSalaries());
        }

        [Test]
        public void CanCalculateSeniorDevPay()
        {
            var calc = new SeniorSalaryStrategy();
            var senior = devReports
                .Where(r => r.MonthlyHours.Developer.Level == Developer.DeveloperLevel.Senior)
                .Select(t => t.MonthlyHours)
                .First();
            Assert.AreEqual(5856d, calc.Calculate(senior));
        }

        [Test]
        public void CanCalclateJuniorDevPay()
        {
            var calc = new JuniorSalaryStrategy();
            var junior = devReports
                .Where(r => r.MonthlyHours.Developer.Level == Developer.DeveloperLevel.Junior)
                .Select(t => t.MonthlyHours)
                .First();
            Assert.AreEqual(3000d, calc.Calculate(junior));
        }

    }
}