using System;
using System.Collections.Generic;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var devReports = new List<DeveloperReport>
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

    public class SalaryCalculator
    {
        private readonly IEnumerable<DeveloperReport> _developerReports;

        public SalaryCalculator(List<DeveloperReport> developerReports)
        {
            _developerReports = developerReports;
        }

        public double CalculateTotalSalaries()
        {
            double totalSalaries = 0D;

            foreach (var devReport in _developerReports)
            {
                if (devReport.Developer.Level == Developer.DeveloperLevel.Senior)
                {
                    totalSalaries += devReport.HourlyRate * devReport.WorkingHours * 1.2;
                }
                else
                {
                    totalSalaries += devReport.HourlyRate * devReport.WorkingHours;
                }
            }

            return totalSalaries;
        }
    }

}