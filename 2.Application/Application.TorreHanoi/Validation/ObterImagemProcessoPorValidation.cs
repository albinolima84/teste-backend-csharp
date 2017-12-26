using Application.TorreHanoi.Message;
using System;

namespace Application.TorreHanoi.Validation
{
    internal static class ObterImagemProcessoPorValidation
    {
        internal static ObterImagemProcessoPorResponse ValidationImagem(this string id)
        {
            var response = new ObterImagemProcessoPorResponse();
            Guid guid = new Guid(id);
            if (Guid.TryParse(id, out guid))
            {
                return response;
            }
            response.AdicionarMensagemDeErro($"É o id {id} não esta em um formato valido");
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            return response;
        }
    }
}
