﻿using System.Diagnostics.CodeAnalysis;


namespace NModbus.Device
{

    /// <summary>
    ///    Modbus IP master device.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
    internal class ModbusIpMaster : ModbusMaster
    {
        /// <summary>
        ///     Modbus IP master device.
        /// </summary>
        /// <param name="transport">Transport used by this master.</param>
        public ModbusIpMaster(IModbusTransport transport)
            : base(transport)
        {
        }

        /// <summary>
        ///    Reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status.</returns>
        public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
        {
            return base.ReadCoils(Modbus.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
        }

        

        /// <summary>
        ///    Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
        {
            return base.ReadInputs(Modbus.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
        }

        

        /// <summary>
        ///    Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return base.ReadHoldingRegisters(Modbus.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
        }

       
        /// <summary>
        ///    Reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return base.ReadInputRegisters(Modbus.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
        }

        

        /// <summary>
        ///    Writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(ushort coilAddress, bool value)
        {
            base.WriteSingleCoil(Modbus.DefaultIpSlaveUnitId, coilAddress, value);
        }

        

        /// <summary>
        ///     Write a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(ushort registerAddress, ushort value)
        {
            base.WriteSingleRegister(Modbus.DefaultIpSlaveUnitId, registerAddress, value);
        }

        

        /// <summary>
        ///     Write a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
        {
            base.WriteMultipleRegisters(Modbus.DefaultIpSlaveUnitId, startAddress, data);
        }

       

        /// <summary>
        ///     Force each coil in a sequence of coils to a provided value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(ushort startAddress, bool[] data)
        {
            base.WriteMultipleCoils(Modbus.DefaultIpSlaveUnitId, startAddress, data);
        }

        

        /// <summary>
        ///     Performs a combination of one read operation and one write operation in a single MODBUS transaction.
        ///     The write operation is performed before the read.
        ///     Message uses default TCP slave id of 0.
        /// </summary>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        public ushort[] ReadWriteMultipleRegisters(
            ushort startReadAddress,
            ushort numberOfPointsToRead,
            ushort startWriteAddress,
            ushort[] writeData)
        {
            return base.ReadWriteMultipleRegisters(
                Modbus.DefaultIpSlaveUnitId,
                startReadAddress,
                numberOfPointsToRead,
                startWriteAddress,
                writeData);
        }

       
    }
}
