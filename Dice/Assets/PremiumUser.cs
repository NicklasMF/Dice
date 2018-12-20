using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremiumUser : MonoBehaviour {

    public Text priceTxt;
    [SerializeField] Button buyBtn;
    [SerializeField] Button backBtn;

    void Start() {
        buyBtn.onClick.AddListener(delegate {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<IAPController>().BuyPremiumUser();
        });
        backBtn.onClick.AddListener(delegate {
            gameObject.SetActive(false);
        });
    }

}
