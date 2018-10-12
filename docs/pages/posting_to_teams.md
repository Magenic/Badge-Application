## Posting to Microsoft Teams

### Setting up an MS Teams Webhook URL

1. Click on the ellipsis beside the channel name of your team, and then select _Connectors_  

   ![Channel connectors option][channelConnectorsOption]

2. The Connectors dialog will open. Select the _Incoming Webhook_ - click on _Configure_.

   ![Channel connectors][channelConnectors]

3. On the next page is where you will set up your webhook. Provide a name, and optionally change the image, and then click on _Create_.

   ![Set up webhook][setUpWebhook]

4. You will be provided a generated URL that you can use for POSTing to that channel

   ![Webhook generated URL][webhookGeneratedUrl]

   Don't forget to Save.

### Sending a message to the team channel

You can try sending a message directly to the channel via Postman:

![Send to Teams via Postman][sendToTeamsViaPostman]

Messages sent to the webhook must follow the [MessageCard][messageCard] format. Below is the complete request body from the sample in the screenshot above:  
```json
{
    "@type": "MessageCard",
    "@context": "http://schema.org/extensions",
    "summary": "12345 earned badge",
    "title": "Attention Magenicons: 12345 has earned the Consultant of the Quarter badge!",
    "sections": [
        {
            "images": [
                {
                    "image":"https://magenicbadgeapp.blob.core.windows.net/badgeimagesprod/badgeimage8",
                    "title": "Consultant of the Quarter"
                }
            ]
        },
        {
            "activityTitle": "Consultant of the Quarter",
            "activityText": "Way to go"
        }
    ],
    "potentialAction": [
        {
            "@context": "http://schema.org",
            "@type": "ViewAction",
            "name": "View",
            "target": [
                "https://badgeapplication.magenic.com/Leaderboard/show/RockyL"
            ]
        }
    ]
}
```

Note though that in our implementation, we are not directly calling the Teams webhook URL. Instead, we have set up a Flow that does that, providing flexibility as to where the notification message should be sent, depending on certain provided parameters in the request body formed in code. See _[Creating the Flow][createFlow]_.

[channelConnectorsOption]: ./../files/channel_connectors_option.jpg
[channelConnectors]: ./../files/channel_connectors.jpg
[setUpWebhook]: ./../files/set_up_webhook.png
[webhookGeneratedUrl]: ./../files/webhook_generated_url.jpg
[messageCard]: https://docs.microsoft.com/en-us/outlook/actionable-messages/actionable-messages-via-connectors
[sendToTeamsViaPostman]: ./../files/send_to_teams_via_postman.jpg
[createFlow]: create_flow.md