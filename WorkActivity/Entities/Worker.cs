using System;
using System.Collections.Generic;
using WorkActivity.Entities.Enums;



namespace WorkActivity.Entities
{
    class Worker
    { 
        public string Name { get; set; }
        public WorkerLevel Level { get; set; }

        public double BaseSalary { get; set; }

        public Department Department { get; set; }

        public List<HourContract> Contracts { get; set; } = new List<HourContract>();

        public Worker(string name) 
        { 
        }
        public Worker(string name, WorkerLevel level, double baseSalary, Department department)
        {
            Name = name;
            Level = level;
            BaseSalary = baseSalary;
            Department = department;
        }
        public void AddContracts(HourContract Contract) 
        {
            Contracts.Add(Contract);
        }
        public void RemoveContract(HourContract Contract)
        {
            Contracts.Remove(Contract);
        }

        public double Income(int year, int month)
        {
            double sum = BaseSalary;
            foreach(HourContract contract in Contracts)
            {
                if(contract.Date.Year == year && contract.Date.Month == month)
                {
                    sum += contract.TotalValue();
                }
            }
            return sum;
        }

        
    }
}
