using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace IoTDevice
{
    class Program
    {
        //kbX8y6PzSatH0xUpzJX0hVVS+SN+gRLoXOzPbfKMlek=
        static RegistryManager registryManager;
        static string connectionString="HostName=logeshIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=v8S3el/ZNsD8FjuKFiuuRoMzmYK3cwMP7lDSn8L5Mv4=";
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();

            Console.ReadLine();
        }
        private static async Task AddDeviceAsync()
        {
            string deviceID = "ConsoleTestDemo";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceID));
            }
            catch (DeviceAlreadyExistsException)
            {

                device = await registryManager.GetDeviceAsync(deviceID);
            }
            Console.WriteLine(device);
            Console.WriteLine("The Device Symmetric Key is Generated {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
