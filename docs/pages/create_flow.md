## Creating the Flow

### Creating the Flow used in publishing notifications to Teams

_Prerequisite: [Posting to Microsoft Teams][postingToTeams]_

1. You can create Flows via [Office 365][office365]. Once on the landing page, you'll find Flow under Apps.

   ![Office Flow][officeFlow]

2. Once in the _Manage your flows_ page, click on _Create from blank_

   ![Create from blank][flow1Create1]

   ![Create from blank again][flow1Create2]

3. You'll be asked to search for connectors and triggers. In the search box, you can just type _http_, and you should see the HTTP trigger in the suggestions that will appear. Select it.

   ![HTTP request trigger][flow2HttpReqTrigger]

4. The HTTP trigger will allow you to manually call and therefore trigger this Flow from an external application, using a POST URL that will be generated once the Flow is saved. But you need to define the JSON schema that this Flow will accept, which will be the request body of all POST calls to this Flow. (Thus also note that all requests must include the Content-Type header that is set to `application/json`.)

   After defining the schema, click on _New step_.

   ![HTTP request trigger schema][flow3HttpReqTriggerSchema]

5. The next step is to select an action to be done once this Flow gets triggered.

   The business scenario was to have the flexibility to send notification messages to specific applications based on the event from the Badge Application, and the _switch_ action answered this requirement.

   In the search box, type _switch_, and then select the Switch control that will appear in the suggestions.

   ![Switch action][flow4SwitchAction]

6. The JSON schema in the current official Flow includes an _eventType_ property that specifies the event from the Badge Application, which serves as the condition for the Switch control.

   Once you focus on the field, you will be shown the dynamic content popup. You might notice that these suggested content essentially came from your JSON schema. Pick the _eventType_ property.

   ![Switch action - On][flow5SwitchActionOn]

7. Next is where you'll specify the tasks -like sending the message to Teams- depending on the event.

   _Note: The value in the screenshot currently serves as placeholder, until we identify the specific events._

   Click on _Add an action_.

   ![Switch Case Action][flow6SwitchCaseAction]

8. In the search box that will appear, type _http_ and then select HTTP from the options under Actions.

   ![Switch Case Action HTTP][flow7SwitchCaseActionHttp]

9. Configuring the HTTP action, Part 1:

   a. Since the Teams Webhook accepts POST, select it as Method  
   b. Enter the Webhook URL in the URI field  
   c. The webhook also accepts JSON; add it in the Headers

   ![Switch Case Action HTTP fields 1][flow8SwitchCaseActionHttpFlds1]

10. Configuring the HTTP action, Part 2: set the request body for the webhook.  
You will again be provided dynamic content suggestions from your JSON schema. The current official Flow's schema defines all necessary properties to form the body.

   ![Switch Case Action HTTP fields 2][flow9SwitchCaseActionHttpFlds2]

11. Like in a normal `switch-case` statement, add a default in case the value of the switch condition doesn't match any case. Since we are dealing with Teams for now, simply copy what you did for the first case onto the default.

   ![Switch Case Action Default][flow10SwitchCaseActionDef]

12. You can now save your Flow.

   ![Save][flow11Save]

13. And then you should be provided the generated POST URL for calling this Flow.

   ![Generated URL][flow12GenUrl]



[postingToTeams]: posting_to_teams.md
[office365]: https://www.office.com
[officeFlow]: ./../files/office_flow.png
[flow1Create1]: ./../files/flow_1_create_1.png
[flow1Create2]: ./../files/flow_1_create_2.png
[flow2HttpReqTrigger]: ./../files/flow_2_httpreq_trigger.jpg
[flow3HttpReqTriggerSchema]: ./../files/flow_3_httpreq_trigger_schema.png
[flow4SwitchAction]: ./../files/flow_4_switch_action.jpg
[flow5SwitchActionOn]: ./../files/flow_5_switch_action_on.png
[flow6SwitchCaseAction]: ./../files/flow_6_switch_case_action.png
[flow7SwitchCaseActionHttp]: ./../files/flow_7_switch_case_action_http.jpg
[flow8SwitchCaseActionHttpFlds1]: ./../files/flow_8_switch_case_action_http_flds1.jpg
[flow9SwitchCaseActionHttpFlds2]: ./../files/flow_9_switch_case_action_http_flds2.png
[flow10SwitchCaseActionDef]: ./../files/flow_10_switch_case_action_def.jpg
[flow11Save]: ./../files/flow_11_save.png
[flow12GenUrl]: ./../files/flow_12_gen_url.jpg