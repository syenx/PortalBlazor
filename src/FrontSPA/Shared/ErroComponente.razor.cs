using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace FrontSPA.Shared
{
    public partial class ErroComponente
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        public void ProcessarErro(Exception ex)
        {
            ProcessarErro(ex.GetBaseException().Message);
            Console.WriteLine(ex);
        }

        public void ProcessarErro(string msg)
        {
            Snackbar.Add(msg.Substring(0, 20), Severity.Error);
            Console.WriteLine(msg);
        }
    }
}
