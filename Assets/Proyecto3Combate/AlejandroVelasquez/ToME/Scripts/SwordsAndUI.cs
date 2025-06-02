using UnityEngine;
using UnityEngine.UI;
public class SwordsAndUI : MonoBehaviour
{
    [Header("Swords")]
    [SerializeField] private GameObject _lightSword;
    [SerializeField] private GameObject _heavySword;
    [Header("Titles")]
    [SerializeField] private Text _swordType;
    [SerializeField] private Text _attackType;

    private AttackController _attackController;
    private void Awake()
    {
        SwordsDeactivated();
        _swordType.text = null;
        _attackType.text = null;
        _attackController = FindFirstObjectByType<AttackController>();

    }
    public void SwordsDeactivated()
    {
        _lightSword.gameObject.SetActive(false);
        _heavySword.gameObject.SetActive(false);
        _swordType.text = null;
    }
    public void AttackType()
    {
        if (_attackController == null) return;

        if (_attackController._lightAttack == true)
        {
            _attackType.text = "Light Attack";
        }
        else if (_attackController._heavyAttack == true)
        {
            _attackType.text = "Heavy Attack";
        }
        else
        {
            _attackType.text = null; // O string.Empty
        }
    }

    private void Update()
    {
        AttackType();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("LightSword tecla T");
            _lightSword.gameObject.SetActive(true);
            _heavySword.gameObject.SetActive(false);
            _swordType.text = "Light Sword";

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("HeavySword tecla f");
            _lightSword.gameObject.SetActive(false);
            _heavySword.gameObject.SetActive(true);
            _swordType.text = "Heavy Sword";

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwordsDeactivated();
        }
    }
}
