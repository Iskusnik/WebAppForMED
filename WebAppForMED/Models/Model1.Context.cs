﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppForMED.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelMEDContainer : DbContext
    {
        public ModelMEDContainer()
            : base("name=ModelMEDContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MedCard> MedCardSet { get; set; }
        public virtual DbSet<Illness> IllnessSet { get; set; }
        public virtual DbSet<DocRecord> DocRecordSet { get; set; }
        public virtual DbSet<FreeTime> FreeTimeSet { get; set; }
        public virtual DbSet<WorkTime> WorkTimeSet { get; set; }
        public virtual DbSet<Doctor> DoctorSet { get; set; }
        public virtual DbSet<Patient> PatientSet { get; set; }
    }
}
