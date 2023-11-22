using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    float _dirX, _dirY;
    Vector3 _dir;

    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        _dirY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        _dir = new Vector3(_dirX, _dirY, 0);
        transform.position += _dir;
        transform.right = _dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FinishLine"))GameManager.Instance.ResetLevel();
    }
}
