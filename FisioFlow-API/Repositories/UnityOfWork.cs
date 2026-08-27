using FisioFlow_API.Context;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Repositories
{
    public class UnityOfWork : IUnitOfWork
    {
        private IPatientRepository _patientRepository;

        private IPhysiotherapistRepository _physiotherapistRepository;

        public AppDbContext _context;

        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPatientRepository PatientRepository
        {
            get
            {
               return _patientRepository ?? new PatientRepository(_context);
            }
        }

        public Task Commit()
        {
            throw new NotImplementedException();
        }
    }
}
