using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttacks
{
    Transform _player;
    Enemies _enemies;
    float _timerShoot;
    float _shootView;
    float _timeToShoot;
    float _minDis;
    int random;
    Animator _anim;

    public EnemiesAttacks(Transform p, Enemies e, float tm, float sv, float tshoot, float min, Animator anim)
    {
        _player = p;
        _enemies = e;
        _timerShoot = tm;
        _shootView = sv;
        _timeToShoot = tshoot;
        _minDis = min;
        _anim = anim;
    }


    public void NormalAttack()
    {
        var dis = Vector3.Distance(_player.position, _enemies.transform.position);

        if (dis <= _minDis)
        {
            _anim.SetTrigger("Attack");
        }

    }

    public void Shoot()
    {
        _timerShoot += Time.deltaTime;

        bool distance = (_player.transform.position - _enemies.transform.position).magnitude <= _shootView;

        if (_timerShoot >= _timeToShoot && distance)
        {
            _anim.SetTrigger("Attack");
            _timerShoot = 0;
        }
        else if(!distance)
            _timerShoot = 0;
    }

    public void HugeAttack()
    {


        var dis = Vector3.Distance(_player.position, _enemies.transform.position);

        if (dis <= _minDis)
        {
            _anim.SetTrigger("SecondAttack");
        }
        else if(dis <= _minDis * 0.5f)
            _anim.SetTrigger("Attack");



    }
}
