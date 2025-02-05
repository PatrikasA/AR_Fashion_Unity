using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DressList : MonoBehaviour
{
    public static DressList Instance { get; private set; }

    [SerializeField] private List<GameObject> DressesList;
    [SerializeField] private GameObject startingDress;
    [SerializeField] private GameObject mannequinsParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (DataManager.Instance.GetSelectedDressIndex() != -1)
        {
            startingDress.SetActive(false);
            DressesList[DataManager.Instance.LoadSelectedDressIndex()].SetActive(true);
        }

        else if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectTextures_02)
        {
            startingDress.SetActive(false);
            foreach (GameObject dress in DressesList)
            {
                dress.SetActive(false);
            }

            DressesList[DataManager.Instance.LoadSelectedDressIndex()].SetActive(true);
        }


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateDressList();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {

    }

    public List<GameObject> GetDressesList()
    {
        return DressesList;
    }

    public GameObject GetStartingDress()
    {
        return startingDress;
    }

    public void DisableDresses()
    {
        foreach (GameObject dress in DressesList)
        {
            dress.gameObject.SetActive(false);
        }
    }

    public void UpdateDressList()
    {
        if (this.gameObject != null)
        {
            if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectClothes_01 || DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectTextures_02)
            {
                mannequinsParent.SetActive(true);
            }
            else
            {
                mannequinsParent.SetActive(false);
            }
        }

    }




}
