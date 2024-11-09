using esWMS.Client.Services.Notification;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Components.Notification
{
    public partial class GlobalAlert
    {
        [Inject]
        public IAlertService AlertService { get; set; }
        private readonly List<Alert> _alerts = [];
        private const int _alertMaxVisible = 5;

        private void ShowAlert(MarkupString message, Severity severity)
        {
            _alerts.Add(new Alert(Guid.NewGuid(), message, severity));

            StateHasChanged();
        }

        private void HideAlert(Guid id)
        {
            _alerts.RemoveAll(a => a.Id == id);

            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            AlertService.Subscribe(ShowAlert);
        }

        protected record Alert(Guid Id, MarkupString Message, Severity Severity);
    }
}