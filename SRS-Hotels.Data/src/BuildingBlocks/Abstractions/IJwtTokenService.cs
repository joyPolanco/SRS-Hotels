using SRS_Hotels.Data.src.BuildingBlocks.Contracts.SRS_Hotels.Data.src.BuildingBlocks.Contracts;

namespace SRS_Hotels.Data.src.BuildingBlocks.Abstractions
{
    public interface IJwtTokenService
    {
        string GenerateToken(GenerateJwtRequest request);

    }
}
