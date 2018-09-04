using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public float minScale = 0.2f;
    public float maxScale = 1.0f;
    public float zoomRate;

    public GameObject[] blockingElements;

    Vector3 panMapStartPosition;
    Vector3 panMouseStartPosition;

    const float maxPanDistanceX = 22.75f;
    const float maxPanDistanceY = 20f;

    private bool CanInteract
    {
        get
        {
            foreach (GameObject element in blockingElements)
            {
                if (element.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
            {
                panMapStartPosition = transform.position;
                panMouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 difference = mousePosition - panMouseStartPosition;
                transform.position = panMapStartPosition + difference;
            }

            if (Input.mouseScrollDelta.y != 0f)
            {
                Vector3 anchorWorldPositionBefore =
                    Input.mouseScrollDelta.y > 0 ?
                    Camera.main.ScreenToWorldPoint(Input.mousePosition) :
                    Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

                Vector3 anchorLocalPosition = transform.InverseTransformPoint(anchorWorldPositionBefore);

                float zoom = Mathf.Clamp(transform.localScale.x + Time.deltaTime * Input.mouseScrollDelta.y * zoomRate, minScale, maxScale);
                transform.localScale = new Vector3(zoom, zoom, zoom);

                Vector3 anchorWorldPositionAfter = transform.TransformPoint(anchorLocalPosition);

                transform.position += (anchorWorldPositionBefore - anchorWorldPositionAfter);
            }

            float zoomAmount = (transform.localScale.x - minScale) / (maxScale - minScale);
            Debug.Log(zoomAmount);
            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, -maxPanDistanceX * zoomAmount, maxPanDistanceX * zoomAmount),
                Mathf.Clamp(transform.localPosition.y, -maxPanDistanceY * zoomAmount, maxPanDistanceY * zoomAmount),
                transform.localPosition.z);
        }
        else
        {
            panMapStartPosition = transform.position;
            panMouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
