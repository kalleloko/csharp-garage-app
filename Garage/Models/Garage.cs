using GarageApp.Interfaces;

namespace GarageApp.Models;

public class Garage<T> : IGarage<T> where T : IVehicle
{

}