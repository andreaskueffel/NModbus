using System.Net.Sockets;
using NModbus.IO;

namespace NModbus
{
    /// <summary>
    /// Container for modbus function services.
    /// </summary>
    public interface IModbusFactory
    {
        /// <summary>
        /// Get the service for a given function code.
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        IModbusFunctionService GetFunctionService(byte functionCode);

        /// <summary>
        /// Gets all of the services.
        /// </summary>
        /// <returns></returns>
        IModbusFunctionService[] GetAllFunctionServices();

        #region Master

        /// <summary>
        /// Create an rtu master.
        /// </summary>
        /// <param name="transport"></param>
        /// <returns></returns>
        IModbusSerialMaster CreateMaster(IModbusSerialTransport transport);

        /// <summary>
        /// Create a TCP master.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        IModbusMaster CreateMaster(UdpClient client);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        IModbusMaster CreateMaster(TcpClient client);

        #endregion

    
        #region Transport

        /// <summary>
        /// Creates an RTU transpoort. 
        /// </summary>
        /// <param name="streamResource"></param>
        /// <returns></returns>
        IModbusRtuTransport CreateRtuTransport(IStreamResource streamResource);

        /// <summary>
        /// Creates an Ascii Transport.
        /// </summary>
        /// <param name="streamResource"></param>
        /// <returns></returns>
        IModbusAsciiTransport CreateAsciiTransport(IStreamResource streamResource);

        #endregion

        IModbusLogger Logger { get; }
    }
}