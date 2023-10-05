using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UOAmarking.Models;
using UOAmarking.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using Application = UOAmarking.Models.Application;

namespace UOAmarking.Data
{
    public class DBWebAPIRepo : IWebAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBWebAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteApplicationById(int id)
        {
            Application application = _dbContext.Applications.FirstOrDefault(e => e.Id == id);
            if (application != null)
            {
                _dbContext.Applications.Remove(application);
                _dbContext.SaveChanges();
            }
        }



        public void DeleteCourseByCourseNumber(int courseNumber)
        {
            Course course = _dbContext.Courses.FirstOrDefault(e => e.CourseNumber == courseNumber);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteSemesterById(int id)
        {
            Semester semester = _dbContext.Semesters.FirstOrDefault(e => e.Id == id);
            if (semester != null)
            {
                _dbContext.Semesters.Remove(semester);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteUserById(int id)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        
        public void DeleteUserByUPI(string upi)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.UPI == upi);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Application> GetAllApplicationsByAscending()
        {
            IEnumerable<Application> applications = _dbContext.Applications.Include(a => a.Course).Include(a => a.user).ToList<Application>().OrderBy(e => e.Id);
            return applications;
        }


        public IEnumerable<Course> GetAllCoursesByDescending()
        {
            IEnumerable<Course> courses = _dbContext.Courses.Include(c => c.Semester).ToList<Course>().OrderByDescending(e => e.Semester.Year).ThenByDescending(e => e.Semester.SemesterType)
                                                                            .ThenBy(e => e.CourseName.Substring(0, e.CourseName.Length - 3)).ThenBy(e => e.CourseName.Substring(e.CourseName.Length - 3, e.CourseName.Length));
            return courses;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            IEnumerable<Course> courses = _dbContext.Courses.Include(c => c.Semester).Include(c => c.assignments).Include(c => c.applications).Include(c => c.markers).Include(c => c.CourseSupervisors).Include(c => c.remainHours).ToList<Course>();
            return courses;
        }

        public IEnumerable<Semester> GetAllSemestersByDescending()
        {
            IEnumerable<Semester> semesters = _dbContext.Semesters.Include(c => c.Courses).ToList<Semester>().OrderByDescending(e => e.Year).OrderByDescending(e => e.SemesterType);
            return (semesters);
        }

        public IEnumerable<User> GetAllUsersByLastNameAscending()
        {
            IEnumerable<User> users = _dbContext.Users.Include(c => c.applications).Include(c => c.remainHours).Include(c => c.courses).ToList<User>().OrderBy(e => e.Name.Split(" ")[1]).ThenBy(e => e.Name.Split(" ")[0]);
            return users;
        }

        public Application GetApplicationById(int id)
        {
            Application application = _dbContext.Applications.Include(c => c.Course).Include(c => c.user).FirstOrDefault(e => e.Id == id);
            return application;
        }

        public User AddUser(User user)
        {
            EntityEntry<User> a = _dbContext.Users.Add(user);
            User c = a.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public Application NewApplication(Application application)
        {
            EntityEntry<Application> a = _dbContext.Applications.Add(application);
            Application c = a.Entity;
            _dbContext.SaveChanges();
            return c;
        }


        public User updateUser(User userIn, int userId)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if(user == null)
            {
                return null;
            }
            

            user.AcademicRecord = userIn.AcademicRecord;
            user.applications = userIn.applications;
            user.AUID = userIn.AUID;
            user.courses = userIn.courses;
            user.CV = userIn.CV;
            user.descriptionOfContracts = userIn.descriptionOfContracts;
            user.haveOtherContracts = userIn.haveOtherContracts;
            user.isOverseas = userIn.isOverseas;
            user.isCitizenOrPermanent = userIn.isCitizenOrPermanent;
            user.enrolmentDetail = userIn.enrolmentDetail;
            user.UnderOrPost = userIn.UnderOrPost;
           
            _dbContext.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            
            User user = _dbContext.Users.Include(u=>u.applications).Include(u=>u.courses).Include(u=>u.remainHours).FirstOrDefault(e => e.Email == email);
            return user;
        }
        

        public Course GetCourseByNumber(int courseNumber)
        {
            Course course = _dbContext.Courses.Include(c => c.Semester).FirstOrDefault(c => c.CourseNumber == courseNumber);
            return course;
        }

        public Semester GetSemesterById(int id)
        {
            Semester semester = _dbContext.Semesters.Include(s => s.Courses).FirstOrDefault(e => e.Id == id);
            return semester;
        }

        public User GetUserById(int id)
        {
            User user = _dbContext.Users.Include(u => u.courses).Include(u => u.applications).Include(u => u.remainHours).FirstOrDefault(e => e.Id == id);
            return user;
        }

        public User GetUserByUPI(string upi)
        {
            User user = _dbContext.Users.Include(c => c.applications).Include(c => c.remainHours).Include(c => c.courses).FirstOrDefault(e => e.UPI == upi);
            return user;
        }


        public IEnumerable<User> GetUsersByCourseID(int courseID)
        {
            return _dbContext.Users.Include(u => u.courses).Where(u => u.courses.Any(c => c.Id == courseID)).ToList();

        }
        public bool IsApplicationExistedById(int id)
        {
            Application application = _dbContext.Applications.FirstOrDefault(e => e.Id == id);
            if (application == null)
            {
                return false;
            }
            return true;
        }


        public bool IsCourseExistedById(int courseNumber)
        {
            Course course = _dbContext.Courses.FirstOrDefault(e => e.CourseNumber == courseNumber);
            if (course == null)
            {
                return false;
            }
            return true;
        }

        public bool IsSemesterExistedByYearAndSemesterType(int year, string SemesterType)
        {
            Semester semester = _dbContext.Semesters.FirstOrDefault(e => e.Year == year && e.SemesterType == SemesterType);
            if (semester == null)
            {
                return false;
            }
            return true;
        }

        public bool IsUserExistedById(int id)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public bool IsUserExistedByUPI(string upi)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.UPI == upi);
            if (user == null)
            {
                return false;
            }
            return true;
        }

       

