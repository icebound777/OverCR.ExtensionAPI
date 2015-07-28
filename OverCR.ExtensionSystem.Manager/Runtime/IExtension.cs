namespace OverCR.ExtensionSystem.Manager.Runtime
{
    public interface IExtension
    {
        string Name { get; }
        string Author { get; }
        string Contact { get; }

        void WakeUp();
        void Update();
    }
}
