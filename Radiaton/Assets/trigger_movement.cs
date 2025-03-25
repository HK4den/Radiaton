using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class trigger: MonoBehaviour

{ 
    [SerializeField] bool destroyOnTriggerEnter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] string tagFilter;
public GameObject VictoryScreen;
    // Start is called before the first frame update
    void Start()
    { VictoryScreen.SetActive(false);
     
     
    }

   void OnTriggerEnter(Collider other)
   {
    if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
    onTriggerEnter.Invoke();
   }
}
