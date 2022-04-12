using UnityEngine;

[ExecuteInEditMode]
public class FollowClamp : MonoBehaviour
{
    [SerializeField] private Transform source;

    private float posX = 0;
    private float posY = 0;
    private float _posX;
    private float _posY;
    private TalkSytem tSystem;

    private void Start()
    {
        tSystem = TalkSytem.Instance;
    }

    private void Update()
    {
        posX = Mathf.Clamp(source.localPosition.x, -tSystem.ResEstirableX, tSystem.ResEstirableX);
        posY = Mathf.Clamp(source.localPosition.y, -tSystem.ResEstirableY, tSystem.ResEstirableY);

        if (source.localPosition.y >= tSystem.Res3 && source.localPosition.y <= tSystem.Res1)
        {
            posY = source.localPosition.y;
        }
        if (source.localPosition.y <= -tSystem.Res3 && source.localPosition.y >= -tSystem.Res1)
        {
            posY = source.localPosition.y;
        }
        if (source.localPosition.y >= -tSystem.Res3 && source.localPosition.y <= tSystem.Res3)
        {
            if (posX >= tSystem.Res1 || posX <= -tSystem.Res1)
            {
                posY = source.localPosition.y;
            }
        }

        if (source.localPosition.x >= tSystem.Res1 && source.localPosition.x <= tSystem.ResEstirableX)
        {
            posX = source.localPosition.x;
        }
        if (source.localPosition.x <= -tSystem.Res1 && source.localPosition.x >= -tSystem.ResEstirableX)
        {
            posX = source.localPosition.x;
        }
        if (source.localPosition.x >= -tSystem.Res1 && source.localPosition.x <= tSystem.Res1)
        {
            if (posY >= tSystem.Res3 || posY <= -tSystem.Res3)
            {
                posX = source.localPosition.x;
            }
        }

        if (posX > -tSystem.Res1 && posX < tSystem.Res1 && posY > -tSystem.Res3 && posY < tSystem.Res3)
        {
            posX = _posX;
            posY = _posY;
        }

        transform.localPosition = new Vector2(posX, posY);

        if (_posX != posX)
            _posX = transform.localPosition.x;
        if (_posY != posY)
            _posY = transform.localPosition.y;
    }
}