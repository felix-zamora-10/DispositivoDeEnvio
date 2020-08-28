using System.Collections.Generic;
using System.Threading.Tasks;

namespace DispositivoDeEnvio.BW.MensajesAzure.Contratos {
    public interface IConectorDeAzure {
        Task<string> EnviaMensajeAsync(string mensaje, List<string> dispositivos, Dictionary<string, string> paresDeNombres);

        Task<string> RecibeMensajeAsync(string dispositivo);

    }
}
