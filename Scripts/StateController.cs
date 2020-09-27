using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Security.Permissions;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public ControllerType controllerType;
    public State currentState;
    public GameObject[] navPoints;
    public int navPointNum;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;
    public GameObject targetToChase;

    public float lookSpeed = 5f;
    public Transform npcTransform;
    public GameObject HelperCanvas;

    public enum ControllerType
    {
        Enemy, Champion, NPC
    }

    public GameController gameController;
    private GameObject playerCamera;

    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }

    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            foreach(Material m in r.materials)
            {
                m.color = color;
            }
        }
    }

    public void LookAt()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = destination.position - npcTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        npcTransform.rotation = Quaternion.Lerp(npcTransform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    public void seeChampText()
    {
        gameController.seeChampionText();
    }

    public void removeChampText()
    {
        gameController.removeChampionText();
    }

    public void SeeHelperText()
    {
        HelperCanvas.SetActive(true);
    }

    public void RemoveHelperText()
    {
        HelperCanvas.SetActive(false);
    }

    public bool CheckIfInRange(string tag, float range)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);

        if (enemies != null)
        {
            foreach(GameObject g in enemies)
            {
                if(Vector3.Distance(g.transform.position, transform.position) < range)
                {
                    targetToChase = g;
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckIfInRange(string tag)
    {
        return CheckIfInRange(tag, detectionRange);
    }

    public bool CheckIfInRange(float range)
    {
        return CheckIfInRange("Player", range);
    }

    public bool CheckIfVisible()
    {
        bool visible = Utility.IsInView(playerCamera, gameObject);
        return visible;
    }

    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        playerCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        //SetState(new PatrolState(this));
        switch (controllerType)
        {
            case ControllerType.Enemy:
                UnityEngine.Debug.Log("Enemy");
                SetState(new EnemyDormantState(this));
                break;
            case ControllerType.Champion:
                SetState(new ChampIdleState(this));
                UnityEngine.Debug.Log("Champion");
                break;
            case ControllerType.NPC:
                SetState(new NPCIdleState(this));
                UnityEngine.Debug.Log("NPC");
                break;
            default:
                SetState(new PatrolState(this));
                break;
        }
    }

    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
