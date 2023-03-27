using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public LineRenderer laserBeam;
    public float laserDistance = 100f;
    public float laserWidth = 0.1f;
    public LayerMask layerMask;
    public bool reflectRight = true;

    public Vector2 reflectStartPosition; // Store the starting position of the current reflection
    public bool isReflecting = false; // Track if the laser beam is currently reflecting

    private void Start()
    {
        // Set up the Line Renderer
        laserBeam.startWidth = laserWidth;
        laserBeam.endWidth = laserWidth;
        laserBeam.positionCount = 2;
        laserBeam.SetPosition(0, transform.position);
    }

    private void Update()
    {
        if (isReflecting)
        {
            // If the laser beam is reflecting, calculate the reflection direction from the previous hit point
            Vector2 reflectDirection = Vector2.Reflect((reflectStartPosition - (Vector2)transform.position).normalized, reflectRight ? Vector2.left : Vector2.right);
            RaycastHit2D hit = Physics2D.Raycast(reflectStartPosition, reflectDirection, laserDistance, layerMask);

            if (hit)
            {
                // If the raycast hit another Reflective object, continue reflecting the laser beam
                if (hit.collider.CompareTag("Reflective"))
                {
                    reflectStartPosition = hit.point;
                    laserBeam.SetPosition(1, hit.point + reflectDirection * laserWidth);
                }
                else
                {
                    // If the raycast hit a non-Reflective object, stop reflecting the laser beam
                    laserBeam.SetPosition(1, hit.point);
                    isReflecting = false;
                }
            }
            else
            {
                // If the raycast didn't hit anything, extend the laser beam to its maximum distance
                laserBeam.SetPosition(1, reflectStartPosition + reflectDirection.normalized * laserDistance);
                isReflecting = false;
            }
        }
        else
        {
            // Cast a ray to determine the end point of the laser beam
            RaycastHit2D hit = Physics2D.Raycast(transform.position, reflectRight ? Vector2.right : Vector2.left, laserDistance, layerMask);

            if (hit)
            {
                // If the raycast hit a Reflective object, start reflecting the laser beam
                if (hit.collider.CompareTag("Reflective"))
                {
                    reflectStartPosition = hit.point;
                    isReflecting = true;
                    laserBeam.SetPosition(1, hit.point);
                }
                else
                {
                    // If the raycast hit a non-Reflective object, stop the laser beam
                    laserBeam.SetPosition(1, hit.point);
                }
            }
            else
            {
                // If the raycast didn't hit anything, extend the laser beam to its maximum distance
                laserBeam.SetPosition(1, transform.position + (reflectRight ? Vector3.right : Vector3.left) * laserDistance);
            }
        }
    }
}