## Emulating a Badge Award

### Emulating a Badge Award on your database

1. Verify if the required data exists:  
```sql
-- Verify if there are existing records

SELECT * FROM [dbo].[QueueItem]

GO


SELECT * FROM [dbo].[BadgeAward]

GO


SELECT * FROM [dbo].[Employee]

GO


SELECT * FROM [dbo].[Badge]

GO
```

2. If not, add the records:  
```sql
-- Add records

INSERT INTO [dbo].[BadgeAward]

           ([BadgeId]

           ,[EmployeeId]

           ,[AwardDate]

           ,[AwardAmount]

           ,[PaidOut])

     VALUES

           (38

           ,2

           ,GETDATE()

           ,20000

           ,0)

GO


INSERT INTO [dbo].[QueueItem]

           ([BadgeAwardId]

           ,[QueueItemCreated])

     VALUES

           (1

           ,GETDATE())

GO
```