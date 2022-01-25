using System;
using System.Collections.Generic;
using System.Linq;
using Landis_Teste.Business.Interfaces;
using Landis_Teste.Business.Models;
using Landis_Teste.Business.Models.Enum;
using Landis_Teste.Business.Notificacoes;

namespace Landis_Teste.Main
{
    public class Menu
    {
        private readonly IEndpointRepository _repository;
        private readonly IEndpointService _service;
        private readonly INotificador _notificador;
        public Menu(IEndpointRepository repository, IEndpointService service, INotificador notificador)
        {
            this._notificador = notificador;
            this._repository = repository;
            this._service = service;

            var programa = true;

            while (programa)
            {
                var entidade = new Endpoint();

                Console.WriteLine("Seja Bem-vindo!");
                Console.WriteLine("Favor escolher uma das opções abaixo!");

                Console.WriteLine("1) Inserir um novo Endpoint.");
                Console.WriteLine("2) Editar um Endpoint existente.");
                Console.WriteLine("3) Deletar Endpoint existente.");
                Console.WriteLine("4) Listar todos os Endpoints.");
                Console.WriteLine("5) Buscar um Endpoint por 'Endpoint Serial Number'.");
                Console.WriteLine("6) Sair. \n");

                var input = Console.ReadLine();

                int number;
                bool success = int.TryParse(input, out number);

                switch (number)
                {
                    case 1:
                        InserirEndpoint(entidade);
                        break;
                    case 2:
                        EditarEndpoint(entidade);
                        break;
                    case 3:
                        DeletarEndpoint(entidade);
                        break;
                    case 4:
                        ListarEndpoints();
                        break;
                    case 5:
                        BuscarEndpoint();
                        break;
                    case 6:
                        Sair();
                        break;
                    default:
                        Console.WriteLine("É obrigatório informar um número válido.");
                        Console.WriteLine(Environment.NewLine);
                        break;
                }
            }
        }

        #region Casos

        private void InserirEndpoint(Endpoint entidade)
        {
            Console.WriteLine("Criando um novo Endpoint: ");
            Console.WriteLine("Favor inserir o Serial Number: ");
            var serialNumber = Console.ReadLine();

            Console.WriteLine("Favor escolher o Meter ModelId: ");
            Console.WriteLine("NSX1P2W: 16");
            Console.WriteLine("NSX1P3W: 17");
            Console.WriteLine("NSX2P3W: 18");
            Console.WriteLine("NSX3P4W: 19");
            var meterModelId = Console.ReadLine();

            int number;
            bool success = int.TryParse(meterModelId, out number);
            if (success)
            {
                switch (number)
                {
                    case 16:
                        entidade.MeterModelId = MeterModelId.NSX1P2W;
                        break;
                    case 17:
                        entidade.MeterModelId = MeterModelId.NSX1P3W;
                        break;
                    case 18:
                        entidade.MeterModelId = MeterModelId.NSX2P3W;
                        break;
                    case 19:
                        entidade.MeterModelId = MeterModelId.NSX3P4W;
                        break;
                    default:
                        entidade.MeterModelId = null;
                        break;
                }
            }
            else
            {
                entidade.MeterModelId = null;
            }

            Console.WriteLine("Favor inserir o Meter Number: ");
            var meterNumber = Console.ReadLine();

            success = int.TryParse(meterNumber, out number);
            if (success)
            {
                entidade.MeterNumber = number;
            }
            else
            {
                entidade.MeterNumber = null;
            }

            Console.WriteLine("Favor inserir o Meter Firmware Version: ");
            var meterFirmwareVersion = Console.ReadLine();

            Console.WriteLine("Favor escolher o Switch State: ");
            Console.WriteLine("Disconnected: 0");
            Console.WriteLine("Connected: 1");
            Console.WriteLine("Armed: 2");
            var switchState = Console.ReadLine();

            Console.WriteLine(Environment.NewLine);

            success = int.TryParse(switchState, out number);
            if (success)
            {
                switch (number)
                {
                    case 0:
                        entidade.SwitchState = SwitchState.Disconnected;
                        break;
                    case 1:
                        entidade.SwitchState = SwitchState.Connected;
                        break;
                    case 2:
                        entidade.SwitchState = SwitchState.Armed;
                        break;
                    default:
                        entidade.SwitchState = null;
                        break;
                }
            }
            else
            {
                entidade.SwitchState = null;
            }

            entidade.EndpointSerialNumber = serialNumber;
            entidade.MeterFirmwareVersion = meterFirmwareVersion;
            _service.Adicionar(entidade);

            var notificacoes = _notificador.ObterNotificacoes();

            if (VerificarNotificacoes(notificacoes))
            {
                Console.Clear();
            }
        }

