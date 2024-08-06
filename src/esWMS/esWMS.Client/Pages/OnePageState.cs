﻿using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace esWMS.Client.Pages
{
    public class OnePageState
    {
        public List<TabView> UserTabs = new();
        public int UserIndex;
        public event Action? OnAddTab;

        private void NotifyStateChanged() => OnAddTab?.Invoke();


        public void AddTab(TabView tabView)
        {
            UserTabs.Add(tabView);
            UserIndex = UserTabs.Count - 1; // Automatically switch to the new tab.
            NotifyStateChanged();
        }
    }

    public class TabView
    {
        public TabView(string label, ComponentBase content, Guid id, string icon = Icons.Material.Filled.CropSquare)
        {
            Label = label;
            Content = content;
            Id = id;
            Icon = icon;
        }

        public string Label { get; set; }
        public ComponentBase Content { get; set; }
        public Guid Id { get; set; }
        public string Icon { get; set; }
    }
}