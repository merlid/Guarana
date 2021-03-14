#region System namespaces
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion


public class DeviceManager{
	public Dictionary<System.Uri, string> registeredDevices;

	public Dictionary<System.Uri, string> RegisteredDevices{ get {return registeredDevices;	} }

	public DeviceManager(){}

	public async Task RegisterDevicesSSDP(){
		registeredDevices = new Dictionary<System.Uri, string>();

		DiscovererSSDP ssdpDiscoverer = new DiscovererSSDP();

		await ssdpDiscoverer.SearchAndRegister(registeredDevices);

		CanvasConsole.PrintOutput("Done!");
	}

}