        public Course NewCourse(Course course)
        {

            EntityEntry<Course> e = _dbContext.Courses.Add(course);
            Course c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public Semester NewSemester(Semester semester)
        {
            EntityEntry<Semester> s = _dbContext.Semesters.Add(semester);
            Semester c = s.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public User Register(User user)
        {
            EntityEntry<User> u = _dbContext.Users.Add(user);
            User c = u.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public bool IsSemesterExistedById(int id)
        {
            Semester semester = _dbContext.Semesters.FirstOrDefault(e => e.Id == id);
            if (semester == null)
            {
                return false;
            }
            return true;
        }

        public void DeleteSemesterByYearAndSemesterType(int year, string SemesterType)
        {
            Semester semester = _dbContext.Semesters.FirstOrDefault(e => e.Year == year && e.SemesterType == SemesterType);
            if (semester != null)
            {
                _dbContext.Semesters.Remove(semester);
                _dbContext.SaveChanges();
            }
        }

        public bool ValidLogin(string ValidInfor, string password)
        {
            User userEmail = _dbContext.Users.FirstOrDefault(e => e.Email == ValidInfor);

            User userId = _dbContext.Users.FirstOrDefault(e => e.Id == int.Parse(ValidInfor));

            User userUPI = _dbContext.Users.FirstOrDefault(e => e.UPI == ValidInfor);
            if (userEmail == null && userId == null && userUPI == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidAdmin(string ValidInfor, string password)
        {
            Admin adminEmail = _dbContext.Admins.FirstOrDefault(e => e.Username == ValidInfor && e.Password == password);

            Admin adminId = _dbContext.Admins.FirstOrDefault(e => e.Id == int.Parse(ValidInfor) && e.Password == password);

            Admin adminUPI = _dbContext.Admins.FirstOrDefault(e => e.Username == ValidInfor && e.Password == password);
            if (adminEmail == null && adminId == null && adminUPI == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Assignment NewAssignment(Assignment assignment)
        {
            EntityEntry<Assignment> a = _dbContext.Assignments.Add(assignment);
            Assignment c = a.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public Assignment GetAssignmentById(int id)
        {
            Assignment assignment = _dbContext.Assignments.Include(c => c.Course).FirstOrDefault(e => e.Id == id);
            return assignment;
        }

        public IEnumerable<Assignment> GetAllAssignmentsByAscending()
        {
            IEnumerable<Assignment> assignments = _dbContext.Assignments.Include(c => c.Course).ToList<Assignment>().OrderBy(e => e.Id);
            return assignments;
        }

        public void DeleteAssignmentById(int id)
        {
            Assignment assignment = _dbContext.Assignments.FirstOrDefault(e => e.Id == id);
            if (assignment != null)
            {
                _dbContext.Assignments.Remove(assignment);
                _dbContext.SaveChanges();
            }
        }

        public bool IsAssignmentExistedByid(int id)
        {
            Assignment assignment = _dbContext.Assignments.FirstOrDefault(e => e.Id == id);
            if (assignment == null)
            {
                return false;
            }
            return true;
        }

        public bool IsCourseSupervisorExistByemail(string email)
        {
            CourseSupervisor coursesuper = _dbContext.courseSupervisors.FirstOrDefault(e => e.email == email);
            if (coursesuper == null)
            {
                return false;
            }
            return true;
        }
        public CourseSupervisor NewCourseSupervisor(CourseSupervisor courseSupervisor)
        {
            EntityEntry<CourseSupervisor> coursesuper = _dbContext.courseSupervisors.Add(courseSupervisor);
            CourseSupervisor cs = coursesuper.Entity;
            _dbContext.SaveChanges();
            return cs;
        }

        public CourseSupervisor GetCourseSupervisorByemail(string email)
        {
            // CourseSupervisor Coursesupervisor = _dbContext.courseSupervisors.Include(c => c.courses).FirstOrDefault(e => e.email == email);
            CourseSupervisor Coursesupervisor = _dbContext.courseSupervisors.FirstOrDefault(e => e.email == email);
            return Coursesupervisor;
        }

        public MarkerCoordinator GetMarkerCoordinatorByEmail(string email)
        {
            MarkerCoordinator markerCoordinator = _dbContext.markerCoordinators.FirstOrDefault(e => e.email == email);
            return markerCoordinator;
        }

        public MarkerCoordinator NewMarkerCoordinator(MarkerCoordinator m)
        {
            EntityEntry<MarkerCoordinator> mc = _dbContext.markerCoordinators.Add(m);
            MarkerCoordinator mc1 = mc.Entity;
            _dbContext.SaveChanges();
            return mc1;
        }


    }
}

