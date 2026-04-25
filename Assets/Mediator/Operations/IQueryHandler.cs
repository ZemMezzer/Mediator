namespace TiredSiren.Mediator.Operations
{
    public interface IQueryHandler<in T, out TResult> where T : IQuery<TResult>
    {
        public TResult Handle(T command);
    }
}