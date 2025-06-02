using UnityEngine;

public class DeathEventHandler : MonoBehaviour
{
    public bool isPlayer = false;
    public PanelController panelController;


    public void OnDeath()
    {
        if (isPlayer)
        {
            panelController.ShowDeathPanel();
        }
        else
        {
            panelController.ShowWinPanel();
        }
    }
    
}
