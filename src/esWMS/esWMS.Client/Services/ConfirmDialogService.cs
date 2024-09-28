using esWMS.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Services
{
    public interface IConfirmDialogService
    {
        Task OpenDeleteDialogAsync(string dialogTitle, string dialogContent, ConfirmDialogService.ButtonDialogType actionButtonType, EventCallback? onConfirm = null, EventCallback? onCancel = null);
    }

    public class ConfirmDialogService : IConfirmDialogService
    {
        private readonly IDialogService _dialogService;
        public ConfirmDialogService(IDialogService dialogService) => _dialogService = dialogService;

        public async Task OpenDeleteDialogAsync(
            string dialogTitle,
            string dialogContent,
            ButtonDialogType actionButtonType,
            EventCallback? onConfirm = null,
            EventCallback? onCancel = null)
        {
            var parameters = new DialogParameters<ConfirmDialog>
            {
                { x => x.TittleText, dialogTitle},
                { x => x.ContentText, dialogContent},
                { x => x.ButtonType, actionButtonType}
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _dialogService.ShowAsync<ConfirmDialog>(dialogTitle, parameters, options);
            var result = await dialog.Result;

            if (result?.Canceled ?? false)
            {
                onCancel?.InvokeAsync();
            }
            else
            {
                onConfirm?.InvokeAsync();
            }
        }

        public enum ButtonDialogType
        {
            Ok,
            Edit,
            Delete
        }
    }
}
