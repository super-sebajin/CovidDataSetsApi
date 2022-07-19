using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidDataSetsApi.DataAccessLayer
{
    [Table("CovidDataSets")]
    public class CovidDataSets
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Column("DataSetProviderShortName")]
        [Required]
        public string? DataSetProviderShortName { get; set; }

        [Column("DataSetProviderLongName")]
        [Required]
        public string? DataSetProviderLongName { get; set; }
        
        [Column("DataSetName")]
        [Required]
        [StringLength(150)]
        public string DataSetName { get; set; }

        [Column("DataSetPublicUrl")]
        public string DataSetPublicUrl { get; set; }

        [Column("DataSetPublicUrlHttpMethod")]
        public string DataSetPublicUrlHttpMethod { get; set; }

        [InverseProperty(nameof(CovidCasesOverTimeUsa.CovidDataSet))]
        public List<CovidCasesOverTimeUsa> CovidCasesOverTimesUsa { get; set; }


    }
}
