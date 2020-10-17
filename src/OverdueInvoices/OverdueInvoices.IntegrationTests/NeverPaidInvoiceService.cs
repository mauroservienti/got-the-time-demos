using Finance;

namespace OverdueInvoices.IntegrationTests
{
    class NeverPaidInvoiceService : IInvoiceService
    {
        public bool IsInvoicePaid(int number)
        {
            return false;
        }
    }
}