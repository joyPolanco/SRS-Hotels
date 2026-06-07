using MediatR;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface ICommandHandler< in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
    }

}
