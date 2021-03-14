#region System namespaces
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
#endregion

#region Plugins namespaces
using Rssdp;
#endregion

public class DiscovererSSDP{

	private IEnumerable<DiscoveredSsdpDevice> discoveredSsdpDevices;

	public async Task SearchAndRegister(Dictionary<System.Uri, string> devicesLocations, Dictionary<System.Uri, ContentProviderData> registeredProviders){
		await SearchAndRegisterAllDevices(devicesLocations, registeredProviders);
	}

	public async Task SearchAndRegisterAllDevices(Dictionary<System.Uri, string> devicesLocations, Dictionary<System.Uri, ContentProviderData> registeredProviders){
		discoveredSsdpDevices = new List<DiscoveredSsdpDevice>();

		await SearchForAllDevices();

		foreach (var device in discoveredSsdpDevices)
		{
			await AckDevice(device, devicesLocations, registeredProviders);
		}
	}

	/*public async Task SearchAndRegisterGingaDevices(Dictionary<System.Uri, string> devicesLocations){
		discoveredSsdpDevices = new List<DiscoveredSsdpDevice>();

		await SearchForGingaDevices();

		foreach (var device in discoveredSsdpDevices)
		{
			await AckDevice(device, devicesLocations);
		}
	}*/

	private async Task SearchForAllDevices()
	{

		using (var deviceLocator = new SsdpDeviceLocator())
		{
			discoveredSsdpDevices = await deviceLocator.SearchAsync();
		}
	}

	private async Task SearchForGingaDevices()
	{

		using (var deviceLocator = new SsdpDeviceLocator())
		{
			discoveredSsdpDevices = await deviceLocator.SearchAsync("urn:schemas-sbtvd-org:service:GingaCCWebServices:1");
		}
	}

	async private Task AckDevice(DiscoveredSsdpDevice device, Dictionary<System.Uri, string> devicesLocations, Dictionary<System.Uri, ContentProviderData> registeredProviders){
		using (var deviceLocator = new SsdpDeviceLocator()){
			CanvasConsole.PrintOutput("Encontrei a USN: " + device.Usn);
			await RegisterProviderData(device, devicesLocations, registeredProviders);
		}
	}

	private async Task RegisterProviderData(DiscoveredSsdpDevice device, Dictionary<System.Uri, string> devicesLocations, Dictionary<System.Uri, ContentProviderData> registeredProviders){
		if(!registeredProviders.ContainsKey(device.DescriptionLocation)){
			ContentProviderData provider = new ContentProviderData(device.DescriptionLocation, ContentProviderType.ANYTHING, device.Usn);
			registeredProviders.Add(device.DescriptionLocation, provider);
			await RegisterDeviceInProvider(device, provider);
		}
	}

	private async Task RegisterDeviceInProvider(DiscoveredSsdpDevice device, ContentProviderData provider){
		try{
			var deviceInfo = await device.GetDeviceInfo();
			provider.Nickname = deviceInfo.FriendlyName;
			//provider.Device = temp;
		}
		catch (Exception e){
			string mesage = "It is not possible to connect with the device found at " + device.DescriptionLocation + "\nThe problem is related with the exception: " + e.Message;
			CanvasConsole.PrintOutput(mesage);
			CanvasConsole.WriteWarning(mesage);
		}
	}

	/*private async Task RegisterDeviceInformation(DiscoveredSsdpDevice device, Dictionary<System.Uri, string> devicesLocations, Dictionary<System.Uri, ContentProviderData> registeredProviders)
	{
		var fullDevice = await device.GetDeviceInfo();
		if (!devicesLocations.ContainsKey(device.DescriptionLocation))
		{
			devicesLocations.Add(device.DescriptionLocation, fullDevice.FriendlyName);
			CanvasConsole.PrintOutput(fullDevice.FriendlyName + " is a device device at " + device.DescriptionLocation);
		}
	}*/
}
