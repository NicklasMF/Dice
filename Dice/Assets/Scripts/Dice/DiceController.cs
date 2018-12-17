using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour {


    public LayerMask dieValueColliderLayer;
	[SerializeField] GameObject die6PlayerPrefab;
	[SerializeField] GameObject die20PlayerPrefab;
    [SerializeField] Transform dieStartPosition;
    float forceAmount = 15f;
    float angularForce = 30f;

    bool canRoll = false;
	bool hasRolled = false;
	bool diceStill = false;
	Vector3 rotationPoint;
    //List<GameObject> dice = new List<GameObject>();
    GameObject dice;

	public Text txtStatus;
	Vector3 velocity = Vector3.zero;
    int currentValue;

    void Update() {

        /*if (dice.Count == 0) {
			return;
		}*/

        if (dice == null) {
            return;
        }

		/* Dice rotates
		 * if (canRoll && !hasRolled) {
			dice.GetComponent<Rigidbody>().isKinematic = true;
			dice.GetComponent<Rigidbody>().useGravity = false;
			dice.transform.Rotate(Vector3.up * Time.deltaTime * 80);
			dice.transform.Rotate(rotationPoint * Time.deltaTime * 50);
		}*/

        if (diceStill) {
        //    foreach(GameObject die in dice) {
                RaycastHit hit;
                if (Physics.Raycast(dice.transform.position, Vector3.up, out hit, Mathf.Infinity, dieValueColliderLayer))
                {
                    currentValue = hit.collider.GetComponent<Die>().value;
                }
        //    }

        }

		if (hasRolled) {
			Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, dice.transform.position + new Vector3(0, 16f, 0), ref velocity, 0.7f);

			if (currentValue != 0 && diceStill) {
                //txtStatus.text = currentValue.ToString();
			}
			if (!diceStill && dice.GetComponent<Rigidbody>().IsSleeping()) {
				diceStill = true;
			}
		} else {
			Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, new Vector3(0, 16f, 0), ref velocity, 1.5f);
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
        /*foreach(GameObject die in dice) {
            Destroy(die);
        }
        dice.Clear();
        for (int i = 0; i < dieCount; i++) {*/
            //GameObject die;
        if (dice == null) {
            switch (dieNo) {
                case 6:
                    dice = Instantiate(die6PlayerPrefab);
                    //dice.Add(die);
                    break;
                case 20:
                    dice = Instantiate(die20PlayerPrefab);
                    //dice.Add(die);
                    break;
                default:
                    Debug.LogError("Wrong dieNo for die");
                    break;
            }
            //}
        }

        dice.transform.position = dieStartPosition.position;
        rotationPoint = (Random.Range(0, 1) == 0) ? Vector3.left : Vector3.right;
        canRoll = true;
        hasRolled = false;
        diceStill = false;
    }

	public void RollDice() {
		hasRolled = true;
        canRoll = false;
        GetComponent<AnalyticsController>().hits++;
        //foreach(GameObject die in dice) {
            dice.GetComponent<Rigidbody>().isKinematic = false;
            dice.GetComponent<Rigidbody>().useGravity = true;
            dice.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 3f, ForceMode.VelocityChange);
            dice.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(forceAmount - 3f, forceAmount + 3f), ForceMode.VelocityChange);
            dice.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * Random.Range(angularForce - 3f, angularForce + 13f), ForceMode.VelocityChange);
        //}
    }
}
