using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    DiceController diceController;
    public int diceType = 6;
    public int diceCount = 1;


	void Awake() {
		//DeletePlayerPrefs();
        SetDefaultPlayerPrefs();
        diceController = GetComponent<DiceController>();
        Application.targetFrameRate = 60;
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
        diceController.SetDieReady(diceCount, diceType);
	}

    public void SetDiceCount(int _diceCount) {
        diceCount = _diceCount;
    }

    public void SetDiceType() {

    }

	void FinishedRolling(int _sum) {
        string txt = "Du slog " + _sum + ". ";

		GetComponent<DiceController>().txtStatus.text = txt;	
	}

}
