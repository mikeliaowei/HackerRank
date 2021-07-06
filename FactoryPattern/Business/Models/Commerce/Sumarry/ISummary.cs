using System;
using System.Collections.Generic;
using System.Text;

namespace Factory_Pattern_First_Look.Business.Models.Commerce.Sumarry
{
	public interface ISummary
	{
		string CreateOrderSummary(Order order);

		void Send();
	}

	public class EmailSummary : ISummary
	{
		public string CreateOrderSummary(Order order)
		{
			return "This is an email summary!";
		}

		public void Send()
		{
			throw new NotImplementedException();
		}
	}

	public class CsvSummary : ISummary
	{
		public string CreateOrderSummary(Order order)
		{
			return "This, is, an, Csv, summary!";
		}

		public void Send()
		{
			throw new NotImplementedException();
		}
	}
}
