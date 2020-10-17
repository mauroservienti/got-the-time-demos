namespace Finance
{
    internal interface IInvoiceService
    {
        bool IsInvoicePaid(int number);
    }
}