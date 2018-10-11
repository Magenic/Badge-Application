## Creating the Database

### Creating the database on your local machine

1. Publish the .sqlproj using the _LocalSQL2012.publish.xml_ profile. Make sure to edit the target database connection string to point to your server.

2. Delete the _CurrentActiveBadges_ and _EarnedBadges_ views

3. Run _drop_create_db_from_edmx_singularized.sql_

4. Run _PostDeployScaffolding.sql_ (can be found in the .sqlproj directory)

5. If the above script returns an error in inserting records to `[dbo].[QueueEvent]` due to being non-identity (row 85), run the following:  
```sql
USE [Magenic.BadgeApplication.DB]

GO


INSERT INTO [dbo].[QueueEvent]

           ([QueueEventId]

           ,[QueueEventName]

           ,[QueueEventDescription])

     VALUES

           (1, 'Processed', 'The item successfully processed'),

           (2, 'Processing', 'The item is processing'),

           (3, 'Failed', 'The item failed to process')

GO
```

6. Run _LocalTestDataScript.sql_ (can be found in the .sqlproj directory)