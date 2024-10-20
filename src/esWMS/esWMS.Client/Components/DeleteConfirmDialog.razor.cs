using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Components
{
    public partial class DeleteConfirmDialog
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string ContentText { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}