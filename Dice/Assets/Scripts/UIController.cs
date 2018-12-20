using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Color[] buttonColor;

    [SerializeField] Button showMenuBtn;

    public GameObject Menu;

    void Start() {
        showMenuBtn.onClick.AddListener(delegate {
            ShowMenu(true);
        });
    }

    public void ShowMenu(bool _show) {
        Menu.SetActive(_show);
    }

}
