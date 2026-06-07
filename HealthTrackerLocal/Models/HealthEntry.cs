namespace HealthTrackerLocal.Models;
using System.ComponentModel.DataAnnotations;

public class HealthEntry
{
	public DateTime Date { get; set; } = DateTime.Today;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int CycleDay { get; set; }
	public string? CyclePhase { get; set; }
    [Range(0, 4)]
    public int Bleeding { get; set; } // 0-4
    [Range(0, 10)]
    public int Cramps { get; set; }   // 0-10
    [Range(0, 10)]
    public int Pms { get; set; }      // 0-10
	public string? OvulationSigns { get; set; }

	public TimeSpan? SleepStart { get; set; }
	public TimeSpan? WakeTime { get; set; }
	public double SleepHours { get; set; }
	public int NightAwakenings { get; set; }
    [Range(1, 10)]
    public int SleepQuality { get; set; } // 1-10
    [Range(0, 100)]
    public int DeepSleepPercent { get; set; } // 0-100

    [Range(0, 10)]
    public int Mood { get; set; }         // 1-10
    [Range(0, 10)]
    public int Energy { get; set; }       // 1-10
    [Range(0, 10)]
    public int Anxiety { get; set; }      // 1-10
    [Range(0, 10)]
    public int Irritability { get; set; } // 1-10
    [Range(0, 10)]
    public int Focus { get; set; }       // 1-10
    [Range(0, 10)]
    public int Motivation { get; set; }  // 1-10

	public bool Headache { get; set; }
	public bool Migraine { get; set; }
	public bool Ibuprofen { get; set; }

    [Range(0, 10)]
    public int BodyPain { get; set; }    // 1-10
    [Range(0, 10)]
    public int Bloating { get; set; }     // 1-10
    [Range(0, 10)]
    public int Nausea { get; set; }      // 1-10
    [Range(0, 10)]
    public int Appetite { get; set; }    // 1-10
    [Range(0, 10)]
    public int Cravings { get; set; }    // 1-10

    [Range(0, 10)]
    public int CaffeineCups { get; set; }
	public TimeSpan? LastCaffeineTime { get; set; }
	public bool Alcohol { get; set; }
	public bool PhysicalActivity { get; set; }
    [Range(0, 10)]
    public int Stress { get; set; }      // 1-10
	public string? UnusualEvents { get; set; }

	public bool DailyMedsTaken { get; set; }
	public string? Supplements { get; set; }

	public string? Notes { get; set; }
}