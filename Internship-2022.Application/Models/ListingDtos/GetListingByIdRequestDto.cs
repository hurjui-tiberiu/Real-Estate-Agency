using Internship_2022.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Models.ListingDtos
{
    public class GetListingByIdRequestDto
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public string[]? Images { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
    }
}
