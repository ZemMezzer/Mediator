namespace TiredSiren.Mediator.Operations
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        public void Handle(T command);
    }
}