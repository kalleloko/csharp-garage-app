
using GarageApp.Interfaces;

namespace GarageApp.Models;

internal class SearchParser : ISearchParser
{
    public string Instructions
    {
        get => "-r: [registreringsnummer (sträng)], -w: [antal hjul (heltal). Lägg till '+' efter talet för att ange min-antal, och tvärtom med '-'], -t: [tillverkare (sträng). ]";
    }

    public IEnumerable<Func<IVehicle, bool>> Parse(string search)
    {
        throw new NotImplementedException();
    }

}