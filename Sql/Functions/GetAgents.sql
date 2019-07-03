CREATE FUNCTION [dbo].[GetAgents]
(
	-- Add the parameters for the stored procedure here
	@SupervisorUserId nvarchar(128),
	@FirstName nvarchar(350) = null,
	@LastName nvarchar(350) = null,
	@Dni nvarchar(350) = null,
	@SortBy varchar(100) = NULL
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
	U.Id,
	U.FirstName,
	U.LastName, 
	Row =  ROW_NUMBER() OVER (ORDER BY         
			(CASE UPPER(@SortBy)
			    WHEN 'FIRSTNAME ASC' THEN U.FirstName
				WHEN 'LASTNAME ASC' THEN U.LastName
			END) ASC,
			(CASE UPPER(@SortBy)
				 WHEN 'FIRSTNAME DESC' THEN U.FirstName
				 WHEN 'LASTNAME DESC' THEN U.LastName
			END) DESC)
FROM AspNetUsers U  
LEFT JOIN SupervisorUserAgents SUA ON U.Id = SUA.AgentId
WHERE SUA.SupervisorId = @SupervisorUserId
)