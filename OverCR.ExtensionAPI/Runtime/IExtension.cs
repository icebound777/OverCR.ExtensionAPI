namespace OverCR.ExtensionAPI.Runtime
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
