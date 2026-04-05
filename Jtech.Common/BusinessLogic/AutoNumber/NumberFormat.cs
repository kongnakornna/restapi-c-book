using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.AutoNumber
{
    [Table("NUMBER_FORMAT")]
    [PrimaryKey(nameof(Prefix),nameof(Year),nameof(Month))]
    public class NumberFormat
    {
        [Column("PREFIX")]
        public string Prefix { get; set; }

        [Column("YEAR")]
        public int Year { get; set; }

        [Column("MONTH")]
        public int Month { get; set; }

        [Key]
        [Column("CURRENT")]
        public int Current { get; set; }
    }
}
