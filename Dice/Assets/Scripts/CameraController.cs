using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public List<GameObject> targets = new List<GameObject>();

    [SerializeField] float boundingBoxPadding = 2f;
    [SerializeField] float minimumOrthographicSize = 8f;
    [SerializeField] float zoomSpeed = 20f;

    new Camera camera;

    void Awake() {
        camera = GetComponent<Camera>();
    }

    void LateUpdate() {
        if (targets.Count == 0) {
            return;
        }
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
        camera.fieldOfView = CalculateOrthographicSize(boundingBox);
    }

    Rect CalculateTargetsBoundingBox() {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (GameObject target in targets) {
            Vector3 position = target.transform.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.z);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.z);
        }

        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }

    Vector3 CalculateCameraPosition(Rect boundingBox) {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, camera.transform.position.y, boundingBoxCenter.y);
    }

    float CalculateOrthographicSize(Rect boundingBox) {
        float orthographicSize = camera.fieldOfView;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y) {
            orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
        } else {
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;
        }
        orthographicSize = orthographicSize / Mathf.Tan((Camera.main.fieldOfView * Mathf.Deg2Rad) / 2f);

        return Mathf.Clamp(Mathf.Lerp(camera.fieldOfView, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }

}
