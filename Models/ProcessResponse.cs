using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_UI.Models
{
    public class ProcessResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ComponentProcessing")]
        public int RequestId { get; set; }
        public ComponentProcessing ComponentProcessing { get; set; }
        public double ProcessingCharge { get; set; }
        public double PackagingAndDeliveryCharge { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public double TotalCharge { get; set; }
    }
}
