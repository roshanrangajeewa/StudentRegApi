using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegApi.Models;
using StudentRegApi.Services;
using StudentRegApi.Utilities;

namespace StudentRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(
            IStudentService studentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseAPI<List<StudentDTO>> _response = new ResponseAPI<List<StudentDTO>>();

            try
            {
                List<Student> studentlist = await _studentService.GetList();
                if (studentlist.Count() > 0)
                {
                    List<StudentDTO> stdDTOs = _mapper.Map<List<StudentDTO>>(studentlist);
                    _response = new ResponseAPI<List<StudentDTO>>() { Status = true, Msg = "OK", Value = stdDTOs };
                }
                else
                {
                    _response = new ResponseAPI<List<StudentDTO>>() { Status = false, Msg = "No Data" };
                }
                return StatusCode(StatusCodes.Status200OK, _response);

            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<List<StudentDTO>>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDTO request)
        {
            ResponseAPI<StudentDTO> _response = new ResponseAPI<StudentDTO>();

            try
            {
                Student _model = _mapper.Map<Student>(request);
                Student _studentCreated = await _studentService.Add(_model);
                if (_studentCreated.StudentId != 0)
                {
                    _response = new ResponseAPI<StudentDTO>()
                    {
                        Status = true,
                        Msg = "OK",
                        Value = _mapper.Map<StudentDTO>(_studentCreated)
                    };

                }
                else
                {
                    _response = new ResponseAPI<StudentDTO>() { Status = false, Msg = "Could not Create" };
                }

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<StudentDTO>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put(StudentDTO request)
        {
            ResponseAPI<StudentDTO> _response = new ResponseAPI<StudentDTO>();

            try
            {
                Student _model = _mapper.Map<Student>(request);
                Student _studentEdited = await _studentService.Update(_model);


                _response = new ResponseAPI<StudentDTO>()
                {
                    Status = true,
                    Msg = "OK",
                    Value = _mapper.Map<StudentDTO>(_studentEdited)
                };



                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<StudentDTO>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseAPI<bool> _response = new ResponseAPI<bool>();

            try
            {
                Student _studentFound = await _studentService.Get(id);

                bool deleted = await _studentService.Delete(_studentFound);


                if (deleted)
                    _response = new ResponseAPI<bool> { Status = true, Msg = "OK" };
                else
                    _response = new ResponseAPI<bool> { Status = true, Msg = "No Deleted" };


                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseAPI<bool>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }
    }
}
