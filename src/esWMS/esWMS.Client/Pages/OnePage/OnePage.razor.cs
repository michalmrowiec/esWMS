using esWMS.Client.States;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Pages.OnePage
{
    public partial class OnePage
    {
        public MudDynamicTabs DynamicTabs;

        private static RenderFragment GetRenderFragment(Type type, Dictionary<string, object>? parameters = null)
        {
            RenderFragment renderFragment = renderTreeBuilder =>
            {
                renderTreeBuilder.OpenComponent(0, type);

                if (parameters != null)
                {
                    int parNumber = 1;
                    foreach (var param in parameters)
                    {
                        renderTreeBuilder.AddAttribute(parNumber, param.Key, param.Value);
                        parNumber++;
                    }
                }

                renderTreeBuilder.CloseComponent();
            };
            return renderFragment;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            OnePageState.OnAddTab += async () => await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var loged = await AuthService.CheckLogin();

            if (!loged && firstRender)
            {
                Navigation.NavigateTo("/login", true);
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (OnePageState.StateHasChanged)
            {
                OnePageState.StateHasChanged = false;
                StateHasChanged();
            }
        }

        async Task CloseTabCallback(MudTabPanel panel) => await OnePageState.RemoveTab(panel.ID.ToString());
    }
}