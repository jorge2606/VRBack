using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class SolicitationSubsidyByUser
    {
        public SolicitationSubsidyByUser()
        {
            Dni = "";
            Email = "";
            FullName = "";
            Localities = "";
            Motive = "";
            PhoneNumber = "";
            State = "";
        }
        public string FullName { set; get; }
        public string Dni { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public DateTime CreateDate { set; get; }
        public string State { set; get; }
        public DateTime BeginDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public string Localities { set; get; }
        public Decimal TotalExpenditure { set; get; }
        public Decimal TotalDetination { set; get; }
    }

    public class SolicitationSubsidyByOrganism
    {
        public SolicitationSubsidyByOrganism()
        {
            Dni = "";
            Email = "";
            FullName = "";
            Localities = "";
            Motive = "";
            PhoneNumber = "";
            State = "";
        }
        public string FullName { set; get; }
        public string Dni { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public DateTime CreateDate { set; get; }
        public string State { set; get; }
        public DateTime BeginDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public string Localities { set; get; }
        public Decimal TotalExpenditure { set; get; }
        public Decimal TotalDestination { set; get; }
    }

    public class SolicitationSubsidySendEmail
    {
        public string FirstName { set; get; }
        public string FirstNameSupervisor { set; get; }
        public string LastNameSupervisor { set; get; }
        public string Dni { set; get; }
        public string LastName { set; get; }
        public string EmailAgent { set; get; }
        public string EmailSupervisor { set; get; }
        public string Localities { set; get; }
        public DateTime FirstDate { set; get; }
        public DateTime LastDate { set; get; }
        public int NotifySupervisor { set; get; }
        public int ElapsedDays { set; get; }
        public string UrlCallBack { set; get; }
        public string Messsage { set; get; }
    }
}
