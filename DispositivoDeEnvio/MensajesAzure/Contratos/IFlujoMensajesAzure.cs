using System.Threading.Tasks;

namespace DispositivoDeEnvio.BW.MensajesAzure.Contratos {
    public interface IFlujoMensajesAzure {
        Task<string> EnviaMensajeAsync(string mensaje, string dispositivo);

        Task<string> RecibeMensajeAsync(string dispositivo);
    }
}
