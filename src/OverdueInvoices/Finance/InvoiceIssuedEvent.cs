using System;
using Messages;

namespace OverdueInvoices
{
    class InvoiceIssuedEvent : InvoiceIssued
    {
        public int InvoiceNumber { get; set; }
        public DateTime DueDate { get; set; }
        public string CustomerCountry { get; set; }
    }
}