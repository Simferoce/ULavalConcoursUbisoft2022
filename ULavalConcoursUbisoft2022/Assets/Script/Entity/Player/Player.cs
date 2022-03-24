using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private MovespeedAttribute _movespeedAttribute = null;
    [SerializeField] private Transform _aim = null;
    
    [SerializeField] private Entity _entity = null;

    public UnityEvent _onRevive;
    public UnityEvent _onPlayerDefeated;

    private Vector3 direction = Vector3.zero;

    public Entity Entity { get => _entity; set => _entity = value; }
    public bool Lock { get { return _lockControlSemaphore > 0; } }

    private int _lockControlSemaphore = 0;

    private Quaternion lastRot;
    public Quaternion LastRot { get => lastRot; set => lastRot = value; }

    private void Awake()
    {
        Instantiate(GameManager.Instance.Class.ModelPrefab, _entity.Root);

        if (GameManager.Instance.Class.StartingItem) {
            GameObject itemInstance = Instantiate(GameManager.Instance.Class.StartingItem.Prefab, Entity.Inventory.transform);
            Entity.Inventory.Items.Add(itemInstance);
        }
        
        _entity.WeaponHandler.SetWeapon(GameManager.Instance.Class.Weapon);
        _entity.Health.OnDeath.AddListener(OnDead);
    }

    public void OnDead(Health health)
    {
        health.Invicible = true;
        if(GameManager.Instance.IsStoryMode)
        {
            //Revived();
        }
        else
        {
            _onPlayerDefeated?.Invoke();
        }
    }

    private void Revived()
    {
        _onRevive?.Invoke();
        _entity.Health.HealthPoint = _entity.Health.MaxHealth;
        _entity.Health.Invicible = false;
    }

    private void Update()
    {
        if (!Lock)
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Fire1"))
            {
                _entity.Attack(true);
            }

            _characterController.Move(direction.normalized * _movespeedAttribute.GetValue(_entity.Inventory) * Time.deltaTime);
            _entity.Translation = transform.InverseTransformDirection(direction.normalized);
            lastRot = transform.rotation;
            Vector3 positionToLookAt = new Vector3(_aim.transform.position.x, this.transform.position.y, _aim.transform.position.z);
            transform.LookAt(positionToLookAt, Vector3.up);
        }
        else
        {
            _entity.Translation = Vector3.zero;
        }
    }

    public void OnDestroy()
    {
        _entity.Health.OnDeath.RemoveListener(OnDead);
    }

    public void LockControl()
    {
        _lockControlSemaphore++;
    }

    public void UnlockControl()
    {
        _lockControlSemaphore--;
    }
}
