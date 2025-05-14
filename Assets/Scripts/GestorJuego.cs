/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace UCM.IAV.Movimiento
{

    public class GestorJuego : MonoBehaviour
    {
        public static GestorJuego instance = null;

        [SerializeField]
        GameObject scenario = null;

        [SerializeField]
        GameObject rataPrefab = null;

        [SerializeField]
        private GameObject popup;
        private TMP_InputField inputField;

        // textos UI
        [SerializeField]
        Text fRText;   
        [SerializeField]
        Text ratText;

        private GameObject rataGO = null;
        private int frameRate = 60;

        // Variables de timer de framerate
        int m_frameCounter = 0;
        float m_timeCounter = 0.0f;
        float m_lastFramerate = 0.0f;
        float m_refreshTime = 0.5f;

        private int numRats;

        private bool cameraPerspective = true;
        private void Awake()
        {
            //Cosa que viene en los apuntes para que el gestor del juego no se destruya entre escenas
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Lo primero que se llama al activarse (tras el Awake)
        void OnEnable()
        {

            // No necesito este delegado
            //SceneManager.activeSceneChanged += OnSceneWasSwitched;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // Delegado para hacer cosas cuando una escena termina de cargar (no necesariamente cuando ha cambiado/switched)
        // Antiguamente se usaba un método del SceneManager llamado OnLevelWasLoaded(int level), ahora obsoleto
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            rataGO = GameObject.Find("Ratas");
            ratText = GameObject.Find("NumRats").GetComponent<Text>();
            fRText = GameObject.Find("Framerate").GetComponent<Text>();
            numRats = rataGO.transform.childCount;
            ratText.text = numRats.ToString();

        }

        // Se llama para poner en marcha el gestor
        private void Start()
        {
            rataGO = GameObject.Find("Ratas");
            Application.targetFrameRate = frameRate;
            numRats = rataGO.transform.childCount;
            ratText.text = numRats.ToString();

            Debug.Log(popup);
            inputField = popup.GetComponentInChildren<TMP_InputField>();
            Button acceptButton = popup.transform.Find("Button").GetComponent<Button>();
            acceptButton.onClick.AddListener(() => ReadInput());
            popup.SetActive(false);
        }

        // Se llama cuando el juego ha terminado
        void OnDisable()
        { 
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        // Update is called once per frame
        void Update()
        {
            // Timer para mostrar el frameRate a intervalos
            if (m_timeCounter < m_refreshTime)
            {
                m_timeCounter += Time.deltaTime;
                m_frameCounter++;
            }
            else
            {
                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;
            }

            // Texto con el framerate y 2 decimales
            fRText.text = (((int)(m_lastFramerate * 100 + .5) / 100.0)).ToString();

            //Input
            
            if (Input.GetKeyDown(KeyCode.R))
                Restart();
            if (Input.GetKeyDown(KeyCode.T))
                HideScenario();
            if (Input.GetKeyDown(KeyCode.O))
                SpawnRata();
            if (Input.GetKeyDown(KeyCode.P))
                DespawnRata();
            if (Input.GetKeyDown(KeyCode.F))
                ChangeFrameRate();
            if (Input.GetKeyDown(KeyCode.N))
                ChangeCameraView();
            if (Input.GetKeyDown(KeyCode.E))
                ShowPopup();
            
            

        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void HideScenario()
        {
            if (scenario == null)
                return;

            if (scenario.activeSelf)
                scenario.SetActive(false);
            else
                scenario.SetActive(true);
        }

        private void SpawnRata()
        {
            if (rataPrefab == null || rataGO == null)
                return;

 
            GameObject nuevaRata = Instantiate(rataPrefab, rataGO.transform);

            Llegada llegada = nuevaRata.GetComponent<Llegada>();
            if (llegada != null)
            {
                llegada.slowRadius = 10.0f;
                llegada.targetRadius = 1.5f;
                GameObject objetivo = GameObject.Find("Avatar");

                if (objetivo != null)
                {
                    llegada.objetivo = objetivo;
                }
                else
                {
                    Debug.LogError("No se ha podido asignar el objetivo a la rata.");
                }
            }
            Separacion separacion = nuevaRata.GetComponent<Separacion>();

            if (separacion != null)
            {
                separacion.targEmpty = rataGO;
            }
            else
            {
                Debug.LogError("El componente 'Separacion' no se encontró en el objeto instanciado.");
            }

            numRats++;
            ratText.text = numRats.ToString();
        }

        private void DespawnRata()
        {
            if (rataGO == null || rataGO.transform.childCount < 1)
                return;

            Destroy(rataGO.transform.GetChild(0).gameObject);

            numRats--;
            ratText.text = numRats.ToString();
        }

        private void ChangeFrameRate()
        {
            if (frameRate == 30)
            {
                frameRate = 60;
                Application.targetFrameRate = 60;
            }
            else
            {
                frameRate = 30;
                Application.targetFrameRate = 30;
            }
        }

        private void ChangeCameraView()
        {
            if (cameraPerspective){
                Camera.main.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 15, -2);
                cameraPerspective = false;
            }
            else{
                Camera.main.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 7, -10);
                cameraPerspective = true;
            }
        }

        void ShowPopup()
        {
            popup.SetActive(true);
        }

        void ReadInput()
        {

            if (int.TryParse(inputField.text, out int number))
            {
                Debug.Log("Número ingresado: " + number);
                popup.SetActive(false);
                changeNumberOfRats(number);
            }
        }
        private void changeNumberOfRats(int n)
        {
            if (n >= 0)
            {
                //si son menos
                while (numRats > n)
                {
                    DespawnRata();
                    
                }

                //si son mas
                while (numRats < n)
                {
                    SpawnRata();
                }
            }
            
        }
    }
}