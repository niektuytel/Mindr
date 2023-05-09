using Microsoft.EntityFrameworkCore;

namespace Mindr.WebAssembly.Server.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
