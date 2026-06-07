using MediatR;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface ICommand<out TResponse>: IRequest<TResponse>
    {
    }
    public interface ICommand : IRequest<Unit>
    {
    }


}
