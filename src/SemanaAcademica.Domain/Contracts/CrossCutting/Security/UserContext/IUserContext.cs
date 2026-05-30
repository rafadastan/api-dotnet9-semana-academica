namespace SemanaAcademica.Domain.Contracts.CrossCutting.Security.UserContext
{
    public interface IUserContext
    {
        string Name { get; }
        bool IsAuthenticated();
    }
}
