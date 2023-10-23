using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_CursorIcon : MonoBehaviour
{
    [SerializeField]private GameObject cursorImg;

    P_MainController _mainController;
    public void Initialize(P_MainController mainController)
    {
        _mainController = mainController;
    }
    private void Start()
    {
        cursorImg = MainManager.Instance.Utilities.transform.Find("CursorParent").gameObject;
    }
    private void Update()
    {
        cursorImg.transform.position=MainManager.Instance.Player.GetChild(0).transform.position;
        SetRotation(MainManager.Instance.InputManager.GetWorlPos());
    }
    public void Aim()
    {     
        cursorImg.transform.position = (MainManager.Instance.InputManager.GetWorlPos());
         
    }
    void SetRotation(Vector3 targetPos)
    {
        targetPos.z = 0f;
        //burdan devam
        targetPos.x -= cursorImg.transform.position.x;
        targetPos.y -= cursorImg.transform.position.y;

        float angle = Mathf.Atan2(targetPos.y,targetPos.x) * Mathf.Rad2Deg;

        //cursorImg.transform.localRotation = cursorImg.transform.parent.localRotation;
        cursorImg.transform.localRotation = Quaternion.Euler(new Vector3(0, 0,angle));
    }
}
