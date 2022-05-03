using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashDistance;
    [SerializeField] TrailRenderer _dashTrail;
    [SerializeField] Animator _animator;

    Camera _camera;
    Vector2 _newCharacterPosition;
    bool _needMoveTowards;
    bool _isDashing;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        //_dashTrail.enabled = false;
        _camera = Camera.main;
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    void Update()
    {
        if (_isDashing)
        {
            Dash();
            return;
        }

        _newCharacterPosition = transform.position;

        float verticalAxis = Input.GetAxis("Vertical");
        bool needChangeVerticalPosition = verticalAxis != 0;
        if (needChangeVerticalPosition)
            _newCharacterPosition += Vector2.up * verticalAxis;

        float horizontalAxis = Input.GetAxis("Horizontal");
        bool needChangeHorizontalPosition = horizontalAxis != 0;
        if (needChangeHorizontalPosition)
            _newCharacterPosition += Vector2.right * horizontalAxis;

        _animator.SetFloat("Run", Mathf.Abs(verticalAxis) + Mathf.Abs(horizontalAxis));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 dir = RecognizeDash(verticalAxis, horizontalAxis);
            if (dir != Vector2.zero)
            {
                _newCharacterPosition = DetermineEndPointPosition(dir);
                _newCharacterPosition = GetClampedByScreenPosition(_newCharacterPosition);
                //_dashTrail.enabled = true;
                //_dashTrail.Clear();
                _isDashing = true;
            }
        }
        else if (needChangeVerticalPosition || needChangeHorizontalPosition)
        {
            Vector2 position = transform.position;
            position = Vector2.MoveTowards(position, _newCharacterPosition, _speed * Time.deltaTime);
            transform.position = GetClampedByScreenPosition(position);
        }

        Vector3 diff = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
    }

    Vector2 GetClampedByScreenPosition(Vector2 position)
    {
        Vector2 minScreenBounds = _camera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return new Vector2(
            Mathf.Clamp(position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
            Mathf.Clamp(position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1));
    }

    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, _newCharacterPosition, _dashSpeed * Time.deltaTime);
        PlayDashAudio();
        if ((_newCharacterPosition - (Vector2) transform.position).magnitude < 0.1f)
        {
            //_dashTrail.enabled = false;
            _isDashing = false;
        }
    }

    Vector2 RecognizeDash(float verticalAxis, float horizontalAxis)
    {
        if (verticalAxis > 0 && horizontalAxis > 0)
            return Vector2.up + Vector2.right;

        if (verticalAxis > 0 && horizontalAxis < 0)
            return Vector2.up + Vector2.left;

        if (verticalAxis > 0 && horizontalAxis == 0)
            return Vector2.up;

        if (verticalAxis < 0 && horizontalAxis > 0)
            return Vector2.down + Vector2.right;

        if (verticalAxis < 0 && horizontalAxis < 0)
            return Vector2.down + Vector2.left;

        if (verticalAxis < 0 && horizontalAxis == 0)
            return Vector2.down;

        if (verticalAxis == 0 && horizontalAxis > 0)
            return Vector2.right;

        if (verticalAxis == 0 && horizontalAxis < 0)
            return Vector2.left;

        return Vector2.zero;
    }

    Vector2 DetermineEndPointPosition(Vector2 dir)
    {
        return ((Vector2)transform.position + dir * _dashDistance);
    }

    public void PlayDashAudio()
    {
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(audioClipArray[Random.Range(0, audioClipArray.Length)]);
    }
}
