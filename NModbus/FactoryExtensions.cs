using NModbus.Device;
using NModbus.IO;

namespace NModbus
{
    /// <summary>
    /// Extension methods for the IModbusFactory interface.
    /// </summary>
    public static class FactoryExtensions
    {
        /// <summary>
        /// Creates an RTU master.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="streamResource"></param>
        /// <returns></returns>
        public static IModbusSerialMaster CreateRtuMaster(this IModbusFactory factory, IStreamResource streamResource)
        {
            IModbusRtuTransport transport = factory.CreateRtuTransport(streamResource);

            return new ModbusSerialMaster(transport);
        }

        /// <summary>
        /// Creates an ASCII master.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="streamResource"></param>
        /// <returns></returns>
        public static IModbusSerialMaster CreateAsciiMaster(this IModbusFactory factory, IStreamResource streamResource)
        {
            IModbusAsciiTransport transport = factory.CreateAsciiTransport(streamResource);

            return new ModbusSerialMaster(transport);
        }

       

      
           
    }
}