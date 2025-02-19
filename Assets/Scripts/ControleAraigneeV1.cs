using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleAraigneeV1 : MonoBehaviour
{
    // varaibles de mouvement et contr�le
    [SerializeField] private float vitessePromenade;
    private Rigidbody _rb;
    private Vector3 directionInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnPromener(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * vitessePromenade;
        directionInput = new Vector3(directionAvecVitesse.x, 0f, directionAvecVitesse.y);
    }

    void FixedUpdate()
    {
        // calculer et appliquer la translation
        Vector3 mouvement = directionInput;

        // si on a une direction d'input
        if (directionInput.magnitude > 0f)
        {
            // calculer rotation cible
            float rotationCible = Vector3.SignedAngle(-Vector3.forward, directionInput.normalized , Vector3.up);
            // appliquer la rotation cible directement
            _rb.MoveRotation(Quaternion.Euler(0.0f, rotationCible, 0.0f));
            _rb.AddRelativeForce(0, 0, mouvement.magnitude, ForceMode.VelocityChange);
        }
    }
}
