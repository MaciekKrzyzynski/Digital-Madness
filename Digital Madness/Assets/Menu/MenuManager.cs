using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public GameObject levels;
    public GameObject Sphere;
    public GameObject Cylinder;
    public GameObject Player;
    public GameObject playerCylinderAttach;
    public GameObject playerSphereAttach;
    public GameObject projectilePrefab;

    private void Awake()
    {
        Player.SetActive(false);
        projectilePrefab.GetComponent<ProjectileCylinderScript>().enabled = false;
        projectilePrefab.GetComponent<ProjectileSphereScript>().enabled = false;
        projectilePrefab.GetComponent<ProjectilePlaneScript>().enabled = false;
    }
    public void ShowInstructions()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        mainMenu.SetActive(false);
        levels.SetActive(false);
        instructions.SetActive(true);
        Player.SetActive(false);
    }

    public void ShowMainMenu()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(true);
        Player.SetActive(false); 
    }

    public void showLevels()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(true);
        mainMenu.SetActive(false);
        Player.SetActive(false);
    }

    public void sphereLevel()
    {
        Player.SetActive(true);
        Player.transform.SetParent(playerSphereAttach.transform, true);
        Player.transform.position = new Vector3(0.0f, 18.0f, 0.0f);
        projectilePrefab.GetComponent<ProjectileCylinderScript>().enabled = false;
        projectilePrefab.GetComponent<ProjectileSphereScript>().enabled = true;

        Cylinder.SetActive(false);
        Sphere.SetActive(true);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void cylinderLevel()
    {
        Player.SetActive(true);
        Player.transform.SetParent(playerCylinderAttach.transform, true);
        Player.transform.position = new Vector3(0.0f, 13.0f, 0.0f);
        projectilePrefab.GetComponent<ProjectileCylinderScript>().enabled = true;
        projectilePrefab.GetComponent<ProjectileSphereScript>().enabled = false;
        Cylinder.SetActive(true);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void game()
    {
        Cylinder.SetActive(false);
        Sphere.SetActive(false);
        instructions.SetActive(false);
        levels.SetActive(false);
        mainMenu.SetActive(false);
    }
}
