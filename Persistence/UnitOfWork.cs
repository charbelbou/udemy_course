using System.Threading.Tasks;
using udemy.Persistence;

namespace udemy_course.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UdemyDbContext context;

        public UnitOfWork(UdemyDbContext context)
        {
            this.context = context;

        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}