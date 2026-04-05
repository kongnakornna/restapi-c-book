using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.Query
{
    [Table("QUERIES")]
    public class Query
    {
        [Key]
        [Column("QUERY_NAME")]
        public string QueryName { get; set; }

        [Column("QUERY")]
        public string? QueryCommand { get; set; }

        public Connections Connection { get; set; }
    }
}
