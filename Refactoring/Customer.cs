﻿namespace Refactoring
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Customer
    {
        private IList<Rental> _rentals;

        public Customer(string name)
        {
            Name = name;
            _rentals = new List<Rental>();
        }

        public string Name { get; private set; }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string Statement()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Rental Record for {0}\n", Name);
            
            foreach (var rental in _rentals)
            {
                // Show figures for this rental
                result.AppendFormat("\t{0}\t{1}\n", rental.Movie.Title, rental.GetCharge());
            }

            // Add footer lines
            result.AppendFormat("Amount owed is {0}\n", GetTotalAmount());
            result.AppendFormat("You earned {0} frequent renter points", GetTotalFrequentRenterPoints());

            return result.ToString();
        }

        public string HtmlStatement()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<H1>Rentals for <EM>{0}</EM></H1><P>\n", Name);

            foreach (var rental in _rentals)
            {
                // Show figures for this rental
                result.AppendFormat("{0}: {1}<BR>\n", rental.Movie.Title, rental.GetCharge());
            }

            // Add footer lines
            result.AppendFormat("<P>You owe <EM>{0}</EM><P>\n", GetTotalAmount());
            result.AppendFormat("On this rental you earned <EM>{0}</EM> frequent renter points<P>", GetTotalFrequentRenterPoints());

            return result.ToString();
        }

        private double GetTotalAmount()
        {
            return _rentals.Sum(rental => rental.GetCharge());
        }

        private int GetTotalFrequentRenterPoints()
        {
            return _rentals.Sum(rental => rental.GetFrequentRenterPoints());
        }
    }
}