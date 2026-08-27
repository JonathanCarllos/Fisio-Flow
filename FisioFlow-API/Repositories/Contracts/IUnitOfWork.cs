namespace FisioFlow_API.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
        IPhysiotherapistRepository PhysiotherapistRepository { get; }

        Task Commit();
    }
}
