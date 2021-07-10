using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}

public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

    public GameConstants gameConstants;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    public void AddPowerupUI(int index, Texture t)
    {
        Debug.Log("Add power up UI called");
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        Debug.Log("Add power up called");
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    private void ResetValues()
    {
        resetPowerup();
        powerupInventory.gameStarted = false;
        powerupInventory.Clear();
        Debug.Log("Reset values called, not implemented");
        // throw new NotImplementedException();
    }

    public void CheckPowerUp(int idx)
    {
        if(powerupIcons[idx].activeSelf == true)
        {
            removePowerupIcon(idx);
            ConsumePowerup(idx);
        }
    }

    public void ConsumePowerup(int idx)
    {
        Powerup p = powerupInventory.Get(idx);
        if(p != null)
        {
            marioMaxSpeed.ApplyChange(p.absoluteSpeedBooster);
            marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
            StartCoroutine(removeEffect(idx, p));
        }
    }

    IEnumerator removeEffect(int idx, Powerup p)
    {
        yield return new WaitForSeconds(5);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        marioJumpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        powerupInventory.Remove(idx);

    }

    void removePowerupIcon(int idx)
    {
        powerupIcons[idx].SetActive(false);
    }

    public void AttemptConsumePowerup(KeyCode k)
    {
        switch (k)
        {
            case KeyCode.Z:
                CheckPowerUp(0);
                break;
            case KeyCode.X:
                CheckPowerUp(1);
                break;
            default:
                break;
        }
    }
}