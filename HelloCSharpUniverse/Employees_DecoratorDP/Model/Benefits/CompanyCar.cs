namespace Employees_DecoratorDP.Model.Benefits
{
    public class CompanyCar : IBenefit
    {
        public string Vehicle { get; }
        public string LicensePlate { get; }

        public CompanyCar(string vehicle, string licensePlate)
        {
            Vehicle = vehicle;
            LicensePlate = licensePlate;
        }

        public string GetPrintableDetails()
        {
            return $"\tAssigned Company Car: {Vehicle}\n" +
                $"\tLicense Plate: {LicensePlate}";
        }
    }
}
