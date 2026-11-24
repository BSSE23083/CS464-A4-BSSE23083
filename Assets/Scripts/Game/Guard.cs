using UnityEngine;

// Walks between two points. If the player is inside the cone and nothing
// is in the way, the guard stops, turns to them and zaps them.
public class Guard : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 2f;
    [SerializeField] float waitTime = 1f;

    [Header("Seeing")]
    [SerializeField] float viewRange = 7f;
    [SerializeField] float viewAngle = 70f;
    [SerializeField] int damage = 34;
    [SerializeField] AudioClip alertClip;
    [SerializeField] VisionCone cone;

    Health player;
    int next;
    float waitUntil;
    bool sawPlayer;

    public float ViewRange => viewRange;
    public float ViewAngle => viewAngle;

    void Start()
    {
        player = FindFirstObjectByType<Health>();
    }

    void Update()
    {
        bool sees = CanSeePlayer();

        if (sees && !sawPlayer && alertClip != null)
            SfxPlayer.Instance.Play(alertClip);
        sawPlayer = sees;
        if (cone != null) cone.SetAlert(sees);

        if (sees)
        {
            Vector3 to = player.transform.position - transform.position;
            to.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(to), 360f * Time.deltaTime);
            player.TakeDamage(damage);
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        if (points == null || points.Length < 2) return;
        if (Time.time < waitUntil) return;

        Vector3 target = points[next].position;
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 to = target - transform.position;
        if (to.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(to), 240f * Time.deltaTime);

        if (to.sqrMagnitude < 0.01f)
        {
            next = (next + 1) % points.Length;
            waitUntil = Time.time + waitTime;
        }
    }

    bool CanSeePlayer()
    {
        if (player == null || player.IsDead) return false;

        Vector3 eye = transform.position + Vector3.up * 0.5f;
        Vector3 to = player.transform.position + Vector3.up * 0.5f - eye;
        if (to.magnitude > viewRange) return false;
        if (Vector3.Angle(transform.forward, to) > viewAngle * 0.5f) return false;

        // walls block the view
        if (Physics.Raycast(eye, to.normalized, out RaycastHit hit, viewRange, ~0, QueryTriggerInteraction.Ignore))
            return hit.transform == player.transform;
        return false;
    }
}
