using FisioFlow_API.Context;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IPatientRepository? _patientRepository;

        private IPhysiotherapistRepository? _physiotherapistRepository;

        private ITreatment? _treatmentRepository;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPatientRepository PatientRepository
        {
            get
            {
               return _patientRepository ??= new PatientRepository(_context);
            }
        }

        public IPhysiotherapistRepository PhysiotherapistRepository
        {
            get
            {
                return _physiotherapistRepository ??= new PhysiotherapistRepository(_context);
            }
        }

        public ITreatment TreatmentRepository
        {
            get
            {
                return _treatmentRepository ??= new TreatmentRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }
}
