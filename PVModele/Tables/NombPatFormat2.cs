using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{

    [Table(Name = "TNombPatFormat2")]
    public class NombPatFormat2
    {
        /// <summary>
        /// Entier
        /// </summary>
        [Column(Name = "Nombre")]
        public int Nombre { get; set; }
    }
}
