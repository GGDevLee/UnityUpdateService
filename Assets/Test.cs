using LeeFramework.Update;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {

        UpdateSvc.instance.Register(UpdateType.Update, Cb100, 100);

        UpdateSvc.instance.Register(UpdateType.Update, Cb200, 200);

        UpdateSvc.instance.Register(UpdateType.Update, Cb30, 30);

        UpdateSvc.instance.Register(UpdateType.Update, Cb45, 45);

        UpdateSvc.instance.Register(UpdateType.Update, Cb23, 23);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSvc.instance.Unregister(UpdateType.Update, Cb100, 100);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateSvc.instance.Unregister(UpdateType.Update, Cb45, 45);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateSvc.instance.Unregister(UpdateType.Update, Cb200, 200);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateSvc.instance.Register(UpdateType.Update, Cb200, 200);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            UpdateSvc.instance.Clear(UpdateType.Update);
        }
    }

    private void Cb100()
    {
        Debug.Log("100");
    }
    private void Cb200()
    {
        Debug.Log("200");
    }
    private void Cb30()
    {
        Debug.Log("30");
    }
    private void Cb45()
    {
        Debug.Log("45");
    }
    private void Cb23()
    {
        Debug.Log("23");
    }

}
