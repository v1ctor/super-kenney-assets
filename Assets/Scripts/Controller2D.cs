using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    struct RaycastOrigins
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above;
        public bool below;
        public bool left;
        public bool right;

        public void Reset()
        {
            above = false;
            below = false;
            right = false;
            left = false;
        }
    }


    const float skinWidth = 0.015f;

    public float raycastSpacing = 0.1f;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private int verticalRayCount;
    private int horizontalRayCount;

    private BoxCollider2D boxCollider;

    public LayerMask collisionMask;

    public CollisionInfo collisions;

    [HideInInspector]
    public Vector2 velocity;


    private RaycastOrigins raycastOrigins;


    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector2 direction)
    {
        UpdateRaycastOrigins();

        collisions.Reset();

        if (Mathf.Abs(direction.x) > float.Epsilon)
        {
            HorizontalCollisions(ref direction);
        }
        if (Mathf.Abs(direction.y) > float.Epsilon)
        {
            VerticalCollisions(ref direction);
        }

        transform.Translate(direction);

        if (Time.deltaTime > 0f)
        {
            velocity = direction / Time.deltaTime;
        }
    }

    void HorizontalCollisions(ref Vector2 direction)
    {
        bool isMovingLeft = direction.x < 0.0;
        float rayLength = Mathf.Abs(direction.x) + skinWidth;
        Vector2 rayDirection = Mathf.Sign(direction.x) * Vector2.right;
        Vector2 initialRayOrigin = isMovingLeft ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = new Vector2(initialRayOrigin.x, initialRayOrigin.y + i * horizontalRaySpacing);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);

            if (hit && !hit.collider.isTrigger)
            {
                direction.x = hit.point.x - rayOrigin.x;
                rayLength = Mathf.Abs(direction.x);

                if (isMovingLeft)
                {
                    direction.x += skinWidth;
                    collisions.left = true;
                }
                else
                {
                    direction.x -= skinWidth;
                    collisions.right = false;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector2 direction)
    {
        bool isMovingDown = direction.y < 0.0;
        float rayLength = Mathf.Abs(direction.y) + skinWidth;

        Vector2 rayDirection = Vector2.up * Mathf.Sign(direction.y);
        Vector2 initialRayOrigin = isMovingDown ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

        initialRayOrigin.x += direction.x;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = new Vector2(initialRayOrigin.x + i * verticalRaySpacing, initialRayOrigin.y);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);

            if (hit && !hit.collider.isTrigger)
            {
                direction.y = hit.point.y - rayOrigin.y;
                rayLength = Mathf.Abs(direction.y);

                if (isMovingDown)
                {
                    direction.y += skinWidth;
                    collisions.below = true;
                }
                else
                {
                    direction.y -= skinWidth;
                    collisions.above = true;
                }
            }
        }
    }

    private void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = (int)Mathf.Ceil(bounds.size.y / raycastSpacing);
        verticalRayCount = (int)Mathf.Ceil(bounds.size.x / raycastSpacing);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

}