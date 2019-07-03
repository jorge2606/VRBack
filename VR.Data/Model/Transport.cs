using System;
using System.Collections.Generic;
using System.Text;
using VR.Data.Interfaces;

namespace VR.Data.Model
{
    public class Transport : IsDeletedInterface
    {
        public Guid Id { set; get; }
        public string Type { set; get; }
        public string Brand { set; get; }
        public string Model { set; get; }
        public string CarPlate { set; get; }
        public Boolean IsDeleted { set; get; }
    }
}
