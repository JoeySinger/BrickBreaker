using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration Paramaters
    [SerializeField] float minX = 87f;
    [SerializeField] float maxX = 1704f;
    
    
    [SerializeField] float screenWidthInUnits = 1792f;


    GameStatus theGameStatus;
    Ball theBall;
    

    // Start is called before the first frame update
    void Start()
    {
        theGameStatus = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
         //float currentMousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
         Vector3 paddlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
         paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);
         transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (theGameStatus.IsAutoPlayEnabled() && theBall.HasStarted())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
