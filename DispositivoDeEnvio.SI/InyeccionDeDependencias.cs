using DispositivoDeEnvio.BW.MensajesAzure;
using DispositivoDeEnvio.BW.MensajesAzure.Contratos;
using DispositivoDeEnvio.SG.AzureIoT.Conector;
using Microsoft.Extensions.DependencyInjection;

namespace DispositivoDeEnvio.SI {
    public class InyeccionDeDependencias {

        IServiceCollection services;

        public InyeccionDeDependencias(IServiceCollection services) {
            this.services = services;
        }

        public void InyectaDependencias() {
            services.AddTransient<IConectorDeAzure, ConectorDeAzure>();
            services.AddTransient<IFlujoMensajesAzure, FlujoMensajesAzure>();
        }
    }
}
