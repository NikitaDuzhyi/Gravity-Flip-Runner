using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.left * GameManager.Instance.GameSpeed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
