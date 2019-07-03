using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model.ModelStoreProcedure
{
    public class Get_all_solicitationResult
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string CategoryDesc { set; get; }
        public string CategoryName { set; get; }
        public DateTime StartDateSolicitation { set; get; }
        public string Cuil { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public string TotalLetter { set; get; }
        public string Localities { set; get; }
    }
}
