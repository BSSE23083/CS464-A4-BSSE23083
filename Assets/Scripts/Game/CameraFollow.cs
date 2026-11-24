using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 13f, -6f);
    [SerializeField] float smooth = 6f;

    float shakeLeft;
    float shakeAmount;

    void Start()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        Vector3 wanted = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, wanted, smooth * Time.deltaTime);

        if (shakeLeft > 0)
        {
            shakeLeft -= Time.deltaTime;
            transform.position += Random.insideUnitSphere * shakeAmount;
        }
    }

    public void Shake(float amount, float time)
    {
        shakeAmount = amount;
        shakeLeft = time;
    }
}
