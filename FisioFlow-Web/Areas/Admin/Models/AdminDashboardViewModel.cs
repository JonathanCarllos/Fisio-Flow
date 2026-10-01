namespace FisioFlow_Web.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalPatients { get; set; }

        public int TotalPhysiotherapists { get; set; }

        public int TotalExpenses { get; set; }

        public decimal TotalExpenseAmount { get; set; }
    }
}