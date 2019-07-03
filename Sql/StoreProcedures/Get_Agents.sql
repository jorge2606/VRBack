CREATE PROCEDURE [dbo].[Get_Agents]
    -- Add the parameters for the stored procedure here
    @SupervisorUserId nvarchar(128)= NULL,
	@FirstName nvarchar(350)= NULL,
	@LastName nvarchar(350)= NULL,
	@Dni nvarchar(350)= NULL,
	@SortBy varchar(100) = NULL,
    @PageSize int,
    @PageIndex int,    
    @PageTotal int OUT
    
AS
DECLARE    @FirstRecord int,
           @LastRecord int
BEGIN
SELECT    @FirstRecord = (@PageIndex - 1) * @PageSize + 1,
          @LastRecord = (@PageIndex - 1) * @PageSize + @PageSize;
    
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SELECT A.*
    FROM dbo.GetAgents(@SupervisorUserId, @FirstName, @LastName, @Dni, @SortBy) A
            WHERE A.Row BETWEEN @FirstRecord AND @LastRecord
    SET @PageTotal = ISNULL((SELECT MAX(A.Row)
                            FROM dbo.GetAgents(@SupervisorUserId, @FirstName, @LastName, @Dni, @SortBy) A),0)
END