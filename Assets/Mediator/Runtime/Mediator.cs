using System.Threading.Tasks;
using TiredSiren.Mediator.Operations;

namespace TiredSiren.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IMediatorResolver _resolver;
        
        public Mediator(IMediatorResolver resolver)
        {
            _resolver = resolver;
        }
        
        public void Send<TCommand>(TCommand command) where TCommand : Operations.ICommand
        {
            var handler = _resolver.Resolve(typeof(ICommandHandler<TCommand>));
            ((ICommandHandler<TCommand>) handler).Handle(command);
        }

        public TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _resolver.Resolve(typeof(IQueryHandler<TQuery, TResult>));
            return ((IQueryHandler<TQuery, TResult>) handler).Handle(query);
        }

        public async Task<TResult> Request<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>
        {
            var handler = _resolver.Resolve(typeof(IRequestHandler<TRequest, TResult>));
            return await ((IRequestHandler<TRequest, TResult>) handler).Handle(request);
        }
    }
}