using OverCR.ExtensionAPI.Runtime;

namespace OverCR.Example.Extension
{
    public class Entry : IExtension
    {
        public string Author { get; } = "OverCR Solutions";
        public string Contact { get; } = "[DATA ERASED]";
        public string Name { get; } = "Example Extension";

        public void WakeUp()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
