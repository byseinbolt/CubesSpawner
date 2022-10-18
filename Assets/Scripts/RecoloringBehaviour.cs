using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RecoloringBehaviour : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration = 1f;
    [SerializeField] private float _detentionAfterRecoloring = 2f;

    private Renderer _renderer;
    private Color _startColor;
    private Color _nextColor;
    private float _cooldown;
    private float _currentTime;
    
     private void Start()
     {
         _renderer = GetComponent<Renderer>();
         GenerateNextColor();
     }

     private void Update()
     {
         _cooldown += Time.deltaTime;
         if (_cooldown < _detentionAfterRecoloring)
         {
             return;
         }

         _currentTime += Time.deltaTime;

         var progress = _currentTime / _recoloringDuration;
         var currentColor = Color.Lerp(_startColor, _nextColor, progress);
         _renderer.material.color = currentColor;

         if (_currentTime >= _recoloringDuration)
         {
             _cooldown = 0f;
             _currentTime = 0f;
             GenerateNextColor();
         }
     }

     private void GenerateNextColor()
     {
         _startColor = _renderer.material.color;
         _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
     }
    
}
