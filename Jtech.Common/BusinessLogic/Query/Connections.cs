using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.Query
{
    [Table("CONNECTIONS")]
    public class Connections
    {
        [Key]
        [Column("NAME")]
        public string ConnectionName { get; set; }

        [Column("CONNECTION_STRING")]
        public string ConnectionString { get; set; }


        [Column("PAGING_CMD_TEMPLATE")]
        public string? PagingCommandTemplate { get; set; }
    }
}
