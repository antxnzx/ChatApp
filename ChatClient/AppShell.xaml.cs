﻿using ChatClient.Views;

namespace ChatClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ChatView), typeof(ChatView));
            Routing.RegisterRoute(nameof(EditSubsView), typeof(EditSubsView));
            Routing.RegisterRoute(nameof(UserInfoTablePage), typeof(UserInfoTablePage));
            Routing.RegisterRoute(nameof(UserLoginInfoPage), typeof(UserLoginInfoPage));
            Routing.RegisterRoute(nameof(UserSubsTablePage), typeof(UserSubsTablePage));
            Routing.RegisterRoute(nameof(AdminPanelPage), typeof(AdminPanelPage));
        }
    }
}
