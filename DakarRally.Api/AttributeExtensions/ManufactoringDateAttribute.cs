using System;
using System.ComponentModel.DataAnnotations;

namespace DakarRally.Api.AttributeExtensions
{
    public class ManufactoringDateAttribute: RangeAttribute
    {
        public ManufactoringDateAttribute() : base(typeof(DateTime), new DateTime(1886,1,29).ToString(), DateTime.UtcNow.ToString())
        { }
    }
}