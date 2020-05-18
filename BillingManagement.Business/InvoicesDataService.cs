using BillingManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.Business
{
    public class InvoicesDataService : IDataService<Invoice>
    {
        List<Invoice> _invoices;
        IEnumerable<Customer> _customers;

        public InvoicesDataService(IEnumerable<Customer> customers)
        {
            _invoices = new List<Invoice>();
            _customers = customers;

            initValues();
        }

        private void initValues()
        {
            Random rnd = new Random();


            foreach (var customer in _customers)
            {
                int nbInvoices = rnd.Next(0,10);

                for (int i = 0; i < nbInvoices; i++)
                {
                    var invoice = new Invoice(customer);
                    invoice.SubTotal = rnd.NextDouble() * 100 + 50;

                    customer.Invoices.Add(invoice);
                    _invoices.Add(invoice);
                }
            }
        }

        public IEnumerable<Invoice> GetAll()
        {
            foreach (var item in _invoices) {
                yield return item;
            }

        }
    }
}
