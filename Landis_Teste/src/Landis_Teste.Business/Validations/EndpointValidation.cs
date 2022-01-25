using FluentValidation;
using Landis_Teste.Business.Models;

namespace Landis_Teste.Business.Validations
{
    public class EndpointValidation : AbstractValidator<Endpoint>
    {
        public EndpointValidation()
        {
            RuleFor(x => x.EndpointSerialNumber)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(x => x.MeterFirmwareVersion)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(x => x.MeterModelId)
                .NotEmpty().WithMessage("O {PropertyName} precisa ter um valor válido.")
                .IsInEnum();

            RuleFor(x => x.MeterNumber)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ter um valor válido.");

            RuleFor(x => x.SwitchState).IsInEnum()
                .NotEmpty().WithMessage("O {PropertyName} precisa ter um valor válido.")
                .IsInEnum();
        }
    }
}