using System.Threading.Tasks;

namespace udemy_course.Persistence
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}