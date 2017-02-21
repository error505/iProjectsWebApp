using System.Linq;
using System.Threading.Tasks;
using iProjects.Models;

namespace iProjects.Statistics
{
    public class Statistics
    {
        private readonly ApplicationDbContext db;

    public Statistics(ApplicationDbContext context)
    {
      db = context;
    }
    public async Task<int> GetCountProjects()
    {
        return await Task.FromResult(db.Projects.Count());
    }

    }
}