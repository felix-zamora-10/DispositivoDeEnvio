using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DispositivoDeEnvio.SI.Clases {
    public class AdministradorDeMensajes {
        //private const int MessageCount = 5;
        //private const int TemperatureThreshold = 30;
        //private static Random s_randomGenerator = new Random();
        //private float _temperature;
        //private float _humidity;
        private readonly DeviceClient _deviceClient;

        public AdministradorDeMensajes(DeviceClient deviceClient) {
            _deviceClient = deviceClient ?? throw new ArgumentNullException(nameof(deviceClient));
        }

        public async Task RunSampleAsync(string mensaje) {
            await SendEventAsync(mensaje).ConfigureAwait(false);
            //await ReceiveMessagesAsync().ConfigureAwait(false);
        }

        private async Task SendEventAsync(string mensaje) {
            //Console.WriteLine("Device sending {0} messages to IoTHub...\n", MessageCount);

            //for (int count = 0; count < MessageCount; count++) {
            //    _temperature = s_randomGenerator.Next(20, 35);
            //    _humidity = s_randomGenerator.Next(60, 80);
            //    string dataBuffer = $"{{\"messageId\":{count},\"temperature\":{_temperature},\"humidity\":{_humidity}}}";

            //    using (var eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer))) {
            //        eventMessage.Properties.Add("temperatureAlert", (_temperature > TemperatureThreshold) ? "true" : "false");
            //        Console.WriteLine("\t{0}> Sending message: {1}, Data: [{2}]", DateTime.Now.ToLocalTime(), count, dataBuffer);

            //        await _deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);
            //    }
            //}

            using (var eventMessage = new Message(Encoding.UTF8.GetBytes(mensaje))) {
                Console.WriteLine("\t Enviando mensaje: {0}", mensaje);

                await _deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);
            }
        }

        private async Task ReceiveMessagesAsync() {
            Console.WriteLine("\nDevice waiting for C2D messages from the hub...\n");
            Console.WriteLine("Use the IoT Hub Azure Portal or Azure IoT Explorer to send a message to this device.\n");

            using (Message receivedMessage = await _deviceClient.ReceiveAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false)) {
                if (receivedMessage != null) {
                    string messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    Console.WriteLine("\t{0}> Received message: {1}", DateTime.Now.ToLocalTime(), messageData);

                    int propCount = 0;
                    foreach (var prop in receivedMessage.Properties) {
                        Console.WriteLine("\t\tProperty[{0}> Key={1} : Value={2}", propCount++, prop.Key, prop.Value);
                    }

                    await _deviceClient.CompleteAsync(receivedMessage).ConfigureAwait(false);
                } else {
                    Console.WriteLine("\t{0}> Timed out", DateTime.Now.ToLocalTime());
                }
            }
        }
    }
}
