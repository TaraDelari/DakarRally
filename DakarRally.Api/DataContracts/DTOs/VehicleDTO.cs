namespace DakarRally.Api.DataContracts.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string ManufactoringDate { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
        public string Distance { get; set; }
    }
}