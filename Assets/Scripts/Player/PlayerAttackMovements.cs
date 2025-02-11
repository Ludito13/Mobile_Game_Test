using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMovements
{
    Player _player;
    Animator _anim;

    public PlayerAttackMovements(Player p, Animator a)
    {
        _player = p;
        _anim = a;
    }

    public void ExecuteAttack()
    {
        if (_player.attackCoroutine != null)
            _player.StopCoroutine(_player.attackCoroutine);
        _player.attackCoroutine = _player.StartCoroutine(BufferAttack());
    }

    IEnumerator BufferAttack()
    {
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(_player.attackBuffDuration);
        _player.anim.ResetTrigger("Attack");
        _player.attackCoroutine = null;
    }

}
