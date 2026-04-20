using UnityEngine;
using UnityEngine.Rendering;

public class Guided : MonoBehaviour
{
    public Transform tranformPlayer;
    public float speed;
    public float angleInitial;
    public float speedRotation;

  private void Start()
  {
    GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
    if (playerGameObject == null)
        {
            Destroy(gameObject);
        }
        else
        {
            tranformPlayer = playerGameObject.transform;
        }
  }

  private void Update()
  {
    transform.Translate(speed * Time.deltaTime * Vector2.right, Space.Self);

    if (tranformPlayer == null) { return;}

    float angleRad = Mathf.Atan2(tranformPlayer.position.y - transform.position.y, tranformPlayer.position.x - transform.position.x);
    float angleDeg = 180 / Mathf.PI * angleRad - angleInitial;
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angleDeg), speedRotation * Time.deltaTime);
  }

private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.TryGetComponent(out Lives lives))
    {
        lives.TakeDamage(1);
    }


    if (collision.CompareTag("Player"))
    {
        Destroy(gameObject);
    }

    if (collision.gameObject.layer == LayerMask.NameToLayer("MapObject"))
    {
        Destroy(gameObject);
    }
  }

}
