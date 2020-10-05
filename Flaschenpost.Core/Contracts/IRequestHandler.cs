using System.Threading.Tasks;

namespace Flaschenpost.Core.Contracts
{
    public interface IRequestHandler<TRequest,TResult>
    {
        Task<TResult> HandleRequest(TRequest payload);
    }

}