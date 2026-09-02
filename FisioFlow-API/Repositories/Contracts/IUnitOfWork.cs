namespace FisioFlow_API.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository PatientRepository { get; }
        IPhysiotherapistRepository PhysiotherapistRepository { get; }
        ITreatment TreatmentRepository { get; }  
        ISessionRepository SessionRepository { get; }

        Task Commit();
    }
}
