using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    float _dirX, _dirY;
    Vector3 _dir;

    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _dirY = Input.GetAxisRaw("Vertical");
        _dir = new Vector3(_dirX,_dirY, 0);
        _dir.Normalize();
        _dir *= _speed * Time.deltaTime;
        transform.position += _dir;
        if(_dir==Vector3.zero) return;
        transform.right = _dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FinishLine"))GameManager.Instance.ResetLevel();
    }
}
