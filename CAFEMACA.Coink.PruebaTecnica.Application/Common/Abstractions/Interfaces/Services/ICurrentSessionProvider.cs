namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services
{
    public interface ICurrentSessionProvider
    {
        Guid? GetUserId();
    }
}
