namespace Foca.ExportImport
{
    public interface IFocaPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
        void Initialize();
    }

    public sealed class FocaSerpApiDuckDuckGoPlugin : IFocaPlugin
    {
        public string Name => "FOCA SerpApi DuckDuckGo";
        public string Description => "Búsqueda avanzada de documentos via SerpApi (DuckDuckGo)";
        public string Author => "Andrés Nacimiento";
        public string Version => "1.0.0";

        public void Initialize()
        {
            // Igual que foca-excel-export: inicializa el resolver antes de dependencias
            Foca.SerpApiDuckDuckGo.AssemblyResolver.Init();
            System.Windows.Forms.Application.ApplicationExit += (s, e) => { };
        }
    }
}


