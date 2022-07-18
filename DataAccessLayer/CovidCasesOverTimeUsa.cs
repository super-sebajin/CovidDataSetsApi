using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidDataSetsApi.DataAccessLayer
{
    [Table("CovidCasesOverTimeUsa")]
    public class CovidCasesOverTimeUsa
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Column("DateStamp")]
        [Required]
        public DateTime DateStamp { get; set; }

        [Column("CountConfirmed")]
        [Required]
        public int CountConfirmed { get; set; }

        [Column("CountDeath")]
        [Required]
        public int CountDeath { get; set; }

        [Column("CountRecovered")]
        [Required]
        public int CountRecovered { get; set; }

        [InverseProperty(nameof(CovidDataSets.CovidCasesOverTimesUsa))]
        public CovidDataSets  CovidDataSet { get; set; }


    }
}
