namespace Juconda.Domain.DomainModel;

public class BaseEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    public bool Actual { get; set; } = true;

    public DateTimeOffset? CreateDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdateDate { get; set; }
}