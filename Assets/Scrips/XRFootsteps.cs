using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class XRFootsteps : MonoBehaviour
{
    public float movementThreshold = 0.01f; // Sensibilidad para detectar movimiento
    public AudioSource audioSource; // Arrastra el AudioSource aquí desde el Inspector
    private Vector3 lastPosition;

    private CharacterController characterController;

    void Start()
    {
        // Obtenemos el CharacterController
        characterController = GetComponent<CharacterController>();

        // Comprobamos si el AudioSource está asignado manualmente
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado. Por favor asigna el AudioSource en el Inspector.");
        }

        // Iniciamos la última posición en la posición inicial del CharacterController
        lastPosition = characterController.transform.position;
    }

    void Update()
    {
        // Calcula la distancia recorrida desde el último frame usando el CharacterController
        float distanceMoved = Vector3.Distance(characterController.transform.position, lastPosition);

        // Si la distancia es mayor al umbral y el audio no está sonando, comienza a reproducirlo
        if (distanceMoved > movementThreshold && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // Si la distancia es menor al umbral y el audio está sonando, detén el audio
        else if (distanceMoved <= movementThreshold && audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        // Actualiza la última posición con la posición actual del CharacterController
        lastPosition = characterController.transform.position;
    }
}

