namespace GarageApp.Models;

using GarageApp.Interfaces;
using System.Text.RegularExpressions;

public abstract class Vehicle : IVehicle
{
    private int _wheelCount = 4;
    private string _registrationNumber = string.Empty;
    private string _manufacturer = string.Empty;
    private string _model = string.Empty;
    private static List<string> _globalRegistrationNumbers = new List<string>();

    public required string Model
    {
        get => _model;
        init
        {
            if (value.Length < 1)
            {
                throw new ArgumentException("Modell måste matas in", "Model");
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
                throw new ArgumentException("Tillverkare måste matas in", "Manufacturer");
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
                throw new ArgumentOutOfRangeException("Fordonet kan inte ha negativt antal hjul", "WheelCount");
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
                throw new ArgumentException("Registreringsnummer kan endast bestå av alfa-numeriska värden", "RegistrationNumber");
            }
            string ucValue = value.ToUpper();
            if (_globalRegistrationNumbers.Contains(ucValue))
            {
                // Todo: låt inte fordonklassen själv hålla reda på detta, utan någon RegistrationHandler-klass
                throw new ArgumentException($"Registreringsnumret '{ucValue}' finns redan registrerat");
            }
            _globalRegistrationNumbers.Add(ucValue);
            _registrationNumber = ucValue;
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {Manufacturer} ({Model}), reg.nr: '{RegistrationNumber}'";
    }
}