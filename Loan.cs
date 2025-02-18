using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POOActivity.Entities;

namespace POOActivity.Entities
{
    class Loan
    {
        public User User { get; set; }
        public libraryItems Item { get; set; }
       
        public DateTime LoanDate { get; set; }

        public DateTime BackDate { get; set; }

        public Loan(User user, libraryItems item)
        {
            User = user;
            Item = item;
            LoanDate = DateTime.Now;
            BackDate = LoanDate.AddDays(14);
            item.Availability = false;
        }
        public void ExibirDetalhes()
        {
            Console.WriteLine($"Usuário: {User.Name}, Item: {Item.Title}, Data Empréstimo: {LoanDate}, Data Devolução: {BackDate}");
        }

    }
}
