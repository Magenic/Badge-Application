CREATE VIEW [dbo].[BadgeRequestItemsToPublish]
	AS 
SELECT        br.BadgeRequestId, br.EmployeeId, emp.FirstName, emp.LastName, emp.EmailAddress, emp.ADName, br.BadgeName, br.BadgeDescription, br.CreatedDate, br.NotifySentDate
FROM            dbo.BadgeRequest AS br INNER JOIN
                         dbo.Employee AS emp ON emp.EmployeeId = br.EmployeeId
WHERE        (br.NotifySentDate IS NULL)
