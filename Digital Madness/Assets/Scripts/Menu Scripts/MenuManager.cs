using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public GameObject levels;
    public GameObject Sphere;
    public GameObject Cylinder;
    public GameObject Plane;
    public GameObject Player;
    public GameObject playerCylinderAttach;
    public GameObject playerSphereAttach;
    public GameObject playerPlaneAttach;
    public GameObject projectilePrefab;
    public Level level; 

    private void Awake()
    {
        level.currentLevel = 0;
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        playerCylinderAttach.SetActive(false);
        playerSphereAttach.SetActive(false);
        playerPlaneAttach.SetActive(false);
        projectilePrefab.GetComponent<ProjectileScript>().enabled = false;

    }
    public void ShowInstructions()
    {
        turnEverythingOff(); 
        instructions.SetActive(true);
        
    }

    public void ShowMainMenu()
    {
        turnEverythingOff(); 
        mainMenu.SetActive(true);
         
    }

    public void showLevels()
    {
        turnEverythingOff(); 
        levels.SetActive(true);
       
    }

    public void sphereLevel()
    {
        level.currentLevel = 3;
        Player.SetActive(true);
        Player.transform.SetParent(playerSphereAttach.transform, true);
        Player.transform.parent.gameObject.SetActive(true);
        Player.transform.position = new Vector3(0.0f, 18.0f, 0.0f);
        projectilePrefab.GetComponent<ProjectileScript>().enabled = true;

        turnEverythingOff(); 
        Sphere.SetActive(true);
    }

    public void cylinderLevel()
    {
        level.currentLevel = 2;
        Player.SetActive(true);
        Player.transform.SetParent(playerCylinderAttach.transform, true);
        Player.transform.parent.gameObject.SetActive(true);
        Player.transform.position = new Vector3(0.0f, 13.0f, 0.0f);
        projectilePrefab.GetComponent<ProjectileScript>().enabled = true;

        turnEverythingOff();
        Cylinder.SetActive(true);
    }

    public void planeLevel()
    {
        level.currentLevel = 1;
        Player.SetActive(true);
        Player.transform.SetParent(playerPlaneAttach.transform, true);
        Player.transform.parent.gameObject.SetActive(true);
        Player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        projectilePrefab.GetComponent<ProjectileScript>().enabled = true;
        turnEverythingOff();
        Plane.SetActive(true);
    }
    public void game()
    {
        turnEverythingOff(); 
    }

    void turnEverythingOff()
    {
        Plane.SetActive(false);
        Sphere.SetActive(false);
        Cylinder.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }
}
