using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Components.Alert
{
    public interface IAlertService
    {
        void Subscribe(Action<MarkupString, Severity> showAction);
        void ShowAlert(MarkupString message, Severity severity = Severity.Error);
    }

    public class AlertService : IAlertService
    {
        private Action<MarkupString, Severity>? OnShow;

        public void Subscribe(Action<MarkupString, Severity> showAction)
        {
            OnShow = showAction;
        }

        public void ShowAlert(MarkupString message, Severity severity = Severity.Error)
        {
            OnShow?.Invoke(message, severity);
        }
    }
}
