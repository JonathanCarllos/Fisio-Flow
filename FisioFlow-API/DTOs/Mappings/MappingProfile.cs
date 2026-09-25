using AutoMapper;
using FisioFlow_API.Models;

namespace FisioFlow_API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>()
                .ReverseMap();


            CreateMap<Physiotherapist, PhysiotherapistDTO>()
                .ReverseMap();


            CreateMap<Treatment, TreatmentDTO>()
                .ReverseMap();


            CreateMap<Payment, PaymentDTO>()
                .ReverseMap();


            CreateMap<Expense, ExpenseDTO>()
                .ReverseMap();



            // Session Entity -> DTO (GET)
            CreateMap<Session, SessionDTO>()
                .ForMember(
                    dest => dest.PatientName,
                    opt => opt.MapFrom(
                        src => src.Patient.Name
                    )
                )
                .ForMember(
                    dest => dest.PhysiotherapistName,
                    opt => opt.MapFrom(
                        src => src.Physiotherapist.Name
                    )
                );



            // DTO -> Session (POST / PUT)
            CreateMap<SessionDTO, Session>()
                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Physiotherapist,
                    opt => opt.Ignore()
                );



            CreateMap<MedicalRecord, MedicalRecordDTO>()
                .ReverseMap();
        }
    }
}