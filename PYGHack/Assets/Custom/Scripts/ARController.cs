using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    private List<DetectedPlane> trackedPlanes = new List<DetectedPlane>();

    [SerializeField]
    private GameObject gridPrefab;

    [SerializeField]
    private GameObject portal;

    [SerializeField]
    private GameObject ARCamera;

    // Update is called once per frame
    void Update()
    {
        // Check AR Core Session Status
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // Fills the list with newly detected planes.
        Session.GetTrackables<DetectedPlane>(trackedPlanes, TrackableQueryFilter.New);

        for (int i = 0; i < trackedPlanes.Count; i++)
        {
            // Make the grid object
            GameObject gridObject = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity, this.transform);

            // Set the position of the grid and change the attached mesh.
            gridObject.GetComponent<GridVisualizer>().Initialize(trackedPlanes[i]);
        }

        // Check if user has already touched or is touching
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Check if the user has touched anywhere with the tracked plane
        TrackableHit hit;
        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            // When detecting plane, spawn the door.
            portal.SetActive(true);

            // Create anchor where the portal should be
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // Set position of portal
            portal.transform.position = hit.Pose.position;
            portal.transform.rotation = hit.Pose.rotation;

            Vector3 ARCameraPosition = ARCamera.transform.position;

            // The portal should only rotate around y axis to face camera.
            ARCameraPosition.y = hit.Pose.position.y;

            portal.transform.LookAt(ARCameraPosition, portal.transform.up);

            // Anchor will keep updataing, so we set portal's parent to anchor so its rotation and positions is changing.
            portal.transform.parent = anchor.transform;
        }
    }
}
