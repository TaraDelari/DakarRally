using DakarRally.Core.Models;
using System;

namespace DakarRally.Api.DataContracts.DTOs
{
    public class MalfunctionStatDTO
    {
        public DateTime Time { get; set; }
        public MalfunctionType MalfunctionType { get; set; }
    }
}