namespace FarmConnect.Core.DTOs
{
    public class ChatRequestDto
    {
        public string Message { get; set; }
        public string Context { get; set; } // crops, soil, pests, diseases, etc.
    }

    public class ChatResponseDto
    {
        public int Id { get; set; }
        public string UserMessage { get; set; }
        public string AIResponse { get; set; }
        public string Context { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class YieldPredictionRequestDto
    {
        public int CropId { get; set; }
        public double SoilPH { get; set; }
        public double Moisture { get; set; }
        public double Temperature { get; set; }
        public double Nitrogen { get; set; }
        public double Phosphorus { get; set; }
        public double Potassium { get; set; }
        public double FertilizerUsed { get; set; }
        public double PesticidesUsed { get; set; }
    }

    public class YieldPredictionResponseDto
    {
        public int Id { get; set; }
        public double PredictedYield { get; set; }
        public double Confidence { get; set; }
        public string RiskLevel { get; set; }
        public string Recommendations { get; set; }
    }

    public class FarmHealthRequestDto
    {
        public int FarmId { get; set; }
    }

    public class FarmHealthResponseDto
    {
        public int Id { get; set; }
        public int HealthScore { get; set; }
        public string OverallHealth { get; set; }
        public string Recommendations { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
