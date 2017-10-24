using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Mapping;

namespace PVModele.Tables
{

    [Table(Name = "TPointsCumulFormatOuvert")]
    public class PointsCumulFormatOuvert
    {
        /// <summary>
        /// texte 20
        /// </summary>
        [Column(Name = "NotPatCompe")]
        public int NotPatCompe { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "PointsDist1")]
        public int PointsDist1 { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "PointsDist2")]
        public int PointsDist2 { get; set; }

        /// <summary>
        /// Texte 50
        /// </summary>
        [Column(Name = "RangGlobal")]
        public int RangGlobal { get; set; }

    }


}
