/* .sql query managed by QueryFirst add-in */
-- designTime - put parameter declarations and design time initialization here
declare @custId nchar(5);
-- endDesignTime
select * from Customers C 
where C.CustomerID = @custId
