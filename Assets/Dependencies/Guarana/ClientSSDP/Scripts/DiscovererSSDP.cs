#region System namespaces
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

#region Plugins namespaces
using Rssdp;
#endregion

public class DiscovererSSDP{

	private IEnumerable<DiscoveredSsdpDevice> discoveredSsdpDevices;

	public async Task SearchAndRegister(Dictionary<System.Uri, string> devicesLocations){
		discoveredSsdpDevices = new List<DiscoveredSsdpDevice>();

		await SearchForAllDevices();

		foreach (var device in discoveredSsdpDevices)
		{
			await AckDevice(device, devicesLocations);
		}
	}

	private async Task SearchForAllDevices(){

		using (var deviceLocator = new SsdpDeviceLocator()){
			discoveredSsdpDevices = await deviceLocator.SearchAsync();
		}
	}

	async private  Task AckDevice(DiscoveredSsdpDevice device, Dictionary<System.Uri, string> devicesLocations){
		using (var deviceLocator = new SsdpDeviceLocator()){
			try{
				await RegisterDeviceInformation(device, devicesLocations);
			}
			catch (Exception e){
				CanvasConsole.PrintOutput("It is not possible to connect with the device found at " + device.DescriptionLocation);
				CanvasConsole.PrintOutput("The problem is related with the exception: " + e.Message + "\n");
			}
		}
	}

	private async Task RegisterDeviceInformation(DiscoveredSsdpDevice device, Dictionary<System.Uri, string> devicesLocations)
	{
		var fullDevice = await device.GetDeviceInfo();
		if (!devicesLocations.ContainsKey(device.DescriptionLocation))
		{
			devicesLocations.Add(device.DescriptionLocation, fullDevice.FriendlyName);
			CanvasConsole.PrintOutput(fullDevice.FriendlyName + " is a device device at " + device.DescriptionLocation);
		}
	}
}
