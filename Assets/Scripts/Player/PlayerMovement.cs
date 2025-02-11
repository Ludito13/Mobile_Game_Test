using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
//Constructor del movimiento del jugador
public class PlayerMovement
{
    Player _player;
    bool _canMove;
    Vector3 _input;
    Animator _anim;
    float _moveSpeed;
    float _dashForce;

    public PlayerMovement(Player player, bool canMove, Vector3 input, Animator anim, float speed, float dashf)
    {
        _player = player;
        _canMove = canMove;
        _input = input;
        _anim = anim;
        _moveSpeed = speed;
        _dashForce = dashf;
    }

    public void NewFixedUpdate()
    {
        if (_input.sqrMagnitude > 0)
        {
            _player.rb.MovePosition(_player.transform.position + _input * _moveSpeed * Time.fixedDeltaTime);
        }
        MovementAnimation();

    }


    public void MovementAnimation()
    {
        if (_anim.GetFloat("Movement") != _input.magnitude)
            _anim.SetFloat("Movement", _input.magnitude);
    }

    public void OnStick(Vector2 newInput)
    {
        _canMove = true;
        
        if (_canMove)
        {
            _input.x = newInput.x;
            _input.z = newInput.y;

            if (_input.sqrMagnitude > 0)
            {
                if (_input.sqrMagnitude > 1)
                    _input.Normalize();

                _player.transform.forward = _input;
            }
        }
    }

    public void OnStickReleased()
    {
        if (_canMove)
        {
            _input.x = 0f;
            _input.z = 0f;
        }
    }

    public void DashEvent()
    {
        _player.rb.AddForce(_player.transform.forward * _dashForce, ForceMode.Impulse);
    }

    public void StopDash()
    {
        _player.rb.velocity = Vector3.zero;
        _player.rb.angularVelocity = Vector3.zero;
    }

    public void ExecuteDash(Vector2 direction)
    {
        if (_player.dashBuffCoroutine != null)
            _player.StopCoroutine(_player.dashBuffCoroutine);

        _player.dashBuffCoroutine = _player.StartCoroutine(DashBuff());
    }

    IEnumerator DashBuff()
    {
        _anim.SetTrigger("Dash");
        yield return new WaitForSeconds(_player.dashDuration);
        _anim.ResetTrigger("Dash");
        _player.dashBuffCoroutine = null;
    }
}
