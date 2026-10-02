using System;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public float force = 10f;
    public KeyDirection[] keys;
    public KeyDirection jump;
    public int collectableUnits;
    public event Action<int, int> OnCollected;
    public event Action OnFall;


    
    private Rigidbody _rigidbody;
    private Vector3 _torque =Vector3.zero;
    private Vector3 _jump;
    private int _collected;
    
    [System.Serializable]
    public struct KeyDirection
    {
        public KeyCode key;
        public Vector3 direction;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_collected != collectableUnits) {
        foreach (KeyDirection key in keys)
        {
            if (Input.GetKey(key.key))
            {
                _torque += key.direction;
            }
        }

        if (Input.GetKeyDown(jump.key))
        {
            _jump+=jump.direction;
        }
        } else
        {
            Debug.Log("U win gng");
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddTorque(_torque * speed * Time.fixedDeltaTime);
        _torque = Vector3.zero;
        
        _rigidbody.AddForce(_jump * force);
        _jump = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            _collected +=1;
            OnCollected?.Invoke(_collected, collectableUnits);
        }

        if (other.CompareTag("Barrier"))
        {
            OnFall?.Invoke();
        }
    }

    
}
