## Running the Project

### Running the project: Windows service - for the notifications to MS Teams

_Prerequisite: [Setting up test data][emulateAward]_

_Note: URLs necessary for running the project are already set up, but you may refer to the following links on how they were done:  
[Posting to Microsoft Teams][postingToTeams]  
[Creating the Flow][createFlow]_

1. Ensure the connection string in the App.config in _Magenic.BadgeApplication.DataAccess.EF_ points to the correct database server/instance

2. Set _Magenic.BadgeApplication.Console_ as the startup project

3. Since we have not removed the Yammer project yet, you might want to add a reference in the _Magenic.BadgeApplication.Processor_ project to the Teams project, and then edit _QueueItemProcessor.cs_ to only process the items for Teams:  

   ![Add reference to the Teams project][refToTeams]

   ![Only process the items for Teams][processItemsForTeams]

   Just remember not to commit this reference.

4. You might also want to set a breakpoint after publishing the notification to prevent the queue item you inserted as test data from being cleared (still in _QueueItemProcessor.cs_):

   ![Prevent test data from being cleared][preventDataFromClearing]

5. Once you debug the project and hit the breakpoint above, you should see a notification message on Teams like below:

   ![Sample notification message on Teams][sampleOnTeams]

[emulateAward]: emulate_award.md
[postingToTeams]: posting_to_teams.md
[createFlow]: create_flow.md
[refToTeams]: files/add_ref_to_teams.png
[processItemsForTeams]: files/process_items_for_teams.png
[preventDataFromClearing]: files/prevent_test_data_from_clearing.png
[sampleOnTeams]: files/sample_on_teams.PNG