using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NModbus.Data;
using NModbus.Message;

namespace NModbus.Device
{
    /// <summary>
    ///     Modbus master device.
    /// </summary>
    internal abstract class ModbusMaster : ModbusDevice, IModbusMaster
    {
        internal ModbusMaster(IModbusTransport transport)
            : base(transport)
        {
        }

        /// <summary>
        ///    Reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status.</returns>
        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 2000);

            var request = new ReadCoilsInputsRequest(
                ModbusFunctionCodes.ReadCoils,
                slaveAddress,
                startAddress,
                numberOfPoints);

            return PerformReadDiscretes(request);
        }

        
        /// <summary>
        ///    Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 2000);

            var request = new ReadCoilsInputsRequest(
                ModbusFunctionCodes.ReadInputs,
                slaveAddress,
                startAddress,
                numberOfPoints);

            return PerformReadDiscretes(request);
        }

        

        /// <summary>
        ///    Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 125);

            var request = new ReadHoldingInputRegistersRequest(
                ModbusFunctionCodes.ReadHoldingRegisters,
                slaveAddress,
                startAddress,
                numberOfPoints);

            return PerformReadRegisters(request);
        }


        /// <summary>
        ///    Reads contiguous block of input registers.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 125);

            var request = new ReadHoldingInputRegistersRequest(
                ModbusFunctionCodes.ReadInputRegisters,
                slaveAddress,
                startAddress,
                numberOfPoints);

            return PerformReadRegisters(request);
        }

        

        /// <summary>
        ///    Writes a single coil value.
        /// </summary>
        /// <param name="slaveAddress">Address of the device to write to.</param>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            var request = new WriteSingleCoilRequestResponse(slaveAddress, coilAddress, value);
            Transport.UnicastMessage<WriteSingleCoilRequestResponse>(request);
        }

        

        /// <summary>
        ///    Writes a single holding register.
        /// </summary>
        /// <param name="slaveAddress">Address of the device to write to.</param>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            var request = new WriteSingleRegisterRequestResponse(
                slaveAddress,
                registerAddress,
                value);

            Transport.UnicastMessage<WriteSingleRegisterRequestResponse>(request);
        }

       

        /// <summary>
        ///     Write a block of 1 to 123 contiguous 16 bit holding registers.
        /// </summary>
        /// <param name="slaveAddress">Address of the device to write to.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            ValidateData("data", data, 123);

            var request = new WriteMultipleRegistersRequest(
                slaveAddress,
                startAddress,
                new RegisterCollection(data));

            Transport.UnicastMessage<WriteMultipleRegistersResponse>(request);
        }

      

        /// <summary>
        ///    Writes a sequence of coils.
        /// </summary>
        /// <param name="slaveAddress">Address of the device to write to.</param>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            ValidateData("data", data, 1968);

            var request = new WriteMultipleCoilsRequest(
                slaveAddress,
                startAddress,
                new DiscreteCollection(data));

            Transport.UnicastMessage<WriteMultipleCoilsResponse>(request);
        }

      
        /// <summary>
        ///    Performs a combination of one read operation and one write operation in a single Modbus transaction.
        ///    The write operation is performed before the read.
        /// </summary>
        /// <param name="slaveAddress">Address of device to read values from.</param>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        public ushort[] ReadWriteMultipleRegisters(
            byte slaveAddress,
            ushort startReadAddress,
            ushort numberOfPointsToRead,
            ushort startWriteAddress,
            ushort[] writeData)
        {
            ValidateNumberOfPoints("numberOfPointsToRead", numberOfPointsToRead, 125);
            ValidateData("writeData", writeData, 121);

            var request = new ReadWriteMultipleRegistersRequest(
                slaveAddress,
                startReadAddress,
                numberOfPointsToRead,
                startWriteAddress,
                new RegisterCollection(writeData));

            return PerformReadRegisters(request);
        }

        

        /// <summary>
        /// Write a file record to the device.
        /// </summary>
        /// <param name="slaveAdress">Address of device to write values to</param>
        /// <param name="fileNumber">The Extended Memory file number</param>
        /// <param name="startingAddress">The starting register address within the file</param>
        /// <param name="data">The data to be written</param>
        public void WriteFileRecord(byte slaveAdress, ushort fileNumber, ushort startingAddress, byte[] data)
        {
            ValidateMaxData("data", data, 244);
            var request = new WriteFileRecordRequest(slaveAdress, new FileRecordCollection(
                fileNumber, startingAddress, data));

            Transport.UnicastMessage<WriteFileRecordResponse>(request);
        }

        /// <summary>
        ///    Executes the custom message.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        [SuppressMessage("Microsoft.Usage", "CA2223:MembersShouldDifferByMoreThanReturnType")]
        public TResponse ExecuteCustomMessage<TResponse>(IModbusMessage request)
            where TResponse : IModbusMessage, new()
        {
            return Transport.UnicastMessage<TResponse>(request);
        }

        private static void ValidateData<T>(string argumentName, T[] data, int maxDataLength)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length == 0 || data.Length > maxDataLength)
            {
                string msg = $"The length of argument {argumentName} must be between 1 and {maxDataLength} inclusive.";
                throw new ArgumentException(msg);
            }
        }

        private static void ValidateMaxData<T>(string argumentName, T[] data, int maxDataLength)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length > maxDataLength)
            {
                string msg = $"The length of argument {argumentName} must not be greater than {maxDataLength}.";
                throw new ArgumentException(msg);
            }
        }

        private static void ValidateNumberOfPoints(string argumentName, ushort numberOfPoints, ushort maxNumberOfPoints)
        {
            if (numberOfPoints < 1 || numberOfPoints > maxNumberOfPoints)
            {
                string msg = $"Argument {argumentName} must be between 1 and {maxNumberOfPoints} inclusive.";
                throw new ArgumentException(msg);
            }
        }

        private bool[] PerformReadDiscretes(ReadCoilsInputsRequest request)
        {
            ReadCoilsInputsResponse response = Transport.UnicastMessage<ReadCoilsInputsResponse>(request);
            return response.Data.Take(request.NumberOfPoints).ToArray();
        }


        private ushort[] PerformReadRegisters(ReadHoldingInputRegistersRequest request)
        {
            ReadHoldingInputRegistersResponse response =
                Transport.UnicastMessage<ReadHoldingInputRegistersResponse>(request);

            return response.Data.Take(request.NumberOfPoints).ToArray();
        }

       
        private ushort[] PerformReadRegisters(ReadWriteMultipleRegistersRequest request)
        {
            ReadHoldingInputRegistersResponse response =
                Transport.UnicastMessage<ReadHoldingInputRegistersResponse>(request);

            return response.Data.Take(request.ReadRequest.NumberOfPoints).ToArray();
        }

    }
}
