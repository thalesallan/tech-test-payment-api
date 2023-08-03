using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Helpers
{
    public static class GetStatusStringHelper
    {
        public static string GetStringStatus(StatusEnum statusEnum)
        {
            return statusEnum switch
            {
                StatusEnum.AguardandoPagamento => StatusConstants.AGUARDANDO_PAGAMENTO,
                StatusEnum.PagamentoAprovado => StatusConstants.PAGAMENTO_APROVADO,
                StatusEnum.EnviadoParaTransportadora => StatusConstants.ENVIADO_PARA_TRANSPORTADORA,
                StatusEnum.Entregue => StatusConstants.ENTREGUE,
                StatusEnum.Cancelada => StatusConstants.CANCELADA,
                _ => throw new NotImplementedException()
            };
        }
    }
}
