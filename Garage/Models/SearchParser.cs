
namespace GarageApp.Models
{
    internal class SearchParser
    {
        internal static readonly string? Instructions = "-r: [registreringsnummer (sträng)], -w: [antal hjul (heltal). Lägg till '+' efter talet för att ange min-antal, och tvärtom med '-'], -t: [tillverkare (sträng). ]";

        internal static void Parse(string search)
        {
            throw new NotImplementedException();
        }
    }
}