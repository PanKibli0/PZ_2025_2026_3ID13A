public abstract class StatusEffect
{
    public bool Finished { get; protected set; }

    public abstract void OnApply();
    public abstract void Tick(float deltaTime);
    public abstract void OnExpire();

    public virtual void Refresh() { }
    
}