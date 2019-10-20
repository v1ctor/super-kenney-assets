using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform focusObject;

    // TODO camera controller always focus on a center which is incorrect
    public Vector2 focusAreaSize;
    public float smoothingRate = 0.1f;

    private FocusArea focusArea;

    void Start()
    {
        focusArea = new FocusArea(focusObject.position, focusAreaSize);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        focusArea.Update(focusObject.position);

        float x = Mathf.Lerp(transform.position.x, focusArea.center.x, smoothingRate);
        float y = Mathf.Lerp(transform.position.y, focusArea.center.y, smoothingRate);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    struct FocusArea
    {
        private float top;
        private float bottom;
        private float left;
        private float right;

        public Vector2 center;

        public FocusArea(Vector3 position, Vector2 size)
        {
            top = position.y + size.y / 2f;
            bottom = position.y - size.y / 2f;
            left = position.x - size.x / 2f;
            right = position.x + size.x / 2f;

            center = new Vector2((left + right) / 2f, (top + bottom) / 2f);
        }

        public void Update(Vector3 position)
        {
            float shiftX = 0f;
            if (position.x < left)
            {
                shiftX = position.x - left;
            }
            else if (position.x > right)
            {
                shiftX = position.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0f;
            if (position.y < bottom)
            {
                shiftY = position.y - bottom;
            }
            else if (position.y > top)
            {
                shiftY = position.y - top;
            }

            top += shiftY;
            bottom += shiftY;

            center = new Vector2((left + right) / 2f, (top + bottom) / 2f);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireCube(focusArea.center, focusAreaSize);
    }
}