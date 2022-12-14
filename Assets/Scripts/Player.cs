using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayerControlble
{
    public static Player instance;

    [SerializeField] private Weapon _playerWeapon;

    [SerializeField] private GameObject _dead;
    [SerializeField] private GameObject _levelUp;

    [Header("Parametrs: ")]
    [SerializeField] private float _speed;
    [SerializeField] private float _startHp;

    [Header("Uping: ")]
    [SerializeField] private float _plusHp;

    [Header("Texts: ")]
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _levelPointsText;

    private int _level;

    private int _countLevel;

    private float _hp;

    private bool _onLookRight = true;

    private GameObject _weaponObject;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        instance = this;

        _hp = PlayerPrefs.GetFloat("hp");
        if (_hp == 0)
        {
            _hp = _startHp;
        }

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weaponObject = _playerWeapon.gameObject;
        _level = PlayerPrefs.GetInt("level");
        _countLevel = PlayerPrefs.GetInt("countLevel");
        if (_level == 0)
        {
            _level = 1;
        }
        if (_level == 1)
        {
            _speed += 0.02f;
            _plusHp += 0.5f;
            _playerWeapon.SetDamage(0.2f);
            _playerWeapon.SetRecharge(0.3f);
            _playerWeapon.SetTimeSpeedShooting(3);
        }
        if (_level == 2)
        {
            _speed += 0.05f;
            _plusHp += 0.6f;
            _playerWeapon.SetDamage(0.4f);
            _playerWeapon.SetRecharge(0.5f);
            _playerWeapon.SetTimeSpeedShooting(4);
        }
        if (_level == 3)
        {
            _speed += 0.06f;
            _plusHp += 0.65f;
            _playerWeapon.SetDamage(0.5f);
            _playerWeapon.SetRecharge(0.5f);
            _playerWeapon.SetTimeSpeedShooting(4);
        }
        if (_level > 3)
        {
            _speed += 0.03f;
            _plusHp += 0.7f;
            _playerWeapon.SetDamage(0.5f);
            _playerWeapon.SetRecharge(0.5f);
            _playerWeapon.SetTimeSpeedShooting(5);
        }
        _levelText.text = "LEVEL: " + _level;
        _hpText.text = "HP " + Mathf.Floor(_hp); 
        _levelPointsText.text = "LEVEL POINTS: " + _countLevel + "/100";
        PlayerPrefs.SetInt("level", _level);
    }

    public void Idle()
    {
        _animator.SetBool("OnIdle", true);
        _animator.SetBool("OnRun", false);
    }

    public void Run(float horizontal, float vertical)
    {
        _animator.SetBool("OnIdle", false);
        _animator.SetBool("OnRun", true);

        Vector2 movePosition = new Vector2(horizontal * _speed, vertical * _speed);
        transform.Translate(movePosition);

        if (horizontal < 0 && _onLookRight == true || horizontal > 0 && _onLookRight == false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _onLookRight = !_onLookRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    public void GetDamage(float damage)
    {
        _hp -= damage;
        _hpText.text = "HP: " + Mathf.Floor(_hp);
        if (_hp <= 0)
        {
            _animator.SetBool("OnDead", true);
            Instantiate(_dead, transform.position, Quaternion.identity);
            Destroy(_weaponObject);
        }
        PlayerPrefs.SetFloat("hp", _hp);
    }

    public void UsingItem(int numberItem)
    {
        switch (numberItem)
        {
            case (1):
                if (_hp != _startHp)
                {
                    if (_startHp - _hp >= _plusHp)
                    {
                        _hp += _plusHp;

                    }
                    else
                    {
                        _hp = _startHp;
                    }
                    _hpText.text = "HP " + Mathf.Floor(_hp);
                }
                break;
            case (2):
                _playerWeapon.SetSpeedShoot(true);
                break;
        }
    }

    public void PlusCountLevel(int valuePlus)
    {
        _countLevel += valuePlus;
        if (_countLevel >= 100)
        {
            _level++;
            Instantiate(_levelUp, transform.position, Quaternion.identity);
            _countLevel -= 100;
        }
        _levelText.text = "LEVEL: " + _level;
        _levelPointsText.text = "LEVEL POINTS: " + _countLevel + "/100";
        PlayerPrefs.SetInt("level", _level);
        PlayerPrefs.SetInt("countLevel", _countLevel);
    }

    private void TransitionToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
