namespace EnviosYa.Domain.Common;

public class Entity<TId>(TId id)
{
    public TId Id { get; set; } = id;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id is null || other.Id is null)
            return false;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId>? a, Entity<TId> b)
    {
        return a?.Equals(b) ?? false;
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? base.GetHashCode();
    }
}