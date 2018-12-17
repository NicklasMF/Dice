using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {


    UIController uiController;
    public UnityEvent unityEvent;
    public bool chosen = false;
    public string groupName;
    Button button;

    List<ButtonScript> groupButtons = new List<ButtonScript>();

	// Use this for initialization
	void OnEnable () {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        button = GetComponent<Button>();
        ButtonScript[] buttonArray = FindObjectsOfType(typeof(ButtonScript)) as ButtonScript[];
        foreach(ButtonScript buttonScript in buttonArray) {
            if (buttonScript.groupName == groupName) {
                groupButtons.Add(buttonScript);
            }
        }

        button.onClick.AddListener(delegate{
            SetButton(true);
        });

        SetButton(chosen);
	}
	
    public void SetButton(bool _isSet) {
        chosen = _isSet;
        if (_isSet) {
            foreach(ButtonScript buttonScript in groupButtons) {
                if (buttonScript == this) {
                    continue;
                }
                buttonScript.SetButton(false);
            }
            GetComponent<Image>().color = uiController.buttonColor[1];
            unityEvent.Invoke();
        } else {
            GetComponent<Image>().color = uiController.buttonColor[0];
        }
    }

}
