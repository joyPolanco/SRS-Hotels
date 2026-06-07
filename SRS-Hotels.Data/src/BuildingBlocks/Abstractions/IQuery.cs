using MediatR;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
