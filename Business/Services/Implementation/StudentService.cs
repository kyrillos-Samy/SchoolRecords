using AutoMapper;
using Business.Helper.Extensions;
using Business.Services.Contract;
using Common.Logger.Contract;
using DTO;
using Entities;
using Persistence.UnitOfWork.Contract;
using System.Linq.Expressions;

namespace Business.Services.Implementation
{
    public class StudentService : BaseService<Student, StudentDTO>, IStudentService
    {
        public StudentService(IUnitOfWork<Student> unitOfWork, IMapper mapper, ILoggerBase loggerBase) : base(unitOfWork, mapper, loggerBase)
        {
        }

        public override async Task<Response<IEnumerable<StudentDTO>>> GetAll()
        {
            var includes = new List<Expression<Func<Student, object>>>() { x => x.StudentData };
            var students = await _unitOfWork.StudentRepository.GetAll(includes: includes);
            var configurations = (await _unitOfWork.StudentDataConfigurationRepository.GetWhere(x => x.IsVisable)).ToDTOList<StudentDataConfigurationDTO>();
            var studentClasses = (await _unitOfWork.StudentClassRepository.GetWhere()).ToDTOList<StudentClassDTO>();

            var studentsDTO = students.ToDTOList<StudentDTO>();
            studentsDTO.ForEach(student => { student.StudentDataConfigurations = configurations; student.StudentClasses = studentClasses.Where(x => x.StudentId == student.Id).ToList(); });
            return new Response<IEnumerable<StudentDTO>> 
            { 
                Code = ResponseStatusEnum.Success,
                Data = studentsDTO
            };
        }

        public override async Task<Response<StudentDTO>> Get(int id)
        {
            var includes = new List<Expression<Func<Student, object>>>() { x => x.StudentData };
            var student = await _unitOfWork.StudentRepository.GetFirstOrDefault(filter: x => x.Id == id , includes: includes);
            var configurations = (await _unitOfWork.StudentDataConfigurationRepository.GetWhere(x => x.IsVisable)).ToDTOList<StudentDataConfigurationDTO>();
            var studentClasses = (await _unitOfWork.StudentClassRepository.GetWhere()).ToDTOList<StudentClassDTO>();
            var studentDTO = student.ToDTO<StudentDTO>();
            studentDTO.StudentDataConfigurations = configurations;
            studentDTO.StudentClasses = studentClasses.Where(x => x.StudentId == studentDTO.Id).ToList();
            return new Response<StudentDTO>
            {
                Code = ResponseStatusEnum.Success,
                Data = studentDTO
            };
        }

        public async Task<Response<StudentDTO>> Enroll(StudentDTO student)
        {
            var response = new Response<StudentDTO>();
            if (student == null || student.StudentClasses == null) return new Response<StudentDTO>() { };
            var _class = await _unitOfWork.ClassRepository.GetFirstOrDefault(x => x.CourseId == student.StudentClasses.FirstOrDefault().Class.CourseId);
            var studentClass = new StudentClass() { ClassId = _class.Id, StudentId = student.Id };
            await _unitOfWork.StudentClassRepository.Add(studentClass);
            if (await _unitOfWork.SaveAsync())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Saved Successfully!";
                response.Data = student;
                return response;
            }
            response.Code = ResponseStatusEnum.Error;
            response.Message = "Not Saved!";
            response.Data = student;
            return response;
        }
    }
}
