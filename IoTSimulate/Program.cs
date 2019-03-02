using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IoTSimulate
{
    class Program
    {
        static DeviceClient deviceClient;
        static string IoTHubUri = "logeshIoTHub.azure-devices.net";
        static string DeviceKey = "kbX8y6PzSatH0xUpzJX0hVVS+SN+gRLoXOzPbfKMlek=";
        static string deviceID = "ConsoleTestDemo";
        static string location = "Thiruvannamalai";
        static void Main(string[] args)
        {
            Console.WriteLine("Simulate Device \n");
            deviceClient = DeviceClient.Create(IoTHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceID,DeviceKey),TransportType.Amqp);
            deviceClient.ProductInfo="IoTTestConsole";
            SendDevicetoCloudTelemetry();
            Console.Read();
        }
        private static async void  SendDevicetoCloudTelemetry()
        {
            int messageid = 1;
            DateTime dateTime;
            double Temperture = 50;
            double Humidity = 40;
            Random random = new Random();
            while (true)
            {
               double temperture = Temperture + random.NextDouble()*20;
               double humidity = Humidity + random.NextDouble() * 15;
                var telemetryPoint = new
                {
                    messageid = messageid++,
                    temperture = temperture,
                    humidity = humidity,
                    location = location,
                    deviceID = deviceID,
                    dateTime = DateTime.UtcNow
                };
                var messageTelemetry = JsonConvert.SerializeObject(telemetryPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageTelemetry));
                message.Properties.Add("TempertureAleart ", (temperture > 30) ? "true" : "false");

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("The Message sent id {0} and message {1}", DateTime.UtcNow, messageTelemetry);
                await Task.Delay(10000);
                
            }
            
        }
    }
}
