using esWMS.Components.Alert;
using MudBlazor;
using Newtonsoft.Json;

namespace esWMS.Client.Services
{
    public static class ApiResponseHandler
    {
        public static async Task HandleFailure(this HttpResponseMessage response, AlertService alertService)
        {
            var json = await response.Content.ReadAsStringAsync();
            try
            {
                var errors = JsonConvert.DeserializeObject<List<string>>(json);

                if (errors != null && errors.Any())
                {
                    alertService.ShowAlert(new(string.Join("<br/>", errors)), Severity.Error);
                }
                else
                {
                    alertService.ShowAlert(new("Coś poszło nie tak."), Severity.Error);
                }
            }
            catch (Exception)
            {
                alertService.ShowAlert(new(json), Severity.Error);
            }
        }
    }
}
