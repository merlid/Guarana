using UnityEngine;
using TMPro;

public class CanvasConsole : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI outputTextbox;

	public static CanvasConsole instance;

	private void Awake(){
		instance = this;
	}

	public static void PrintOutput(string text){
		if(instance != null) instance.outputTextbox.text += "\n" + text;
	}

	public static void SetOutput(string text){
		if (instance != null) instance.outputTextbox.text = text;
	}

}
