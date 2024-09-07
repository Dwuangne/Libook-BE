using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class VoucherDTO : IValidatableObject
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title has to be a minimum of 3 characters")]
        public string Title { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Discount must be greater than 0")]
        public double Discount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Remain must be greater than 0")]
        public int Remain { get; set; }

        // Method này sẽ thực hiện kiểm tra khi class được validate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            // Kiểm tra StartDate phải sau ngày hiện tại
            if (StartDate <= DateTime.Now)
            {
                validationResults.Add(new ValidationResult("Start date must be after the current date.", new[] { nameof(StartDate) }));
            }

            // Kiểm tra EndDate phải sau StartDate
            if (EndDate <= StartDate)
            {
                validationResults.Add(new ValidationResult("End date must be after the start date.", new[] { nameof(EndDate) }));
            }

            return validationResults;
        }
    }

    public class VoucherResponseDTO
    {
        public Guid VoucherId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Discount { get; set; }

        public int Remain { get; set; }
    }
    public class VoucherRemainUpdateDTO
    {
        public int Remain { get; set; }
    }
}
