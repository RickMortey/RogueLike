using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
 
public class MenuManager : MonoBehaviour 
{ 
    [SerializeField] private int _lastWeaponNumber; 
 
    [SerializeField] private GameObject[] _weaponsImages; 
 
    [SerializeField] private Text[] _parametrs; 
 
    private int _level; 
 
    private float _hp; 
 
    private void Start() 
    { 
        _level = PlayerPrefs.GetInt("level"); 
        if (_level == 0) 
        { 
            _level = 1; 
        } 
        _parametrs[0].text = "Level: " + _level; 
        _hp = PlayerPrefs.GetFloat("hp"); 
        if (_hp <= 0) 
        { 
            _hp = 10; 
        } 
        PlayerPrefs.SetFloat("hp", _hp); 
        _parametrs[1].text = "HP: " + Mathf.Floor(_hp); 
        _lastWeaponNumber = PlayerPrefs.GetInt("lastWeaponNumber"); 
 
        _weaponsImages[_lastWeaponNumber].SetActive(true); 
        if (_lastWeaponNumber == 0 || _lastWeaponNumber == 1) 
        { 
            _parametrs[2].text = "Damage: 1"; 
        } 
        else if (_lastWeaponNumber == 2) 
        { 
            _parametrs[2].text = "Damage: 3"; 
        } 
    } 
 
    public void StartGame() 
    { 
        SceneManager.LoadScene("Location"); 
    } 
}
