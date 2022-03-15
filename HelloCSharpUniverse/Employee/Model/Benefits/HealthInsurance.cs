namespace Employees_DecoratorDP.Model.Benefits
{
    internal class HealthInsurance : IBenefit
    {
        public string InsuranceID { get; }

        public HealthInsurance(string insuranceID)
            => InsuranceID = insuranceID;

        public string GetPrintableDetails()
            => $"\tHealth Insurance personal ID: {InsuranceID}";
    }
}