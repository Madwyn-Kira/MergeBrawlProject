public interface IReloadComponent
{
    public bool IsWeaponReloaded { get; }
    public void Initialize(WeaponData weaponData);
    public void Reload();
}
