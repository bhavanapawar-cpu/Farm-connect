using System;

namespace FarmConnect.Core.Entities
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public double SoilPH { get; set; }
        public double Moisture { get; set; }
        public double Temperature { get; set; }
        public double Nitrogen { get; set; }
        public double Phosphorus { get; set; }
        public double Potassium { get; set; }
        public string OverallHealth { get; set; } // Excellent, Good, Fair, Poor
        public int HealthScore { get; set; } // 0-100
        public string Recommendations { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Farm Farm { get; set; }
    }
}
