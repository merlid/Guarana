#region System namespaces
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

#region Plugins namespaces
using Rssdp;
#endregion

public class DeviceSearcher{
	/*private static async Task SearchForAllDevices(){
		CanvasConsole.SetOutput("Searching for all devices...");

		using (var deviceLocator = new SsdpDeviceLocator()){
			var results = await deviceLocator.SearchAsync();
			foreach (var device in results)
			{
				//await WriteOutDevices(device);
			}
		}
	}*/
	private async Task SearchForAllDevices(IEnumerable<DiscoveredSsdpDevice> discoveredSsdpDevices){
		CanvasConsole.SetOutput("Searching for all devices...");

		using (var deviceLocator = new SsdpDeviceLocator()){

			discoveredSsdpDevices = await deviceLocator.SearchAsync();

			//return await deviceLocator.SearchAsync();

			/*var results = await deviceLocator.SearchAsync();
			foreach (var device in results)
			{
				//await WriteOutDevices(device);
			}*/
		}
	}
}
