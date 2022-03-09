using MudBlazor;

namespace FrontSPA.Shared
{
    public partial class MainLayout
    {
        bool aberto = false;
        MudTheme temaAtual = new MudTheme();

        public void AbrirDrawer()
        {
            aberto = true;
        }

        protected override void OnInitialized()
        {
            temaAtual = temaClaro;
        }

        void DarkMode()
        {
            temaAtual = temaAtual == temaClaro ? temaEscuro : temaClaro;
        }

        MudTheme temaClaro = new MudTheme
        {
            Palette = new Palette
            {
                Black = "#272c34",
                Background = "#f8f9f9"//Colors.Grey.Lighten5
            }
        };

        MudTheme temaEscuro = new MudTheme
        {
            Palette = new Palette
            {
                Black = "#27272f",
                Background = "#32333d",
                BackgroundGrey = "#27272f",
                Surface = "#373740",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255, 255, 255, 0.50)",
                DrawerIcon = "rgba(255, 255, 255, 0.50)",
                AppbarBackground = "#27272f",
                AppbarText = "rgba(255, 255, 255, 0.70)",
                TextPrimary = "rgba(255, 255, 255, 0.70)",
                TextSecondary = "rgba(255, 255, 255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255, 255, 255, 0.26)",
                ActionDisabledBackground = "rgba(255, 255, 255, 0.12)",
            }
        };
    }
}
