//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Korotkov_UP01
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int request_id { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<System.DateTime> date_ended { get; set; }
        public Nullable<int> equipment_id { get; set; }
        public Nullable<int> fault_type_id { get; set; }
        public string problem_description { get; set; }
        public Nullable<int> client_id { get; set; }
        public Nullable<int> technician_id { get; set; }
        public Nullable<int> status_id { get; set; }
        public Nullable<int> priority_id { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual FaultType FaultType { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }
        public virtual Technician Technician { get; set; }
    }
}
