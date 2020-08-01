using System;
using System.Collections.Generic;
using System.Linq;

namespace BitVm.Lib
{
    public class MemoryMapper
    {
        private static List<MemoryRegion> regions = new List<MemoryRegion>();

        public static void Map(IDevice device, ushort start, ushort end, bool remap = true)
        {
            var region = new MemoryRegion(device, start, end, remap);
            regions.Add(region);
        }

        public static MemoryRegion FindRegion(ushort address)
        {
            var region = regions.Where(r => address >= r.Start && address <= r.End);
            if (!region.Any())
            {
                throw new Exception("No memory region found for address " + address);
            }
            return region.First();
        }

        public static ushort GetUInt16(ushort address, CPU cpu)
        {
            var region = FindRegion(address);
            var finalAddress = region.Remap
              ? address - region.Start
              : address;

            return region.Device.GetUInt16((ushort)finalAddress, cpu);
        }

        public static byte GetUInt8(ushort address, CPU cpu)
        {
            var region = FindRegion(address);
            var finalAddress = region.Remap
              ? address - region.Start
              : address;

            return region.Device.GetUInt8((ushort)finalAddress,cpu);
        }

        public static void SetUInt16(ushort address, ushort value, CPU cpu)
        {
            var region = FindRegion(address);
            var finalAddress = region.Remap
              ? address - region.Start
              : address;

            region.Device.SetUInt16((ushort)finalAddress, value, cpu);
        }

        public static void SetUInt8(ushort address, ushort value, CPU cpu)
        {
            var region = FindRegion(address);
            var finalAddress = region.Remap
              ? address - region.Start
              : address;

            region.Device.SetUInt8((ushort)finalAddress, value, cpu);
        }
    }
}