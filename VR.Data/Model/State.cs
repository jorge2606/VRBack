using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class State
    {
        public static readonly Guid Accepted = Guid.Parse("3535DEC7-3CA2-4930-BE5B-32FDBBA7D02C");
        public static readonly Guid Rejected = Guid.Parse("723CCAD5-5F9A-496C-8F99-D27817F8E3F2");
        public static readonly Guid Pending = Guid.Parse("D4E28CE0-D7F6-46A2-849E-471F7A4EAACE");
        public static readonly Guid Sent = Guid.Parse("A152C5B9-D0B5-4CDA-92E8-F479568CFEF6");
        public static readonly Guid Accounted = Guid.Parse("D385AE53-F2DB-48A6-AF68-5E1FBE80F0E0");
        public static readonly Guid Finished = Guid.Parse("2C332F3C-CF16-4BEB-85C4-C4677700CF2C");
        public static readonly Guid AccountForAcepted = Guid.Parse("00CE5670-BC4B-4435-8C67-D754824D0FF2");
        public static readonly Guid AccountForRejected = Guid.Parse("74DFD597-8669-4AF8-8564-350B1E1996E4");
        public static readonly Guid Aprobado_1ra_Instancia = Guid.Parse("A24D92F5-BF62-4026-9564-DA9518DE343B");
        public static readonly Guid Aprobado_2da_Instancia = Guid.Parse("92A437C6-B915-4789-B64C-DC07EADB526E");
        public static readonly Guid Rendicion_Aprobada_1ra_Instancia = Guid.Parse("BADDF1A9-F191-4215-B724-4B0EAC57D28A");
        public static readonly Guid Rendicion_Aprobada_2da_Instancia = Guid.Parse("F545C0A1-3319-4E50-898D-1E3B5DAFEB11");
        public static readonly Guid Postponed = Guid.Parse("CD7FBE8C-E410-4A27-8362-381C43D42564");

        public Guid Id { set; get; }
        public string Description { set; get; }
        public string NormalizedName { set; get; }
    }
}
