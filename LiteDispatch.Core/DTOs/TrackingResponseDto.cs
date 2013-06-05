namespace LiteDispatch.Core.DTOs
{
  using System;

  public class TrackingResponseDto
  {
    public Guid NotificationId { get; set; }
    public long DispatchNoteId { get; set; }
    public bool Accepted { get; set; }
    public string Error { get; set; }
  }
}