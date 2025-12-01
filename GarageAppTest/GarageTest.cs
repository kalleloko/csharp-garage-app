using GarageApp.Interfaces;
using GarageApp.Models;

namespace GarageAppTest;

public class GarageTest : IDisposable
{
    private IEnumerable<IVehicle> _vehicles;

    private Garage<IVehicle> _garage;

    public GarageTest()
    {
        _vehicles = new List<IVehicle> {
            new Car()
            {
                Manufacturer = "BMW",
                Model = "Z1",
                RegistrationNumber = "ABC 123",
                WheelCount = 4
            },
            new Bike()
            {
                Manufacturer = "Crescent",
                Model = "A2",
                RegistrationNumber = "ZK88881929",
                WheelCount = 2
            },
        };
        _garage = new Garage<IVehicle>();
    }

    private void SetupGarage(int size)
    {
        _garage.MaxCapacity = size;
        foreach (IVehicle vehicle in _vehicles)
        {
            _garage.AddVehicle(vehicle);
        }
    }

    [Fact]
    public void Garage_CanBeCreated()
    {
        SetupGarage(10);
        Assert.NotNull(_garage);
    }

    [Fact]
    public void AddVehicle_IsAdding()
    {
        SetupGarage(_vehicles.Count() + 1);

        Car vehicle = new Car() { Manufacturer = "a", Model = "a", RegistrationNumber = "a", WheelCount = 1 };

        Assert.True(_garage.AddVehicle(vehicle));
        Assert.Equal(_vehicles.Count() + 1, _garage.Count());
    }
    [Fact]
    public void AddVehicle_IsNotAddingWhenFull()
    {
        SetupGarage(_vehicles.Count());

        Car vehicle = new Car() { Manufacturer = "a", Model = "a", RegistrationNumber = "a", WheelCount = 1 };

        Assert.False(_garage.AddVehicle(vehicle));
        Assert.Equal(_vehicles.Count(), _garage.Count());
    }

    [Fact]
    public void RemoveVehicle_IsRemoving()
    {
        SetupGarage(_vehicles.Count());
        _garage.RemoveVehicle(_vehicles.FirstOrDefault()!);
        Assert.Equal(_vehicles.Count() - 1, _garage.Count());
    }

    [Fact]
    public void GetVehicle_WhenExisting_ReturnsVehicle()
    {
        SetupGarage(_vehicles.Count());

        IVehicle vehicle = _vehicles.First();
        Assert.Equal(vehicle, _garage.GetVehicle(vehicle));
    }

    [Fact]
    public void GetVehicle_WhenNotExisting_ReturnsNull()
    {
        SetupGarage(_vehicles.Count());

        Car vehicle = new Car() { Manufacturer = "a", Model = "a", RegistrationNumber = "a", WheelCount = 1 };
        Assert.Null(_garage.GetVehicle(vehicle));
    }

    [Fact]
    public void Garage_CanBeFull()
    {
        SetupGarage(_vehicles.Count());
        Assert.True(_garage.IsFull());
    }

    [Fact]
    public void Garage_CanBeUsedWithLinq()
    {
        SetupGarage(_vehicles.Count());
        string lookForRegNr = _garage.First().RegistrationNumber;
        Assert.Single(_garage, v => v.RegistrationNumber == lookForRegNr);
    }

    public void Dispose()
    {
        // Sätt _globalRegistrationNumbers till en ny tom lista efter varje test
        // Vehicle:s statiska fält måste nollställas efter varje test. Tack
        // chatgpt för denna lösning :)
        var field = typeof(Vehicle).GetField(
            "_globalRegistrationNumbers",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static
        );

        field?.SetValue(null, new List<string>());
    }
}
