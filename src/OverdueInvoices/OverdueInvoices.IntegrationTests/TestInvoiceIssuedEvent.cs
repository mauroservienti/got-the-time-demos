using System;
using Messages;

namespace OverdueInvoices.IntegrationTests
{
    class TestInvoiceIssuedEvent : InvoiceIssued
    {
        public int InvoiceNumber { get; set; }
        public DateTime DueDate { get; set; }
        public string CustomerCountry { get; set; }
    }

    class TestInvoicePaidEvent : InvoicePaid
    {
        public int InvoiceNumber { get; set; }
    }
}