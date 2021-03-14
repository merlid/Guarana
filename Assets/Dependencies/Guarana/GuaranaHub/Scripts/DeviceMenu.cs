#region System namespaces
using System.Collections.Generic;
#endregion

#region Unity namespaces
using UnityEngine;
using TMPro;
#endregion

public class DeviceMenu : MonoBehaviour{

	[SerializeField]
	private GameObject buttonPrefab;

	[SerializeField]
	private GameObject buttonsContainer;

	private DeviceManager deviceManager;
	public Dictionary<System.Uri, string> registeredDevices;

	async private void Start(){

		EmptyPanel();

		deviceManager = new DeviceManager();

		await deviceManager.RegisterDevicesSSDP();

		CanvasConsole.PrintOutput(deviceManager.registeredDevices.Count + "");

		foreach (KeyValuePair<System.Uri, string> deviceEntry in deviceManager.registeredDevices){
			GameObject button = Instantiate(buttonPrefab, buttonsContainer.transform);

			button.name = deviceEntry.Key.ToString();

			button.GetComponentInChildren<TextMeshProUGUI>().text = deviceEntry.Value;
		}
	}

	private void EmptyPanel(){
		int childCount = buttonsContainer.transform.childCount;
		if(childCount > 0){
			for(int i =0; i< childCount; i++){
				Destroy(buttonsContainer.transform.GetChild(i).gameObject);
			}
		}
	}

	
}