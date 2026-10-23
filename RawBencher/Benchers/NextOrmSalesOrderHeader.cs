using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NextORM.Core;

namespace RawBencher.Benchers
{
	/// <summary>
	/// Entity class used by the <see cref="NextOrmBencher"/>. It mirrors the Sales.SalesOrderHeader table; the property names match the column names so only
	/// the table and the two columns which differ in casing have to be mapped.
	/// </summary>
	[SqlTable("Sales.SalesOrderHeader")]
	public class NextOrmSalesOrderHeader
	{
		[Key]
		[Column("SalesOrderID")]
		public int SalesOrderId { get; set; }
		public byte RevisionNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime? ShipDate { get; set; }
		public byte Status { get; set; }
		public bool OnlineOrderFlag { get; set; }
		public string SalesOrderNumber { get; set; }
		public string PurchaseOrderNumber { get; set; }
		public string AccountNumber { get; set; }
		public int CustomerID { get; set; }
		public int? SalesPersonID { get; set; }
		public int? TerritoryID { get; set; }
		public int BillToAddressID { get; set; }
		public int ShipToAddressID { get; set; }
		public int ShipMethodID { get; set; }
		public int? CreditCardID { get; set; }
		public string CreditCardApprovalCode { get; set; }
		public int? CurrencyRateID { get; set; }
		public decimal SubTotal { get; set; }
		public decimal TaxAmt { get; set; }
		public decimal Freight { get; set; }
		public decimal TotalDue { get; set; }
		public string Comment { get; set; }
		[Column("rowguid")]
		public Guid Rowguid { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}
