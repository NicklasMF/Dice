using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    DiceController diceController;
    public UIController uIController;
    public int diceType = 6;
    public int diceCount = 1;
    int diceCountMax = 2;
    bool premiumUser;

	void Awake() {
		//DeletePlayerPrefs();
        SetDefaultPlayerPrefs();
        diceController = GetComponent<DiceController>();
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        Application.targetFrameRate = 60;

    }

    void Start() {
        PrepareDice();
    }

    void SetDefaultPlayerPrefs() {
        if (PlayerPrefs.HasKey("diceCount")) {
            diceCount = PlayerPrefs.GetInt("diceCount");
        } else {
            SetDiceCount(1);
        }
        if (PlayerPrefs.HasKey("diceType")) {
            diceCount = PlayerPrefs.GetInt("diceType");
        } else {
            SetDiceType(6);
        }
    }

    void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

	public void PrepareDice() {
        diceController.SetDieReady(diceCount, diceType);
	}

    public void SetDiceCount(int _diceCount) {
        diceCount = _diceCount;
        GetComponent<DiceController>().SetDieReady(diceCount, diceType);
        PlayerPrefs.SetInt("diceCount", diceCount);
    }

    public void SetDiceCount(bool _higher) {
        if (_higher) {
            if (diceCount < diceCountMax) {
                SetDiceCount(diceCount+1);
            }
        } else {
            if (diceCount > 1) {
                SetDiceCount(diceCount - 1);
            }
        }
    }

    public void SetDiceType(int _diceType) {
        diceType = _diceType;
        GetComponent<DiceController>().SetDieReady(diceCount, diceType);
        PlayerPrefs.SetInt("diceType", diceType);
    }

    void FinishedRolling(int _sum) {
        string txt = "Du slog " + _sum + ". ";

		GetComponent<DiceController>().txtStatus.text = txt;	
	}

}
