using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.States
{
    public class OnePageState
    {
        public List<TabView> UserTabs = new();
        public int UserIndex;
        public event Func<Task>? OnAddTab;
        public bool StateHasChanged { get; set; }

        private async Task NotifyStateChanged()
        {
            if (OnAddTab is not null)
            {
                await OnAddTab.Invoke();
            }
        }
        public async Task AddTab(TabView tabView)
        {
            UserTabs.Add(tabView);
            UserIndex = UserTabs.Count - 1; // Automatically switch to the new tab.
            await NotifyStateChanged();
        }

        public async Task RemoveTab(string tabId)
        {
            var tabView = UserTabs.SingleOrDefault((t) => Equals(t.Id, tabId));
            if (tabView is not null)
            {
                UserTabs.Remove(tabView);
                await NotifyStateChanged();
            }
        }

        public async Task RemoveTab(Guid tabId)
        {
            await RemoveTab(tabId.ToString());
        }
    }

    public class TabView
    {
        public TabView(
            string label,
            ComponentBase content,
            Guid? id,
            string icon = Icons.Material.Filled.CropSquare,
            Dictionary<string, object>? componentParameters = null)
        {
            Label = label;
            Content = content;
            Id = id.ToString() ?? Guid.NewGuid().ToString();
            Icon = icon;
            ComponentParameters = componentParameters;
        }

        public TabView(
            string label,
            ComponentBase content,
            string? id,
            string icon = Icons.Material.Filled.CropSquare,
            Dictionary<string, object>? componentParameters = null)
        {
            Label = label;
            Content = content;
            Id = id ?? Guid.NewGuid().ToString();
            Icon = icon;
            ComponentParameters = componentParameters;
        }

        public string Label { get; set; }
        public ComponentBase Content { get; set; }
        public string Id { get; set; }
        public string Icon { get; set; }
        public Dictionary<string, object>? ComponentParameters { get; set; } = new();
    }
}