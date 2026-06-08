using Polly;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;

namespace SRS_Hotels.Data.src.BuildingBlocks.Helpers
{
    public class RetryPolicyService : IRetryPolicy
    {
        public IAsyncPolicy Policy { get; }

        public RetryPolicyService()
        {
            Policy = Polly.Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(200 * attempt)
                );
        }
    }
}
