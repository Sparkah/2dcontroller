using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float jumpForce = 400f;
    [Min(0)]
    [SerializeField] private float maxMoveSpeed = 1f;
    [Min(0)]
    [SerializeField] private float gravityModifier = 1f;

    private Rigidbody2D _playerRigidBody;
    private bool _moveRight;
    private bool _moveLeft;
    private bool _decelerate;
    private bool _canJump = true;
    private bool _increaseGravity;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && _canJump==true)
        {
            _canJump = false;
            _playerRigidBody.AddForce(Vector2.up * jumpForce);
        }

        if(_playerRigidBody.velocity.y<0 && _canJump==false)
        {
            _increaseGravity = true;
        }
        if(_playerRigidBody.velocity.y>=0)
        {
            _increaseGravity = false;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            _moveLeft = true;
            _moveRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveRight = true;
            _moveLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _moveLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _moveRight = false;
        }

        if(Input.GetKey(KeyCode.A) ==false && Input.GetKey(KeyCode.D) == false)
        {
            _decelerate = true;
        }

        if (_playerRigidBody.velocity.x <= -maxMoveSpeed)
        {
            _playerRigidBody.velocity = new Vector2(-maxMoveSpeed, _playerRigidBody.velocity.y);
        }

        if (_playerRigidBody.velocity.x >= maxMoveSpeed)
        {
            _playerRigidBody.velocity = new Vector2(maxMoveSpeed, _playerRigidBody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if(_moveLeft)
        {
            _playerRigidBody.AddForce(Vector3.left * 100);
        }
        if(_moveRight)
        {
            _playerRigidBody.AddForce(Vector3.right * 100);
        }


        if(_decelerate)
        {
            //Do something (animations),
            _decelerate = false;
        }

        if(_increaseGravity==true)
        {
            _playerRigidBody.AddForce(Vector2.down * gravityModifier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            Invoke(nameof(CanNotJump), 0.5f);
        }
    }

    private void CanNotJump()
    {
        _canJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}