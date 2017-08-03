using System.Data.Entity;

namespace Calc.Models
{
    public class CalcContext : DbContext
    {
        public DbSet<Calculating> Calcs { get; set; }
    }
}