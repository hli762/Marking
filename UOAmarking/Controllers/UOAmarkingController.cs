using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UOAmarking.Models;
using UOAmarking.Data;
using System.Net;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UOAmarking.Dtos;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace UOAmarking.Controllers
{
    [Route("webapi")]
    [ApiController]

    public class UOAmarkingController : Controller
    {
        private readonly IWebAPIRepo _repository;

        public UOAmarkingController(IWebAPIRepo repository)
        {
            _repository = repository;
        }



        //[HttpOptions]
        
        // 
        //  User APIs
        //
        //Finished APIs
        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthDto googleAuthDto)
        {
            if (googleAuthDto == null || string.IsNullOrEmpty(googleAuthDto.Code))
            {
                return BadRequest("Invalid token.");
            }
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    // Your app's client ID. Must be obtained from the Google Developer Console.
                    Audience = new[] { "280146137420-cd3akf52jet1oh6i4ptivjeffosbmdr4.apps.googleusercontent.com" }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleAuthDto.Code, settings);
                // Extract user information from the payload
                string emaill = payload.Email;
                string namee = payload.Name;

                if (emaill.EndsWith("@aucklanduni.ac.nz") || emaill == "uoamarker52@gmail.com")
                {
                    User user = _repository.GetUserByEmail(emaill);
                    if (user != null)
                    {
                        GoogleUser googleUser1 = new GoogleUser { Id = user.Id, Type="User", Email=user.Email, Name=user.Name, UPI=user.UPI, AUID=user.AUID, isOverseas=user.isOverseas, applications=user.applications, courses=user.courses, descriptionOfContracts=user.descriptionOfContracts, enrolmentDetail=user.enrolmentDetail, haveOtherContracts=user.haveOtherContracts, isCitizenOrPermanent=user.isCitizenOrPermanent, remainHours=user.remainHours, UnderOrPost=user.UnderOrPost };
                        return Ok(googleUser1);
                    }
                    User newUser = new User { Email = emaill, Name = namee };
                    User newUUser = _repository.AddUser(newUser);

                    GoogleUser googleUser = new GoogleUser { Id = newUUser.Id, Type = "User", Email = newUUser.Email, Name = newUUser.Name };
                    return Ok(googleUser);


                }else if (emaill == "markercoordinator@gmail.com")
                {
                    MarkerCoordinator markerc = _repository.GetMarkerCoordinatorByEmail(emaill);
                    if(markerc != null)
                    {
                        markerCdto markerCdto = new markerCdto { Email = markerc.email, Id = markerc.Id, Name = markerc.name, Type = "MarkerCoordinator" };
                        return Ok(markerCdto);
                    }

                    MarkerCoordinator markerCoordinator = new MarkerCoordinator { name = namee, email=emaill };
                    MarkerCoordinator m = _repository.NewMarkerCoordinator(markerCoordinator);
                    markerCdto markerCdto1 = new markerCdto { Id = m.Id, Email = m.email, Name = m.name, Type = "MarkerCoordinator" };
                    return Ok(markerCdto1);

                }else if (emaill.EndsWith("@auckland.ac.nz") || emaill == "coursesupervisor16@gmail.com")
                {
                    
                    CourseSupervisor courseSupervisor = _repository.GetCourseSupervisorByemail(emaill);
              
                    if (courseSupervisor != null)
                    {
                        GoogleCourseSupervisor googleCourseSupervisor1 = new GoogleCourseSupervisor { email = courseSupervisor.email, name = courseSupervisor.name, courses = courseSupervisor.courses, id = courseSupervisor.Id, isDirector = courseSupervisor.isDirector, Type = "CourseSupervisor" };
                        return Ok(googleCourseSupervisor1);
                    }
                    
                    CourseSupervisor courseSupervisor2 = new CourseSupervisor { name = namee, email = emaill};
                   
                    
                    CourseSupervisor courseSupervisor1 = _repository.NewCourseSupervisor(courseSupervisor2);
                    GoogleCourseSupervisor googleCourseSupervisor = new GoogleCourseSupervisor { email = emaill, name = emaill, courses=courseSupervisor1.courses, id=courseSupervisor1.Id, isDirector = courseSupervisor1.isDirector, Type="CourseSupervisor"};
                    return Ok(googleCourseSupervisor);

                }
                else
                {
                    return NotFound();
                }

                
            }
            catch (InvalidJwtException)
            {
                return BadRequest("Invalid Google authentication token.");
            }
            catch (Exception ex)
            {
                // Log exception details for debugging purposes.
                return BadRequest($"An error occurred while authenticating: {ex.Message}");
            }
        }
             
        [HttpGet("{userId}/academic-record")]
        public async Task<IActionResult> GetAcademicRecord(int userId)
        {
            User user = _repository.GetUserById(userId); // Replace with your data retrieval method
            if (user == null)
                return NotFound();

            var recordStream = new MemoryStream(user.AcademicRecord);
            return new FileStreamResult(recordStream, "application/octet-stream")
            {
                FileDownloadName = $"AcademicRecord_{user.Name}.pdf" // Again, adjust the file type as needed.
            };
        }

        [HttpGet("{userId}/cv")]
        public async Task<IActionResult> GetCv(int userId)
        {
            User user = _repository.GetUserById(userId); // Replace with your data retrieval method
            if (user == null)
                return NotFound();

            var cvStream = new MemoryStream(user.CV);
            return new FileStreamResult(cvStream, "application/octet-stream")
            {
                FileDownloadName = $"CV_{user.Name}.pdf" // Assuming it's a PDF. Adjust as needed.
            };
        }



        [HttpPost("AddUser")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddUser([FromForm] UserUploadDto userDto)
        {
            User user = new User
            {
                Name = userDto.name,
                UPI = userDto.upi,
                Email = userDto.email,
                AUID = userDto.auid,
                isOverseas = userDto.isOverseas,
                isCitizenOrPermanent = userDto.isCitizenOrPermanent,
                enrolmentDetail = userDto.enrolmentDetail,
                UnderOrPost = userDto.underOrPost,
                haveOtherContracts = userDto.haveOtherContracts,
                descriptionOfContracts = userDto.descriptionOfContracts,
                CV = await ToByteArrayAsync(userDto.cv),
                AcademicRecord = await ToByteArrayAsync(userDto.academicRecord),
            };

            User use = _repository.updateUser(user, userDto.userID);

            if(use == null)
            {
                return NotFound();
            }

            return Ok(use);
        }



        //converting IFormFile to Byte[]
        private async Task<byte[]> ToByteArrayAsync(IFormFile file)
        {
            if (file == null)
                return null;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }


        //Testing API
        [HttpPost("TestingPost")]
        public ActionResult PostingSemester(SemesterInput semesterInput)
        {

            Semester semester = new Semester();
            semester.SemesterType = semesterInput.SemesterType;
            semester.Year = semesterInput.Year;

            Semester res = _repository.NewSemester(semester);
            return Ok(res);

        }
        /// <summary>
        /// ////EVERYTHING ABOVE WORKS////////EVERYTHING ABOVE WORKS///////////////EVERYTHING ABOVE WORKS////////////////EVERYTHING ABOVE WORKS/////////////////EVERYTHING ABOVE WORKS////////////EVERYTHING ABOVE WORKS/////////////EVERYTHING ABOVE WORKS//////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        // GET /GetUserById
        [HttpGet("GetUserById/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            User u = _repository.GetUserById(id);

            if (u == null)
            {
                string message = string.Format("User {0} does not exist.", id);

                return BadRequest(message);
            }

            else
            {
                u.CV = null;
                u.AcademicRecord = null;
                return Ok(u);
            }
        }

        // GET /GetUserByUPI
        [HttpGet("GetUserByUPI/{upi}")]
        public ActionResult<User> GetUserByUPI(string upi)
        {
            User u = _repository.GetUserByUPI(upi);

            if (u == null)
            {
                string message = string.Format("User {0} does not exist.", upi);

                return BadRequest(message);
            }

            else
            {
                u.CV = null;
                u.AcademicRecord = null;
                return Ok(u);
            }
        }

        //Get /GetAllUsersByLastNameAscending
        [HttpGet("GetAllUsersByLastNameAscending")]
        public ActionResult GetAllUsersByLastNameAscending()
        {
            return Ok(_repository.GetAllUsersByLastNameAscending());
        }

        // DELETE /webapi/DeleteUserById/{id}
        [HttpDelete("DeleteUserById/{id}")]
        public ActionResult DeleteUserById(int id)
        {
            bool c = _repository.IsUserExistedById(id);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteUserById(id);
                return NoContent();
            }
        }

        // DELETE /webapi/DeleteUserByUPI/{upi}
        [HttpDelete("DeleteUserByUPI/{upi}")]
        public ActionResult DeleteUserByUPI(string upi)
        {
            bool c = _repository.IsUserExistedByUPI(upi);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteUserByUPI(upi);
                return NoContent();
            }
        }

        // 
        //  Application APIs
        //

        // POST /New Application


        // GET /GetApplicationById
        [HttpGet("GetApplicationById/{id}")]
        public ActionResult<Application> GetApplicationById(int id)
        {
            Application a = _repository.GetApplicationById(id);

            if (a == null)
            {
                string message = string.Format("Application {0} does not exist.", id);

                return BadRequest(message);
            }

            else
            {
                return Ok(a);
            }
        }

        //Get /GetAllApplications
        [HttpGet("GetAllApplications")]
        public ActionResult GetAllApplications()
        {
            return Ok(_repository.GetAllApplicationsByAscending());
        }

        // DELETE /webapi/DeleteApplicationById/{id}
        [HttpDelete("DeleteApplicationById/{id}")]
        public ActionResult DeleteApplicationById(int id)
        {
            bool c = _repository.IsApplicationExistedById(id);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteApplicationById(id);
                return NoContent();
            }
        }

        // 
        //  Course APIs
        //

        // POST /New Course
        [HttpPost("NewCourse")]
        public ActionResult NewCourse(CourseInput courseInput)
        {
            Course current = _repository.GetCourseByNumber(courseInput.CourseNumber);
            
            if (current != null)
            {
                int semesterID = current.Semester.Id;
                if(courseInput.SemesterID == semesterID)
                {
                    string respone1 = string.Format("Course {0} is existed already", courseInput.CourseName);
                    return Ok(respone1);
                }
                
            }

            

            string respone = "Course created successsfully";

            /* Users inputs Markers' id by CourseInput, then 
             * convert the List of Markers' id to List<User> 
             * as a parameter of the variable course.      */

            //IEnumerable<string> makersIds = courseInput.MarkersIds;
            //List<string> WrongIds = new List<string>();
            //List<User> users = new List<User>();
            /*User courseCoordinator = _repository.GetUserByUPI(courseInput.CourseCoordinatorUPI);
            foreach (string id in makersIds)
            {
                User currentUser = _repository.GetUserById(int.Parse(id));
                if (currentUser != null)
                {
                    users.Add(currentUser);
                }
                else
                {
                    WrongIds.Add(id);
                }
            }*/

            Semester semester = _repository.GetSemesterById(courseInput.SemesterID);
            if(semester == null)
            {
                string respone1 = string.Format("Semester {0} is doesn't exist", courseInput.SemesterID);
                return Ok(respone1);
            }

            CourseSupervisor courseCoordinator = _repository.GetCourseSupervisorByemail(courseInput.CourseCoordinatorEmail);
            CourseSupervisor courseDirect = _repository.GetCourseSupervisorByemail(courseInput.CourseDirectorEmail);
            if(courseCoordinator == null)
            {
                courseCoordinator = new CourseSupervisor { email = courseInput.CourseCoordinatorEmail, isDirector=false};

            }

            if (courseDirect == null)
            {
                courseDirect = new CourseSupervisor { email = courseInput.CourseDirectorEmail, isDirector = true };
            }
            Course course = new Course
            {
                CourseNumber = courseInput.CourseNumber,
                CourseName = courseInput.CourseName,
                //CourseCoordinator = courseCoordinator,
                //Marker = users,
                EstimatedStudents = courseInput.EstimatedStudents,
                EnrolledStudents = courseInput.EnrolledStudents,
                NeedsMarker = courseInput.NeedsMarker,
                TotalMarkingHour = courseInput.TotalMarkingHour,
                Semester = semester
                

                //CanPreAssignMarkers = bool.Parse(courseInput.CanPreAssignMarkers),
                //CourseCoordinatorID = courseInput.CourseCoordinatorID
            };
            
            //semester.Courses.Add(course);
            //string wrongIdsrespone = string.Format("User ids {0} don't exist", string.Join(", ", WrongIds));
            course.CourseSupervisors.Add(courseDirect);
            course.CourseSupervisors.Add(courseCoordinator);
            Course AddedCourse = _repository.NewCourse(course);
            return CreatedAtAction(nameof(GetCourseByCNumber), new { CourseNumber = course.CourseNumber }, course);
        }

        //Get /GetCourseByCNumber
        [HttpGet("GetCourseByCNumber/{CourseNumber}")]
        public ActionResult<Course> GetCourseByCNumber(int CourseNumber)
        {
            Course course = _repository.GetCourseByNumber(CourseNumber);

            if (course == null)
            {
                string message = string.Format("Course {0} does not exist.", course.CourseName);

                return BadRequest(message);
            }

            else
            {
                return Ok(course);
            }
        }

        [HttpGet("GetAllCourses")]
        public ActionResult<Course> GetAllCourses()
        {
            return Ok(_repository.GetAllCourses());
        }



        //Get /GetAllCoursesByDescending
        [HttpGet("GetAllCoursesByDescending")]
        public ActionResult GetAllCoursesByDescending()
        {
            return Ok(_repository.GetAllCoursesByDescending());
        }

        // DELETE /webapi/DeleteCourseByCourseNumber/{CourseNumber}
        [HttpDelete("DeleteCourseByCourseNumber/{CourseNumber}")]
        public ActionResult DeleteCourseByCourseNumber(int CourseNumber)
        {
            bool c = _repository.IsCourseExistedById(CourseNumber);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteCourseByCourseNumber(CourseNumber);
                return NoContent();
            }
        }

        // 
        //  Semester APIs
        //

        // POST /New Semester
        [HttpPost("NewSemester")]
        public ActionResult NewSemester(SemesterInput semesterInput)
        {
            bool ExistedCheck = _repository.IsSemesterExistedByYearAndSemesterType(semesterInput.Year, semesterInput.SemesterType);
            if (ExistedCheck)
            {
                string respone1 = string.Format("{0} {1} is existed already", semesterInput.Year, semesterInput.SemesterType);
                return Ok(respone1);
            }

            /*IEnumerable<string> coursesNumbers = semesterInput.CoursesNumbers;
            List<string> WrongIds = new List<string>();
            List<Course> courses = new List<Course>();

            foreach (string id in coursesNumbers)
            {
                Course currentCourse = _repository.GetCourseByNumber(int.Parse(id));
                if (currentCourse != null)
                {
                    courses.Add(currentCourse);
                }
                else
                {
                    WrongIds.Add(id);
                }
            }*/

            Semester semester = new Semester
            {
                Year = semesterInput.Year,
                SemesterType = semesterInput.SemesterType,
               // Courses = courses
            };

            Semester AddedSemester = _repository.NewSemester(semester);

            return CreatedAtAction(nameof(GetSemesterById), new { id = semester.Id }, semester);

        }

        //Get /GetSemesterById
        [HttpGet("GetSemesterById/{id}")]
        public ActionResult<SemesterOutput> GetSemesterById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Semester semester = _repository.GetSemesterById(id);
            List<CourseOutput> courseOutputs = new List<CourseOutput>();
            if (semester.Courses.Count > 0)
            {
                foreach(Course c in semester.Courses)
                {
                    CourseOutput courseOutput = new CourseOutput
                    { CourseId = c.Id, CourseName=c.CourseName, CourseNumber=c.CourseNumber, EnrolledStudents=c.EnrolledStudents, EstimatedStudents= c.EstimatedStudents, NeedsMarker = c.NeedsMarker, TotalMarkingHour = c.TotalMarkingHour
                    };
                    courseOutputs.Add(courseOutput);

                }
            }
            SemesterOutput semesterOutput = new SemesterOutput
            {
                Year = semester.Year,
                SemesterType = semester.SemesterType,
                Courses = courseOutputs
            };

            if (semester == null)
            {
                string message = string.Format("{0} does not exist.", id);

                return BadRequest(message);
            }else
            {
                return Ok(semester);
            }
        }

        //Get /GetAllSemesters
        [HttpGet("GetAllSemesters")]
        public ActionResult GetAllSemesters()
        {
            return Ok(_repository.GetAllSemestersByDescending());
        }

        // DELETE /webapi/DeleteSemesterByYearAndSemesterType/{year}{semesterType}
        [HttpDelete("DeleteSemesterByYearAndSemesterType/{year}/{semesterType}")]
        public ActionResult DeleteSemesterByYearAndSemesterType(int year, string semesterType)
        {
            bool c = _repository.IsSemesterExistedByYearAndSemesterType(year, semesterType);
            if (c == false)
                return NotFound();
            else
            {
                _repository.IsSemesterExistedByYearAndSemesterType(year, semesterType);
                return NoContent();
            }
        }

        // DELETE /webapi/DeleteSemesterById/{id}
        [HttpDelete("DeleteSemesterById/{id}")]
        public ActionResult DeleteSemesterById(int id)
        {
            bool c = _repository.IsSemesterExistedById(id);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteSemesterById(id);
                return NoContent();
            }
        }

        // 
        //  Assignment APIs
        //

        // POST /New Assignment
        [HttpPost("NewAssignment")]
        public ActionResult NewAssignment(AssignmentInput assignmentInput)
        {
            Assignment assignment = new Assignment
            {
                assignmentType = assignmentInput.assignmentType,
                description = assignmentInput.description,
                Course = assignmentInput.Course
            };
            Assignment AddedAssignment = _repository.NewAssignment(assignment);

            return CreatedAtAction(nameof(GetAssignmentById), new { id = AddedAssignment.Id }, AddedAssignment);
        }

        //Get /GetAssignmentById
        [HttpGet("GetAssignmentById/{Id}")]
        public ActionResult GetAssignmentById(int id)
        {
            Assignment assignment = _repository.GetAssignmentById(id);
            if (assignment == null)
            {
                string message = string.Format("{0} {1} does not exist.", assignment.Id, assignment.description);

                return BadRequest(message);
            }

            else
            {
                return Ok(assignment);
            }
        }

        //Get /GetAllAssignmentsByAscending
        [HttpGet("GetAllAssignmentsByAscending")]
        public ActionResult GetAllAssignmentsByAscending()
        {
            return Ok(_repository.GetAllAssignmentsByAscending());
        }

        // DELETE /webapi/DeleteAssignmentById/{id}
        [HttpDelete("DeleteAssignmentById/{id}")]
        public ActionResult DeleteAssignmentById(int id)
        {
            bool c = _repository.IsAssignmentExistedByid(id);
            if (c == false)
                return NotFound();
            else
            {
                _repository.DeleteAssignmentById(id);
                return NoContent();
            }
        }

        //post a course Supervisor
        [HttpPost("NewCourseSupervisor")]
        public ActionResult NewCourseSupervisor(CourseSupervisorInputDto CourseSupervisorInput)
        {
            bool ExistedCheck = _repository.IsCourseSupervisorExistByemail(CourseSupervisorInput.email);
            if (ExistedCheck)
            {
                string respone = string.Format("{0} is existed already", CourseSupervisorInput.name);
                return Ok(respone);
            }
            CourseSupervisor NewCourseSupervisor = new CourseSupervisor
            {
                email = CourseSupervisorInput.email,
                name = CourseSupervisorInput.name,
                isDirector = CourseSupervisorInput.isDirector
            };
            CourseSupervisor AddCourseSupervisor = _repository.NewCourseSupervisor(NewCourseSupervisor);
            return CreatedAtAction(nameof(GetCourseSupervisorByEmail), new { email = NewCourseSupervisor.email }, NewCourseSupervisor);
        }

        //Get /GetCourseSupervisorByEmail
        [HttpGet("GetCourseSupervisorByEmail/{email}")]
        public ActionResult<CourseSupervisor> GetCourseSupervisorByEmail(string email)
        {
            CourseSupervisor courseSupervisor = _repository.GetCourseSupervisorByemail(email);
            if (courseSupervisor == null)
            {
                string message = string.Format("{0} does not exist.", courseSupervisor.name);

                return BadRequest(message);
            }
            return Ok(courseSupervisor);
        }

    }
}