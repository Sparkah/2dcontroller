using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _jumpForce = 400f;
    [Min(0)]
    [SerializeField] private float _maxMoveSpeed = 1f;
    [Min(0)]
    [SerializeField] private float _gravityModifier = 30f;
    [Min(0f)]
    [SerializeField] private float _delayBeforeFalling = 0.1f;

    private Rigidbody2D _rigidBody;
    private bool _moveRight;
    private bool _moveLeft;
    private bool _decelerate;
    private bool _canJump = true;
    private bool _increaseGravity;
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _canJump==true)
        {
            _playerAnimator.AnimationJumping();
            _canJump = false;
            _rigidBody.AddForce(Vector2.up * _jumpForce);
        }

        if(_rigidBody.velocity.y<0 && _canJump==false)
        {
            _increaseGravity = true;
        }
        if(_rigidBody.velocity.y>=0)
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

        if (_rigidBody.velocity.x <= -_maxMoveSpeed)
        {
            _rigidBody.velocity = new Vector2(-_maxMoveSpeed, _rigidBody.velocity.y);
        }

        if (_rigidBody.velocity.x >= _maxMoveSpeed)
        {
            _rigidBody.velocity = new Vector2(_maxMoveSpeed, _rigidBody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if(_moveLeft)
        {
            _rigidBody.AddForce(Vector3.left * 100);
            _playerAnimator.AnimationWalking(_moveLeft); //true
        }
        if(_moveRight)
        {
            _rigidBody.AddForce(Vector3.right * 100);
            _playerAnimator.AnimationWalking(_moveLeft); //false
        }


        if(_decelerate)
        {
            //Do something (animations),
            _decelerate = false;
        }

        if(_increaseGravity==true)
        {
            _rigidBody.AddForce(Vector2.down * _gravityModifier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _rigidBody.gravityScale = 0;
            Invoke(nameof(CanNotJump), _delayBeforeFalling);
        }
    }

    private void CanNotJump()
    {
        _canJump = false;
        _rigidBody.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)// CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}