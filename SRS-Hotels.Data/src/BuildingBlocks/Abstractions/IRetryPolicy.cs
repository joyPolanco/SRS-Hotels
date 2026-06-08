using Polly;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public class IRetryPolicy
    {
       public IAsyncPolicy Policy { get;  }
    }
}
