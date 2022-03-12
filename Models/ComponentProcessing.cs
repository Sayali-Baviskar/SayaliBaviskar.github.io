using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_UI.Models
{
    public class ComponentProcessing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]

        [RegularExpression(@"(\S\D)+", ErrorMessage = " Space and numbers not allowed")]
        public string Name { get; set; }

        //[StringLength(10, ErrorMessage = "Contact number should be of 10 digits")]
        //[Required]
        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNumber { get; set; }

        [StringLength(16, ErrorMessage = "Credit Card Number should be of 16 digits")]
        [RegularExpression(@"^\(?([0-9]{5})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{6})$", ErrorMessage = "Credit Card Number should be of 16 digits")]
        [Required(ErrorMessage = "You must provide a Card number")]
        public string CreditCardNumber { get; set; }
        [Required]
        public string ComponentType { get; set; }
        [Required]
        [RegularExpression(@"^(([A-za-z]+[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "ComponentName is not valid")]
        public string ComponentName { get; set; }
        [Required]
        [Display]
        //[StringLength(100, MinimumLength = 1, ErrorMessage ="Number between 1-100")]
        public int Quantity { get; set; }

        public bool IsPriorityRequest { get; set; }
        public DateTime OrderPlacedDate { get; set; }
    }
}
