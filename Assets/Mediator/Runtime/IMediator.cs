using System.Threading.Tasks;
using TiredSiren.Mediator.Operations;

namespace TiredSiren.Mediator
{
    public interface IMediator
    {
        public void Send<TCommand>(TCommand command) where TCommand : ICommand;
        public TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        public Task<TResult> Request<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;
    }
}