using System;
namespace BitVm.Lib.Devices
{
    public class ScreenDevice : IDevice
    {
        public IDevice Create(int size)
        {
            return new ScreenDevice();
        }

        public ushort GetUInt16(ushort address, CPU cpu)
        {
            return 0;
        }

        public byte GetUInt8(ushort address, CPU cpu)
        {
            return 0;
        }

        public void SetUInt16(ushort address, ushort data, CPU cpu)
        {
            var command = (data & 0xff00) >> 8;
            var characterValue = data & 0x00ff;

            if (command == 0xff)
            {
                eraseScreen();
            }

            var character = (char)characterValue;
            Console.Write(character);
        }

        public void SetUInt8(ushort address, ushort value, CPU cpu)
        {
            //nothing to do
        }

        void eraseScreen()
        {
            Console.Clear();
        }
    }
}