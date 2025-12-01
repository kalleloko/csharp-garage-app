using GarageApp.Interfaces;
using System.Reflection;

namespace GarageApp;

internal class VehicleBuilder<T> : IVehicleBuilder<T> where T : IVehicle
{
    private readonly IUI _ui;
    public VehicleBuilder(IUI ui)
    {
        _ui = ui;
    }

    public T Build()
    {
        var vehicle = Activator.CreateInstance<T>();

        var type = typeof(T);

        PropertyInfo[] props = type.GetProperties();

        foreach (var prop in props)
        {
            TrySetPropertyWithRetry(vehicle, prop);
        }

        return vehicle;
    }

    private void TrySetPropertyWithRetry(object target, PropertyInfo prop)
    {
        string asterisk = prop.CustomAttributes.Any(a => a.AttributeType.Name == "RequiredMemberAttribute") ?
                 " (*)" : "";

        object? defaultValue = prop.GetValue(target);
        bool hasDefaultValue = !prop.PropertyType.IsEnum && !string.IsNullOrEmpty(defaultValue?.ToString() ?? "");
        string defaultText = hasDefaultValue ? $" (default: {defaultValue})" : "";

        // TODO: Jag får "Exception has been thrown by the target of an invocation." om 
        // jag i console-appen sätter ett property-värde som inte valideras korrekt - trots
        // följande try-catch-block: 
        while (true)
        {

            string value = AskForValue(prop.PropertyType, $"{prop.Name}{asterisk}{defaultText}:") ?? string.Empty;
            //prop.PropertyType

            if (string.IsNullOrWhiteSpace(value) && hasDefaultValue)
            {
                // acceptera default
                try
                {
                    prop.SetValue(target, defaultValue);
                    break;
                }
                catch (Exception e)
                {
                    _ui.PrintErrorLine(e.Message);
                }
            }

            try
            {
                object? typedValue = ConvertStringToType(value, prop.PropertyType);

                prop.SetValue(target, typedValue);
                break;
            }
            catch (TargetInvocationException tie) // fånga reflection wrapper
            {
                if (tie.InnerException != null)
                    _ui.PrintErrorLine(tie.InnerException.Message);
                else
                    _ui.PrintErrorLine(tie.Message);
            }
            catch (Exception e)
            {
                _ui.PrintErrorLine(e.Message);
            }
        }
    }

    private object ConvertStringToType(string input, Type type)
    {
        if (type.IsEnum)
        {
            return Enum.Parse(type, input);
        }

        if (type == typeof(int))
            return int.Parse(input);

        if (type == typeof(double))
            return double.Parse(input);

        if (type == typeof(bool))
            return bool.Parse(input);

        if (type == typeof(string))
            return input;

        // fallback för andra typer
        return Convert.ChangeType(input, type);
    }

    private string? AskForValue(Type type, string prompt)
    {
        if (type.IsEnum)
        {
            while (true)
            {

                var names = Enum.GetNames(type);

                for (int i = 0; i < names.Length; i++)
                    _ui.PrintLine($"{i + 1}. {names[i]}");

                int choice = _ui.AskForInput<int>(prompt);

                if (choice < 1 || choice > names.Length)
                {
                    _ui.PrintErrorLine("Ogiltigt val!");
                }
                else
                {
                    return names[choice - 1];
                }
            }
        }

        return _ui.AskForInput<string>(prompt);
    }
}