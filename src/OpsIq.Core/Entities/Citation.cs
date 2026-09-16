namespace OpsIq.Core.Entities;


public class Citation
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid MessageId { get; set; }
    public Guid ChunkId { get; set; }
    public float Score { get; set; }
    public int Rank { get; set; }
}

