using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UOAmarking.Models;

namespace UOAmarking.Data
{
    public interface IWebAPIRepo
    {
        //User
        User Register(User user);

        User GetUserById(int id);

        User GetUserByUPI(String upi);

       

        IEnumerable<User> GetAllUsersByLastNameAscending();

        IEnumerable<User> GetUsersByCourseID(int courseID);

        bool IsUserExistedById(int id);

        bool IsUserExistedByUPI(String upi);

        void DeleteUserById(int id);

        void DeleteUserByUPI(string upi);

        public bool ValidLogin(string userName, string password);

        public bool ValidAdmin(string userName, string password);

        //Application
        Application NewApplication(Application application);

        Application GetApplicationById(int id);

        IEnumerable<Application> GetAllApplicationsByAscending();
        public User GetUserByEmail(string email);
        public IEnumerable<Course> GetAllCourses();
        bool IsApplicationExistedById(int id);

        void DeleteApplicationById(int id);

        //Course
        Course NewCourse(Course course);

        Course GetCourseByNumber(int courseNumber);

        IEnumerable<Course> GetAllCoursesByDescending();

        bool IsCourseExistedById(int courseNumber);

        void DeleteCourseByCourseNumber(int courseNumber);


        //Semester
        Semester NewSemester(Semester semester);

        Semester GetSemesterById(int id);

        IEnumerable<Semester> GetAllSemestersByDescending();

        bool IsSemesterExistedByYearAndSemesterType(int year, string SemesterType);

        bool IsSemesterExistedById(int id);

        void DeleteSemesterByYearAndSemesterType(int year, string SemesterType);

        void DeleteSemesterById(int id);
        public User AddUser(User user);

        //Assignment
        Assignment NewAssignment(Assignment assignment);

        Assignment GetAssignmentById(int id);

        IEnumerable<Assignment> GetAllAssignmentsByAscending();
        public User updateUser(User user,int UserId);
        void DeleteAssignmentById(int id);

        bool IsAssignmentExistedByid(int id);

        bool IsCourseSupervisorExistByemail(String email);

        CourseSupervisor NewCourseSupervisor(CourseSupervisor CourseSupervisor);

        CourseSupervisor GetCourseSupervisorByemail(String email);
        public MarkerCoordinator GetMarkerCoordinatorByEmail(string email);
        public MarkerCoordinator NewMarkerCoordinator(MarkerCoordinator m);
    }
}
