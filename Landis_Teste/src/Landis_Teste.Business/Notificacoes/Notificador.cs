using System.Collections.Generic;
using System.Linq;
using Landis_Teste.Business.Interfaces;

namespace Landis_Teste.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>(0);
        }
        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }
}