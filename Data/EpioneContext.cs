namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entities;

    public partial class EpioneContext : DbContext
    {

        public EpioneContext()
            : base("name=EpioneContext")
        {
        }

        public virtual DbSet<Appointment> appointments { get; set; }
        public virtual DbSet<Availability> availabilities { get; set; }
        public virtual DbSet<Consultation> consultations { get; set; }
        public virtual DbSet<Doctor> doctors { get; set; }
        public virtual DbSet<Message> messages { get; set; }
        public virtual DbSet<Notificationapp> notificationapps { get; set; }
        public virtual DbSet<Path> paths { get; set; }
        public virtual DbSet<Patient> patients { get; set; }
        public virtual DbSet<Profile> profiles { get; set; }
        public virtual DbSet<Rating> ratings { get; set; }
        public virtual DbSet<Reason> reasons { get; set; }
        public virtual DbSet<Report> reports { get; set; }
        public virtual DbSet<Speciality> specialities { get; set; }
        public virtual DbSet<Treatment> treatments { get; set; }
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Skill> skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.consultations)
                .WithOptional(e => e.appointment)
                .HasForeignKey(e => e.appointment_id);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.treatments)
                .WithOptional(e => e.appointment)
                .HasForeignKey(e => e.appointment_id);

            modelBuilder.Entity<Availability>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Consultation>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Consultation>()
                .HasMany(e => e.reports)
                .WithOptional(e => e.consultation)
                .HasForeignKey(e => e.consultation_id);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.presentation)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.adresseSocialSiege)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.formeJuridique)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.memberAGA)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.nbreInscriptionOrdre)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.nbreRCS)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.nbreRPPS)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.socialReason)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.statuts)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.appointments)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.availabilities)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<Doctor>()
                .HasOptional(e => e.skill)
                .WithRequired(e => e.doctor);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.paths)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.messages)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.reasons)
                .WithMany(e => e.doctors)
                .Map(m => m.ToTable("doctor_reason", "epione").MapLeftKey("doctors_id").MapRightKey("reasons_id"));

            modelBuilder.Entity<Message>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Notificationapp>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Path>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Path>()
                .HasMany(e => e.treatments)
                .WithOptional(e => e.path)
                .HasForeignKey(e => e.path_id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.appointments)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.messages)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.notificationapps)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patientnotif_id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.paths)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.reports)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<Profile>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .HasMany(e => e.users)
                .WithOptional(e => e.profile)
                .HasForeignKey(e => e.profile_id);

            modelBuilder.Entity<Rating>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Reason>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Reason>()
                .HasMany(e => e.appointments)
                .WithOptional(e => e.reason)
                .HasForeignKey(e => e.reason_id);

            modelBuilder.Entity<Report>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .Property(e => e.pathFile)
                .IsUnicode(false);

            modelBuilder.Entity<Speciality>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Speciality>()
                .HasMany(e => e.doctors)
                .WithOptional(e => e.speciality)
                .HasForeignKey(e => e.speciality_id);

            modelBuilder.Entity<Speciality>()
                .HasMany(e => e.reasons)
                .WithOptional(e => e.speciality)
                .HasForeignKey(e => e.speciality_id);

            modelBuilder.Entity<Treatment>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Treatment>()
                .Property(e => e.recomended_doc)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.doctor)
                .WithRequired(e => e.user);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.patient)
                .WithRequired(e => e.user);

            modelBuilder.Entity<Skill>()
                .Property(e => e.skills)
                .IsUnicode(false);
        }
    }
}
