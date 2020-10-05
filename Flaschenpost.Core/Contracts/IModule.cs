namespace Flaschenpost.Core.Contracts
{
    public interface IModule<TContainer>
    {
        TContainer RegisterServices(TContainer container);
    }

}