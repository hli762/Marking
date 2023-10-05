using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UOAmarking.Models;

namespace UOAmarking.Data
{
	public class WebAPIDBContext : DbContext
	{

		public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options){ }

		public DbSet<Course> Courses { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Application> Applications { get; set; }

		public DbSet<MarkerCoordinator> markerCoordinators { get; set; }

		public DbSet<Semester> Semesters { get; set; }

		public DbSet<Admin> Admins { get; set; }

		public DbSet<Assignment> Assignments { get; set; }

        public DbSet<MarkingHours> MarkingHours { get; set; }

        public DbSet<CourseSupervisor> courseSupervisors { get; set; }
	}

}