using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<ITimecard> devReports = new List<ITimecard>
            {
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 1, Name = "Dev1", Level = Developer.DeveloperLevel.Senior,
                        HourlyRate  = 30.5 }, WorkingHours = 160
                    },
                    SalaryStrategy = SeniorSalaryStrategy.Instance
                },
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 2, Name = "Dev2", Level = Developer.DeveloperLevel.Junior,
                        HourlyRate  = 20 }, WorkingHours = 150
                    },
                    SalaryStrategy = JuniorSalaryStrategy.Instance
                },
                new Timecard { MonthlyHours =
                    new DeveloperReport {
                        Developer = new Developer { Id = 3, Name = "Dev3", Level = Developer.DeveloperLevel.Senior,
                        HourlyRate  = 30.5 }, WorkingHours = 180
                    },
                    SalaryStrategy = SeniorSalaryStrategy.Instance
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
        public double HourlyRate { get; set; }
    }

    public class DeveloperReport
    {
        public Developer Developer { get; set; }
        public int WorkingHours { get; set; }
    }

    public interface ISalaryStrategy
    {
        double Calculate(DeveloperReport rpt);
    }

    public class SeniorSalaryStrategy : ISalaryStrategy
    {
        private static readonly SeniorSalaryStrategy _instance = new SeniorSalaryStrategy();
        public static SeniorSalaryStrategy Instance => _instance;

        public double Calculate(DeveloperReport rpt)
        {
            return rpt.WorkingHours * rpt.Developer.HourlyRate * 1.2;
        }
    }

    public class JuniorSalaryStrategy : ISalaryStrategy
    {
        private static readonly JuniorSalaryStrategy _instance = new JuniorSalaryStrategy();
        public static JuniorSalaryStrategy Instance => _instance;
        public double Calculate(DeveloperReport rpt)
        {
            return rpt.WorkingHours * rpt.Developer.HourlyRate;
        }
    }

    public interface ITimecard
    {
        ISalaryStrategy SalaryStrategy { get; set; }
        DeveloperReport MonthlyHours { get; set; }
        double CalculateSalary();
    }

    public class Timecard : ITimecard
    {
        public ISalaryStrategy SalaryStrategy { get; set; }
        public DeveloperReport MonthlyHours { get; set; }

        public double CalculateSalary()
        {
            return SalaryStrategy.Calculate(MonthlyHours);
        }
    }

    public class SalaryCalculator
    {
        private readonly IEnumerable<ITimecard> _developerTime;

        public SalaryCalculator(IEnumerable<ITimecard> developerTime)
        {
            _developerTime = developerTime;
        }

        public double CalculateTotalSalaries()
        {
            return _developerTime.Sum(time => time.CalculateSalary());
        }
    }

}