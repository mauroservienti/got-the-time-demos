using Finance;

namespace OverdueInvoices.IntegrationTests
{
    class AlwaysPaidInvoiceService : IInvoiceService
    {
        public bool IsInvoicePaid(int number)
        {
            return true;
        }
    }
}