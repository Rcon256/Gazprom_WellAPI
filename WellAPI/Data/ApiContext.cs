using Microsoft.EntityFrameworkCore;
using WellAPI.Models;

namespace WellAPI.Data
{
    public class ApiContext: DbContext
    {
        public DbSet<Well> Wells { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }
    }
}
