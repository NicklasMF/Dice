using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class IAPController : MonoBehaviour {

    public bool isPremiumUser;
    BillingProduct premiumUserProduct;

    void Start() {
        RequestBillingProducts();
    }

    public void RequestBillingProducts() {
        NPBinding.Billing.RequestForBillingProducts(NPSettings.Billing.Products);

        // At this point you can display an activity indicator to inform user that task is in progress
    }

    void OnEnable() {
        // Register for callbacks
        Billing.DidFinishRequestForBillingProductsEvent += OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent += OnDidFinishTransaction;

        // For receiving restored transactions.
        Billing.DidFinishRestoringPurchasesEvent += OnDidFinishRestoringPurchases;
    }

    void OnDisable() {
        // Deregister for callbacks
        Billing.DidFinishRequestForBillingProductsEvent -= OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent -= OnDidFinishTransaction;
        Billing.DidFinishRestoringPurchasesEvent -= OnDidFinishRestoringPurchases;
    }

    void OnDidFinishProductsRequest(BillingProduct[] _regProductsList, string _error) {
        // Hide activity indicator

        // Handle response
        if (_error != null) {
            // Something went wrong
        } else {
            // Inject code to display received products
            premiumUserProduct = _regProductsList[0];
            print(premiumUserProduct.Name);
            print(premiumUserProduct.Price);
            SetupPremiumUser(premiumUserProduct);
        }
    }

    void SetupPremiumUser(BillingProduct _premiumUser) {
        isPremiumUser = NPBinding.Billing.IsProductPurchased(_premiumUser);
        
        GetComponent<GameController>().uIController.Menu.GetComponent<SettingsController>().premiumUser.priceTxt.text = "Price:" + premiumUserProduct.Price.ToString();
    }

    public void BuyItem(BillingProduct _product) {
        if (NPBinding.Billing.IsProductPurchased(_product)) {
            // Show alert message that item is already purchased
            print("Er allerede købt");
            return;
        }

        // Call method to make purchase
        NPBinding.Billing.BuyProduct(_product);

        // At this point you can display an activity indicator to inform user that task is in progress
    }

    public void BuyPremiumUser() {
        if (NPBinding.Billing.IsProductPurchased(premiumUserProduct)) {
            // Show alert message that item is already purchased
            print("Er allerede købt");

            return;
        }

        // Call method to make purchase
        NPBinding.Billing.BuyProduct(premiumUserProduct);

        // At this point you can display an activity indicator to inform user that task is in progress
    }

    void OnDidFinishTransaction(BillingTransaction _transaction) {
        if (_transaction != null) {

            if (_transaction.VerificationState == eBillingTransactionVerificationState.SUCCESS) {
                if (_transaction.TransactionState == eBillingTransactionState.PURCHASED) {
                    // Your code to handle purchased products
                }
            }
        }
    }

    void RestorePurchases() {
        NPBinding.Billing.RestorePurchases();
    }

    void OnDidFinishRestoringPurchases(BillingTransaction[] _transactions, string _error) {
        Debug.Log(string.Format("Received restore purchases response. Error = {0}.", _error));

        if (_transactions != null) {
            Debug.Log(string.Format("Count of transaction information received = {0}.", _transactions.Length));

            foreach (BillingTransaction _currentTransaction in _transactions) {
                Debug.Log("Product Identifier = " + _currentTransaction.ProductIdentifier);
                Debug.Log("Transaction State = " + _currentTransaction.TransactionState);
                Debug.Log("Verification State = " + _currentTransaction.VerificationState);
                Debug.Log("Transaction Date[UTC] = " + _currentTransaction.TransactionDateUTC);
                Debug.Log("Transaction Date[Local] = " + _currentTransaction.TransactionDateLocal);
                Debug.Log("Transaction Identifier = " + _currentTransaction.TransactionIdentifier);
                Debug.Log("Transaction Receipt = " + _currentTransaction.TransactionReceipt);
                Debug.Log("Error = " + _currentTransaction.Error);
            }
        }
    }

}
