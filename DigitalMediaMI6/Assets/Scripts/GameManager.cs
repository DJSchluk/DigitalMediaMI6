﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }
    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    public InputField nameInput;

    private void Start()
    {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
        Debug.Log("Connect");
    }

    public void HostButton()
    {
        try
        {
            Server s = Instantiate(serverPrefab).GetComponent<Server>();
            s.Init();

            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            
            //bug wegen Name?
            c.clientName = nameInput.text;
            c.isHost = true;

            if (c.clientName == "")
            {
                c.clientName = "Host";
            }
            c.ConnectToServer("127.0.0.1", 6321);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
        Debug.Log("Host");
    }

    public void ConnectToServerButton()
    {
        string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if(hostAddress == "")
        {
            hostAddress = "127.0.0.1";
        }

        try
        {
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            //bug wegen Name?
            c.clientName = nameInput.text;
            if(c.clientName == "")
            {
                c.clientName = "Client";
            }
            c.ConnectToServer(hostAddress, 6321);
            connectMenu.SetActive(false);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void BackButton()
    {
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        mainMenu.SetActive(true);

        Server s = FindObjectOfType<Server>();
        if(s != null)
        {
            Destroy(s.gameObject);
        }

        Client c = FindObjectOfType<Client>();
        if (s != null)
        {
            Destroy(c.gameObject);
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("fabio_scene");
    }
}
