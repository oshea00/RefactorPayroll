using System;
using System.Collections.Generic;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Timecard> devReports = new List<Timecard>
            {
                new Timecard { WeeklyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 1, Name = "Dev1", Level = Developer.DeveloperLevel.Senior },
                        HourlyRate  = 30.5, WorkingHours = 160
                    },
                    SalaryStrategy = new SeniorSalaryStrategy()
                },
                new Timecard { WeeklyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 2, Name = "Dev2", Level = Developer.DeveloperLevel.Junior },
                        HourlyRate  = 20, WorkingHours = 150
                    },
                    SalaryStrategy = new JuniorSalaryStrategy()
                },
                new Timecard { WeeklyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 3, Name = "Dev3", Level = Developer.DeveloperLevel.Senior },
                        HourlyRate  = 30.5, WorkingHours = 180
                    },
                    SalaryStrategy = new SeniorSalaryStrategy()
                }
            };

            var d = new Developer
            {
                Id = 1
            };
            var calculator = new SalaryCalculator(devReports);
            Console.WriteLine($"Sum of all the developer salaries is {calculator.CalculateTotalSalaries()} dollars");
        }
    }

    public class Developer
    {
        public enum DeveloperLevel {Junior, Senior};
        public int Id { get; set; }
        public string Name { get; set; }
        public DeveloperLevel Level { get; set; }
    }

    public class DeveloperReport
    {
        public Developer Developer { get; set; }
        public int WorkingHours { get; set; }
        public double HourlyRate { get; set; }
    }

    public interface ISalaryStrategy
    {
        double Calculate(DeveloperReport rpt);
    }

    public class SeniorSalaryStrategy : ISalaryStrategy
    {
        public double Calculate(DeveloperReport rpt)
        {
            return rpt.WorkingHours * rpt.HourlyRate * 1.2;
        }
    }

    public class JuniorSalaryStrategy : ISalaryStrategy
    {
        public double Calculate(DeveloperReport rpt)
        {
            return rpt.WorkingHours * rpt.HourlyRate;
        }
    }

    public interface ITimecard
    {
        double CalculateSalary();
    }

    public class Timecard : ITimecard
    {
        public ISalaryStrategy SalaryStrategy { get; set; }
        public DeveloperReport WeeklyHours { get; set; }

        public double CalculateSalary()
        {
            return SalaryStrategy.Calculate(WeeklyHours);
        }
    }

    public class SalaryCalculator
    {
        private readonly IEnumerable<Timecard> _developerTime;

        public SalaryCalculator(List<Timecard> developerTime)
        {
            _developerTime = developerTime;
        }

        public double CalculateTotalSalaries()
        {
            double totalSalaries = 0D;

            foreach (var time in _developerTime)
            {
                totalSalaries += time.CalculateSalary(); 
            }

            return totalSalaries;
        }
    }

}