using System;
using System.Collections.Generic;

namespace TamweelyMobileApp.Models
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public object ImagePath { get; set; }
        public string FileName { get; set; }
    }
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public object Name { get; set; }
        public object profile_pic { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }
        public int MonthlyInstallments { get; set; }
        public object MakeId { get; set; }
        public object ModelId { get; set; }
        public object BodyTypeId { get; set; }
        public object EngineTypeId { get; set; }
        public int Year { get; set; }
        public string EngineCapacity { get; set; }
        public object IsActive { get; set; }
        public object IsArchive { get; set; }
        public DateTime CreatedDate { get; set; }
        public object UpdatedDate { get; set; }
        public string MakeName { get; set; }
        public string BodyTypeName { get; set; }
        public string ModelName { get; set; }
        public string EngineTypeName { get; set; }
        public List<VehicleImage> VehicleImages { get; set; }
    }
}
