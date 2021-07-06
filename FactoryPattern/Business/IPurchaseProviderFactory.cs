using Factory_Pattern_First_Look.Business.Models.Commerce;
using Factory_Pattern_First_Look.Business.Models.Commerce.Invoice;
using Factory_Pattern_First_Look.Business.Models.Commerce.Sumarry;
using Factory_Pattern_First_Look.Business.Models.Shipping;
using Factory_Pattern_First_Look.Business.Models.Shipping.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factory_Pattern_First_Look.Business
{
	public interface IPurchaseProviderFactory
	{
		ShippingProvider CreateShippingProvider(Order order);
		
		IInvoice CreateInvoice(Order order);

		ISummary CreateSummary(Order order);
	}

	public class AustraliaPurchaseProviderFactory : IPurchaseProviderFactory
	{
		public IInvoice CreateInvoice(Order order)
		{
			return new GSTInvoice();
		}

		public ShippingProvider CreateShippingProvider(Order order)
		{
			var shippingProviderFactory = new StandardShipingProviderFactory();

			return shippingProviderFactory.GetShippingProvider(order.Sender.Country);
		}

		public ISummary CreateSummary(Order order)
		{
			return new CsvSummary();
		}
	}

	public class SwedenPurchaseProviderFactory : IPurchaseProviderFactory
	{
		public IInvoice CreateInvoice(Order order)
		{
			if(order.Recipient.Country != order.Sender.Country)
			{
				return new NoVATInvoice();
			}

			return new VATInvoice();
		}

		public ShippingProvider CreateShippingProvider(Order order)
		{
			ShippingProviderFactory shippingProviderFactory;
			if (order.Recipient.Country != order.Sender.Country)
			{
				shippingProviderFactory = new GlobalExpressShippingProviderFactory();
			}
			else
			{
				shippingProviderFactory = new StandardShipingProviderFactory();
			}

			return shippingProviderFactory.GetShippingProvider(order.Sender.Country);
		}

		public ISummary CreateSummary(Order order)
		{
			return new EmailSummary();
		}
	}
}
