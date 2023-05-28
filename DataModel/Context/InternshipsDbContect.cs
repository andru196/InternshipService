using DataModel.Context.Configurations;
using DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataModel.Context
{
	public class InternshipsDbContect : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Intern> Interns { get; set; }
		public DbSet<Mentor> Mentors { get; set; }
		public DbSet<Buddy> Buddies { get; set; }
		public DbSet<BuddyInternLink> BuddiesLink { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<InternRequest> InternRequests { get; set; }
		public DbSet<InternResponse> InternResponses { get; set; }
		public DbSet<Organization> Organizations { get; set; }
		public DbSet<University> Universities { get; set; }
		public DbSet<InternshipDirection> InternshipDirections { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<OrganizationAdmin> OrganizationAdmins { get; set; }
		public DbSet<FileRecord> Files { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CaseChempionship> CaseChempionships { get; set; }
		public DbSet<InternCaseChempionshipResult> InternCaseChempionshipResults { get; set; }
		public DbSet<InternsInternship> InternsInternships { get; set; }
		public DbSet<Test> Tests { get; set; }
		public DbSet<UserTraining> UserTrainings { get; set; }
		public DbSet<InternsCourse> InternsCourses { get; set; }
		public DbSet<InternTest> InternTest { get; set; }
		public DbSet<BuddyInternLink> BuddyInternLinks { get; set; }
		public DbSet<ArrivalsFromChannel> Arrivals { get; set; }



		private static bool isFirst = true;
		public InternshipsDbContect(DbContextOptions options) : base(options)
		{
			if (isFirst)
			{
				isFirst = false;
				//Database.EnsureDeleted();
				Database.EnsureCreated();
			}
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.LogTo(System.Console.WriteLine);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v.ToUniversalTime(),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
			var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
				v => v.HasValue ? v.Value.ToUniversalTime() : v,
				v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

			foreach (var entitYType in modelBuilder.Model.GetEntityTypes())
			{
				if (entitYType.IsKeyless)
					continue;

				foreach (var propoerty in entitYType.GetProperties())
				{
					if (propoerty.ClrType == typeof(DateTime))
						propoerty.SetValueConverter(dateTimeConverter);
					else if (propoerty.ClrType == typeof(DateTime?))
						propoerty.SetValueConverter(nullableDateTimeConverter);
				}
			}

			modelBuilder.ApplyConfiguration(new BuddyConfiguration());
			modelBuilder.ApplyConfiguration(new FileRecordsConfiguration());
			modelBuilder.ApplyConfiguration(new InternConfiguration());
			modelBuilder.ApplyConfiguration(new InternRequestConfiguration());
			modelBuilder.ApplyConfiguration(new InternResponseConfiguration());
			modelBuilder.ApplyConfiguration(new ReviewConfiguration());
			modelBuilder.ApplyConfiguration(new InternshipDirectionConfiguration());
			modelBuilder.ApplyConfiguration(new LinkConfiguration());
			modelBuilder.ApplyConfiguration(new MentorConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationAdminConfiguration());
			modelBuilder.ApplyConfiguration(new TagConfiguration());
			modelBuilder.ApplyConfiguration(new UniversityConfiguration());
			modelBuilder.ApplyConfiguration(new UserEventConfiguration());
			modelBuilder.ApplyConfiguration(new UsersConfiguration());
			modelBuilder.ApplyConfiguration(new UserTrainingConfiguration());
			modelBuilder.ApplyConfiguration(new BuddyConfiguration());

			modelBuilder.Entity<InternshipDirection>().HasData(
				new InternshipDirection() { Id = 1, Name = "IT Город",				Guid = new Guid("c999c168-7dfa-447b-975f-71546d26801c") },
				new InternshipDirection() { Id = 2, Name = "Медийный город",		Guid = new Guid("05adf0d4-2906-49d9-a464-3a96584e4d87") },
				new InternshipDirection() { Id = 3, Name = "Социальный город",		Guid = new Guid("8cc882ad-c7f6-4500-96e2-61a363dc69c6") },
				new InternshipDirection() { Id = 4, Name = "Комфортная среда",		Guid = new Guid("677838e1-441d-4cac-9943-856855f00d28") },
				new InternshipDirection() { Id = 5, Name = "Городская экономика",	Guid = new Guid("c999c168-7dfa-447b-975f-71546d36801c") },
				new InternshipDirection() { Id = 6, Name = "HR-город",				Guid = new Guid("17f4f589-14a0-4435-8916-a7b6bb36dac2") },
				new InternshipDirection() { Id = 7, Name = "Правовое пространство",	Guid = new Guid("ebf1725d-de02-493a-bab4-516d99a2f0cd") });

			modelBuilder.Entity<User>().HasData(
				new User() { Email="l@gi.n", Password = "123", FirstName = "Andru", Id = 1, Guid = new Guid("10055197-d6d1-4e6b-8243-671ef546d89d"), SecondName = "Trsw", Type = UserType.Admin, Phone = "1" },
				new User() { Email="st@den.t", Password = "123", FirstName = "stdnt", Id = 2, Guid = new Guid("e1faddde-3ce6-4d89-87f8-e521c5ed2d8f"), SecondName = "Trsw", Type = UserType.Student, Phone = "2" },
				new User() { Email="m@nto.r", Password = "123", FirstName = "mentor", Id = 3, Guid = new Guid("a45ec758-fc2c-4d92-a5c3-3726f124031e"), SecondName = "Trsw", Type = UserType.Mentor, Phone = "3" },
				new User() { Email="b@dd.y", Password = "123", FirstName = "buddy", Id = 4, Guid = new Guid("4e0547c4-9cb2-4571-884a-5621ebcfb716"), SecondName = "Trsw", Type = UserType.Buddy, Phone = "4" },
				new User() { Email="org@niz.n", Password = "123", FirstName = "orgAdmin", Id = 5, Guid = new Guid("11f3d72f-0f6e-434a-990d-a5af6e65b9dc"), SecondName = "Trsw", Type = UserType.OrganizationAdmin, Phone = "5" }
				);

			modelBuilder.EnableAutoHistory();
			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			this.EnsureAutoHistory();
			return base.SaveChanges();
		}

		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.EnsureAutoHistory();
			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
