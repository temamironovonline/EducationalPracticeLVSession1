//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationalPracticeLVSession1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public int OrderNumber { get; set; }
        public int OrderProduct { get; set; }
        public int OrderCountProduct { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DateDelivery { get; set; }
        public int PointDelivery { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public int GetCode { get; set; }
        public int OrderStatus { get; set; }
    
        public virtual DeliveryPoints DeliveryPoints { get; set; }
        public virtual Users Users { get; set; }
        public virtual Products Products { get; set; }
        public virtual OrderStatuses OrderStatuses { get; set; }
    }
}
