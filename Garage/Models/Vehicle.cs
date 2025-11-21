namespace GarageApp.Models;

using GarageApp.Interfaces;
using System.Text.RegularExpressions;

internal abstract class Vehicle : IVehicle
{
    private int _wheelCount = 4;
    private string _registrationNumber = string.Empty;
    private string _manufacturer = string.Empty;
    private string _model = string.Empty;

    public required string Model
    {
        get => _model;
        init
        {
            if (value.Length < 1)
            {
                throw new ArgumentException("Model cannot be empty");
            }
            _model = value;
        }
    }

    public required string Manufacturer
    {
        get => _manufacturer;
        init
        {
            if (value.Length < 1)
            {
                throw new ArgumentException("Manufacturer cannot be empty");
            }
            _manufacturer = value;
        }
    }

    public required int WheelCount
    {
        get => _wheelCount;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _wheelCount = value;
        }
    }

    public required string RegistrationNumber
    {
        get => _registrationNumber;
        init
        {
            if (value == string.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            if (!Regex.IsMatch(value, "^[a-zA-Z0-9]+$"))
            {
                throw new ArgumentException("Registration number can only contain english alphanumerical values");
            }
            _registrationNumber = value;
        }
    }

    //public Vehicle(string manufacturer, string registrationNumber)
    //{
    //    Manufacturer = manufacturer ?? string.Empty;
    //    Model = model ?? string.Empty;
    //    RegistrationNumber = registrationNumber ?? string.Empty;
    //}
}