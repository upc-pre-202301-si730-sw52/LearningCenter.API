namespace LearningCenter.API.Tests.Steps;

[Binding]
public class TutorialsServiceStepDefinitions
{
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/tutorials is available")]
    public void GivenTheEndpointHttpsLocalhostApiVTutorialsIsAvailable(int port, int version)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"a Tutorial Resource is included in Response Body")]
    public void ThenATutorialResourceIsIncludedInResponseBody(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"A Tutorial is already stored")]
    public void GivenATutorialIsAlreadyStored(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"An Error Message is returned with value ""(.*)""")]
    public void ThenAnErrorMessageIsReturnedWithValue(string p0)
    {
        ScenarioContext.StepIsPending();
    }
}