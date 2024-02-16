using System.Collections;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    LineRenderer line;

    [Header("Helix line")]
    public int numberOfSegments = 20;
    public float movementSpeed = 10.0f;
    public float sideSpeed = 5;
    public float duration = 0.5f;
    public float segmentAngle = 15;

    public float threshold = 0.1f;

    [Header("Sinus line")]
    public float RotationDrag = 5;
    public float MovementSpeed = 0.3f;
    public float sineTotalDuration = 3.0f;
    public float distanceThreshold = 0.1f;
    public float scalar;

    [Header("Zigzag line")]
    public float zigzagTotalDuration = 5;
    public float zigzagMinAngle = 35;
    public float zigzagMaxAngle = 50;
    public float zigzagMovementSpeed = 5;
    public float zigzagMinLength = 2;
    public float zigzagMaxLength = 5;
    public float zigzagThreshold = 0.1f;
    public bool isZiggingRight = true;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        StartCoroutine(DrawZigzagCoroutine());
    }

    // TODO: Still too slow and time is not flexible enough. Try with fewer points and threshold
    IEnumerator DrawHelixCoroutine()
    {
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
        for (int i = 1; i < numberOfSegments; i++)
        {
            transform.position += transform.right * movementSpeed + (Vector3.right * sideSpeed);
            transform.Rotate(Vector3.forward, segmentAngle);
            line.positionCount++;
            line.SetPosition(i, transform.position);
            //yield return new WaitForSeconds(duration / (float)numberOfSegments);
            yield return null;
        }
    }

    IEnumerator DrawSineCoroutine()
    {
        float timeLeft = sineTotalDuration;
        Vector3 lastPosition = transform.position;
        float timePast = 0;

        line.positionCount = 1;
        line.SetPosition(0, transform.position);
        while (timeLeft > 0)
        {
            timePast += Time.deltaTime;
            timeLeft -= Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Sin(timePast * RotationDrag) * scalar);
            transform.position += transform.right.normalized * MovementSpeed * Time.deltaTime;
            if (Vector3.Distance(lastPosition, transform.position) > distanceThreshold)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, transform.position);
                lastPosition = transform.position;
            }
            yield return null;
        }
    }

    IEnumerator DrawZigzagCoroutine()
    {
        float timeLeft = zigzagTotalDuration;
        transform.Rotate(Vector3.forward, zigzagMaxAngle / 2);
        Vector3 lastCorner = transform.position;
        Vector3 lastPosition = transform.position;
        float currentLength = Random.Range(zigzagMinLength, zigzagMaxLength);

        line.positionCount = 1;
        line.SetPosition(0, transform.position);
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            transform.position += transform.right * zigzagMovementSpeed * Time.deltaTime;

            if (Vector3.Distance(lastCorner, transform.position) >= currentLength)
            {
                float directionScalar = (isZiggingRight) ? -1 : 1;
                transform.Rotate(Vector3.forward, Random.Range(zigzagMinAngle, zigzagMaxAngle) * directionScalar);

                isZiggingRight = !isZiggingRight;
                currentLength = Random.Range(zigzagMinLength, zigzagMaxLength);
                lastPosition = transform.position;
                lastCorner = transform.position;

                line.positionCount++;
                line.SetPosition(line.positionCount - 1, transform.position);
            }
            else if (Vector3.Distance(lastPosition, transform.position) >= zigzagThreshold)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, transform.position);

                lastPosition = transform.position;
            }

            yield return null;
        }
    }
}
