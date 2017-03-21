namespace Forum.Domain.Contracts
{
    public interface IApplicationContext
    {
        ISecurityContext SecurityContext { get; }
    }
}
