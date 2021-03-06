﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.NativePlugins;

public class SettingsController : MonoBehaviour {

    [SerializeField] Color selectedColor;
    ColorBlock selectedColorBlock;
    ColorBlock notSelectedColorBlock;

    [SerializeField] Button backBtn;

    [SerializeField] Button dieTypeD6;
    [SerializeField] Button dieTypeD20;

    [SerializeField] Button dieCountLess;
    [SerializeField] Button dieCountMore;
    [SerializeField] Text dieCountTxt;

    public PremiumUser premiumUser;
    [SerializeField] Button premiumUserBtn;
    [SerializeField] GameObject premiumUserOverlay;

    int dieCount;
    int dieCountMax;
    int dieType;

    GameController controller;
    UIController uIController;

    void Start() {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        premiumUser.gameObject.SetActive(false);

        selectedColorBlock = notSelectedColorBlock = dieTypeD6.colors;
        selectedColorBlock.normalColor = selectedColor;
        notSelectedColorBlock.normalColor = dieTypeD6.colors.normalColor;
        dieTypeD6.onClick.AddListener(delegate{
            ClickDie(6);
        });
        dieTypeD20.onClick.AddListener(delegate {
            ClickDie(20);
        });

        dieCountLess.onClick.AddListener(delegate {
            ClickCount(false);
        });
        dieCountMore.onClick.AddListener(delegate {
            ClickCount(true);
        });

        backBtn.onClick.AddListener(delegate {
            uIController.ShowMenu(false);
        });
        premiumUserBtn.onClick.AddListener(delegate {
            premiumUser.gameObject.SetActive(true);
        });
        premiumUserOverlay.GetComponentInChildren<Button>().onClick.AddListener(delegate {
            premiumUser.gameObject.SetActive(true);
        });
    }

    public void SetPremiumUser() {
        premiumUserOverlay.SetActive(false);
    }

    public void SetNoPremiumUser(BillingProduct _product) {
        premiumUser.priceTxt.text = "Price:" + _product.Price.ToString();
        premiumUserOverlay.SetActive(true);
    }

    void ClickDie(int _count) {
        switch(_count) {
            case 6:
                controller.SetDiceType(6);
                dieType = 6;
                dieTypeD6.colors = selectedColorBlock;
                dieTypeD20.colors = notSelectedColorBlock;
                break;
            case 20:
                controller.SetDiceType(20);
                dieType = 20;
                dieTypeD6.colors = notSelectedColorBlock;
                dieTypeD20.colors = selectedColorBlock;
                break;
            default:
                break;
        }
    }

    void ClickCount(bool _more) {
        controller.SetDiceCount(_more);
        dieCount = controller.diceCount;
        dieCountTxt.text = dieCount.ToString();
    }


}
