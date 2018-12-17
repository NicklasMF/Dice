using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Color[] buttonColor;

    public GameObject Menu;

    public void ShowMenu(bool _show) {
        Menu.SetActive(_show);
    }

}
