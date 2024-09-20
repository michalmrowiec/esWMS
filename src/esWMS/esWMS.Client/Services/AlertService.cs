using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Components.Alert
{
    public class AlertService
    {
        public Action<MarkupString, Severity>? OnShow;

        public void ShowAlert(MarkupString message, Severity severity = Severity.Error)
        {
            OnShow?.Invoke(message, severity);
        }
    }
}
