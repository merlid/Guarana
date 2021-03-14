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

	async private void Start(){

		EmptyPanel();

		deviceManager = new DeviceManager();

		await deviceManager.RegisterDevicesSSDP();

		CanvasConsole.PrintOutput(deviceManager.registeredProviders.Count + "");

		foreach (KeyValuePair<System.Uri, ContentProviderData> providerEntry in deviceManager.registeredProviders){
			GameObject button = Instantiate(buttonPrefab, buttonsContainer.transform);

			button.name = providerEntry.Key.ToString();

			if(providerEntry.Value.Nickname != "UNKNOWN"){
				button.GetComponentInChildren<TextMeshProUGUI>().text = providerEntry.Value.Nickname;
			}else{
				button.GetComponentInChildren<TextMeshProUGUI>().text = providerEntry.Value.Usn;
			}
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