using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Components.DeleteDialog
{
    public static class DeleteDialogHelper
    {
        internal static async Task<IDialogReference?> OpenDeleteConfirmDialog(
            IDialogService dialogService,
            string contentText,
            Func<Task<HttpResponseMessage>>? deleteFunc,
            MarkupString? snackbarOnDeleteText,
            string? dialogTittle = "Usuwanie",
            List<EventCallback>? funcsOnSubmitDelete = null,
            List<EventCallback>? funcsOnCancel = null,
            MaxWidth maxWidth = MaxWidth.ExtraSmall)
        {
            var parameters = new DialogParameters<DeleteConfirmDialog>
            {
                { x => x.ContentText, contentText },
                { x => x.FuncsOnSubmitDelete, funcsOnSubmitDelete },
                { x => x.FuncsOnCancel, funcsOnCancel },
                { x => x.DeleteFunc, deleteFunc},
                { x => x.SnackbarOnDeleteText, snackbarOnDeleteText ?? new("Usunięto") }
            };

            var options = new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = maxWidth
            };

            return await dialogService.ShowAsync<DeleteConfirmDialog>(dialogTittle, parameters, options);
        }
    }
}
