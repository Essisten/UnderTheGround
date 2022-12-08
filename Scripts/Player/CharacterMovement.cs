using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _extrajumpForce;
    // [SerializeField] private float _dashForce;
    [SerializeField] private Vector3 _groundCheckOffset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 CheckPointPos;
    [SerializeField] private GameObject CheeseOne;
    [SerializeField] private GameObject CheeseOneHalf;
    [SerializeField] private GameObject CheeseTwo;
    [SerializeField] private GameObject CheeseTwo_Half;
    private int lives = 3;

    private Vector3 _input;
    public bool _isMoving;
    public bool _extraJump = true;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isFlying;

    private Rigidbody2D _rigidbody;
    private CharacterAnimations _animations;
    [SerializeField] private SpriteRenderer _characterSprite;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponentInChildren<CharacterAnimations>();
    }

    private void Update()
    {
        Move();
        CheckGround();
        IsFlying();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        _animations.IsMoving = _isMoving;
        _animations.IsFlying = IsFlying();
    }

    private void CheckGround()
    {
        float rayLength = 0.3f;
        Vector3 rayStartPosition = transform.position + _groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, rayStartPosition + Vector3.down, rayLength, groundMask);

        if (hit.collider != null)
        {
            // _isGrounded = hit.collider.CompareTag("Ground") ? true : false;

            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Platform"))
            {
                _extraJump = true;
                _isGrounded = true;
            }
            else
                _isGrounded = false;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private bool IsFlying()
    {
        if (_rigidbody.velocity.y < -0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += _input * _speed * Time.deltaTime;
        _isMoving = _input.x != 0 ? true : false;

        if (_input.x != 0)
        {
            _characterSprite.flipX = _input.x > 0 ? false : true;
        }

        _animations.IsMoving = _isMoving;
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _animations.Jump();
            return;
        }
        if (_extraJump)
        {
            _rigidbody.velocity = Vector2.down * 0f;
            _rigidbody.AddForce(transform.up * _extrajumpForce, ForceMode2D.Impulse);
            _animations.Jump();
            _extraJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            this.transform.parent = collision.transform;

        if (collision.gameObject.tag == "Monster")
        {
            if (lives > 0)
            {
                transform.position = CheckPointPos;
                if (lives == 3)
                {
                    CheeseTwo.SetActive(false);
                    CheeseTwo_Half.SetActive(true);
                    lives--;
                    return;
                }
                if (lives == 2)
                {
                    CheeseTwo_Half.SetActive(false);
                    lives--;
                    return;
                }
                if (lives == 1)
                {
                    CheeseOne.SetActive(false);
                    CheeseOneHalf.SetActive(true);
                    lives--;
                    return;
                }
            }
            else
                SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            Vector3 pos = Vector3.zero;
            pos.x = collision.transform.position.x;
            pos.y = collision.transform.position.y;
            pos.z = -1;
            
            CheckPointPos = pos;
        }

        if (collision.gameObject.tag == "DeadZone")
        {
            if (lives > 0)
            {
                _rigidbody.velocity = Vector2.down * 0f;
                transform.position = CheckPointPos;
                if (lives == 3)
                {
                    CheeseTwo.SetActive(false);
                    CheeseTwo_Half.SetActive(true);
                    lives--;
                    return;
                }
                if (lives == 2)
                {
                    CheeseTwo_Half.SetActive(false);
                    lives--;
                    return;
                }
                if (lives == 1)
                {
                    CheeseOne.SetActive(false);
                    CheeseOneHalf.SetActive(true);
                    lives--;
                    return;
                }
            }
            else
                SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            this.transform.parent = null;

        if (collision.gameObject.tag == "Monster")
            return;
    }
}
