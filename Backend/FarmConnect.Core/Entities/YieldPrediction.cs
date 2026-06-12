using System;

namespace FarmConnect.Core.Entities
{
    public class YieldPrediction
    {
        public int Id { get; set; }
        public int CropId { get; set; }
        public double PredictedYield { get; set; }
        public double Confidence { get; set; } // 0-100
        public string RiskLevel { get; set; } // Low, Medium, High
        public string Recommendations { get; set; }
        public DateTime PredictionDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Crop Crop { get; set; }
    }
}
