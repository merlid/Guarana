#region System namespaces
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

#region Plugins namespaces
using Rssdp;
#endregion

public class DeviceManager{
	public Dictionary<System.Uri, ContentProviderData> registeredProviders;
	public Dictionary<System.Uri, ContentProviderData> RegisteredProviders { get { return registeredProviders; } set { this.registeredProviders = value; }}

	public Dictionary<System.Uri, string> registeredDevices;

	public Dictionary<System.Uri, string> RegisteredDevices{ get {return registeredDevices;} }

	public DeviceManager(){}

	public async Task RegisterDevicesSSDP(){
		registeredDevices = new Dictionary<System.Uri, string>();
		registeredProviders = new Dictionary<System.Uri, ContentProviderData>();

		DiscovererSSDP ssdpDiscoverer = new DiscovererSSDP();

		await ssdpDiscoverer.SearchAndRegister(registeredDevices, RegisteredProviders);

		CanvasConsole.PrintOutput("Done!");
	}

}

public enum ContentProviderType{
	ANYTHING,
	GUARANA_MOCK,
	GINGA
}

public struct ContentProviderData{

	private string usn;

	public string Usn { get { return this.usn; } }

	private string nickname;

	public string Nickname { get { return this.nickname; } set {this.nickname = value;} }

	private System.Uri descriptionLocation;
	public System.Uri DescriptionLocation {get {return this.descriptionLocation; }}

	private ContentProviderType providerType;
	public ContentProviderType ProviderType { get { return this.providerType; } }

	/*private SsdpDevice device;
	public SsdpDevice Device { get { return this.device; } set{
		CanvasConsole.PrintOutput("ACHEI O DEVICE: " + value.FriendlyName); 
		this.device = value;
		CanvasConsole.PrintOutput("REGISTROU O DEVICE: " + this.device.FriendlyName);
	} }*/

	public ContentProviderData(System.Uri descriptionLocation, ContentProviderType providerType, string usn){
		this.descriptionLocation = descriptionLocation;
		this.providerType = providerType;
		this.usn = usn;
		this.nickname = "UNKNOWN";

		CanvasConsole.PrintOutput("Novo provider em :");
	}

	/*public void RegisterDevice(SsdpDevice device){
		CanvasConsole.PrintOutput("ACHEI O DEVICE: " + device.FriendlyName);
		this.device = device;
		CanvasConsole.PrintOutput("REGISTROU O DEVICE: " + this.device.FriendlyName);
	}*/
}