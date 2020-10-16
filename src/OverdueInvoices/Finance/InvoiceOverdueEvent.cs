namespace Finance
{
    class InvoiceOverdueEvent : InvoiceOverdue
    {
        public int InvoiceNumber { get; set; }
    }
}