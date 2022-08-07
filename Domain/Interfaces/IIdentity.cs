namespace Domain.Enums;

public interface IIdentity<TIdentifier> : IEquatable<TIdentifier>
{
    TIdentifier Id { get; }

}