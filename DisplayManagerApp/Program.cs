using System;
using NvAPIWrapper;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;

namespace DisplayManagerApp;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Hello Display!");
        try
        {
            // Initialize NVAPI
            // NvAPIWrapper.Native.GeneralApi.Initialize();
            // API.Initialize();
            Console.WriteLine("\u221A NVAPI initialized successfully.\n");
            // DisplayApi.GetAssociatedNvidiaDisplayName()
            // List GPUs
            Console.WriteLine("=== NVIDIA GPUs ===");
            foreach (var gpu in PhysicalGPU.GetPhysicalGPUs())
            {
                Console.WriteLine($"GPU: {gpu.FullName}, Bus: {gpu.BusInformation.BusId}, Driver: {gpu.DriverModel}");
                // List connected displays
                Console.WriteLine("=== Connected Displays ===");
                foreach (var device in gpu.GetConnectedDisplayDevices(NvAPIWrapper.Native.GPU.ConnectedIdsFlag.None))
                {
                    Console.WriteLine($"Device: {device.DisplayId} | GPU: {device.PhysicalGPU.FullName} | Connector: {device.ConnectionType}");
                    foreach (var display in Display.GetDisplays())
                    {
                        if (DisplayApi.GetDisplayIdByDisplayName(display.Name).Equals(device.DisplayId))
                        {
                            Console.WriteLine($"Display: {display.Name}");
                            break;
                        }
                    }
                }
            }
            Console.WriteLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u00D7 Error: {ex.Message}");
        }
    }
}