using System.Collections.Generic;
using Landis_Teste.Business.Notificacoes;

namespace Landis_Teste.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}