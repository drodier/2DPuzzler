using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public LineRenderer laserBeam;
    public float laserDistance = 100f;
    public float laserWidth = 0.1f;
    public LayerMask layerMask;
    public bool reflectRight = true;
    public GameObject reflectPrefab;
    private bool hasReflected = false;

    private Vector2 reflectStartPosition;
    private bool isReflecting = false;

    private void Start()
    {
        laserBeam.startWidth = laserWidth;
        laserBeam.endWidth = laserWidth;
        laserBeam.positionCount = 2;
        laserBeam.SetPosition(0, transform.position + transform.up * laserWidth / 2f);
    }

    private void Update()
    {
        // Check the direction of the object and update the reflectRight variable accordingly
        reflectRight = transform.localScale.x > 0;

        // Update the starting position of the laser beam based on the position and rotation of the object
        laserBeam.SetPosition(0, transform.position + transform.up * laserWidth / 2f);

        if (isReflecting)
        {
            Vector2 reflectDirection = Vector2.Reflect((reflectStartPosition - (Vector2)transform.position).normalized, reflectRight ? Vector2.left : Vector2.right);
            RaycastHit2D hit = Physics2D.Raycast(reflectStartPosition, reflectDirection, laserDistance, layerMask);

            if (hit)
            {
                if (hit.collider.CompareTag("Reflective"))
                {
                    reflectStartPosition = hit.collider.bounds.center;
                    laserBeam.SetPosition(1, hit.point + reflectDirection * laserWidth);

                    // Spawn a new laser beam at the point of reflection, with an upward direction
                    if (!hasReflected) {
                        GameObject newReflectBeam = Instantiate(reflectPrefab, hit.collider.bounds.center, Quaternion.identity);
                        LineRenderer newLaserBeam = newReflectBeam.GetComponent<LineRenderer>();
                        newLaserBeam.startWidth = laserWidth;
                        newLaserBeam.endWidth = laserWidth;
                        newLaserBeam.positionCount = 2;
                        newLaserBeam.SetPosition(0, hit.collider.bounds.center);
                        newLaserBeam.SetPosition(1, hit.collider.bounds.center + Vector3.up * laserDistance);
                        hasReflected = true;
                }
            }

                else
                {
                    laserBeam.SetPosition(1, hit.point);
                    isReflecting = false;
                }
            }
            else
            {
                laserBeam.SetPosition(1, reflectStartPosition + reflectDirection.normalized * laserDistance);
                isReflecting = false;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * laserWidth / 2f, reflectRight ? transform.right : -transform.right, laserDistance, layerMask);

            if (hit)
            {
                if (hit.collider.CompareTag("Reflective"))
                {
                    reflectStartPosition = hit.point;
                    isReflecting = true;
                    laserBeam.SetPosition(1, hit.point);
                }
                else
                {
                    laserBeam.SetPosition(1, hit.point);
                }
            }
            else
            {
                laserBeam.SetPosition(1, transform.position + transform.up * laserWidth / 2f + (reflectRight ? transform.right : -transform.right) * laserDistance);
            }
        }
    }
}