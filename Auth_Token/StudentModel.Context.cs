//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Auth_Token
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Student_DBEntities : DbContext
    {
        public Student_DBEntities()
            : base("name=Student_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Datafile> Datafiles { get; set; }
        public virtual DbSet<Student_Details_Sundram> Student_Details_Sundram { get; set; }
        public virtual DbSet<Datafile_Sundram> Datafile_Sundram { get; set; }
    }
}
