using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Constructor de los movimientos de los enemigos
public class EnemiesMovement
{
    Transform _player;
    Enemies _enemies;
    float _radiusView;
    float _currentSpeed;
    float _maxSpeed;
    float _rotate;
    float _minDistance;
    IObserver _observer;
    Animator _anim;
    float mov;
    public EnemiesMovement(Transform p, Enemies e, float currentSpeed, float rotate, IObserver ob, Animator anim)
    {
        _player = p;
        _enemies = e;
        _currentSpeed = currentSpeed;
        _rotate = rotate;
        _observer = ob;
        _anim = anim;
    }

    public EnemiesMovement SetRadius(float radius)
    {
        _radiusView = radius;

        return this;
    }

    public EnemiesMovement SetMaxSpeed(float maxSpeed)
    {
        _maxSpeed = maxSpeed;

        return this;
    }

    public EnemiesMovement SetMinDistance(float minDis) 
    {
        _minDistance = minDis;

        return this;
    }

    public void Movement()
    {
        Follow();
        Rotate();
    }

    public void Follow()
    {
        bool distance = (_player.transform.position - _enemies.transform.position).magnitude <= _radiusView;
        float dis = (_player.transform.position - _enemies.transform.position).magnitude;

        if (distance && dis >= _minDistance)
        {
            _enemies.transform.position = Vector3.MoveTowards(_enemies.transform.position, _player.transform.position, _currentSpeed * Time.deltaTime);
            mov = 1;
        }
        else
        {
            mov = 0;
        }
         _anim.SetFloat("Movement", mov);
    }

    public void Rotate()
    {
        bool distance = (_player.transform.position - _enemies.transform.position).magnitude <= _radiusView;

        if (distance)
        {
            var lookpos = _player.transform.position - _enemies.transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);
            _enemies.transform.rotation = Quaternion.RotateTowards(_enemies.transform.rotation, rotation, _rotate);
        }

    }

    public void Notify(bool n)
    {
        if (n)
        {
            _currentSpeed = _maxSpeed;
        }
        else
        {
            _currentSpeed = 0;
        }
    }
}
