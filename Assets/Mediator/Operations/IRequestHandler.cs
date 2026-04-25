using System.Threading.Tasks;

namespace TiredSiren.Mediator.Operations
{
    public interface IRequestHandler<in T, TResult> where T : IRequest<TResult>
    {
        public Task<TResult> Handle(T request);
    }
}