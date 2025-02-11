using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Padre de los enemigos
public abstract class Enemies : MonoBehaviour, IDamage, IDeath, IObserver
{
    [SerializeField]
    protected float maxLife;
    [SerializeField]
    protected Transform player;
    [SerializeField]
    protected float radiusView;
    [SerializeField]
    protected float timeToShoot;
    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected float maxForce;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float shootView;
    [SerializeField]
    protected float rotateMaxSpeed;
    [SerializeField]
    protected float minDistance;
    [SerializeField]
    protected Animator anim;    
    [SerializeField]
    protected AudioStruct[] sounds;
    [SerializeField]
    protected Weapons enemWeapons;    
    [SerializeField]
    protected BulletSpawner bulletSpawner;

    public AudioSource audios;

    public Dictionary<AudioStruct.Clips, AudioClip> _sound = new Dictionary<AudioStruct.Clips, AudioClip>();

    protected Rigidbody _rb;
    public float _currentLife;
    protected float _rotateSpeed;
    protected float _timerShoot;
    protected float _currentSpeed;
    protected Vector3 _velocity;

    private EnemiesMovement _enemMov;
    private EnemiesAttacks _enemAttack;
    private EnemySmeller smeller;
    private IObserver _observer;
    private int _counter;

    public virtual Vector3 GetVelocity()
    {
        return _velocity;
    }

    private void Awake()
    {
        foreach (var s in sounds)
        {
            if (!_sound.ContainsKey(s.names))
                _sound.Add(s.names, s.aud);
        }
    }

    public virtual void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    public virtual void Start()
    {
        _currentLife = maxLife;
        _currentSpeed = maxSpeed;
        _rotateSpeed = rotateMaxSpeed;
        _rb = this.gameObject.GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>();
        audios = this.gameObject.GetComponent<AudioSource>();
        _enemMov = new EnemiesMovement(player, this, _currentSpeed, _rotateSpeed, _observer, anim).SetRadius(radiusView).SetMaxSpeed(maxSpeed).SetMinDistance(minDistance);
        _enemMov.SetRadius(radiusView);
        _enemMov.SetMaxSpeed(maxSpeed);
        _enemMov.SetMinDistance(minDistance);
        _enemAttack = new EnemiesAttacks(player, this, _timerShoot, shootView, timeToShoot, minDistance, anim);

    }

    public void Update()
    {
        BaseAttack();
        Movement();
        Shoot();
        Rotation();
        HugeAttack();
    }

    public virtual void Death()
    {
        //this.GetComponent<Collider>().isTrigger = true;
        //_rb.useGravity = false;
        //_currentSpeed = 0;
        //_rotateSpeed = 0;
        //_rb.velocity = Vector3.zero;
        //_rb.angularVelocity = Vector3.zero;
        anim.SetTrigger("Dead");
    }

    public virtual void AEV_Death()
    {
        EventManager.Trigger("Counter Wave");
        EventManager.Trigger("Kill Counter");
        Destroy(this.gameObject);      
    }

    public virtual void Movement()
    {

        bool distance = (player.transform.position - transform.position).magnitude <= radiusView;

        if (distance)
        {
            _enemMov.Follow();
            _enemMov.Rotate();
        }
    }

    public virtual void BaseAttack()
    {
        _enemAttack.NormalAttack();
    }

    public void WeaponOn()
    {
        enemWeapons.ActiveCol();
    }

    public void WeaponOff()
    {
        enemWeapons.InactiveCol();
    }

    public virtual void Rotation()
    {
        _enemMov.Rotate();

    }

    public virtual void Shoot()
    {
        if(_currentLife >= 0)
        {
            _enemAttack.Shoot();
        }
    }

    public virtual void InstanceBullet()
    {
        bulletSpawner.ShootBullet();
    }

    public virtual void HugeAttack()
    {
        _enemAttack.HugeAttack();
    }

    public virtual void Damage(float damage)
    {

        if(_currentLife > 0)
        {
            _currentLife -= damage;
            anim.SetTrigger("Damage");
            PlaySound(AudioStruct.Clips.GolpeDeEspada);
        }

        if (_currentLife <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radiusView);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, shootView);
    }

    public virtual void Notify(bool n)
    {
        _enemMov.Notify(n);
    }

    public void OnDestroy()
    {

    }
}
