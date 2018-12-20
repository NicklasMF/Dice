using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsController : MonoBehaviour {

    public int hits = 0;
    public float time = 0;

	void Start () {
        hits = 0;
        time = 0;
	}
	
	void Update () {
        time += Time.deltaTime;
	}

    void OnApplicationPause(bool pause) {
        if (pause) {
            Analytics.CustomEvent("OnPause", new Dictionary<string, object> {
                {"hits", hits },
                {"time", time }
            });
        } else {
            hits = 0;
            time = 0;
        }
    }
}
