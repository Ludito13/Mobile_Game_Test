using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamage, /*IObservable*/ IPlayers, IDeath
{
    public float maxLife;
    public float attackBuffDuration;
    public float dashDuration;
    public float movspeed;
    public float dashForce;
    public Weapons[] wp;
    public Animator anim;
    public Rigidbody rb;
    public bool canMove;
    public bool canAttack;
    public Coroutine attackCoroutine;
    public Coroutine dashBuffCoroutine;
    public Vector3 input;
    public VirtualStick virtualStick;
    public AudioStruct[] sounds;
    public AudioSource aud;

    private Vector3 _velocity;
    public float _currentLife;

    List<IObserver> _observers = new List<IObserver>();

    private PlayerMovement playerMov;
    private PlayerAttackMovements playerAttack;

    Dictionary<AudioStruct.Clips, AudioClip> _sound = new Dictionary<AudioStruct.Clips, AudioClip>();

    private void Awake()
    {
        foreach (var s in sounds)
        {
            if (!_sound.ContainsKey(s.names))
                _sound.Add(s.names, s.aud);
        }
    }

    public void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }

    void Start()
    {
        input.y = 0;
        _currentLife = maxLife;
        anim = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        aud = this.gameObject.GetComponent<AudioSource>();
        NotifyAllObservers(false);
        playerMov = new PlayerMovement(this, canMove, input, anim, movspeed, dashForce);
        playerAttack = new PlayerAttackMovements(this, anim);
    }


    public void OnStickPress(Vector2 movement)
    {
        playerMov.OnStick(movement);
    }

    public void FixedUpdate()
    {
        playerMov.NewFixedUpdate();


        if (_currentLife != maxLife)
            NotifyAllObservers(true);

        if (_currentLife == maxLife)
            NotifyAllObservers(false);

    }

    public void WeaponOn()
    {
        for (int i = 0; i < wp.Length; i++)
        {
            wp[i].ActiveCol();
        }
    }

    public void WeaponOff()
    {
        for (int i = 0; i < wp.Length; i++)
        {
            wp[i].InactiveCol();
        }
    }

    public void Dash()
    {
        playerMov.DashEvent();
        EventManager.Trigger("Shadow Trail");
    }

    public void StopDash()
    {
        playerMov.StopDash();
    }

    ///////////////////////////////////////////////////////////////////////////////
    public void NotifyAllObservers(bool n)
    {
        for (int i = 0; i < _observers.Count; i++)
            _observers[i].Notify(n);
    }

    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        if (_observers.Contains(observer))
            _observers.Remove(observer);
    }
    ///////////////////////////////////////////////////////////////////////////////

    public void StickReleased()
    {
        playerMov.OnStickReleased();
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Damage(float damage)
    {
        _currentLife -= damage;
        UIManager.instance.LifeBar(_currentLife / maxLife);
        NotifyAllObservers(true);
        PlaySound(AudioStruct.Clips.PlayerDamage);


        if (_currentLife <= 0)
            Death();
    }

    public bool Health(float h)
    {
        if (_currentLife < maxLife)
        {
            _currentLife += h;
            UIManager.instance.LifeBar(_currentLife / maxLife);
            if(maxLife > _currentLife)
            {
                _currentLife = maxLife;
                UIManager.instance.LifeBar(_currentLife / maxLife);
                return true;
            }
            return false;
        }
        return false;
    }

    public void Death()
    {
        EventManager.Trigger("Death");
    }

    public void AttackPlayer()
    {
        playerAttack.ExecuteAttack();
    }

    public void Dash(Vector2 dashV)
    {
        playerMov.ExecuteDash(dashV);
        PlaySound(AudioStruct.Clips.Dash);

    }

    private void OnTriggerEnter(Collider other)
    {
        var t = other.gameObject.GetComponent<ITriggerTouch>();

        if (t != null)
            t.Active();

        var d = other.gameObject.GetComponent<IStartDialogue>();

        if (d != null)
            d.Active();
    }
}
