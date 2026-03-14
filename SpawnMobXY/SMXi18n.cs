using System.Collections.Generic;

namespace SpawnMobXY
{
    public static class SMXi18n
    {
        public static string Language = "en";
        private static readonly Dictionary<string, Dictionary<string, string>> _texts = new Dictionary<string, Dictionary<string, string>>
        {
            ["es"] = new Dictionary<string, string>
            {
                ["Usage"] = "Uso: /smx <mob> [cant] [stats] o /smx <attach|list|detach>",
                ["Attached"] = "¡Configuración fijada para {0}!",
                ["Detached"] = "Se han eliminado las stats fijas de {0}.",
                ["ListHeader"] = "=== Mobs con Stats Fijas ===",
                ["NpcNotFound"] = "NPC no encontrado.",
                ["ConsoleError"] = "Error: Desde la consola debes especificar ubicación (x=0 y=0)."
            },
            ["en"] = new Dictionary<string, string>
            {
                ["Usage"] = "Usage: /smx <mob> [amount] [stats] or /smx <attach|list|detach>",
                ["Attached"] = "Stats fixed for {0}!",
                ["Detached"] = "Fixed stats removed for {0}.",
                ["ListHeader"] = "=== Mobs with Fixed Stats ===",
                ["NpcNotFound"] = "NPC not found.",
                ["ConsoleError"] = "Error: From console specify location (x=0 y=0)."
            },
            ["pt"] = new Dictionary<string, string>
            {
                ["Usage"] = "Uso: /smx <mob> [quantidade] [stats] ou /smx <attach|list|detach>",
                ["Attached"] = "Estatísticas fixadas para {0}!",
                ["Detached"] = "Estatísticas fixas removidas de {0}.",
                ["ListHeader"] = "=== Mobs com Estatísticas Fixas ===",
                ["NpcNotFound"] = "NPC não encontrado.",
                ["ConsoleError"] = "Erro: No console especifique a localização (x=0 y=0)."
            }
        };

        public static string Get(string key, params object[] args)
        {
            var lang = _texts.ContainsKey(Language) ? Language : "en";
            return string.Format(_texts[lang][key], args);
        }
    }
}