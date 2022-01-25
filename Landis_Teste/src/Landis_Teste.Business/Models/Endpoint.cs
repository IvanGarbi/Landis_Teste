using Landis_Teste.Business.Models.Enum;

namespace Landis_Teste.Business.Models
{
    public class Endpoint : Entity
    {
        public MeterModelId? MeterModelId { get; set; }
        public int? MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public SwitchState? SwitchState { get; set; }

        public override string ToString()
        {
            return $"EndPoint: " +
                   $"{EndpointSerialNumber}/" +
                   $"{MeterModelId}/{MeterNumber}/" +
                   $"{MeterFirmwareVersion}/" +
                   $"{SwitchState}";
        }
    }
}