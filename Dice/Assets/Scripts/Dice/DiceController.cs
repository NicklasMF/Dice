using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour {


    public LayerMask dieValueColliderLayer;
	[SerializeField] GameObject die6PlayerPrefab;
	[SerializeField] GameObject die20PlayerPrefab;
    [SerializeField] Transform dieStartPositionParent;
    float forceAmount = 15f;
    float angularForce = 30f;

    bool canRoll = false;
	bool hasRolled = false;
	bool diceStill = false;
	Vector3 rotationPoint;
    List<GameObject> dice = new List<GameObject>();

	public Text txtStatus;
	Vector3 velocity = Vector3.zero;
    int currentValue;

    void Update() {

        if (dice.Count == 0) {
			return;
		}

		/* Dice rotates
		 * if (canRoll && !hasRolled) {
			dice.GetComponent<Rigidbody>().isKinematic = true;
			dice.GetComponent<Rigidbody>().useGravity = false;
			dice.transform.Rotate(Vector3.up * Time.deltaTime * 80);
			dice.transform.Rotate(rotationPoint * Time.deltaTime * 50);
		}*/

        if (diceStill && currentValue == 0) {
            currentValue = 0;
            foreach(GameObject die in dice) {
                RaycastHit hit;
                if (Physics.Raycast(die.transform.position, Vector3.up, out hit, Mathf.Infinity, dieValueColliderLayer))
                {
                    currentValue += hit.collider.GetComponent<Die>().value;
                }
            }
            print(currentValue);
        }

		if (hasRolled) {
            if (!diceStill) {
                bool isDiceStill = true;
                foreach (GameObject die in dice) {
                    if (!die.GetComponent<Rigidbody>().IsSleeping()) {
                        isDiceStill = false;
                        break;
                    }
                }
                if (isDiceStill) {
                    diceStill = true;
                }
            }
        }
    }

    public void ScreenTapped() {
        if (canRoll) {
            RollDice();
        } else if (diceStill) {
            SetDieReady(GetComponent<GameController>().diceCount, GetComponent<GameController>().diceType);
        }
    }

    public void SetDieReady(int dieCount, int dieNo)
    {
        foreach(GameObject die in dice) {
            Destroy(die);
        }
        dice.Clear();
        for (int i = 0; i < dieCount; i++) {
            GameObject die;
            switch (dieNo) {
                case 6:
                    die = Instantiate(die6PlayerPrefab);
                    die.transform.position = dieStartPositionParent.GetChild(i).position;
                    dice.Add(die);
                    break;
                case 20:
                    die = Instantiate(die20PlayerPrefab);
                    die.transform.position = dieStartPositionParent.GetChild(i).position;
                    dice.Add(die);
                    break;
                default:
                    Debug.LogError("Wrong dieNo for die");
                    break;
            }
        }

        rotationPoint = (Random.Range(0, 1) == 0) ? Vector3.left : Vector3.right;
        canRoll = true;
        hasRolled = false;
        diceStill = false;
        currentValue = 0;
        Camera.main.GetComponent<CameraController>().targets = dice;
    }

	public void RollDice() {
		hasRolled = true;
        canRoll = false;
        GetComponent<AnalyticsController>().hits++;
        foreach(GameObject die in dice) {
            die.GetComponent<Rigidbody>().isKinematic = false;
            die.GetComponent<Rigidbody>().useGravity = true;
            die.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 3f, ForceMode.VelocityChange);
            die.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(forceAmount - 3f, forceAmount + 3f), ForceMode.VelocityChange);
            die.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * Random.Range(angularForce - 3f, angularForce + 13f), ForceMode.VelocityChange);
        }
    }
}
