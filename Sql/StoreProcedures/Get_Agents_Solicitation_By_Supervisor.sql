USE [VR]
GO
/****** Object:  StoredProcedure [dbo].[ViewSolicitationAsSupervisorProcedure]    Script Date: 28/1/2019 10:44:44 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[get_agents_solicitation_by_supervisor]
	-- Add the parameters for the stored procedure here
	@SupervisorId UNIQUEIDENTIFIER = NULL,
	@AgentId UNIQUEIDENTIFIER = NULL,
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
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT    @FirstRecord = (@PageIndex - 1) * @PageSize + 1,
          @LastRecord = (@PageIndex - 1) * @PageSize + @PageSize;
    -- Insert statements for procedure here
	SELECT GSBS.*
	FROM [dbo].GetAgentsSolicitationBySupervisor(@SupervisorId,@AgentId,@FirstName,@LastName,@Dni,@SortBy) AS GSBS
	WHERE GSBS.Row BETWEEN @FirstRecord AND @LastRecord

	SET @PageTotal = ISNULL((SELECT MAX(GSBS.Row)
         FROM [dbo].GetAgentsSolicitationBySupervisor(@SupervisorId,@AgentId,@FirstName,@LastName,@Dni,@SortBy) AS GSBS),0)
END
