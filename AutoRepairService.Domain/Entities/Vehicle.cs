using System;
using System.Collections.Generic;

namespace AutoRepairService.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public string Engine { get; set; } = null!;
    public string Transmission { get; set; } = null!;
    public string PlateNumber { get; set; } = null!;

    public User User { get; set; } = null!;
    public ICollection<Service> Services { get; set; } = new List<Service>();
}