//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice2021
{
    using System;
    using System.Collections.Generic;
    
    public partial class SearchCampaign
    {
        public int SearchCampaignID { get; set; }
        public int GroupOfVolunteer { get; set; }
        public int SetOfEquipment { get; set; }
        public System.DateTime DateAndTime { get; set; }
        public string MeetingLocation { get; set; }
        public int MissingPersonID { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual MissingPerson MissingPerson { get; set; }
        public virtual Set Set { get; set; }
    }
}