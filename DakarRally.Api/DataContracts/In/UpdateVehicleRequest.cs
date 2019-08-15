using DakarRally.Api.AttributeExtensions;
using DakarRally.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DakarRally.Api.DataContracts.In
{
    public class UpdateVehicleRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string TeamName { get; set; }
        [Required]
        [StringLength(255)]
        public string Model { get; set; }
        [Required]
        [ManufactoringDate]
        public string ManufacturingDate { get; set; }
        [Required]
        [EnumDataType(typeof(VehicleType))]
        public int VehicleType { get; set; }
    }
}