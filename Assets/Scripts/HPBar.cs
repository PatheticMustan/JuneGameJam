using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public int maxHP;
    public float speed;

    private float _width;
    private float _change;
    private int _HP;
    private Vector3 _target;

    // Start is called before the first frame update
    void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _change = _width / maxHP;
        _HP = maxHP;
        _target = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != transform.localPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _target, speed * Time.deltaTime);
        }
    }

    public void ChangeHP(int hp)
    {
        _HP -= hp;
        Vector3 nextSpot = _target;
        float change = _change * hp;
        nextSpot.x -= change;

        if (_HP <= 0)
        {
            nextSpot.x = -_width * 10;
            _HP = 0;
        }
        else if (_HP > maxHP)
        {
            nextSpot.x = 0;
            _HP = maxHP;
        }

        _target = nextSpot;
    }
}
