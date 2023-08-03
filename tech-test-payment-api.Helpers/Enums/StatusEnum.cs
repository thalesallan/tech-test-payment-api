using System.Runtime.Serialization;

namespace tech_test_payment.Helpers.Enums
{
    [DataContract]
    public enum StatusEnum
    {
        [EnumMember(Value = "Aguardando Pagamento")]
        AguardandoPagamento = 1,

        [EnumMember(Value = "Pagamento Aprovado")]
        PagamentoAprovado = 2,

        [EnumMember(Value = "Enviado para Transportadora")]
        EnviadoParaTransportadora = 3,

        [EnumMember(Value = "Entregue")]
        Entregue = 4,

        [EnumMember(Value = "Cancelada")]
        Cancelada = 5
    }
}
