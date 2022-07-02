//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuperCarro
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicle
    {
        public string Id { get; set; }
        public Nullable<int> MarkId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Year { get; set; }
        public decimal Price { get; set; }
        public string FuelType { get; set; }
        public bool Ofert { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public string Puertas { get; set; }
        public string Picture { get; set; }
        public bool Enabled { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Mark Mark { get; set; }
        public virtual Model Model { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
