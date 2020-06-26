using System;

namespace ColdCleartetr
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public DateTime DateA { get; set; }
        public bool hold { get; set; }
        public int TemperatureC { get; set; }
        public int Temperatureb { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
        public string SummaryC { get; set; }
        public int[] SummaryA => (new int[] { 1, 2, 3 });
    }
}
