using AutoMapper;
using StudentRegApi.Models;
using System.Globalization;

namespace StudentRegApi.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Student
            CreateMap<Student, StudentDTO>()
                .ForMember(destiny =>
                destiny.dateofbirth,
                opt => opt.MapFrom(origin => origin.DateOfBirth.ToString("dd/MM/yyyy")))
                .ForMember(destiny =>
                destiny.mobile,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Mobile, CultureInfo.InvariantCulture))
                );

            CreateMap<StudentDTO, Student>()
                .ForMember(destiny =>
                destiny.DateOfBirth,
                opt => opt.MapFrom(origin => DateTime.ParseExact(origin.dateofbirth, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );
            #endregion
        }
    }
}
