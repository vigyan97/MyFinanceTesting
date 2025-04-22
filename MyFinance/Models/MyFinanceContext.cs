using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyFinance.Models;

public class MyFinanceContext : IdentityDbContext
{
    public MyFinanceContext(DbContextOptions<MyFinanceContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Recovery> Recoveries { get; set; }
    public DbSet<Nominee> Nominees { get; set; }
    public DbSet<BankDetails> BankDetails { get; set; }

}
