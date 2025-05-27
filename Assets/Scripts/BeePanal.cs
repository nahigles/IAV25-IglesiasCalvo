using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePanal : MonoBehaviour
{
    bool ocupada = false;

    Vector3 initialPos;
    Quaternion initialRot;

    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        initialPos = myTransform.position;
        initialRot = myTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsFree()
    {
        return !ocupada;
    }

    public void Work()
    {
        ocupada = true;
    }

    public void Free()
    {
        ocupada = false;
    }

    public Vector3 GetInitialPos()
    {
        return initialPos;
    }

    public Quaternion GetInitialRot()
    {
        return initialRot;
    }
}
