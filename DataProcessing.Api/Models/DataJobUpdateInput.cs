using System.ComponentModel.DataAnnotations;
using DataProcessing.Application.Models;

namespace DataProcessing.Api.Models
{
    public class DataJobUpdateInput
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(4096)]
        public string FilePathToProcess { get; set; } = string.Empty;

        public IEnumerable<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
