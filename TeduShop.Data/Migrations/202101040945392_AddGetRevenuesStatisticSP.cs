namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGetRevenuesStatisticSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenuesStatisticSP"
                , p => new
                    {
                        fromDate = p.String(),
                        toDate = p.String()
                    }
                , @"select DATEADD(DAY,0, DATEDIFF(DAY,0, o.CreatedDate)) as Date
                    , SUM(od.Quantity * od.UnitPrice) as Revenues
                    , SUM(od.Quantity * od.UnitPrice - od.Quantity * p.OriginalPrice) as Benefits
                    from dbo.[Orders] o
                    inner join dbo.[OrderDetails] od on o.ID = od.OrderID
                    inner join dbo.[Products] p on od.ProductID = p.ID
                    where o.CreatedDate >= cast(@fromDate as date) and o.CreatedDate <= cast(@toDate as date)
                    group by DATEADD(DAY,0, DATEDIFF(DAY,0, o.CreatedDate))"
            );
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetRevenuesStatisticSP");
        }
    }
}
