using Client.Infrastructure.Settings;
using MudBlazor;

namespace MudBlazorClient.Shared
{
    public partial class MainLayout
    {
        private MudTheme? _currentTheme;
        private bool _drawerOpen = true;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _currentTheme = CleanTheme.DefaultTheme;
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
