using MediatR;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IQueryHandler <in TQuery,TResponse> : IRequestHandler<TQuery,TResponse> where TQuery : IQuery<TResponse> where TResponse : class
    {
    }
}
