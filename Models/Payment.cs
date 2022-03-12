using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_UI.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ComponentProcessing")]
        public int RequestId { get; set; }
        public ComponentProcessing ComponentProcessing { get; set; }
        public string Name { get; set; }
        public double ProcessingCharge { get; set; } //I made it DOUBLE
        public double PackagingAndDeliveryCharge { get; set; } //I made it DOUBLE

        [StringLength(16, ErrorMessage = "Credit Card Number should be of 16 digits")]        
        public string CreditCardNumber { get; set; }
        public double TotalCharge { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
