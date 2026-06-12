using FarmConnect.Core.DTOs;
using System.Threading.Tasks;

namespace FarmConnect.Infrastructure.Services
{
    public interface IAIService
    {
        Task<ChatResponseDto> ChatAsync(int userId, ChatRequestDto request);
        Task<YieldPredictionResponseDto> PredictYieldAsync(int farmId, YieldPredictionRequestDto request);
        Task<FarmHealthResponseDto> AnalyzeFarmHealthAsync(int farmId);
    }

    public interface IWeatherService
    {
        Task<dynamic> GetCurrentWeatherAsync(string location);
        Task<dynamic> GetForecastAsync(string location, int days = 5);
        Task<dynamic> GetAlertsAsync(string location);
    }

    public interface IMarketplaceService
    {
        Task<dynamic> GetListingsAsync(int page = 1, int pageSize = 20);
        Task<dynamic> GetListingByIdAsync(int id);
        Task<dynamic> CreateListingAsync(int userId, dynamic listing);
        Task<dynamic> UpdateListingAsync(int id, dynamic listing);
        Task<bool> DeleteListingAsync(int id);
    }

    public interface ICommunityService
    {
        Task<dynamic> GetPostsByCategoryAsync(string category, int page = 1);
        Task<dynamic> GetPostByIdAsync(int id);
        Task<dynamic> CreatePostAsync(int userId, dynamic post);
        Task<dynamic> AddReplyAsync(int postId, int userId, string message);
        Task<bool> DeletePostAsync(int id);
    }

    public interface IHealthService
    {
        Task<FarmHealthResponseDto> AnalyzeHealthAsync(int farmId);
        Task<dynamic> GetHealthHistoryAsync(int farmId);
    }
}
