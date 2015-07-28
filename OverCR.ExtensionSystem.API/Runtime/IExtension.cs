namespace OverCR.ExtensionSystem.API.Runtime
{
    public interface IExtension
    {
        string Name { get; }
        string Author { get; }
        string Contact { get; }

        void WakeUp(IManager manager);
        void Update();
    }
}
