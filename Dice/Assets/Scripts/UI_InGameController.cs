using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameController : MonoBehaviour {

    [SerializeField] Button rollBtn;
    [SerializeField] Text statusTxt;

    DiceController diceController;

    private void Start()
    {
        diceController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DiceController>();
        rollBtn.onClick.AddListener(delegate {
            diceController.ScreenTapped();
            statusTxt.gameObject.SetActive(false);
        });
    }

}
