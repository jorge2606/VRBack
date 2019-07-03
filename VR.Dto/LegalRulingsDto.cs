using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class LegalRulingsBaseDto
    {
        public Guid Id { set; get; }
        public int Number { set; get; }
        public DateDto Date { set; get; }
        public string Description { set; get; }
        public bool IsDeleted { set; get; }
    }

    public class FilterLegalRulingsBaseDto
    {
        public int? Page { set; get; }
        public string Number { set; get; }
        public string Description { set; get; }
        public DateDto Date { set; get; }
        public int Day { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }
    }

    public class DeleteLegalDto
    {
        public Guid Id { set; get; }
    }
}
