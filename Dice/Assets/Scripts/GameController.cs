using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    DiceController diceController;
    public int diceNumber = 6;

	void Awake() {
		//DeletePlayerPrefs();
        SetDefaultPlayerPrefs();
        diceController = GetComponent<DiceController>();
        
		//DiceController.HasRolledDie += FinishedRolling;
    }

    private void Start()
    {
        PrepareDice();
    }

    void SetDefaultPlayerPrefs() {
    }

	void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

	public void PrepareDice() {
        diceController.SetDieReady(diceNumber);
	}


	void FinishedRolling(int sum) {
		string txt = "Du slog " + sum + ". ";

		GetComponent<DiceController>().txtStatus.text = txt;	
	}

}