        private void EditarEndpoint(Endpoint entidade)
        {
            Console.WriteLine("Editando um Endpoint existente.");
            Console.WriteLine("Favor informar o Serial Number: ");
            var serialNumber = Console.ReadLine();
            entidade = _service.BuscarPorSerialNumber(serialNumber).Result;

            var notificacoes = _notificador.ObterNotificacoes();

            if (!VerificarNotificacoes(notificacoes))
                return;

            Console.WriteLine("Favor escolher o Switch State: ");
            Console.WriteLine("Disconnected: 0");
            Console.WriteLine("Connected: 1");
            Console.WriteLine("Armed: 2");
            var switchState = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);

            var switchStateOld = entidade.SwitchState;

            switch (Convert.ToInt16(switchState))
            {
                case 0:
                    entidade.SwitchState = SwitchState.Disconnected;
                    break;
                case 1:
                    entidade.SwitchState = SwitchState.Connected;
                    break;
                case 2:
                    entidade.SwitchState = SwitchState.Armed;
                    break;
                default:
                    entidade.SwitchState = null;
                    break;
            }

            notificacoes = _notificador.ObterNotificacoes();

            _service.Atualizar(entidade);

            if (!VerificarNotificacoes(notificacoes))
            {
                entidade.SwitchState = switchStateOld;
            }
        }

        private void ListarEndpoints()
        {
            Console.WriteLine("Listando todos os Endpoints existentes.");
            var entidades = _repository.ListarTodos();

            if (entidades.Result.Count() > 0)
            {
                foreach (var entity in entidades.Result)
                {
                    Console.WriteLine(entity);
                }
                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                Console.WriteLine("Não há Endpoints.");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private void DeletarEndpoint(Endpoint entidade)
        {
            Console.WriteLine("Deletando um Endpoint existente.");
            Console.WriteLine("Favor informar o Serial Number: ");
            var serialNumber = Console.ReadLine();
            var busca = _service.BuscarPorSerialNumber(serialNumber);

            var notificacoes = _notificador.ObterNotificacoes();

            if (VerificarNotificacoes(notificacoes))
            {
                Console.WriteLine("Deseja realmente sair?");
                Console.WriteLine("S para Sim");
                Console.WriteLine("N para Não");
                var input = Console.ReadLine();
                if (input.ToLower() == "s")
                {
                    _service.Remover(serialNumber);
                }
                else if (input.ToLower() != "n")
                {
                    Console.WriteLine("Favor inserir 's' ou 'n'");
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        private void BuscarEndpoint()
        {
            Console.WriteLine("Buscando um Endpoint existente.");
            Console.WriteLine("Favor informar o Serial Number: ");
            var serialNumber = Console.ReadLine();
            var busca = _service.BuscarPorSerialNumber(serialNumber);

            var notificacoes = _notificador.ObterNotificacoes();

            if (VerificarNotificacoes(notificacoes))
            {
                Console.WriteLine(busca.Result);
                Console.WriteLine(Environment.NewLine);
            }
        }

        private void Sair()
        {
            Console.WriteLine("Deseja realmente sair?");
            Console.WriteLine("S para Sim");
            Console.WriteLine("N para Não");
            var input = Console.ReadLine();
            if (input.ToLower() == "s")
            {
                Environment.Exit(0);
            }
            else if (input.ToLower() == "n")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Favor inserir 's' ou 'n'");
                Console.WriteLine(Environment.NewLine);
            }
        }

        #endregion

        #region FunçõesAuxiliares

        private static bool VerificarNotificacoes(List<Notificacao> notificacoes)
        {
            if (notificacoes.Count > 0)
            {
                foreach (var notificacao in notificacoes)
                {
                    Console.WriteLine(notificacao.Mensagem);
                }
                notificacoes.Clear();
                Console.WriteLine(Environment.NewLine);
                return false;
            }
            return true;
        }

        #endregion
    }
}