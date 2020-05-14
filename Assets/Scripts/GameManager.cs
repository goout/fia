using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;

public class GameManager : MonoBehaviour
{
    //  public static GameManager instance = null;
    public static int karma = 0;

    public static Vector3 currentPosition = new Vector3(-9.6f, -0.1f, 0f);

    public static Vector3 startPosition = new Vector3(-14f, -0.1f, 0f);

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

    public void ChangeKarma(int addendum)
    {
        karma += addendum;
        if (!currentForm.name.StartsWith(GetForm().name))
            CreateNewForm(GetForm(), currentPosition);
        saveSettings();
    }

    private UnityEngine.Object GetForm()
    {
        return karma > -1 ? neutral : evil;
    }

    private void CreateNewForm(UnityEngine.Object form, Vector3 position)
    {
        int currentHealth = 100;
        if (currentForm)
            currentHealth = currentForm.GetComponent<Player>().currentHealth;
        Destroy(currentForm);
        currentForm = (GameObject)Instantiate(form, position, Quaternion.identity);
        currentForm.GetComponent<Player>().currentHealth = currentHealth;
        currentForm.GetComponent<Player>().healthbar = healthbar;
        cinemaCamera.m_Follow = currentForm.transform;
    }

    public void Move(float InputAxis)
    {
        currentForm.GetComponent<Player>().Move(InputAxis);
    }

    public void Jump()
    {
        currentForm.GetComponent<Player>().Jump();
    }

    public void Attack()
    {
        currentForm.GetComponent<Player>().Attack();
    }

}
