using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //  public static GameManager instance = null;
    public static int karma = 0;

    public static Vector3 currentPosition = new Vector3(-9.6f, -0.1f, 0f);

    public static Vector3 startPosition = new Vector3(-9.6f, -0.1f, 0f);

    [SerializeField] UnityEngine.Object neutral;
    [SerializeField] UnityEngine.Object evil;
    [SerializeField] Healthbar healthbar;

    public static GameObject currentForm;

    private static Cinemachine.CinemachineVirtualCamera cinemaCamera;

    public static GameManager instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        cinemaCamera = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
       // DontDestroyOnLoad(gameObject);
        InitializeManager();
        CreateNewForm(GetForm(), startPosition);
    }

    private void InitializeManager()
    {
        karma = PlayerPrefs.GetInt("karma", 0);
    }

    public static void saveSettings()
    {
        PlayerPrefs.SetInt("karma", karma);
        PlayerPrefs.Save();
    }

    public void ChangeForm(int addendum)
    {
        karma += addendum;
        CreateNewForm(GetForm(), currentPosition);
        saveSettings();
    }

    private UnityEngine.Object GetForm() {
        return karma > -1 ? neutral : evil;
    }

    private void CreateNewForm(UnityEngine.Object form, Vector3 position)
    {
        int currentHealth = 100;
        if (currentForm)
            currentHealth = currentForm.GetComponent<CharacterController2D>().currentHealth;
        Destroy(currentForm);
        currentForm = (GameObject)Instantiate(form, position, Quaternion.identity);
        currentForm.GetComponent<CharacterController2D>().currentHealth = currentHealth;
        currentForm.GetComponent<CharacterController2D>().healthbar = healthbar;
        cinemaCamera.m_Follow = currentForm.transform;
    }

}
