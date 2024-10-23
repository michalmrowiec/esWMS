using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Components.DeleteDialog
{
    public static class DeleteDialogHelper
    {
        internal static async Task<IDialogReference?> OpenDeleteConfirmDialog(
            IDialogService dialogService,
            string contentText,
            string? uri,
            Dictionary<string, string>? queryParams,
            MarkupString? snackbarOnDeleteText,
            List<EventCallback>? funcsOnSubmitDelete = null,
            List<EventCallback>? funcsOnCancel = null,
            MaxWidth maxWidth = MaxWidth.ExtraSmall)
        {
            var parameters = new DialogParameters<DeleteConfirmDialog>
            {
                { x => x.ContentText, contentText },
                { x => x.FuncsOnSubmitDelete, funcsOnSubmitDelete },
                { x => x.FuncsOnCancel, funcsOnCancel },
                { x => x.Uri, uri },
                { x => x.QueryParams, queryParams },
                { x => x.SnackbarOnDeleteText, snackbarOnDeleteText ?? new("Usunięto") }
            };

            var options = new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = maxWidth
            };

            return await dialogService.ShowAsync<DeleteConfirmDialog>("Delete", parameters, options);
        }
    }
}
