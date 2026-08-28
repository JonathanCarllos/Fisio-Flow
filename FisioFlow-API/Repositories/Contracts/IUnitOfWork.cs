namespace FisioFlow_API.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository PatientRepository { get; }
        IPhysiotherapistRepository PhysiotherapistRepository { get; }

        Task Commit();
    }
}
