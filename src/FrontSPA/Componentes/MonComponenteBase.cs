using FrontSPA.Extensoes;
using FrontSPA.Shared;
using FrontSPA.VOs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontSPA.Componentes
{
    public abstract class MonComponenteBase : ComponentBase
    {
        [CascadingParameter] public ErroComponente Error { get; set; }
        [Parameter] public IndicadorVO Indicador { get; set; }
        [Inject] public HttpClient httpClient { get; set; }
        protected bool carregando;

        protected async override Task OnParametersSetAsync()
        {
            if (Indicador != null)
                await CarregarDados();

            await base.OnParametersSetAsync();
        }

        protected abstract Task TratarDado(IEnumerable<object> dados);
        protected virtual void AtualizarLoading(bool carregando)
        {
            this.carregando = carregando;
        }
        private async Task CarregarDados()
        {
            try
            {
                var dados = await httpClient.GetAsync<IEnumerable<object>>($"/Dashboard/{Indicador.Id}",
                   new Dictionary<string, string> { { "data", DateTimeOffset.UtcNow.ToString("O") } }, AtualizarLoading);
                if (dados == null || !dados.Any()) return;

                await TratarDado(dados);
            }
            catch (Exception ex)
            {
                AtualizarLoading(false);
                Error.ProcessarErro(ex);
            }
        }
    }
}
