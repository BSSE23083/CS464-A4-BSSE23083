using UnityEngine;

// The camera is ~14 m away from the player so the AudioListener lives here
// instead, on an empty object that sits on the player. Rotation stays fixed
// so left/right in the sound matches left/right on screen.
public class ListenerOnPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    void LateUpdate()
    {
        transform.position = player.position + Vector3.up;
        transform.rotation = Quaternion.identity;
    }
}
