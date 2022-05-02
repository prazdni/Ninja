using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float _speed;

    Camera _camera;
    Vector2 _newCharacterPosition;
    bool _needMoveTowards;

    void Awake()
    {
        _camera = Camera.main;
    }

    void FixedUpdate()
    {
        _newCharacterPosition = transform.position;

        float verticalAxis = Input.GetAxis("Vertical");
        bool needChangeVerticalPosition = verticalAxis != 0;
        if (needChangeVerticalPosition)
            _newCharacterPosition += Vector2.up * verticalAxis;

        float horizontalAxis = Input.GetAxis("Horizontal");
        bool needChangeHorizontalPosition = horizontalAxis != 0;
        if (needChangeHorizontalPosition)
            _newCharacterPosition += Vector2.right * horizontalAxis;

        _needMoveTowards = needChangeVerticalPosition || needChangeHorizontalPosition;

    }

    void Update()
    {
        if (_needMoveTowards)
        {
            Vector2 position = transform.position;
            position = Vector2.MoveTowards(position, _newCharacterPosition, _speed * Time.deltaTime);

            Vector3 minScreenBounds = _camera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector3 maxScreenBounds = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            position = new Vector2(
                Mathf.Clamp(position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
                Mathf.Clamp(position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1));

            transform.position = position;
        }

        Vector3 diff = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
    }
}
