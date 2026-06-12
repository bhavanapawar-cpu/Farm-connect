using FarmConnect.Core.DTOs;
using FarmConnect.Core.Entities;
using FarmConnect.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FarmConnect.Infrastructure.Services
{
    public class HealthService : IHealthService
    {
        private readonly FarmConnectDbContext _context;
        private readonly IConfiguration _configuration;

        public HealthService(FarmConnectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<FarmHealthResponseDto> AnalyzeHealthAsync(int farmId)
        {
            var farm = await _context.Farms.FindAsync(farmId);
            if (farm == null)
                throw new Exception("Farm not found");

            // Calculate health score based on soil metrics
            int healthScore = CalculateHealthScore(farm);
            string overallHealth = GetHealthStatus(healthScore);

            var healthRecord = new HealthRecord
            {
                FarmId = farmId,
                SoilPH = farm.SoilPH,
                Moisture = farm.Moisture,
                Temperature = farm.Temperature,
                HealthScore = healthScore,
                OverallHealth = overallHealth,
                Recommendations = GenerateRecommendations(farm, healthScore),
                RecordedAt = DateTime.UtcNow
            };

            _context.HealthRecords.Add(healthRecord);
            farm.HealthScore = healthScore;
            _context.Farms.Update(farm);
            await _context.SaveChangesAsync();

            return new FarmHealthResponseDto
            {
                Id = healthRecord.Id,
                HealthScore = healthScore,
                OverallHealth = overallHealth,
                Recommendations = healthRecord.Recommendations,
                RecordedAt = healthRecord.RecordedAt
            };
        }

        public async Task<dynamic> GetHealthHistoryAsync(int farmId)
        {
            var healthRecords = await Task.Run(() =>
                _context.HealthRecords
                    .Where(h => h.FarmId == farmId)
                    .OrderByDescending(h => h.RecordedAt)
                    .Take(30)
                    .ToList()
            );

            return healthRecords;
        }

        private int CalculateHealthScore(Farm farm)
        {
            int score = 50; // Base score

            // Soil pH scoring (optimal: 6.5-7.5)
            if (farm.SoilPH >= 6.5 && farm.SoilPH <= 7.5)
                score += 20;
            else if (farm.SoilPH >= 6.0 && farm.SoilPH <= 8.0)
                score += 10;

            // Moisture scoring (optimal: 40-60%)
            if (farm.Moisture >= 40 && farm.Moisture <= 60)
                score += 20;
            else if (farm.Moisture >= 30 && farm.Moisture <= 70)
                score += 10;

            // Temperature scoring (optimal: 15-30°C)
            if (farm.Temperature >= 15 && farm.Temperature <= 30)
                score += 10;

            return Math.Min(score, 100);
        }

        private string GetHealthStatus(int score)
        {
            return score switch
            {
                >= 80 => "Excellent",
                >= 60 => "Good",
                >= 40 => "Fair",
                _ => "Poor"
            };
        }

        private string GenerateRecommendations(Farm farm, int healthScore)
        {
            var recommendations = new List<string>();

            if (farm.SoilPH < 6.5)
                recommendations.Add("Soil is too acidic. Consider adding lime.");
            else if (farm.SoilPH > 7.5)
                recommendations.Add("Soil is too alkaline. Consider adding sulfur.");

            if (farm.Moisture < 40)
                recommendations.Add("Soil moisture is low. Increase irrigation.");
            else if (farm.Moisture > 60)
                recommendations.Add("Soil moisture is high. Improve drainage.");

            if (healthScore < 60)
                recommendations.Add("Consider soil testing and fertilizer application.");

            return string.Join("; ", recommendations);
        }
    }
}
