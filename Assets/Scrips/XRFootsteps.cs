using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class XRFootsteps : MonoBehaviour
{
    public float movementThreshold = 0.01f; // Sensibilidad para detectar movimiento
    public AudioSource audioSource; // Arrastra el AudioSource aqu� desde el Inspector
    private Vector3 lastPosition;

    private CharacterController characterController;

    void Start()
    {
        // Obtenemos el CharacterController
        characterController = GetComponent<CharacterController>();

        // Comprobamos si el AudioSource est� asignado manualmente
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado. Por favor asigna el AudioSource en el Inspector.");
        }

        // Iniciamos la �ltima posici�n en la posici�n inicial del CharacterController
        lastPosition = characterController.transform.position;
    }

    void Update()
    {
        // Calcula la distancia recorrida desde el �ltimo frame usando el CharacterController
        float distanceMoved = Vector3.Distance(characterController.transform.position, lastPosition);

        // Si la distancia es mayor al umbral y el audio no est� sonando, comienza a reproducirlo
        if (distanceMoved > movementThreshold && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // Si la distancia es menor al umbral y el audio est� sonando, det�n el audio
        else if (distanceMoved <= movementThreshold && audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        // Actualiza la �ltima posici�n con la posici�n actual del CharacterController
        lastPosition = characterController.transform.position;
    }
}

