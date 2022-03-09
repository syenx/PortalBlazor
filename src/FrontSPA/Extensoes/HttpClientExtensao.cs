using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace FrontSPA.Extensoes
{
    public static class HttpClientExtensao
    {
        #region GET

        public async static Task<T> GetAsync<T>(this HttpClient cliente, string path, Dictionary<string, string> parametros = null, Action<bool> atualizaLoading = null)
        {
            string corpoResposta = await GetAsync(cliente, path, parametros);

            return JsonConvert.DeserializeObject<T>(corpoResposta);
        }
        public async static Task<string> GetAsync(this HttpClient cliente, string path, Dictionary<string, string> parametros = null, Action<bool> atualizaLoading = null)
        {
            atualizaLoading.TrataAtualizacaoLoading(true);
            var url = MontaUrl(cliente.BaseAddress, path, parametros);

            var resposta = await cliente.GetAsync(url);
            VerificaRetorno(resposta);
            atualizaLoading.TrataAtualizacaoLoading(false);
            return await MontaResposta(resposta);
        }

        public async static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient http, string path, T corpoPost, Action<bool> atualizaLoading)
        {
            atualizaLoading.TrataAtualizacaoLoading(true);
            var resposta = await http.PostAsJsonAsync(path, corpoPost);
            VerificaRetorno(resposta);
            atualizaLoading.TrataAtualizacaoLoading(false);
            return resposta;
        }

        public async static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient http, string path, T corpoPost, Action<bool> atualizaLoading)
        {
            atualizaLoading.TrataAtualizacaoLoading(true);
            var resposta = await http.PutAsJsonAsync(path, corpoPost);
            VerificaRetorno(resposta);
            atualizaLoading.TrataAtualizacaoLoading(false);
            return resposta;
        }

        public async static Task<HttpResponseMessage> DeleteAsync<T>(this HttpClient http, string path, Action<bool> atualizaLoading)
        {
            atualizaLoading.TrataAtualizacaoLoading(true);
            var resposta = await http.DeleteAsync(path);
            VerificaRetorno(resposta);
            atualizaLoading.TrataAtualizacaoLoading(false);
            return resposta;
        }


        #endregion

        #region Suportes as chamadas http

        private static void VerificaRetorno(HttpResponseMessage resposta)
        {
            if (resposta.IsSuccessStatusCode) return;

            throw new Exception($"[{resposta.StatusCode}] {resposta.ReasonPhrase}");
        }

        private static void TrataAtualizacaoLoading(this Action<bool> atualizaLoading, bool valor)
        {
            if (atualizaLoading != null)
                atualizaLoading(valor);
        }

        private async static Task<string> MontaResposta(HttpResponseMessage resposta)
        {
            string corpoResposta = await resposta.Content.ReadAsStringAsync();
            resposta.EnsureSuccessStatusCode();

            return corpoResposta;
        }
        private static string MontaUrl(Uri urlBase, string path, Dictionary<string, string> parametros = null)
        {
            var builder = new UriBuilder(urlBase) { Path = path };

            if (parametros != null)
                builder.Query = MontarQuery(parametros);

            return builder.ToString();
        }
        private static string MontarQuery(Dictionary<string, string> parametros)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var item in parametros)
                query[item.Key] = item.Value;

            return query.ToString();
        }

        #endregion
    }
}
