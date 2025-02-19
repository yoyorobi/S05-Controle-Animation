using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleAraigneeV2 : MonoBehaviour
{
    // variables de mouvement et contr�le
    [SerializeField] private float _vitessePromenade;
    private Rigidbody _rb;
    private Vector3 directionInput;

    // variables de contr�le d'animation
    private Animator _animator;
    private float _rotationVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void OnPromener(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade;
        directionInput = new Vector3(directionAvecVitesse.x, 0f, directionAvecVitesse.y);
        _animator.SetFloat("Deplacement", directionInput.magnitude);

        
    }

    void OnAttack(){
        _animator.SetTrigger("Attack");
    }

    void FixedUpdate()
    {
        // calculer et appliquer la translation
        Vector3 mouvement = directionInput;
        float rotation = 0f;

        // si on a une direction d'input
        if (directionInput.magnitude > 0f)
        {
            // calculer rotation cible
            float rotationCible = Vector3.SignedAngle(-Vector3.forward, directionInput, Vector3.up);
            // faire le changement plus graduel avec une interpolation
            rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationCible, ref _rotationVelocity, 0.12f);
            // appliquer la rotation cible directement
            _rb.MoveRotation(Quaternion.Euler(0.0f, rotation, 0.0f));
        }
        // appliquer la vitesse de translation
        _rb.AddForce(mouvement, ForceMode.VelocityChange);
    }
}
