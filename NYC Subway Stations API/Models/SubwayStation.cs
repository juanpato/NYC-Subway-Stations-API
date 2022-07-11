using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NYC_Subway_Stations_API.Models
{
    public partial class SubwayStation
    {
        public SubwayStation()
        {
            UserSubwayStation = new HashSet<UserSubwayStation>();
        }

        public string Url { get; set; }
        public string Objectid { get; set; }
        public string Name { get; set; }
        public string TheGeomtype { get; set; }
        public decimal TheGeomcoordinates0 { get; set; }
        public decimal TheGeomcoordinates1 { get; set; }
        public string Line { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<UserSubwayStation> UserSubwayStation { get; set; }
    }
}
