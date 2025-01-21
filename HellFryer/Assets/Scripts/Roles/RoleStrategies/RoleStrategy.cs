using UnityEngine;

/// <summary>
/// Template class for all type of spawn order strategies (Weak/Strong enemies first)
/// </summary>
public abstract class RoleStrategy : Strategy
{
    /// <summary>
    /// Either Attack or Cook
    /// </summary>
    public abstract void PerformTask();

    /// <summary>
    /// Open either Hunter's or Cook's book
    /// </summary>
    public abstract void OpenBook();
}