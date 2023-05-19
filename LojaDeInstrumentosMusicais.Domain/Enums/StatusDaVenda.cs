using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Domain.Enums
{
    public enum StatusDaVenda
    {
        [Description("Aguardando Pagamento")]
        AguardandoPagamento,
        [Description("Pagamento Aprovado")]
        PagamentoAprovado,
        [Description("Enviado para a transportadora")]
        EnviadoParaTransportadora,
        Entregue,
        Cancelado
    }
}
