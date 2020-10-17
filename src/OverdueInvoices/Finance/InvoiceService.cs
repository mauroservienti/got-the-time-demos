using System;

namespace Finance
{
    class InvoiceService : IInvoiceService
    {
        public bool IsInvoicePaid(int number)
        {
            Math.DivRem(DateTime.Now.Second, 2, out var rem);
            var isInvoicePaid = rem == 0;

            return isInvoicePaid;
        }
    }
}