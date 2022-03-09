using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace FrontSPA.Extensoes
{
    public static class ObjectExtensao
    {
        public static Dictionary<string, object> ParaDicionario(this object objeto)
        {
            if (objeto is JObject jobj)
                return jobj.ToObject<Dictionary<string, object>>();
            return objeto.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(objeto, null));
        }

        public static IEnumerable<string> ObterNomePropriedades(this object objeto)
        {
            if (objeto is JObject jobj)
                return jobj.Properties().Select(c => c.Name);
            return objeto.GetType().GetProperties().Select(c => c.Name);
        }
    }
}
