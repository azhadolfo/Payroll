using Payroll.API.Models;

namespace Payroll.API.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Comment> AddAsync(Comment comment, CancellationToken cancellationToken = default);

        Task<Comment?> UpdateAsync(Comment comment, CancellationToken cancellationToken = default);

        Task<Comment?> DeleteAsync(CancellationToken cancellationToken = default);
    }
}