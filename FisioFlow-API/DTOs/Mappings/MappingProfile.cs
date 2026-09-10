using AutoMapper;
using FisioFlow_API.Models;

namespace FisioFlow_API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Physiotherapist, PhysiotherapistDTO>().ReverseMap();
            CreateMap<Treatment, TreatmentDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Expense, ExpenseDTO>().ReverseMap();
            CreateMap<Session, SessionDTO>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDTO>().ReverseMap();
        }
    }
}
