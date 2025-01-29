using UnityEngine;

/// <summary>
/// Template class for all type of spawn order strategies (Weak/Strong enemies first)
/// </summary>

public abstract class RoleStrategy : Strategy
{
    protected EquipmentController equipmentController;

    /// <summary>
    /// Updates equipment type when the player changes roles
    /// </summary>
    public virtual void UpdateEquipmentType()
    {
        equipmentController = transform.GetComponentInChildren<EquipmentController>();
    }

    /// <summary>
    /// Either Attack or Cook
    /// </summary>
    public virtual void PerformTask()
    {
        equipmentController = transform.GetComponentInChildren<EquipmentController>();
        if (equipmentController != null)
        {
            equipmentController.Perform();
        }
    }

    /// <summary>
    /// Open either Hunter's or Cook's book
    /// </summary>
    public abstract void OpenBook(GameObject book);
}