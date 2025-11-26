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
        while (true)
        {
            var value = _ui.AskForInput(prop.PropertyType, $"{prop.Name}{asterisk}:");
            try
            {
                prop.SetValue(target, value);
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
}