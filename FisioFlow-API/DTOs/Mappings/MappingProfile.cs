using AutoMapper;
using FisioFlow_API.Models;

namespace FisioFlow_API.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ============================================================
            // PATIENT
            // ============================================================

            CreateMap<Patient, PatientDTO>()
                .ReverseMap();


            // ============================================================
            // PHYSIOTHERAPIST
            // ============================================================

            CreateMap<Physiotherapist, PhysiotherapistDTO>()
                .ReverseMap();


            // ============================================================
            // TREATMENT
            // ============================================================


            // Entity -> DTO
            CreateMap<Treatment, TreatmentDTO>()
                .ForMember(
                    dest => dest.PatientName,
                    opt => opt.MapFrom(
                        src => src.Patient != null
                            ? src.Patient.Name
                            : null
                    )
                )
                .ForMember(
                    dest => dest.PhysiotherapistName,
                    opt => opt.MapFrom(
                        src => src.Physiotherapist != null
                            ? src.Physiotherapist.Name
                            : null
                    )
                );




            // DTO -> Entity
            CreateMap<TreatmentDTO, Treatment>()
                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Physiotherapist,
                    opt => opt.Ignore()
                );


            CreateMap<Payment, PaymentDTO>()

    .ForMember(
        dest => dest.PatientName,
        opt => opt.MapFrom(
            src => src.Patient != null
                ? src.Patient.Name
                : null
        )
    )

    .ForMember(
        dest => dest.TreatmentName,
        opt => opt.MapFrom(
            src => src.Treatment != null
                ? src.Treatment.Type
                : null
        )
    );



            CreateMap<PaymentDTO, Payment>()

                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                )

                .ForMember(
                    dest => dest.Treatment,
                    opt => opt.Ignore()
                );


            // ============================================================
            // EXPENSE
            // ============================================================

            CreateMap<Expense, ExpenseDTO>()
                .ReverseMap();


            // ============================================================
            // SESSION
            // ============================================================

            // Entity -> DTO
            CreateMap<Session, SessionDTO>()
                .ForMember(
                    dest => dest.PatientName,
                    opt => opt.MapFrom(
                        src => src.Patient != null
                            ? src.Patient.Name
                            : null
                    )
                )
                .ForMember(
                    dest => dest.PhysiotherapistName,
                    opt => opt.MapFrom(
                        src => src.Physiotherapist != null
                            ? src.Physiotherapist.Name
                            : null
                    )
                );

            // DTO -> Entity
            CreateMap<SessionDTO, Session>()
                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Physiotherapist,
                    opt => opt.Ignore()
                );


            // ============================================================
            // MEDICAL RECORD
            // ============================================================

            // Entity -> DTO
            // Usado nos GETs
            CreateMap<MedicalRecord, MedicalRecordDTO>()
                .ForMember(
                    dest => dest.PatientName,
                    opt => opt.MapFrom(
                        src => src.Patient != null
                            ? src.Patient.Name
                            : null
                    )
                )
                .ForMember(
                    dest => dest.PhysiotherapistName,
                    opt => opt.MapFrom(
                        src => src.Physiotherapist != null
                            ? src.Physiotherapist.Name
                            : null
                    )
                );

            // DTO -> Entity
            // Usado no POST e PUT
            CreateMap<MedicalRecordDTO, MedicalRecord>()
                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Physiotherapist,
                    opt => opt.Ignore()
                );
        }
    }
}