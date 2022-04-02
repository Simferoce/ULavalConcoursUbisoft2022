using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<string> _names = new List<string>();
    [SerializeField] private List<AudioSource> _sounds = new List<AudioSource>();
    [SerializeField] private float _movementMaxSound = 0.0f;
    [SerializeField] private float _movementLerpDuration = 0.0f;
    [SerializeField] private float _lowHealthPercentage = 0.0f;


    private Player _player = null;
    private Entity _entity = null;

    private float _movementTargetSound = 0.0f;
    private float t = 0.0f;
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _entity = _player.GetComponentInChildren<Entity>();
        _entity.Health.OnHit.AddListener(PlayOnHitSound);
    }

    private void PlayOnHitSound(Health health)
    {
        GetSound("OnHit").Play();
    }

    public void PlaySound(string name)
    {
        GetSound(name).Play();
    }

    public AudioSource GetSound(string name)
    {
        return _sounds[_names.IndexOf(name)];
    }

    private void Update()
    {
        if(_entity.IsMoving && _movementTargetSound != _movementMaxSound)
        {
            _movementTargetSound = _movementMaxSound;
            GetSound("Movement").volume = _movementTargetSound;
        }
        else if (!_entity.IsMoving && _movementTargetSound != 0.0f)
        {
            _movementTargetSound = 0.0f;
            t = 0.0f;
        }


        t += Time.deltaTime / _movementLerpDuration;
        GetSound("Movement").volume = Mathf.Lerp(GetSound("Movement").volume, _movementTargetSound, t);

        if(GetSound("Movement").volume >= 0.0f && !GetSound("Movement").isPlaying)
        {
            GetSound("Movement").Play();
        }
        else if(GetSound("Movement").volume <= 0.0f && GetSound("Movement").isPlaying)
        {
            GetSound("Movement").Stop();
        }
        

        if(_entity.Health.Percentage < _lowHealthPercentage && !GetSound("LowLife").isPlaying)
        {
            GetSound("LowLife").Play();
        }
        else if(_entity.Health.Percentage >= _lowHealthPercentage && GetSound("LowLife").isPlaying)
        {
            GetSound("LowLife").Stop();
        }
    }

    private void OnDestroy()
    {
        _entity.Health.OnHit.RemoveListener(PlayOnHitSound);
    }
}
