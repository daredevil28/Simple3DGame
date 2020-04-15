using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    private Renderer _gunRenderer;
    private Animator _animator;
    public int effectMax;
    public float effectSpeed;
    private int _effectValue;
    private float _animationTime;
    public int ammo;

    public ParticleSystem shootParticle;

    public GameObject ragdoll;
    // Start is called before the first frame update
    void Start()
    {
        _gunRenderer = gameObject.GetComponent<Renderer>();
        InvokeRepeating("CoolEffect", 1, effectSpeed);
        _animator = GetComponent<Animator>();
        _animationTime = GetComponent<Animator>().runtimeAnimatorController.animationClips[1].length;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool("Shooting", true);
            shootParticle.Play();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log(hit);
                if (hit.rigidbody != null)
                {
                    Instantiate(ragdoll, hit.transform.position, hit.transform.rotation);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
    
    //A cool effect for the gun using shader graphs
    void CoolEffect()
    {
        _gunRenderer.sharedMaterial.SetFloat("RandomizedNoise", _effectValue++);
        if (_effectValue >= effectMax)
        {
            _effectValue = 0;
        }
    }

    private void LateUpdate()
    {
        if (_animator.GetBool("Shooting")) {
            _animator.SetBool("Shooting", false);
        }
    }
}
