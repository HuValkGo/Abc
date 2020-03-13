namespace Abc.Facade.Common
{
    public abstract class NamedView :UniqueEntityView
    {
        public string Name { get; set; }
        public string Code { get; set; }
        
    }
}