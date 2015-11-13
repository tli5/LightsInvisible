using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using WindowsInput;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

    // Private fields to store the models
    private GameObject model_1;
    private GameObject model_2;
	private GameObject btn_1;
	private GameObject btn_2;
	private int count;

    /// Called when the scene is loaded
    void Start() {

        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i) {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);

        }
		count = 0;
    }
 
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
		//Debug.Log(vb.VirtualButtonName);
		count++;
		Debug.Log(vb.VirtualButtonName + " Button pressed!");
		print (vb.VirtualButtonName + "press count:"+count );
		switch(vb.VirtualButtonName) {
		case "button_up":
			Debug.Log ("up pressed");
			InputSimulator.SimulateKeyDown(VirtualKeyCode.UP);
            break;
		case "button_down":
			Debug.Log ("down pressed");
			InputSimulator.SimulateKeyDown(VirtualKeyCode.DOWN);
            break;
		case "button_left":
			Debug.Log ("left pressed");
			InputSimulator.SimulateKeyDown(VirtualKeyCode.LEFT);
			break;
		case "button_right":
			Debug.Log ("right pressed");
			InputSimulator.SimulateKeyDown(VirtualKeyCode.RIGHT);
			break;
        }
        
    }

    /// Called when the virtual button has just been released:
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) { 
		Debug.Log(vb.VirtualButtonName + "Button released!");
		print (vb.VirtualButtonName + "release count:"+count );
	}
}